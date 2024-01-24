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
        /// 70. Climbing Stairs
        /// </summary>
        /// <param name="n">Steps to reach the top</param>
        /// <returns>Each time 1 or 2 steps.
        /// Return count of distinct ways to climb to the top.</returns>
        public static int ClimbStairs(int n)
        {
            #region slower, allocation 

            //if (n == 1)
            //    return 1;

            //var steps = new int[n];
            //steps[n - 1] = 1;
            //steps[n - 2] = 2;

            //for (int i = n - 3; i >= 0; i--)
            //{
            //    steps[i] = steps[i + 1] + steps[i + 2];
            //}

            //return steps[0];

            #endregion

            #region faster, low allocation

            var n1 = 1;
            var n2 = 1;

            for (int i = n - 1; i > 0; i--)
            {
                var temp = n2;
                n2 += n1;
                n1 = temp;
            }

            return n2;

            #endregion
        }

        /// <summary>
        /// 645. Set Mismatch
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] FindErrorNums(int[] nums)
        {
            var ans = new int[2];
            var numsCount = new int[nums.Length + 1];

            for (int i = 0; i < nums.Length; i++ )
            {
                numsCount[nums[i]]++;
            }

            for (int i = 0; i < numsCount.Length; i++)
            {
                if (numsCount[i] == 0)
                {
                    ans[1] = i;
                }
                else if (numsCount[i] == 2)
                {
                    ans[0] = i;
                }
            }

            return ans;
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
        /// 1207. Unique Number of Occurrences
        /// </summary>
        /// <param name="arr">an array of integers</param>
        /// <returns>Return true if the number of occurrences of each value
        /// in the array is unique or false otherwise.</returns>
        public static bool UniqueOccurrences(int[] arr)
        {
            var map = new Dictionary<int, int>(); // value : count

            foreach (var t in arr)
            {
                map.TryAdd(t, 0);
                map[t]++;
            }

            #region simple, but slower (79 ms, 42.2 MB)
            //return map.Values.Count == map.Values.Distinct().Count();
            #endregion

            #region faster (67 ms, 42 MB)

            var hash = new HashSet<int>();
            return map.Values.All(v => hash.Add(v));
            
            #endregion
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
        /// 198. House Robber
        /// </summary>
        /// <param name="nums">Amount of money of each house</param>
        /// <returns>Returns the maximum amount of money you can rob tonight without alerting the police.
        /// It will automatically contact the police if two adjacent houses were broken into on the same night.</returns>
        public static int Rob(int[] nums)
        {
            // https://leetcode.com/problems/house-robber/solutions/156523/From-good-to-great.-How-to-approach-most-of-DP-problems/
            if (nums.Length == 0) 
                return 0;

            var prev1 = 0;
            var prev2 = 0;
            foreach (var num in nums)
            {
                var tmp = prev1;
                prev1 = Math.Max(prev2 + num, prev1);
                prev2 = tmp;
            }
            return prev1;
        }

        /// <summary>
        /// 380. Insert Delete GetRandom O(1)
        /// </summary>
        /// <returns></returns>
        public static object[] UniqueRandomizedSet(SetActions[] actions, int[] values)
        {
            var ans = new object[actions.Length];

            var set = new RandomizedSet();

            for (var i = 0; i < actions.Length; i++)
            {
                var action = actions[i];
                var val = values[i];
                ans[i] = action switch
                {
                    SetActions.Insert => set.Insert(val),
                    SetActions.Remove => set.Remove(val),
                    SetActions.GetRandom => set.GetRandom(),
                    _ => ans[i]
                };
            }
            
            return ans;
        }

        /// <summary>
        /// 907. Sum of Subarray Minimums
        /// </summary>
        /// <param name="arr">Array of integers from 1 to 3 * 10^4</param>
        /// <returns>Returns the sum of min(b), where b ranges over every (contiguous) subarray of arr. 
        /// Since the answer may be large, return the answer modulo 10^9 + 7.</returns>
        public static int SumSubarrayMins(int[] arr)
        {
            // O(n^2). For O(n) use increasing monotonic stack
            var max = 1000000007;
            var minSum = 0;

            for (var i = arr.Length - 1; i >= 0; i--)
            {
                var min = Int32.MaxValue;
                for (int j = i; j < arr.Length; j++)
                {
                    min = Math.Min(min, arr[j]);
                    minSum += min;
                }
                minSum %= max;
            }
            return minSum;
        }

        /// <summary>
        /// 931. Minimum Falling Path Sum
        /// </summary>
        /// <param name="matrix">n x n array of integers</param>
        /// <returns>Return the minimum sum of any falling path through matrix.
        /// A falling path starts at any element in the first row and chooses the element in the next row 
        /// that is either directly below or diagonally left/right.</returns>
        public static int MinFallingPathSum(int[][] matrix)
        {
            var len = matrix.Length;

            if (len is 1)
            {
                return matrix[0].Min();
            }

            var fullmin = 100 * 100;
            for (var i = len-2; i>=0; i--)
            {
                var row = matrix[i];
                var downRow = matrix[i+1];

                for (var j = 0; j < len; j++)
                {
                    var min = 100 * (len - 1 - i);
                    for (var k = j - 1; k <= j + 1 && k < len; k++)
                    {
                        if (k < 0)
                            continue;
                        if (downRow[k] < min)
                        {
                            min = downRow[k];
                        }
                    }
                    row[j] += min;

                    if (i == 0 && fullmin > row[j])
                    {
                        fullmin = row[j];
                    }
                }
            }

            return fullmin;
        }

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
                var values = new[] { node.val,node.val };

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
        /// 1239. Maximum Length of a Concatenated String with Unique Characters
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>Returns the maximum possible length of string 
        /// that formed by the concatenation of a subsequence of arr that has unique characters.</returns>
        public static int MaxLength(IList<string> arr)
        {
            return MaxLength(arr, 0);

            static int MaxLength(IList<string> arr, int i = 0, string s = "")
            {
                if (s.Distinct().Count() < s.Length) return 0;

                if (arr.Count == i) return s.Length;

                return Math.Max(
                    MaxLength(arr, i + 1, s),
                    MaxLength(arr, i + 1, s + arr[i])
                );
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
        /// 1457. Pseudo-Palindromic Paths in a Binary Tree
        /// </summary>
        /// <param name="root">a binary tree where node values are digits from 1 to 9</param>
        /// <returns>the number of pseudo-palindromic paths going from the root node to leaf nodes.</returns>
        public static int PseudoPalindromicPaths (TreeNode root)
        {
            return Count(root, new int[10]);

            int Count(TreeNode? node, int[] count)
            {
                if (node is null)
                    return 0;

                count[node.val]++;
                if (node.left is null && node.right is null)
                {
                    var oddCount = 0;
                    for (var i = 1; i <= 9; i++)
                    {
                        if (count[i] % 2 != 0)
                            oddCount++;
                    }
                    count[node.val]--;
                    return oddCount > 1 ? 0 : 1;
                }

                var left = Count(node.left, count);
                var right = Count(node.right, count);
                
                count[node.val]--;
                return left + right;
            }
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
        /// 2225. Find Players With Zero or One Losses
        /// </summary>
        /// <param name="matches"></param>
        /// <returns>Return a list answer of size 2 where:
        /// answer[0] is a list of all players that have not lost any matches.
        /// answer[1] is a list of all players that have lost exactly one match.
        /// The values in the two lists are in increasing order.</returns>
        public static IList<IList<int>> FindWinners(int[][] matches)
        {
            var ans = new List<IList<int>>
            {
                new List<int>(),
                new List<int>()
            };
        
            var looses = new SortedDictionary<int,int>(); // player : count
        
            foreach (var match in matches)
            {
                if (!looses.ContainsKey(match[0]))
                    looses[match[0]] = 0;
            
                if (!looses.ContainsKey(match[1]))
                    looses[match[1]] = 0;
                looses[match[1]]++;
            }

            foreach (var loose in looses)
            {
                switch (loose.Value)
                {
                    case 0:
                        ans[0].Add(loose.Key);
                        break;
                    case 1:
                        ans[1].Add(loose.Key);
                        break;
                }
            }

            return ans;
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