using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorNetwork.Util
{
    public class Tail<E>
    {
        private List<E> list;

        public Tail()
        {
            list = new List<E>();
        }

        public void Add(E element)
        {
            list.Add(element);
        }

        public E GetNext()
        {
            E removed = list[0];

            if (list.Count != 0)
            {
                list.RemoveAt(0);
            }

            return removed;
        }

        public bool Empty()
        {
            return list.Count.Equals(0);
        }
    }
}
