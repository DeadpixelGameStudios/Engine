namespace Game1.Engine.Pathfinding
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
