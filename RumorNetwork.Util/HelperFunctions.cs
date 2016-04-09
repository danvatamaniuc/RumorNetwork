using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorNetwork.Util
{
    public class HelperFunctions
    {

        public static int HammingDistance(List<int> a, List<int> b)
        {
            if (!a.Count.Equals(b.Count))
            {
                return -1;
            }

            int distance = 0;

            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    distance += 1;
                }
            }

            return distance;
        }
        
    }
}
