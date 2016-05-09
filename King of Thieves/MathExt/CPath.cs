using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace King_of_Thieves.MathExt
{
    public struct CPath
    {
        private Queue<CPathNode> _path;
        private CPathNode _currentNode;
        private static readonly CPathNode _nullNode = new CPathNode();

        public CPath(CPathNode[] path)
        {
            _path = new Queue<CPathNode>();
            _currentNode = _nullNode;

            for (int i = 0; i < path.Count(); i++)
            {
                path[i].index = i;
                _path.Enqueue(path[i]);
            }
        }

        public void nextNode()
        {
            if (_path.Count == 0)
                _currentNode = _nullNode;
            else
            {
                _currentNode = _path.Dequeue();
            }
        }

        public void cancelPath()
        {
            _path.Clear();
            _currentNode = _nullNode;
        }

        public CPathNode checkNextNode
        {
            get
            {
                return _path.Peek();
            }
        }

        public bool endOfPath
        {
            get
            {
                return _path.Count == 0 && !currentNodeReady;
            }
        }

        public bool currentNodeReady
        {
            get
            {
                return _currentNode.speed != _nullNode.speed;
            }
        }

        public CPathNode currentNode
        {
            get
            {
                return _currentNode;
            }
        }
    }
}
