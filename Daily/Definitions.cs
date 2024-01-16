﻿namespace Daily
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

    public enum SetActions
    {
        Insert,
        Remove,
        GetRandom
    }
    
    public class RandomizedSet
    {
        private readonly List<int> _nums;
        private readonly Dictionary<int, int> _indexMap;
        private readonly Random _random;
        public RandomizedSet()
        {
            _nums = [];
            _indexMap = [];
            _random = new Random();
        }
    
        public bool Insert(int val)
        {
            // Find val in HashMap is O(1)
            if (_indexMap.ContainsKey(val)) {
                return false;
            }

            _nums.Add(val);
            _indexMap[val] = _nums.Count - 1;
            return true;
        }
    
        public bool Remove(int val) 
        {
            if (!_indexMap.TryGetValue(val, out var index)) {
                return false;
            }

            var lastElement = _nums[_nums.Count - 1];

            _nums[index] = lastElement;
            _indexMap[lastElement] = index;
            
            // Removing last value in array is O(1), you do not need to shift values
            // Therefore we swap the last value and a requested one and remove the last
            _nums.RemoveAt(_nums.Count - 1);
            _indexMap.Remove(val);

            return true;
        }
    
        public int GetRandom() 
        {
            return _nums[_random.Next(_nums.Count)];
        }
    }
}
