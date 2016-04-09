using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RumorNetwork.Util;
using RumorNetwork.Validator;

namespace RumorNetwork.Logic
{
    public class Solver
    {
        private static List<int[]> _relations; 
        /// <summary>
        /// Gets the minimal amount of relations needed to spread a rumor between the members
        /// </summary>
        /// <param name="relations">Relations between the members. If members are not all conected, throws an exception</param>
        /// <param name="memberNr">Number of members</param>
        /// <param name="populationSize">Size of population of EA. Default 100</param>
        /// <param name="generationAmount">Number of generations to run the algorithm. Default 100</param>
        /// <returns>A smaller list of relations needed to spread a rumor</returns>
        public static List<int[]> EvolutiveSearch(List<int[]> relations, int memberNr, int populationSize = 100, int generationAmount = 100)
        {

            _relations = relations;

            //check if relations are valid
            try
            {
                SolutionValidator.ValidateSolution(relations, memberNr);
            }
            catch (Exception e)
            {
                throw e;
            }

            List<Chromosome> population = new List<Chromosome>();

            //populate with members
            while (population.Count < populationSize)
            {
                Chromosome chromosome = new Chromosome(relations.Count);
                population.Add(chromosome);
            }

            Random random = new Random();

            const double splicingProbability = 0.8;
            const double mutationProbability = 0.2;

            //simulate generational evolution
            for (int i = 0; i < generationAmount; i++)
            {

                //select the parent solutions
                Chromosome parentA = EvolutiveAlgorithms<Chromosome>.TournamentSelection(
                    population, 
                    GetBestChromosome,
                    6);

                Chromosome parentB = EvolutiveAlgorithms<Chromosome>.TournamentSelection(
                    population,
                    GetBestChromosome,
                    6);

                //decide randomly on the cut point
                int cutPoint = random.Next(memberNr);

                //get the offsprings of the parents
                List<Chromosome> offsprings = EvolutiveAlgorithms<Chromosome>.SpliceChromosomes(
                    parentA, 
                    parentB,
                    cutPoint, 
                    splicingProbability);

                //if there has been no splicing, skips this if
                if (offsprings != null)
                {
                    //mutate the offsprings
                    for (int j = 0; j < offsprings.Count; j++)
                    {
                        offsprings[j] = EvolutiveAlgorithms<Chromosome>.MutateChromosome(offsprings[j], mutationProbability);
                    }

                    //find the index of the worst member of the population
                    int worstIndex = 0;

                    for (int j = 0; j < population.Count; j++)
                    {
                        if (GetBestChromosome(population[worstIndex], population[j]))
                        {
                            worstIndex = j;
                        }
                    }

                    //select the best descendant
                    Chromosome bestOffspring = offsprings[0];

                    if (GetBestChromosome(offsprings[1], offsprings[0]))
                    {
                        bestOffspring = offsprings[1];
                    }

                    //if the best descendant is better than the worst member of the population
                    //replace him
                    if (GetBestChromosome(bestOffspring, population[worstIndex]))
                    {
                        population[worstIndex] = bestOffspring;
                    }
                }
                
            }

            //after all the generations passed, filter them based on the ones that are a valid solution
            List<Chromosome> validChromosomes = new List<Chromosome>(populationSize);

            foreach (Chromosome chromosome in population)
            {
                int containingMembers = GetNrOfContainingMembers(chromosome);

                List<int[]> chromosomeRelations = new List<int[]>();

                for (int i = 0; i < chromosome.Genes.Count; i++)
                {
                    if (chromosome.Genes[i].Equals(1))
                    {
                        chromosomeRelations.Add(_relations[i]);
                    }
                }

                try
                {
                    SolutionValidator.ValidateSolution(chromosomeRelations, memberNr);

                    if (containingMembers.Equals(memberNr))
                    {
                        validChromosomes.Add(chromosome);
                    }
                }
                catch (Exception)
                {
                    //Deliberate suppression of errors. Checking for valid chromosomes
                }

                
            }

            Chromosome best = null;

            //get the best solution out of all existing
            foreach (Chromosome validChromosome in validChromosomes)
            {
                if (GetBestChromosome(validChromosome, best))
                {
                    best = validChromosome;
                }
            }

            if (best == null)
            {
                return null;
            }

            List<int[]> bestRelations = new List<int[]>();

            for (int i = 0; i < best.Genes.Count; i++)
            {
                if (best.Genes[i].Equals(1))
                {
                    bestRelations.Add(relations[i]);
                }
            }

            return bestRelations;
        }

        /// <summary>
        /// Gets the number of the members a set of relations has
        /// </summary>
        /// <param name="chromosome">Chromosome representing the relations</param>
        /// <returns>An int that shows how many members the relation has</returns>
        private static int GetNrOfContainingMembers(Chromosome chromosome)
        {
            HashSet<int> containingMembers = new HashSet<int>();

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                if (chromosome.Genes[i].Equals(1))
                {
                    int[] relation = _relations[i];
                    containingMembers.Add(relation[0]);
                    containingMembers.Add(relation[1]);
                }
            }

            return containingMembers.Count;
        }

        /// <summary>
        /// Evaluate two chromosomes and get the one that has more members included in relations
        /// </summary>
        /// <param name="x">Chromosome that is being compared</param>
        /// <param name="y">Chromosome compared to</param>
        /// <returns>True if x has more members than y, false otherwise</returns>
        public static bool GetBestChromosome(Chromosome x, Chromosome y)
        {
            if (x == null)
            {
                return false;
            }

            if (y == null)
            {
                return true;
            }

            int xMembers = GetNrOfContainingMembers(x);
            int yMembers = GetNrOfContainingMembers(y);

            if (xMembers.Equals(yMembers))
            {
                if (x.GetAmountOfOnes() < y.GetAmountOfOnes())
                {
                    return true;
                }

                return false;
            }

            if (xMembers > yMembers)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// PSO solver for the rumor problem. 
        /// </summary>
        /// <param name="relations">Set of initial relations</param>
        /// <param name="memberNr">Total number of members in said relations</param>
        /// <param name="populationSize">Size of general population</param>
        /// <param name="time">Time that the particles are going to search </param>
        /// <returns></returns>
        public static List<int[]> ParticleSwarmOptimization(List<int[]> relations, int memberNr, int populationSize = 100, int time = 100)
        {

            _relations = relations.ToList();

            //check if relations are valid
            try
            {
                SolutionValidator.ValidateSolution(relations, memberNr);
            }
            catch (Exception e)
            {
                throw e;
            }


            int inertia = 2;
            int cognitiveFactor = 2;
            int socialFactor = 1;

            Random random = new Random();

            List<Particle> population = new List<Particle>(populationSize);
            List<Particle> personalBest = new List<Particle>(populationSize);

            //create the particle population
            while (!population.Count.Equals(populationSize))
            {
                Particle particle = new Particle(relations.Count);
                population.Add(particle);
                personalBest.Add(particle);
            }

            //simulate the time lapse
            for (int i = 0; i < time; i++)
            {
                //find the best-looking particle
                Particle best = GetBestInPopulation(population);

                for (int j = 0; j < populationSize; j++)
                {

                    //update the speed of each particle
                    population[j].Speed = inertia*population[j].Speed +
                                   cognitiveFactor*random.Next()*
                                   HelperFunctions.HammingDistance(personalBest[j].Genes, population[j].Genes) +
                                   socialFactor*random.Next()*
                                   HelperFunctions.HammingDistance(best.Genes, population[j].Genes);

                    //simulate the movement of a particle
                    population[j] = EvolutiveAlgorithms<Particle>.MutateParticle(population[j]);
                }

            }

            //after the time-lapse, collect all the valid particles

            List<Particle> validParticles = new List<Particle>(populationSize);

            foreach (Particle particle in population)
            {
                int containingMembers = GetNrOfContainingMembers(particle);

                //relation set of the particle
                List<int[]> particleRelations = new List<int[]>();

                for (int i = 0; i < particle.Genes.Count; i++)
                {
                    if (particle.Genes[i].Equals(1))
                    {
                        particleRelations.Add(_relations[i]);
                    }
                }

                try
                {
                    SolutionValidator.ValidateSolution(particleRelations, memberNr);

                    if (containingMembers.Equals(memberNr))
                    {
                        validParticles.Add(particle);
                    }
                }
                catch (Exception)
                {
                    //Deliberate suppression of errors. Checking for valid particles
                }
            }

            Particle bestParticle = null;

            foreach (Particle validParticle in validParticles)
            {
                if (GetBestChromosome(validParticle, bestParticle))
                {
                    bestParticle = validParticle;
                }
            }

            if (bestParticle == null)
            {
                return null;
            }

            List<int[]> bestRelations = new List<int[]>();

            for (int i = 0; i < bestParticle.Genes.Count; i++)
            {
                if (bestParticle.Genes[i].Equals(1))
                {
                    bestRelations.Add(relations[i]);
                }
            }

            return bestRelations;
        }

        private static Particle GetBestInPopulation(List<Particle> population)
        {
            Particle best = null;

            foreach (Particle particle in population)
            {
                if (GetBestChromosome(particle, best))
                {
                    best = particle;
                }
            }

            return best;

        }

        /// <summary>
        /// Gets the number of the members a set of relations has
        /// </summary>
        /// <param name="chromosome">Chromosome representing the relations</param>
        /// <returns>An int that shows how many members the relation has</returns>
        private static int GetNrOfContainingMembers(Particle particle)
        {
            HashSet<int> containingMembers = new HashSet<int>();

            for (int i = 0; i < particle.Genes.Count; i++)
            {
                if (particle.Genes[i].Equals(1))
                {
                    int[] relation = _relations[i];
                    containingMembers.Add(relation[0]);
                    containingMembers.Add(relation[1]);
                }
            }

            return containingMembers.Count;
        }

        /// <summary>
        /// Evaluate two particle and get the one that has more members included in relations
        /// </summary>
        /// <param name="x">Particle that is being compared</param>
        /// <param name="y">Particle compared to</param>
        /// <returns>True if x has more members than y, false otherwise</returns>
        public static bool GetBestChromosome(Particle x, Particle y)
        {
            if (x == null)
            {
                return false;
            }

            if (y == null)
            {
                return true;
            }

            int xMembers = GetNrOfContainingMembers(x);
            int yMembers = GetNrOfContainingMembers(y);

            if (xMembers.Equals(yMembers))
            {
                if (x.GetAmountOfOnes() < y.GetAmountOfOnes())
                {
                    return true;
                }

                return false;
            }

            if (xMembers > yMembers)
            {
                return true;
            }

            return false;
        }
    }
}
