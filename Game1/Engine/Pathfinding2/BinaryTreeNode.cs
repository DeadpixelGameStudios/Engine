using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Pathfinding2
{
    public class BinaryTreeNode : Node, IBinaryTreeNode
    {
        public IBinaryTreeNode Left
        {
            get
            {
                if (Neighbours == null)
                {
                    return null;
                }
                return (IBinaryTreeNode)Neighbours[0];
            }
            set
            {
                if (Neighbours == null)
                {
                    Neighbours = new List<INode>();
                    Neighbours.Add(new Node());
                    Neighbours.Add(new Node());
                }

                Neighbours[0] = (INode)value;
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
                    Neighbours = new List<INode>();
                    Neighbours.Add(new Node());
                    Neighbours.Add(new Node());
                }

                Neighbours[1] = (INode)value;
            }
        }

        public BinaryTreeNode() { }

        public BinaryTreeNode(INode pParent) : base(pParent, null) { }

        public BinaryTreeNode(INode pParent, IBinaryTreeNode pLeft, IBinaryTreeNode pRight)
        {
            Parent = pParent;
            IList<INode> children = new List<INode>
            {
                (BinaryTreeNode)pLeft,
                (BinaryTreeNode)pRight
            };

                Neighbours = children;
        }
    }
}
