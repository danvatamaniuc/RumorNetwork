using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorNetwork.Logic
{
    public class Particle
    {
        private static int counter = 0;

        public int Speed { get; set; }

        public Particle(int size)
        {
            // could have used bit arrays, want more than 32 or 64 relations
            Genes = new List<int>(size);

            counter += 3;

            var random = new Random(counter);

            Speed = random.Next();

            while (Genes.Count != Genes.Capacity)
            {
                Genes.Add(random.Next(2));
            }
        }

        /// <summary>
        /// Gets the gene at position
        /// </summary>
        /// <param name="geneNr">Position from which to get the gene. If not a valid position, will return -1</param>
        /// <returns>Value of the gene. 0 or 1</returns>
        public int GetGene(int geneNr)
        {
            if (geneNr < Genes.Count && geneNr > -1)
            {
                return Genes[geneNr];
            }

            return -1;
        }

        /// <summary>
        /// Sets the value of a gene at position
        /// </summary>
        /// <param name="geneNr">Position of gene. Will do nothing if geneNr is not a valid position</param>
        /// <param name="geneValue">New value of the gene. Has to be 0 or 1</param>
        public void SetGene(int geneNr, int geneValue)
        {
            if (geneNr < Genes.Count && geneNr > -1 && (geneValue == 1 || geneValue == 0))
            {
                Genes[geneNr] = geneValue;
            }
        }

        public int GetAmountOfOnes()
        {
            return Genes.FindAll(e => e == 1).Count;
        }


        public List<int> Genes { get; set; }

        protected bool Equals(Particle other)
        {
            return Genes.SequenceEqual(other.Genes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Chromosome)obj);
        }

        public override int GetHashCode()
        {
            return Genes?.GetHashCode() ?? 0;
        }
    }
}
