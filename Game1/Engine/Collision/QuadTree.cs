using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Engine.Collision
{
    class QuadTree <T>
    {
        Rectangle bounds; // overall bounds we are indexing.
        Quadrant root;
        IDictionary<T, Quadrant> table;



        /// <summary>
        /// This determines the overall quad-tree indexing strategy, changing this bounds
        /// is expensive since it has to re-divide the entire thing - like a re-hash operation.
        /// </summary>
        public Rectangle Bounds
        {
            get { return this.bounds; }
            set { this.bounds = value; ReIndex(); }
        }

        /// <summary>
        /// Insert a node with given bounds into this QuadTree.
        /// </summary>
        /// <param name="node">The node to insert</param>
        /// <param name="bounds">The bounds of this node</param>
        public void Insert(T node, Rectangle bounds)
        {
           
            if (this.root == null)
            {
                this.root = new Quadrant(null, this.bounds);
            }

            Quadrant parent = this.root.Insert(node, bounds);

            if (this.table == null)
            {
                this.table = new Dictionary<T, Quadrant>();
            }
            this.table[node] = parent;


        }

        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        public IEnumerable<T> GetNodesInside(Rectangle bounds)
        {
            foreach (Node<T> n in GetNodes(bounds))
            {
                yield return n.NodeItem;
            }
        }

        /// <summary>
        /// Get a list of the nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>List of zero or mode nodes found inside the given bounds</returns>
        public bool HasNodesInside(Rectangle bounds)
        {
            if (this.root == null)
            {
                return false;
            }
            return this.root.HasIntersectingNodes(bounds);
        }

        /// <summary>
        /// Get list of nodes that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">The bounds to test</param>
        /// <returns>The list of nodes intersecting the given bounds</returns>
        IEnumerable<Node<T>> GetNodes(Rectangle bounds)
        {
            List<Node<T>> result = new List<Node<T>>();
            if (this.root != null)
            {
                this.root.GetIntersectingNodes(result, bounds);
            }
            return result;
        }

        /// <summary>
        /// Remove the given node from this QuadTree.
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>True if the node was found and removed.</returns>
        public bool Remove(T node)
        {
            if (this.table != null)
            {
                Quadrant parent = null;
                if (this.table.TryGetValue(node, out parent))
                {
                    parent.RemoveNode(node);
                    this.table.Remove(node);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Rebuild all the Quadrants according to the current QuadTree Bounds.
        /// </summary>
        void ReIndex()
        {
            this.root = null;
            foreach (Node<T> n in GetNodes(this.bounds))
            {
                Insert(n.NodeItem, n.Bounds);
            }
        }




    }

}
