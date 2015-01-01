using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Data_Struct.Graph
{
    class NodeGraph<T>
    {
        private Node<T> _root;
        public Node<T> iterator;

        public NodeGraph(T data)
        {
            _root = new Node<T>(data);
            iterator = _root;
        }

        public void moveToNode(T data)
        {
            iterator = iterator.neighbor(data);
        }
    }
}
