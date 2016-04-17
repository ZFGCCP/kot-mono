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

        unsafe public CPath(CPathNode[] path)
        {
            _path = new Queue<CPathNode>();
            _currentNode = _nullNode;

            for (int i = 0; i < path.Count(); i++)
                _path.Enqueue(path[i]);
        }

        public unsafe void nextNode()
        {
            if (_path.Count == 0)
                _currentNode = _nullNode;
            else
            {
                _currentNode = _path.Dequeue();
            }
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
                return _path.Count > 0;
            }
        }

        public unsafe bool currentNodeReady
        {
            get
            {
                return _currentNode.speed != _nullNode.speed;
            }
        }

        public unsafe CPathNode currentNode
        {
            get
            {
                return _currentNode;
            }
        }
    }
}
