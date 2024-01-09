namespace Daily
{
    public class Solution
    {
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

        /// <summary>
        /// 938. Range Sum of BST
        /// </summary>
        /// <param name="root"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int RangeSumBST(TreeNode root, int low, int high)
        {
            var sum = 0;

            if (low <= root.val && root.val <= high)
                sum += root.val;

            if (root.left != null)
            {
                sum += RangeSumBST(root.left, low, high);
            }

            if (root.right != null)
            {
                sum += RangeSumBST(root.right, low, high);
            }

            return sum;
        }

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
    }
}