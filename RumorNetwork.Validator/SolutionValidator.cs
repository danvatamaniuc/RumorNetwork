using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using RumorNetwork.Util;

namespace RumorNetwork.Validator
{
    public class SolutionValidator
    {
        /// <summary>
        /// Checks if there are any disconnected vertices from the potential solution
        /// Throws an exception if there is at least a disconnected vertice
        /// </summary>
        /// <param name="paths">Paths between vertices</param>
        /// <param name="verticesNr">Number of vertices</param>
        public static void ValidateSolution(List<int[]> relations, int verticesNr)
        {
            List<int[]> paths = relations.ToList(); 
            
            HashSet<int> reachedVertices = new HashSet<int>();

            Tail<int> verticesToCheck = new Tail<int>();

            int[] initialPath = paths[0];

            verticesToCheck.Add(initialPath[0]);
            reachedVertices.Add(initialPath[0]);

            while (!paths.Count.Equals(0))
            {
                //get the next vertice to check all paths
                int currentVertice = verticesToCheck.GetNext();

                //find all the neighbors of the said vertice and number them
                foreach (int[] path in paths)
                {
                    if (path[0].Equals(currentVertice))
                    {
                        verticesToCheck.Add(path[1]);
                        reachedVertices.Add(path[1]);
                    } else if (path[1].Equals(currentVertice))
                    {
                        verticesToCheck.Add(path[0]);
                        reachedVertices.Add(path[0]);
                    }
                }

                //remove all aparitions of 
                paths.RemoveAll(s => s[0] == currentVertice);
                paths.RemoveAll(s => s[1] == currentVertice);
            }

            if (!reachedVertices.Count.Equals(verticesNr))
            {
                throw new ValidationException("Not all members are connected between them!");
            }
            
        }
    }
}
