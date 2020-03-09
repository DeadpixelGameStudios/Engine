using Game1.Engine.Entity;
using System.Collections.Generic;

namespace Game1.Engine.PathFinding
{
    class DepthFirstSearch : IDepthFirstSearch
    {
        IPathFindingNode Top = null;

        public Stack<IPathFindingNode> Nodes
        {
            get;
            set;
        } = new Stack<IPathFindingNode>();

        public void DFSearch(IBinaryTree binaryTree, iEntity entity)
        {
            Nodes = new Stack<IPathFindingNode>();

            Nodes.Push((IPathFindingNode)binaryTree.Root);

            while (Nodes.Count != 0)
            {
                Top = Nodes.Peek();

                if (!Top.Visited)
                {
                    Top.Visited = true;

                    if (Top.Neighbours != null)
                    {
                        if (Top.Neighbours[0].NodePath != null)
                        {
                            Nodes.Push(Top.Neighbours[0]);
                        }

                        if (Top.Neighbours[1].NodePath != null)
                        {
                            Nodes.Push(Top.Neighbours[1]);
                        }
                    }
                }
                else
                {
                    Nodes.Pop();
                }
            }
        }
    }
}
