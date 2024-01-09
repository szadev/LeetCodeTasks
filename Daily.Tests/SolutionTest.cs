namespace Daily.Tests
{
    public class SolutionTest
    {
        [InlineData(new int[] { 2, 4, 6, 8, 10 }, 7)]
        [InlineData(new int[] { 7, 7, 7, 7, 7 }, 16)]
        [InlineData(new int[] { 0, 2000000000, -294967296 }, 0)]
        [Theory]
        public void NumberOfArithmeticSlices_Test(int[] nums, int expected)
        {
            var res = Solution.NumberOfArithmeticSlices(nums);

            Assert.Equal(expected, res);
        }


        [InlineData(new object?[] { 10, 5, 15, 3, 7, null, 18 }, 7, 15, 32)]
        [InlineData(new object?[] { 10, 5, 15, 3, 7, 13, 18, 1, null, 6}, 6, 10, 23)]
        [Theory]
        public void RangeSumBST_Test(object[] nums, int low, int high, int expected)
        {
            var root = Logic.GetTreeNode(nums);

            Assert.NotNull(root);

            var res = Solution.RangeSumBST(root, low, high);

            Assert.Equal(expected, res);
        }

        [InlineData(new int[] { 2, 7, 11, 15 }, 9, new int[] { 0, 1 })]
        [InlineData(new int[] { 3, 2, 4 }, 6, new int[] { 1, 2 })]
        [InlineData(new int[] { 3, 3 }, 6, new int[] { 0, 1 })]
        [Theory]
        public void TwoSum_Test(int[] nums, int target, int[] expected)
        {
            var res = Solution.TwoSum(nums, target);

            Assert.Equal(expected, res);
        }
    }
}