using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    class BinaryTree : IBinaryTree
    {
        public IBinaryTreeNode Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Clear()
        {
            Root = null;
        }
    }
}
