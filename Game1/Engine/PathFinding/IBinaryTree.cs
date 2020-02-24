using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.PathFinding
{
    interface IBinaryTree
    {
        IBinaryTreeNode Root { get; set; }
        void Clear();
    }
}
