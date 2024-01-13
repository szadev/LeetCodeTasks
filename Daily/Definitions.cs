namespace Daily
{
    public class TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        public int val = val;
        public TreeNode? left = left;
        public TreeNode? right = right;
    }
    public class Job
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Profit { get; set; }
    }
}
