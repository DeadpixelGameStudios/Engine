using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Runtime;
using Microsoft.Xna.Framework;
using Game1.Engine.Entity;

//Code adapted from this source: https://referencesource.microsoft.com/#System.Activities.Presentation/System.Activities.Presentation/System/Activities/Presentation/View/QuadTree.cs

namespace Game1.Engine.Collision
{
    class Quadrant
    {
        private Quadrant parent;
        private Rectangle bounds; // quadrant bounds.

        private Node<iEntity> nodes; // nodes that overlap the sub quadrant boundaries.

        // The quadrant is subdivided when nodes are inserted that are 
        // completely contained within those subdivisions.
        Quadrant topLeft;
        Quadrant topRight;
        Quadrant bottomLeft;
        Quadrant bottomRight;



        /// <summary>
        /// Construct new Quadrant with a given bounds all nodes stored inside this quadrant
        /// will fit inside this bounds.  
        /// </summary>
        /// <param name="parent">The parent quadrant (if any)</param>
        /// <param name="bounds">The bounds of this quadrant</param>
        public Quadrant(Quadrant parent, Rectangle bounds)
        {
            this.parent = parent;
            
            this.bounds = bounds;
        }

        /// <summary>
        /// The parent Quadrant or null if this is the root
        /// </summary>
        public Quadrant Parent
        {
            get { return this.parent; }
        }

        /// <summary>
        /// The bounds of this quadrant
        /// </summary>
        public Rectangle Bounds
        {
            get { return this.bounds; }
        }

        /// <summary>
        /// Insert the given node
        /// </summary>
        /// <param name="node">The node </param>
        /// <param name="bounds">The bounds of that node</param>
        /// <returns></returns>
        public Quadrant Insert<T>(T node, Rectangle bounds)
        {
            
            Quadrant toInsert = this;

            while (true)
            {
                int w = toInsert.bounds.Width / 2;
                if (w < 1)
                {
                    w = 1;
                }
                int h = toInsert.bounds.Height / 2;
                if (h < 1)
                {
                    h = 1;
                }

                // assumption that the Rectangle struct is almost as fast as doing the operations
                // manually since Rectangle is a value type.

                Rectangle topLeft = new Rectangle(toInsert.bounds.Left, toInsert.bounds.Top, w, h);
                Rectangle topRight = new Rectangle(toInsert.bounds.Left + w, toInsert.bounds.Top, w, h);
                Rectangle bottomLeft = new Rectangle(toInsert.bounds.Left, toInsert.bounds.Top + h, w, h);
                Rectangle bottomRight = new Rectangle(toInsert.bounds.Left + w, toInsert.bounds.Top + h, w, h);

                Quadrant child = null;

                // See if any child quadrants completely contain this node.
                if (topLeft.Contains(bounds))
                {
                    if (toInsert.topLeft == null)
                    {
                        toInsert.topLeft = new Quadrant(toInsert, topLeft);
                    }
                    child = toInsert.topLeft;
                }
                else if (topRight.Contains(bounds))
                {
                    if (toInsert.topRight == null)
                    {
                        toInsert.topRight = new Quadrant(toInsert, topRight);
                    }
                    child = toInsert.topRight;
                }
                else if (bottomLeft.Contains(bounds))
                {
                    if (toInsert.bottomLeft == null)
                    {
                        toInsert.bottomLeft = new Quadrant(toInsert, bottomLeft);
                    }
                    child = toInsert.bottomLeft;
                }
                else if (bottomRight.Contains(bounds))
                {
                    if (toInsert.bottomRight == null)
                    {
                        toInsert.bottomRight = new Quadrant(toInsert, bottomRight);
                    }
                    child = toInsert.bottomRight;
                }

                if (child != null)
                {
                    toInsert = child;
                }
                else
                {
                    Node<iEntity> n = new Node<iEntity>(node, bounds);
                    if (toInsert.nodes == null)
                    {
                        n.Next = n;
                    }
                    else
                    {
                        // link up in circular link list.
                        Node<iEntity> x = toInsert.nodes;
                        n.Next = x.Next;
                        x.Next = n;
                    }
                    toInsert.nodes = n;
                    return toInsert;
                }
            }
        }

        /// <summary>
        /// Returns all nodes in this quadrant that intersect the given bounds.
        /// The nodes are returned in pretty much random order as far as the caller is concerned.
        /// </summary>
        /// <param name="nodes">List of nodes found in the given bounds</param>
        /// <param name="bounds">The bounds that contains the nodes you want returned</param>
        public void GetIntersectingNodes(List<Node<iEntity>> nodes, Rectangle bounds)
        {
            if (bounds.IsEmpty) return;
            int w = this.bounds.Width / 2;
            int h = this.bounds.Height / 2;

            // assumption that the Rectangle struct is almost as fast as doing the operations
            // manually since Rectangle is a value type.

            Rectangle topLeft = new Rectangle(this.bounds.Left, this.bounds.Top, w, h);
            Rectangle topRight = new Rectangle(this.bounds.Left + w, this.bounds.Top, w, h);
            Rectangle bottomLeft = new Rectangle(this.bounds.Left, this.bounds.Top + h, w, h);
            Rectangle bottomRight = new Rectangle(this.bounds.Left + w, this.bounds.Top + h, w, h);

            // See if any child quadrants completely contain this node.
            if (topLeft.Intersects(bounds) && this.topLeft != null)
            {
                this.topLeft.GetIntersectingNodes(nodes, bounds);
            }

            if (topRight.Intersects(bounds) && this.topRight != null)
            {
                this.topRight.GetIntersectingNodes(nodes, bounds);
            }

            if (bottomLeft.Intersects(bounds) && this.bottomLeft != null)
            {
                this.bottomLeft.GetIntersectingNodes(nodes, bounds);
            }

            if (bottomRight.Intersects(bounds) && this.bottomRight != null)
            {
                this.bottomRight.GetIntersectingNodes(nodes, bounds);
            }

            GetIntersectingNodes(this.nodes, nodes, bounds);
        }

        /// <summary>
        /// Walk the given linked list of Nodes and check them against the given bounds.
        /// Add all nodes that intersect the bounds in to the list.
        /// </summary>
        /// <param name="last">The last Node in a circularly linked list</param>
        /// <param name="nodes">The resulting nodes are added to this list</param>
        /// <param name="bounds">The bounds to test against each node</param>
        public static void GetIntersectingNodes(Node last, List<Node> nodes, Rectangle bounds)
        {
            if (last != null)
            {
                Node n = last;
                do
                {
                    n = n.Next; // first node.
                    if (n.Bounds.Intersects(bounds))
                    {
                        nodes.Add(n);
                    }
                } while (n != last);
            }
        }

        /// <summary>
        /// Return true if there are any nodes in this Quadrant that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>boolean</returns>
        public bool HasIntersectingNodes(Rectangle bounds)
        {
            if (bounds.IsEmpty) return false;
            int w = this.bounds.Width / 2;
            int h = this.bounds.Height / 2;

            // assumption that the Rectangle struct is almost as fast as doing the operations
            // manually since Rectangle is a value type.

            Rectangle topLeft = new Rectangle(this.bounds.Left, this.bounds.Top, w, h);
            Rectangle topRight = new Rectangle(this.bounds.Left + w, this.bounds.Top, w, h);
            Rectangle bottomLeft = new Rectangle(this.bounds.Left, this.bounds.Top + h, w, h);
            Rectangle bottomRight = new Rectangle(this.bounds.Left + w, this.bounds.Top + h, w, h);

            bool found = false;

            // See if any child quadrants completely contain this node.
            if (topLeft.Intersects(bounds) && this.topLeft != null)
            {
                found = this.topLeft.HasIntersectingNodes(bounds);
            }

            if (!found && topRight.Intersects(bounds) && this.topRight != null)
            {
                found = this.topRight.HasIntersectingNodes(bounds);
            }

            if (!found && bottomLeft.Intersects(bounds) && this.bottomLeft != null)
            {
                found = this.bottomLeft.HasIntersectingNodes(bounds);
            }

            if (!found && bottomRight.Intersects(bounds) && this.bottomRight != null)
            {
                found = this.bottomRight.HasIntersectingNodes(bounds);
            }
            if (!found)
            {
                found = HasIntersectingNodes(this.nodes, bounds);
            }
            return found;
        }

        /// <summary>
        /// Walk the given linked list and test each node against the given bounds/
        /// </summary>
        /// <param name="last">The last node in the circularly linked list.</param>
        /// <param name="bounds">Bounds to test</param>
        /// <returns>Return true if a node in the list intersects the bounds</returns>
        public static bool HasIntersectingNodes(Node last, Rectangle bounds)
        {
            if (last != null)
            {
                Node n = last;
                do
                {
                    n = n.Next; // first node.
                    if (n.Bounds.Intersects(bounds))
                    {
                        return true;
                    }
                } while (n != last);
            }
            return false;
        }

        /// <summary>
        /// Remove the given node from this Quadrant.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>Returns true if the node was found and removed.</returns>
        public bool RemoveNode<T>(T node)
        {
            bool rc = false;
            if (this.nodes != null)
            {
                Node p = this.nodes;
                while (p.Next.Node != node && p.Next != this.nodes)
                {
                    p = p.Next;
                }
                if (p.Next.Node == node)
                {
                    rc = true;
                    Node n = p.Next;
                    if (p == n)
                    {
                        // list goes to empty
                        this.nodes = null;
                    }
                    else
                    {
                        if (this.nodes == n) this.nodes = p;
                        p.Next = n.Next;
                    }
                }
            }
            return rc;
        }

    }
}
}
}
