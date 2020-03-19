using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;
using Microsoft.Xna.Framework;

namespace Game1.Engine.Pathfinding2
{
    public class Node : GameEntity, INode
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=JGgSEVzneM4&list=PLD2t5VOqzPm_wAGD0D5vNf-Jd8DyO1thG&index=19&t=244s
        /// https://www.youtube.com/watch?v=fKNarmR00fw&t=24s
        /// https://www.youtube.com/watch?v=dlVwzKnV6FM&list=PLZ6ofHM1rvK8lQSoKX1USZstM-ZXikFHp&index=21
        /// https://vault16software.com/a-pathfinding-and-unit-movement-system-cmonogame/
        /// https://github.com/Vault16Software/A-Pathfinding-and-movement-system-demo/blob/master/Pathfinding%20Demo/Pathfinding%20Demo/Engine/AI/Astar.cs
        /// https://www.raywenderlich.com/3016-introduction-to-a-pathfinding#toc-anchor-010
        /// https://www.raywenderlich.com/3011-how-to-implement-a-pathfinding-with-cocos2d-tutorial
        /// https://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
        /// https://www.redblobgames.com/pathfinding/a-star/introduction.html
        /// https://www.youtube.com/watch?v=nhiFx28e7JY&t=793s
        /// https://www.youtube.com/watch?v=waEsGu--9P8
        /// https://stackoverflow.com/questions/42893205/implement-a-algorithm-with-c-sharp-understand-pseudocode
        /// </summary>
                          

        public INode Parent { get; set; }

        public IList<INode> Neighbours { get; set; } = null;

        public bool Visited { get; set; } = false;

        public bool Diagonal { get; set; }

        public bool Walkable { get; set; }

        /// <summary>
        /// Movement cost from start node to current node
        /// </summary>
        public int GCost { get; set; }

        /// <summary>
        /// Estimated movement cost from current sqaure to destination point
        /// </summary>
        public int HCost { get; set; }

        /// <summary>
        /// Total Cost
        /// </summary>
        public int FCost => GCost + HCost;

        public Node(Vector2 pPosition)
        {
            //Walkable = pWalkable;
            Position = pPosition;
        }

        public Node()
        {
            Parent = null;
        }

        public Node(INode pParent) : this(pParent, null) { }

        public Node(INode pParent, IList<INode> pNeighbours = default(IList<INode>))
        {
            Parent = pParent;
            Neighbours = pNeighbours;
        }

    }
}
