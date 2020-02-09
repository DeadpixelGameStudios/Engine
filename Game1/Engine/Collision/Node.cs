using Game1.Engine.Entity;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Game1.Engine.Collision
{
    class Node <T>
    {

        private Rectangle bounds;
        private Node<T> next; // linked in a circular list.
        private T node; // the actual visual object being stored here.

        /// <summary>
        /// Construct new Node to wrap the given node with given bounds
        /// </summary>
        /// <param name="node">The node</param>
        /// <param name="bounds">The bounds of that node</param>
        public Node(T node, Rectangle bounds)
        {
            this.node = node;
            this.bounds = bounds;
        }

        /// <summary>
        /// The node
        /// </summary>
        public T NodeItem
        {
            get { return this.node; }
            set { this.node = value; }
        }

        /// <summary>
        /// The Rect bounds of the node
        /// </summary>
        public Rectangle Bounds
        {
            get { return this.bounds; }
        }

        /// <summary>
        /// Nodes form a linked list in the Quadrant.
        /// </summary>
        public Node<T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }


    }
}
