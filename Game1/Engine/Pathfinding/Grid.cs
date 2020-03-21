using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Engine.Pathfinding
{
    public class Grid : IGrid
    {
        public INode[,] grid { get; private set; }
        float tileSizeWidth, tileSizeHeight;

        public Grid(int pMapWidth, int pMapHeight, float pTileSizeWidth, float pTileSizeHeight)
        {
            grid = new Node[pMapWidth, pMapHeight];
            tileSizeWidth = pTileSizeWidth;
            tileSizeHeight = pTileSizeHeight;

            CreateGrid();
        }

        void CreateGrid()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Node(new Vector2(x * tileSizeWidth, y * tileSizeHeight), new Vector2(x,y));
                }
            }
        }

        public INode GetNodePosition(Vector2 pPos)
        {
            // Checking to see its not out of grid.
            if (pPos.X >= 0 && pPos.Y >= 0 && pPos.X < grid.GetLength(0) && pPos.Y < grid.GetLength(1))
            {
                return grid[(int)pPos.X, (int)pPos.Y];
            }
            return null;
        }

        public IList<INode> GetNeighbourNodes(INode pNode)
        {
            IList<INode> neighbour = new List<INode>();

            neighbour.Add(isReal((int)pNode.gridPos.X + 1, (int)pNode.gridPos.Y)); //1                          
            neighbour.Add(isReal((int)pNode.gridPos.X + 1, (int)pNode.gridPos.Y + 1)); //2                                  
            neighbour.Add(isReal((int)pNode.gridPos.X, (int)pNode.gridPos.Y + 1)); //3                                  
            neighbour.Add(isReal((int)pNode.gridPos.X - 1, (int)pNode.gridPos.Y + 1)); //4                                  
            neighbour.Add(isReal((int)pNode.gridPos.X - 1, (int)pNode.gridPos.Y)); //5                                  
            neighbour.Add(isReal((int)pNode.gridPos.X - 1, (int)pNode.gridPos.Y - 1)); //6                                  
            neighbour.Add(isReal((int)pNode.gridPos.X, (int)pNode.gridPos.Y - 1)); //7                          
            neighbour.Add(isReal((int)pNode.gridPos.X + 1, (int)pNode.gridPos.Y - 1)); //8

            IList<INode> actualList = neighbour.Where(x => x != null && x.Walkable).ToList();

            return actualList;
        }

        public INode isReal(int row, int column)
        {
            if (row >= 0 && row <= grid.GetLength(0) && column >= 0 && column <= grid.GetLength(1))
            {
                return grid[row, column];
            }

            return null;
        }
    }
}
