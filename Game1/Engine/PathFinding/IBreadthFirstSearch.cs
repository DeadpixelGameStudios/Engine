using Game1.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    interface IBreadthFirstSearch
    {
        Queue<IPathFindingNode> Nodes { get; set; }

        iEntity BFSearch(IBinaryTree binaryTree, iEntity entity);
    }
}
