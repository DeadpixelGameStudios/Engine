using Game1.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    interface IPathFindingNode
    {
        iEntity NodePath { get; set; }

        IList<IPathFindingNode> Neighbours { get; set; }

        bool Visited { get; set; }
    }
}
