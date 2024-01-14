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
        /// 1026. Maximum Difference Between Node and Ancestor
        /// </summary>
        /// <param name="root"></param>
        /// <returns>Given the root of a binary tree, return the maximum value v for
        /// which there exist different nodes a and b
        /// where v = |a.val - b.val| and a is an ancestor of b.</returns>
        public static int MaxAncestorDiff(TreeNode root) {
            var max = 0;
            GetMaxMinValue(root);

            return max;

            int Substract(int anc, int desc)
            {
                return Math.Abs(anc - desc);
            }

            int GetMax(int anc, params int[] values){
                var maxValue = 0;
                foreach (var value in values)
                {
                    var sub = Substract(anc, value);
                    if (sub > maxValue)
                        maxValue = sub;
                }
                return maxValue;
            }

            // int[2] 0 - min, 1 - max
            int[] GetMaxMinValue(TreeNode node)
            {
                var values = new int[2] {node.val,node.val};

                if (node.left != null)
                {
                    var left = GetMaxMinValue(node.left);
                
                    if (left[0] < values[0])
                        values[0] = left[0];
                    if (left[1] > values[1])
                        values[1] = left[1];
                
                    var diff = GetMax(node.val, left[0], left[1]);
                    if (diff > max)
                        max = diff;
                }
                
                if (node.right != null)
                {
                    var right = GetMaxMinValue(node.right);
                
                    if (right[0] < values[0])
                        values[0] = right[0];
                    if (right[1] > values[1])
                        values[1] = right[1];
                
                    var diff = GetMax(node.val, right[0], right[1]);
                    if (diff > max)
                        max = diff;
                }

                return values;
            }
        }
        
        /// <summary>
        /// 1347. Minimum Number of Steps to Make Two Strings Anagram
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns>Return the minimum number of steps to make t an anagram of s.
        /// An Anagram of a string is a string that contains the same characters with a different (or the same) ordering.</returns>
        public static int MinSteps(string s, string t)
        {
            #region O(n) slower, but general

            // var steps = 0;
            //
            // var map = new Dictionary<char, int>(); // char : count
            // var len = s.Length;
            //
            // for (var i = 0; i < len; i++)
            // {
            //     var sLetter = s[i];
            //     map.TryAdd(sLetter, 0);
            //     map[sLetter]++;
            // }
            //
            // for (var i = 0; i < len; i++)
            // {
            //     var tLetter = t[i];
            //     if (map.TryGetValue(tLetter, out var value))
            //         map[tLetter] = --value;
            // }
            //
            // steps = map.Values.Where(x => x > 0).Sum();
            //
            // return steps;

            #endregion

            #region O(n) quick, due to context

            var arr1 = new int[26]; // 26 letters in English
            var arr2 = new int[26];

            for (var i = 0; i < s.Length; i++)
            {
                arr1[s[i] - 'a']++;
                arr2[t[i] - 'a']++;
            }

            var cnt = 0;

            for (var i = 0; i < 26; i++)
            {
                if (arr2[i] > arr1[i])
                {
                    cnt += arr2[i] - arr1[i];
                }
            }

            return cnt;

            #endregion
            
        }

        /// <summary>
        /// 1657. Determine if Two Strings Are Close
        /// </summary>
        /// <param name="word1">only lowercase English letters</param>
        /// <param name="word2">only lowercase English letters</param>
        /// <returns>Given two strings, word1 and word2, return true if word1 and word2 are close, and false otherwise.
        /// Two strings are considered close if you can attain one from the other.</returns>
        public static bool CloseStrings(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                return false;

            var arr1 = new int[26];
            var arr2 = new int[26];
            var used = new bool[26];

            for (var i = 0; i < word1.Length; i++)
            {
                arr1[word1[i] - 'a']++;
                arr2[word2[i] - 'a']++;
            }

            for (var i = 0; i < 26; i++)
            {
                if (arr1[i] != 0 && arr2[i] is 0
                    || arr2[i] != 0 && arr1[i] is 0)
                    return false;

                var found = false;

                for (var j = 0; j < 26; j++)
                {
                    if (used[j])
                        continue;

                    if (arr2[j] == arr1[i])
                    {
                        used[j] = true;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 2385. Amount of Time for Binary Tree to Be Infected
        /// </summary>
        /// <param name="root"></param>
        /// <param name="start"></param>
        /// <returns>Return the number of minutes needed for the entire tree to be infected.</returns>
        public static int AmountOfTime(TreeNode root, int start)
        {
            var minutes = 0;
            Dfs(root, start);
            return minutes;

            int Dfs(TreeNode? node, int start)
            {
                if (node == null) return 0;

                var leftDepth = Dfs(node.left, start);
                var rightDepth = Dfs(node.right, start);

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