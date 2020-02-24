using Game1.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    class PathFindingNode : IPathFindingNode
    {
        public iEntity NodePath
        {
            get;set;
        }

        public IList<IPathFindingNode> Neighbours
        {
            get; set;
        } = null;

        public bool Visited
        {
            get;
            set;
        } = false;

        public PathFindingNode()
        {
            NodePath = null;
        }

        public PathFindingNode(iEntity pNode, IList<IPathFindingNode> paths = default(IList<IPathFindingNode>))
        {
            NodePath = pNode;
            Neighbours = paths;
        }

    }
}
