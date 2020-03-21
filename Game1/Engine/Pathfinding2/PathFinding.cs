using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Pathfinding2
{
    public class PathFinding : IPathFinding
    {
        public IGrid mGrid { get; }

        IList<INode> openNodes;
        IList<INode> closeNodes;

        const int straightCost = 10;
        const int diagonalCost = 14;

        public PathFinding(IGrid pGrid)
        {
            mGrid = pGrid;
            //mGrid = new Grid(pMapWidth, pMapHeight, pTileSizeWidth, pTileSizeHeight);
            openNodes = new List<INode>();
            closeNodes = new List<INode>();
        }

        public IList<Vector2> FindPath(Vector2 pStartPos, Vector2 pTargetPos)
        {
            if (pStartPos == pTargetPos)
            {
                return new List<Vector2>();
            }

            // Get start node grid position
            INode startNode = mGrid.GetNodePosition(pStartPos);
            // Get target node grid position
            INode targetNode = mGrid.GetNodePosition(pTargetPos);
            // The current node is the start node
            INode currentNode = startNode;

            // keep track of the 'ring'
            var frontier = new Queue<INode>();
            frontier.Clear();
            // add it to the queue
            frontier.Enqueue(startNode);
            // we say its been visited
            startNode.Visited = true;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                Console.WriteLine("Visiting {0}", current.gridPos);

                if (current == targetNode)
                {
                    return GetFinalPath(startNode, targetNode, current);
                }

                foreach (var next in mGrid.GetNeighbourNodes(current))
                {
                    if (next != current)
                    {
                        frontier.Enqueue(next);
                        next.Visited = true;
                        currentNode = current;
                    }
                }
            }

            return null;


            #region
            //// If the start position and target position is the same return
            //if (pStartPos == pTargetPos)
            //{
            //    return new List<Vector2>();
            //}

            //// Get start node grid position
            //INode startNode = mGrid.GetNodePosition(pStartPos);
            //// Get target node grid position
            //INode targetNode = mGrid.GetNodePosition(pTargetPos);
            //// The current node is the start node
            //INode currentNode = startNode;

            //// Clear
            //openNodes.Clear();
            //openNodes.Add(startNode);

            //while (openNodes.Count != 0)
            //{
            //    INode node = openNodes[0];
            //    for (int i = 0; i < openNodes.Count; i++)
            //    {
            //        if (openNodes[i].FCost < currentNode.FCost || openNodes[i].FCost == currentNode.FCost && openNodes[i].HCost < currentNode.HCost)
            //        {
            //            currentNode = openNodes[i];
            //        }
            //    }
            //    openNodes.Remove(currentNode);
            //    currentNode.Visited = true;
            //    closeNodes.Add(currentNode);

            //    if (currentNode == targetNode)
            //    {
            //        return GetFinalPath(startNode, targetNode);
            //    }

            //    foreach (INode neighbour in mGrid.GetNeighbourNodes(currentNode))
            //    {
            //        if (neighbour.Walkable || !closeNodes.Contains(neighbour))
            //        {
            //            startNode.HCost = EstimateHCost(startNode, targetNode);
            //            int movementCost = currentNode.GCost + EstimateHCost(currentNode, neighbour);

            //            if (movementCost < neighbour.GCost || !openNodes.Contains(neighbour))
            //            {
            //                neighbour.GCost = movementCost;
            //                neighbour.HCost = EstimateHCost(neighbour, targetNode);
            //                neighbour.Parent = currentNode;

            //                if (!openNodes.Contains(neighbour))
            //                {
            //                    openNodes.Add(neighbour);
            //                    neighbour.Visited = true;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            closeNodes.Add(neighbour);
            //        }
            //    }
            //}

            //return null;
            #endregion
        }



        /// <summary>
        /// Tracing the path backwards
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="targetNode"></param>
        private IList<Vector2> GetFinalPath(INode startNode, INode targetNode, INode cameFrom)
        {
            //IList<INode> path = new List<INode>();
            IList<Vector2> path = new List<Vector2>();

            INode currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.gridPos);
                currentNode = currentNode.Parent;
            }

            return path.Reverse().ToList();
        }

        public int EstimateHCost(INode pStartNode, INode pTargetNode)
        {
            int distanceX = (int)Math.Abs(pStartNode.Position.X - pTargetNode.Position.X);
            int distanceY = (int)Math.Abs(pStartNode.Position.Y - pTargetNode.Position.Y);
            
            if (distanceX > distanceY)
            {
                return diagonalCost * distanceY + straightCost * (distanceX - distanceY);
            }

            return diagonalCost * distanceX + straightCost * (distanceY - distanceX);
        }
        
    }
}
