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
            get
            {
                if (Neighbours == null)
                {
                    return null;
                }
                return (IBinaryTreeNode) Neighbours[0];
            }
            set
            {
                if (Neighbours == null)
                {
                    Neighbours = new List<IPathFindingNode>();
                    Neighbours.Add(new PathFindingNode());
                    Neighbours.Add(new PathFindingNode());
                }

                Neighbours[0] = (IPathFindingNode)value;
            }
        }

        public IBinaryTreeNode Right
        {
            get
            {
                if (Neighbours == null)
                {
                    return null;
                }
                return (IBinaryTreeNode)Neighbours[1];
            }
            set
            {
                if (Neighbours == null)
                {
                    Neighbours = new List<IPathFindingNode>();
                    Neighbours.Add(new PathFindingNode());
                    Neighbours.Add(new PathFindingNode());
                }

                Neighbours[1] = (IPathFindingNode)value;
            }
        }

        public BinaryTreeNode() { }

        //public BinaryTreeNode(iEntity pNode, IList<IPathFindingNode> paths = default(IList<IPathFindingNode>))
        //{

        //}

        public BinaryTreeNode(iEntity data): base(data, null) { }

        public BinaryTreeNode(iEntity path, IBinaryTreeNode pLeft, IBinaryTreeNode pRight)
        {
            NodePath = path;

            IList<IPathFindingNode> children = new List<IPathFindingNode>
            {
                (BinaryTreeNode)pLeft,
                (BinaryTreeNode)pRight
            };

            Neighbours = children;
        }



    }
}
