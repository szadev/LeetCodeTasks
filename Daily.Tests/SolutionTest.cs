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

            var res = Solution.RangeSumBst(root!, low, high);

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

        [InlineData(new object?[] { 3, 5, 1, 6, 2, 9, 8, null, null, 7, 4 }, new object?[] { 3, 5, 1, 6, 7, 4, 2, null, null, null, null, null, null, 9, 8 }, true)]
        [InlineData(new object?[] { 1, 2, 3 }, new object?[] { 1, 3, 2 }, false)]
        [InlineData(new object?[] { 3, 5, 1, 6, 7, 4, 2, null, null, null, null, null, null, 9, 11, null, null, 8, 10 }, new object?[] { 3, 5, 1, 6, 2, 9, 8, null, null, 7, 4 }, false)]
        [Theory]
        public void LeafSimilar_Test(object[] nums1, object[] nums2, bool expected)
        {
            var root1 = Logic.GetTreeNode(nums1);
            var root2 = Logic.GetTreeNode(nums2);

            var res = Solution.LeafSimilar(root1!, root2!);

            Assert.Equal(expected, res);
        }

        [InlineData(new object?[] { 1, 5, 3, null, 4, 10, 6, 9, 2 }, 3, 4)]
        [InlineData(new object?[] { 1 }, 1, 0)]
        [InlineData(new object?[] { 1, 2, null, 3, null, 4, null, 5 }, 3, 2)]
        [Theory]
        public void AmountOfTime_Test(object[] nums, int start, int expected)
        {
            var root = Logic.GetTreeNode(nums);

            var res = Solution.AmountOfTime(root!, start);

            Assert.Equal(expected, res);
        }

        [InlineData("book", true)]
        [InlineData("textbook", false)]
        [Theory]
        public void HalvesAreAlike_Test(string s, bool expected)
        {
            var res = Solution.HalvesAreAlike(s);
            
            Assert.Equal(expected, res);
        }

        [InlineData(new object?[] { 8,3,10,1,6,null,14,null,null,4,7,13 }, 7)]
        [InlineData(new object?[] { 1,null,2,null,0,3}, 3)]
        [Theory]
        public void MaxAncestorDiff_Test(object[] nums, int expected)
        {
            var root = Logic.GetTreeNode(nums);
            var res = Solution.MaxAncestorDiff(root!);
            
            Assert.Equal(expected, res);
        }

        [InlineData("bab","aba",1)]
        [InlineData("leetcode","practice",5)]
        [InlineData("anagram","mangaar",0)]
        [Theory]
        public void MinSteps_Test(string s, string t, int expected)
        {
            var res = Solution.MinSteps(s, t);
            
            Assert.Equal(expected, res);
        }
    }
}