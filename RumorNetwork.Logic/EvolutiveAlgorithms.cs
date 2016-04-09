using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorNetwork.Logic
{
    public class EvolutiveAlgorithms<E>
    {

        public delegate bool CompareMembers(E member, E otherMember);

        /// <summary>
        /// Perform a tournament selection on the members of a population
        /// </summary>
        /// <param name="population">The population from which the members are selected</param>
        /// <param name="tournamentSize">Size of the tournament - members to be chosen to compete. Cannot be smaller than 1</param>
        /// <param name="fitnessFunction">Fitness function based on which the best is decided</param>
        /// <returns></returns>
        public static E TournamentSelection(List<E> population, CompareMembers fitnessFunction, int tournamentSize = 2)
        {
            E best = default(E);

            Random randomGenerator = new Random();

            for (int i = 0; i < tournamentSize; i++)
            {
                int random = randomGenerator.Next(population.Count);

                E selectedMember = population[random];

                if (fitnessFunction(selectedMember, best))
                {
                    best = selectedMember;
                }
            }

            return best;
        }

        /// <summary>
        /// Splices two parents to obtain two offsprings
        /// </summary>
        /// <param name="parentA">Mother chromosome</param>
        /// <param name="parentB">Father chromosome</param>
        /// <param name="cutPoint">Point where the chomosomes will get separated</param>
        /// <param name="splicingProbability">Probability that the splice will happen</param>
        /// <returns>Null if splice didn't happen, List of chromosomes if it did</returns>
        public static List<Chromosome> SpliceChromosomes(Chromosome parentA, Chromosome parentB, int cutPoint, double splicingProbability)
        {   
            double happening = new Random().NextDouble();

            if (happening > splicingProbability)
            {
                return null;
            }

            List<int> genesA = parentA.Genes;
            List<int> genesB = parentB.Genes;

            //return null if the cutting point is not valid
            if (cutPoint >= genesA.Count)
            {
                return null;
            }

            for (int i = 0; i < cutPoint; i++)
            {
                int pocket = genesA[i];
                genesA[i] = genesB[i];
                genesB[i] = pocket;
            }

            List<Chromosome> offsprings = new List<Chromosome>() {new Chromosome(genesB.Count), new Chromosome(genesA.Count)};

            offsprings[1].Genes = genesA;
            offsprings[0].Genes = genesB;

            return offsprings;

        }

        /// <summary>
        /// Does a strong mutation on a chromosome with a certain probability
        /// </summary>
        /// <param name="chromosome">Chromosome to mutate</param>
        /// <param name="mutationProbability">Probability of individual gene mutation</param>
        /// <returns>A mutated chromosome</returns>
        public static Chromosome MutateChromosome(Chromosome chromosome, double mutationProbability)
        {
            Random random = new Random();

            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                double happening = random.NextDouble();

                if (happening < mutationProbability)
                {
                    if (chromosome.Genes[i].Equals(0))
                    {
                        chromosome.SetGene(i, 1);
                    }
                    else
                    {
                        chromosome.SetGene(i, 0);
                    }
                    
                }
            }

            return chromosome;
        }

        public static Particle MutateParticle(Particle particle)
        {
            Random random = new Random();

            double mutationProbability = (double) 1/(1 + Math.Pow(Math.E, particle.Speed));

            for (int i = 0; i < particle.Genes.Count; i++)
            {
                double happening = random.NextDouble();

                if (happening < mutationProbability)
                {
                    if (particle.Genes[i].Equals(0))
                    {
                        particle.SetGene(i, 1);
                    }
                    else
                    {
                        particle.SetGene(i, 0);
                    }

                }
            }

            return particle;
        }
    }
}
