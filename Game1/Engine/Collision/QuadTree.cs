using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
//Adapted from: https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374


namespace Game1.Engine.Collision
{
    class QuadTree
    {
        private int MAX_OBJECTS = 10;
        private int MAX_LEVELS = 5;

        private int level;
        private List<iEntity> objects;
        private Rectangle bounds;
        private QuadTree[] nodes;

        /*
         * Constructor
         */
        public QuadTree(int pLevel, Rectangle pBounds)
        {
            level = pLevel;
            objects = new List<iEntity>();
            bounds = pBounds;
            nodes = new QuadTree[4];

            
        }

        /*
        * Clears the quadtree
        */
        public void clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].clear();
                    nodes[i] = null;
                }
            }
        }

        /*
        * Splits the node into 4 subnodes
        */
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

        /*
        * Determine which node the object belongs to. -1 means
        * object cannot completely fit within a child node and is part
        * of the parent node
        */
        private int getIndex(iEntity pEnt)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            // Object can completely fit within the top quadrants
            bool topQuadrant = (pEnt.Position.Y < horizontalMidpoint && pEnt.Position.Y + pEnt.HitBox.Height < horizontalMidpoint);
            // Object can completely fit within the bottom quadrants
            bool bottomQuadrant = (pEnt.Position.Y > horizontalMidpoint);

            // Object can completely fit within the left quadrants
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
            // Object can completely fit within the right quadrants
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


        /*
        * Insert the object into the quadtree. If the node
        * exceeds the capacity, it will split and add all
        * objects to their corresponding nodes.
        */
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

            objects.Add(pEnt);

            if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
            {
                if (nodes[0] == null)
                {
                    split();
                }
                

                for (int i = 0; i < objects.Count; i++)
                {
                    int index = getIndex(objects[i]);
                    iEntity Current = objects[i];

                    if (index != -1)
                    {
                        objects.RemoveAt(i);
                        nodes[index].insert(Current);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        /*
        * Return all objects that could collide with the given object
        */
        public void retrieve(List<iEntity> returnObjects, iEntity pEnt)
        {
           
                if (nodes[0] != null)
                {
                    var index = getIndex(pEnt);
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

                returnObjects.AddRange(objects);
            }
        }

    }

