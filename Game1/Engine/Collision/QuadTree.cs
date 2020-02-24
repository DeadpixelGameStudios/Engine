using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
//Adapted from: https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374


namespace Game1.Engine.Collision
{
    class QuadTree
    {
        private int MaxObjects = 3;
        private int MaxLevels = 5;

        private int level;
        private List<iEntity> EntityList;
        private Rectangle bounds;
        private QuadTree[] nodes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLevel"></param>
        /// <param name="pBounds"></param>
        public QuadTree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            EntityList = new List<iEntity>();
            bounds = pBounds;
            nodes = new QuadTree[4];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLevel"></param>
        /// <param name="pBounds"></param>
        /// <param name="pMaxObjects"></param>
        /// <param name="pMaxLevels"></param>
        public QuadTree(int pLevel, Rectangle pBounds, int pMaxObjects, int pMaxLevels)
        {
            level = pLevel;
            EntityList = new List<iEntity>();
            bounds = pBounds;
            nodes = new QuadTree[4];

            MaxObjects = pMaxObjects;
            MaxLevels = pMaxLevels;
            
        }


        /// <summary>
        /// clears quad tree ready for reindexing
        /// </summary>
        public void clear()
        {
            EntityList.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].clear();
                    nodes[i] = null;
                }
            }
        }



        /// <summary>
        /// Split into 4 nodes
        /// </summary>
        private void split()
        {
            int subWidth = (int)(bounds.Width / 2);
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            nodes[0] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }
        
        /// <summary>
        /// Finds the pEnts node , if the entity doesnt fit into one node it is placed in the parent node (index -1)
        /// </summary>
        /// <param name="pEnt">Entity to find index of</param>
        /// <returns></returns>
        private int getIndex(iEntity pEnt)
        {

            // look at storing in each node instead of -1
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // entity fits into top quad
            bool topQuadrant = (pEnt.Position.Y < horizontalMidpoint && pEnt.Position.Y + pEnt.HitBox.Height < horizontalMidpoint);
            // entity fits into bottom quad
            bool bottomQuadrant = (pEnt.Position.Y > horizontalMidpoint);

            // entity fits into left quad
            if (pEnt.Position.X < verticalMidpoint && pEnt.Position.X + pEnt.HitBox.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // entity fits into right quad
            else if (pEnt.Position.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }
        /// <summary>
        /// Insert object into qad tree, if the capacity is reached for the node it gets split
        /// </summary>
        /// <param name="pEnt">Entity to insert</param>
        public void insert(iEntity pEnt)
        {
            if (nodes[0] != null)
            {
                int index = getIndex(pEnt);

                if (index != -1)
                {
                    nodes[index].insert(pEnt);

                    return;
                }
            }

            EntityList.Add(pEnt);

            if (EntityList.Count > MaxObjects && level < MaxLevels)
            {
                if (nodes[0] == null)
                {
                    split();
                }
                

                for (int i = 0; i < EntityList.Count; i++)
                {
                    int index = getIndex(EntityList[i]);
                    iEntity Current = EntityList[i];

                    if (index != -1)
                    {
                        EntityList.RemoveAt(i);
                        nodes[index].insert(Current);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Finds all entities that could collide with a specific entity
        /// </summary>
        /// <param name="returnObjects">List of objects that could collide</param>
        /// <param name="pEnt">Entity to check for potential colliders</param>
        public void retrieve(List<iEntity> returnObjects, iEntity pEnt)
        {
                //if there has been a division
                if (nodes[0] != null)
                {
                    
                    int index = getIndex(pEnt);
                    if (index != -1)
                    {
                        nodes[index].retrieve(returnObjects, pEnt);
                    }
                    else
                    {
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            nodes[i].retrieve(returnObjects, pEnt);
                    }
                    }
                }

                returnObjects.AddRange(EntityList);
            }
        }

    }

