using Game1.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    class BreadthFirstSearch : IBreadthFirstSearch
    {
        IPathFindingNode Top = null;

        public Queue<IPathFindingNode> Nodes
        {
            get;
            set;
        } = new Queue<IPathFindingNode>();

        public BreadthFirstSearch(IBinaryTree binaryTree, iEntity entity)
        {
            Nodes.Enqueue((IPathFindingNode)binaryTree.Root);

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
                            Nodes.Enqueue(Top.Neighbours[0]);
                        }

                        if (Top.Neighbours[1].NodePath != null)
                        {
                            Nodes.Enqueue(Top.Neighbours[1]);
                        }
                    }
                }
                else
                {
                    Nodes.Dequeue();
                }
            }
        }

        public iEntity BFSearch(IBinaryTree binaryTree, iEntity entity)
        {
            Nodes = new Queue<IPathFindingNode>();

            Nodes.Enqueue( (IPathFindingNode) binaryTree.Root);

            while (Nodes.Count != 0)
            {
                Top = Nodes.Peek();

                Top = Nodes.Dequeue();

                if (Top.NodePath == entity)
                {
                    return Top.NodePath;
                }
                else
                {
                    if (Top.Neighbours != null)
                    {
                        if (Top.Neighbours[0].NodePath != null)
                        {
                            Nodes.Enqueue(Top.Neighbours[0]);
                        }

                        if (Top.Neighbours[1].NodePath != null)
                        {
                            Nodes.Enqueue(Top.Neighbours[1]);
                        }
                    }
                }
            }
            return null;
        }
    }
}
