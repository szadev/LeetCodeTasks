namespace Daily
{
    public static class Solution
    {
        #region Easy
        
        /// <summary>
        /// 1. Two Sum
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns>Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.</returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            #region O(n2)
            //var result = new int[2];

            //for (int i = 0; i < nums.Length; i++)
            //{
            //    for (int j = i + 1; j < nums.Length; j++)
            //    {
            //        if (nums[i] + nums[j] == target)
            //        {
            //            result[0] = i;
            //            result[1] = j;
            //            return result;
            //        }
            //    }
            //}

            //return result;
            #endregion

            #region O(n)
            var result = new int[2];

            var diffs = new Dictionary<int, int>(nums.Length);

            for (int i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (diffs.ContainsKey(target - num))
                {
                    result[0] = diffs[target - num];
                    result[1] = i;
                    return result;
                }
                else diffs[num] = i;
            }

            return result;
            #endregion

        }

        /// <summary>
        /// 872. Leaf-Similar Trees
        /// </summary>
        /// <param name="root1"></param>
        /// <param name="root2"></param>
        /// <returns>Return true if and only if the two given trees with head nodes root1 and root2 are leaf-similar.
        /// Two binary trees are considered leaf-similar if their leaf value sequence is the same.</returns>
        public static bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            // Could we scan 2 root in depth and compare each leaf in similar position to aviod allocation?

            List<int> leaf1 = [];
            List<int> leaf2 = [];

            FillLeaf(root1, leaf1);
            FillLeaf(root2, leaf2);

            if (leaf1.Count != leaf2.Count)
                return false;

            for (int i = 0; i < leaf1.Count; i++)
            {
                if (leaf1[i] != leaf2[i])
                {
                    return false;
                }
            }

            return true;

            static void FillLeaf(TreeNode? root, List<int> leaf)
            {
                if (root == null) return;

                if (root.left is null && root.right is null)
                {
                    leaf.Add(root.val);
                }
                else
                {
                    FillLeaf(root.left, leaf);
                    FillLeaf(root.right, leaf);
                }
            }
        }

        /// <summary>
        /// 938. Range Sum of BST
        /// </summary>
        /// <param name="root"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int RangeSumBst(TreeNode root, int low, int high)
        {
            var sum = 0;

            if (low <= root.val && root.val <= high)
                sum += root.val;

            if (root.left != null)
            {
                sum += RangeSumBst(root.left, low, high);
            }

            if (root.right != null)
            {
                sum += RangeSumBst(root.right, low, high);
            }

            return sum;
        }

        /// <summary>
        /// 1704. Determine if String Halves Are Alike
        /// </summary>
        /// <param name="s">string of even length</param>
        /// <returns>Two strings are alike if they have the same number of vowels.
        /// Return true if a and b are alike. Otherwise, return false.</returns>
        public static bool HalvesAreAlike(string s) {
            var number = 0;
            var middle = s.Length / 2;
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            for (var i = 0; i < s.Length; i++)
            {
                if (!vowels.Contains(char.ToLower(s[i]))) continue;
                if (i + 1 <= middle)
                    number++;
                else number--;
            }
            return number is 0;
        }
        
        #endregion

        #region Medium

        /// <summary>
        /// 2385. Amount of Time for Binary Tree to Be Infected
        /// </summary>
        /// <param name="root"></param>
        /// <param name="start"></param>
        /// <returns>Return the number of minutes needed for the entire tree to be infected.</returns>
        public static int AmountOfTime(TreeNode root, int start)
        {
            var minutes = 0;
            DFS(root, start);
            return minutes;

            int DFS(TreeNode? node, int start)
            {
                if (node == null) return 0;

                var leftDepth = DFS(node.left, start);
                var rightDepth = DFS(node.right, start);

                if (node.val == start)
                {
                    minutes = Math.Max(leftDepth, rightDepth);
                    return -1;
                }
                else if (leftDepth >= 0 && rightDepth >= 0)
                    return Math.Max(leftDepth, rightDepth) + 1;

                minutes = Math.Max(minutes, Math.Abs(leftDepth - rightDepth));
                return Math.Min(leftDepth, rightDepth) - 1;
            }
        }

        #endregion

        #region Hard

        /// <summary>
        /// 1235. Maximum Profit in Job Scheduling
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="profit"></param>
        /// <returns></returns>
        public static int JobScheduling(int[] startTime, int[] endTime, int[] profit)
        {
            var max = 0;

            var jobs = startTime
            .Select((_, i) => new Job() { Start = startTime[i], End = endTime[i], Profit = profit[i] })
            .OrderBy(job => job.Start)
            .ToArray();

            var maxByStartDate = new int[jobs.Length];

            for (var i = jobs.Length - 1; i >= 0; i--)
            {
                maxByStartDate[i] = jobs[i].Profit;

                var localMax = 0;
                for (var j = i + 1; j < jobs.Length; j++)
                {
                    if (jobs[j].Start >= jobs[i].End && localMax < maxByStartDate[j])
                    {
                        localMax = maxByStartDate[j];
                    }
                }

                maxByStartDate[i] += localMax;

                if (max < maxByStartDate[i])
                    max = maxByStartDate[i];
            }

            return max;
        }

        /// <summary>
        /// 446. Arithmetic Slices II - Subsequence
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int NumberOfArithmeticSlices(int[] nums)
        {
            var subseqs = 0;
            var n = nums.Length;

            var dp = new Dictionary<int, int>[n]; // difference : count
            for (int i = 0; i < n; ++i)
            {
                dp[i] = new Dictionary<int, int>();
            }
            for (var i = 1; i < n; ++i)
            {
                for (var j = 0; j < i; ++j)
                {
                    var diffLong = (long)nums[i] - nums[j];

                    if (diffLong > int.MaxValue || diffLong < int.MinValue)
                    {
                        continue;
                    }

                    var diff = (int)diffLong;

                    dp[i][diff] = dp[i].GetValueOrDefault(diff) + 1;

                    if (dp[j].ContainsKey(diff))
                    {
                        dp[i][diff] += dp[j][diff];
                        subseqs += dp[j][diff];
                    }
                }
            }

            return subseqs;
        }

        #endregion

    }
}