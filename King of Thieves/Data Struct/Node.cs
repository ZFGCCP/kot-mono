using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Data_Struct
{
    class Node <T>
    {
        private T _data;
        private List<Node<T>> _neighbors;

        public Node(T data, params Node<T>[] neighbors)
        {
            _neighbors = new List<Node<T>>(neighbors);
            _data = data;
        }

        public T data
        {
            get
            {
                return _data;
            }
        }

        public void addNeighbor(Node<T> neighbor)
        {
            //TODO: need to add this node as a neighbor to the neighbor without causing infinite recursion
            _neighbors.Add(neighbor);
        }

        public Node<T> neighbor(int index)
        {
            try
            {
                return _neighbors[index];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public Node<T> neighbor(T data)
        {
            var query = from x in _neighbors
                        where EqualityComparer<T>.Default.Equals(data, x._data)
                        select x;

            foreach (var x in query)
                return x;

            return null;
        }
    }
}
