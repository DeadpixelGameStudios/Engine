using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine.Pathfinding2
{
    public class Grid
    {
        public INode[,] grid;
        float cellSize;

        public int getGridXLength { get; private set; }
        public int getGridYLength { get; private set; }


        public Grid(int pMapWidth, int pMapHeight, float pCellSize)
        {
            grid = new Node[pMapWidth, pMapHeight];
            cellSize = pCellSize;

            CreateGrid();
        }

        void CreateGrid()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Node(new Vector2(x * cellSize, y * cellSize));
                }
            }

            getGridXLength = grid.GetLength(0);
            getGridYLength = grid.GetLength(1);
        }

        public INode GetNodePosition(Vector2 pPos)
        {
            if (pPos.X >= 0 && pPos.Y >= 0 && pPos.X < grid.GetLength(0) && pPos.Y < grid.GetLength(1))
            {
                return grid[(int)pPos.X, (int)pPos.Y];
            }
            return null;
        }

        void CollisionList()
        {
            
        }
    }
}
