using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Pathfinding
{
    public interface IBinaryTree
    {
        IBinaryTreeNode Root { get; set; }
        void Clear();
    }
}
