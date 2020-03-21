using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine.Pathfinding
{
    public interface IBinaryTreeNode
    {
        IBinaryTreeNode Left { get; set; }
        IBinaryTreeNode Right { get; set; }
    }
}
