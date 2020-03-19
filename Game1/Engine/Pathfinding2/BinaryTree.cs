namespace Game1.Engine.Pathfinding2
{
    public class BinaryTree : IBinaryTree
    {
        public IBinaryTreeNode Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        public void Clear()
        {
            Root = null;
        }
    }
}
