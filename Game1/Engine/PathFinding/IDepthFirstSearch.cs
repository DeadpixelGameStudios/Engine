using Game1.Engine.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    interface IDepthFirstSearch
    {
        //http://optlab-server.sce.carleton.ca/POAnimations2007/DijkstrasAlgo.html
        Stack<IPathFindingNode> Nodes { get; set; }

        void DFSearch(IBinaryTree binaryTree, iEntity entity);
    }
}
