using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Daily
{
    public class Logic
    {
        private static TreeNode? CreateTreeDFS(object[] data, int index)
        {
            if (index < 0 || index >= data.Length) return null;

            var val = data[index];
            if (val == null) return null;

            return new TreeNode((int)val, CreateTreeDFS(data, (index + 1) * 2 - 1), CreateTreeDFS(data, (index + 1) * 2));
        }

        /// <summary>
        /// null for missing roots
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode? GetTreeNode(object[] nums)
        {
            return CreateTreeDFS(nums, 0);
        }
    }
}
