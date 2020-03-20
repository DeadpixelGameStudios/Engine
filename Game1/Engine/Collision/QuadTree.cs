using Engine.Shape;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

//Adapted from: https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374


namespace Engine.Collision
{
    class QuadTree
    {
        private int MaxObjects = 3;
        private int MaxLevels = 3;

        private int level;
        private List<IShape> EntityList;
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
            EntityList = new List<IShape>();
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
            EntityList = new List<IShape>();
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
            //Clear ent list of current node
            EntityList.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                //Call clear on all child nodes
                if (nodes[i] != null)
                {
                    nodes[i].clear();
                    //Once cleared dispose of the node
                    nodes[i] = null;
                }
            }
        }
        
        /// <summary>
        /// Split into 4 nodes
        /// </summary>
        private void split()
        {
            // Half width of boundary
            int subWidth = (int)(bounds.Width / 2);
            //Half height of boundary
            int subHeight = (int)(bounds.Height / 2);
            int x = (int)bounds.X;
            int y = (int)bounds.Y;

            //Create each of the new child nodes (4 in total) which will be initialised with their level increased by one and a new bound set 
            nodes[0] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }
        
        /// <summary>
        /// sets the pEnts node , if the entity doesnt fit into one node it is placed in the parent node (index -1)
        /// </summary>
        /// <param name="pEnt">Entity to find index of</param>
        /// <returns></returns>
        private int getIndex(IShape pEnt)
        {
            Vector2 entPos = pEnt.GetPosition();
            Rectangle hitBox = pEnt.GetBoundingBox();
            // look at storing in each node instead of -1
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // entity fits into top quad
            bool topQuadrant = (entPos.Y < horizontalMidpoint && entPos.Y + hitBox.Height < horizontalMidpoint);
            // entity fits into bottom quad
            //bool bottomQuadrant = (pEnt.Position.Y > horizontalMidpoint);

            // entity fits into left quad
            if (entPos.X < verticalMidpoint && entPos.X + hitBox.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (topQuadrant == false)
                {
                    index = 2;
                }
            }
            // entity fits into right quad
            else if (entPos.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (topQuadrant == false)
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
        public void insert(IShape pEnt)
        {
            #region method 2

            int index = getIndex(pEnt);

            if (index >= 0) // pRect should always go in a child if possible
            {
                if (nodes[0] != null) // we have children
                    nodes[index].insert(pEnt); // so put pRect into the child
                else // we don't have children
                {
                    if (level < MaxLevels)
                    {
                        split(); // so we make children if possible
                        nodes[index].insert(pEnt); // and put pRect into the child
                    }
                    else // node can't split any more so put it here
                        EntityList.Add(pEnt);
                }
            }
            else
                EntityList.Add(pEnt); // index == -1 so add here

            #endregion

            #region Method 1 working
            ////Check nodes are created
            //if (nodes[0] != null)
            //{
            //    //Sets index  from -1 to 3 depending on the entities location eg top left quadrant would be 1. 
            //    int index = getIndex(pEnt);

            //    //If the entity doesnt go into the parent node
            //    if (index != -1)
            //    {
            //        //insert entity into either top left/right or bottom left/right node depending on its found index
            //        nodes[index].insert(pEnt);

            //        return;
            //    }
            //}
            ////Add entity to entity list for current node
            //EntityList.Add(pEnt);

            ////if the number of entities in the current node exceeds the maxObjects then split the current node into four seperate ones
            //if (EntityList.Count > MaxObjects && level < MaxLevels)
            //{
            //    //if the child nodes havent already been created
            //    if (nodes[0] == null)
            //    {
            //        //call split to create the 4 child nodes
            //        split();
            //    }
                
            //    //loop through the entity list
            //    for (int i = 0; i < EntityList.Count; i++)
            //    {
            //        //Get the current entities index (top left etc)
            //        int index = getIndex(EntityList[i]);
            //        IShape Current = EntityList[i];

            //        //if the entities found index doesnt overlap with any of the child nodes
            //        if (index != -1)
            //        {
            //            //remove the entity from this quad node
            //            EntityList.RemoveAt(i);
            //            //reinsert the just removed entity into the new child node based on its index value                    
            //            nodes[index].insert(Current);
            //        }
            //        else
            //        {
            //            //increment if the entity index is -1
            //            i++;
            //        }
            //    }
            //}

            #endregion



        }

        /// <summary>
        /// Finds all entities that could collide with a specific entity
        /// </summary>
        /// <param name="returnObjects">List of objects that could collide</param>
        /// <param name="pEnt">Entity to check for potential colliders</param>
        public void retrieve(List<IShape> returnObjects, IShape pEnt)
        {
                //if there has been a division
                if (nodes[0] != null)
                {
                    
                    int index = getIndex(pEnt);
                    //If the index of the entity to check doesnt overlap with any nodes only check the one index and its children
                    if (index != -1)
                    {
                        nodes[index].retrieve(returnObjects, pEnt);
                    }
                    else // if the entity overlaps nodes then run through all the child nodes to make sure collisions arent missed
                    {
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            nodes[i].retrieve(returnObjects, pEnt);
                    }
                    }
                }
                //As the method may get called recursively down through the nodes the list has the entities from each node added to returnobjects instead of returned at each call. this means the list gets built up as it goes through each child node
                returnObjects.AddRange(EntityList);
            }
        }

    }

