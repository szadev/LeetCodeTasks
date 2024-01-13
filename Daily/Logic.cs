namespace Daily
{
    public class Logic
    {
        private static TreeNode? CreateTreeDFS(object[] data, int index, bool isPreviousNull)
        {
            if (index >= data.Length)
                return null;

            var val = data[index];
            if (val == null) 
                return null;

            var leftId = (index + 1) * 2 - 1;
            var rightId = (index + 1) * 2;

            if (leftId >= data.Length && isPreviousNull)
            {
                leftId -= 2;
                rightId -= 2;
            }

            var leftVal = CreateTreeDFS(data, leftId, false);
            var rightVal = CreateTreeDFS(data, rightId, leftVal is null);

            return new TreeNode((int)val, leftVal, rightVal);
        }

        /// <summary>
        /// null for missing roots
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode? GetTreeNode(object[] nums)
        {
            return CreateTreeDFS(nums, 0, false);
        }
    }
}
