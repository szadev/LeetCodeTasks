namespace Daily
{
    public static class Logic
    {
        private static TreeNode? CreateTreeDfs(object?[] data, int index, bool isPreviousNull, int shift)
        {
            if (index < 0 || index >= data.Length)
                return null;

            var val = data[index];
            if (val == null) 
                return null;

            if (isPreviousNull)
            {
                shift++;
            }
            
            var leftVal = CreateTreeDfs(data, (index + 1 - shift) * 2 - 1, isPreviousNull, shift);
            var rightVal = CreateTreeDfs(data, (index + 1 - shift) * 2, leftVal is null, shift);

            return new TreeNode((int)val, leftVal, rightVal);
        }

        /// <summary>
        /// null for missing roots
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode? GetTreeNode(object?[] nums)
        {
            return CreateTreeDfs(nums, 0, false, 0);
        }
    }
}
