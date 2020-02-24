using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;

namespace Game1.Engine.PathFinding
{
    class BinaryTreeNode : PathFindingNode, IBinaryTreeNode
    {
        public IBinaryTreeNode Left
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public IBinaryTreeNode Right
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public BinaryTreeNode() { }

        public BinaryTreeNode(iEntity path, IBinaryTreeNode pLeft, IBinaryTreeNode pRight)
        {

        }

        //(iEntity pNode, IList<IPathFindingNode> paths = default(IList<IPathFindingNode>))

    }
}
