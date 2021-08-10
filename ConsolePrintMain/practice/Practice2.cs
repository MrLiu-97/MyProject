using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrintMain.practice
{
    /// <summary>
    /// LeetCode 练习
    /// </summary>
    public class Practice2
    {
        #region 买卖股票的最佳时机
        //给定一个数组 prices ，它的第 i 个元素 prices[i] 表示一支给定股票第 i 天的价格。
        //你只能选择 某一天 买入这只股票，并选择在 未来的某一个不同的日子 卖出该股票。设计一个算法来计算你所能获取的最大利润。
        //返回你可以从这笔交易中获取的最大利润。如果你不能获取任何利润，返回 0
        //输入：[7,1,5,3,6,4]
        //输出：5
        //解释：在第 2 天（股票价格 = 1）的时候买入，在第 5 天（股票价格 = 6）的时候卖出，最大利润 = 6-1 = 5 。
        //注意利润不能是 7-1 = 6, 因为卖出价格需要大于买入价格；同时，你不能在买入前卖出股票。

        //输入：prices = [7,6,4,3,1]
        //输出：0
        //解释：在这种情况下, 没有交易完成, 所以最大利润为 0。  数组  动态规划

        public static int MaxProfit2(int[] prices)
        {
            return 1;
        }
        public static int MaxProfit(int[] prices)
        {


            // 暴力循环
            int MaxVal = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                for (int j = i + 1; j < prices.Length; j++)
                {
                    MaxVal = Math.Max(MaxVal, prices[j] - prices[i]);
                }
            }
            return MaxVal;
            //
            if (prices == null || prices.Length == 0)
                return 0;
            int maxPro = 0;//记录最大利润
            int min = prices[0];//记录数组中访问过的最小值
            for (int i = 1; i < prices.Length; i++)
            {
                min = Math.Min(min, prices[i]);
                maxPro = Math.Max(prices[i] - min, maxPro);
            }
            return maxPro;



            int tempMax = prices[0];
            int tempMin = prices[0];
            if (prices.Length == 1)
            {
                return 0;
            }
            int left = 1, right = prices.Length - 1;
            int minIndex = 0, maxIndex = right;


            return prices[maxIndex] - prices[minIndex] < 0 ? 0 : prices[maxIndex] - prices[minIndex];

        }


        public static int MaxProfit1(int[] prices)
        {
            //我写的假设描述的不准确，有歧义，重新假设如下：

            // dp[i][0] 意为第i 天及之前卖出股票所得最大利润；
            // dp[i][1] 意为第i 天及之前买入的股票的最小花费；
            // dp[i][0]有两种选择：


            // 最优解是之前某天卖出，利润更高，选择dp[i - 1][0];
            // 最优解是今天卖出，利润更高，选择（之前某天买入的股票的最小花费 + 今天卖出所得的利润），即dp[i - 1][1] + prices[i];
            // dp[i][1]有两种选择：

            // 最优解是之前某天买入，股票更便宜，选择dp[i - 1][1];
            // 最优解是今天买入，股票更便宜，由于只能买入一次，今天买入，之前就不存在买入卖出行为，所以选择 - prices[i];
            // 你根据上述假设，自己去手动求解一个例题就明白了，动态规划纯靠理解，很难。

            // 动态规划
            int length = prices.Length;
            // 确定状态 卖出股票所得最大利润  买入的股票的最小花费；
            int[,] twoArray = new int[length, 2];
            //int[][] twoArray1 = new int[length][];
            //twoArray1[0] = new int[1] { 1 };
            // 寻找初始条件
            twoArray[0, 0] = 0;         // 卖出股票所得最大利润
            twoArray[0, 1] = -prices[0]; // 买入的股票的最小花费
            //twoArray[0][0]= twoArray[i-1][1]
            for (int i = 1; i < length; i++)
            {
                twoArray[i, 0] = Math.Max(twoArray[0, 0], twoArray[i - 1, 1] + prices[i]);
                twoArray[i, 1] = Math.Max(twoArray[i - 1, 1], -prices[i]);
            }
            int res = twoArray[length - 1, 0];
            //1，动态规划解决
            //这题是让求完成一笔交易所获得的最大利润，首先我们来看一下使用动态规划该怎么解决，动态规划还是那常见的几个步骤

            //*确定状态
            //*找到转移公式
            //*确定初始条件以及边界条件
            //*计算结果

            return 1;
        }
        #endregion

        #region 斐波那契数列
        public static void Demo6()
        {
            //ClimbStairs3(3);
            int res1 = MaxProfit1(new int[] { 2, 1, 2, 1, 0, 1, 2 });   // 7, 2, 4, 5, 1  2, 1, 2, 1, 0, 1, 2     3, 2, 6, 5, 0, 3 
            int res = Fbnqsl2(5);
            Math.Sqrt(5);
            //int p= Math.Pow(1);
        }

        /// <summary>
        /// 递推求解
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Fbnqsl2(int n)
        {
            if (n <= 2)
            {
                return 1;
            }

            int temp = 0;
            int a = 1, b = 1;
            for (int i = 3; i <= n; i++)
            {
                temp = a + b;
                a = b;
                b = temp;
            }
            return temp;
        }
        // 1 1 2 3 5 8 斐波那契数列
        public static int Fbnqsl(int n)
        {
            if (n <= 2)
            {
                return 1;
            }

            return Fbnqsl(n - 1) + Fbnqsl(n - 2);
        }
        #endregion

        #region 爬楼梯
        //假设你正在爬楼梯。需要 n 阶你才能到达楼顶。
        //每次你可以爬 1 或 2 个台阶。你有多少种不同的方法可以爬到楼顶呢？
        //注意：给定 n 是一个正整数。
        //输入： 2
        //输出： 2
        //解释： 有两种方法可以爬到楼顶。
        //1.  1 阶 + 1 阶
        //2.  2 阶 记忆化搜索 数学  动态规划

        /// <summary>
        /// 动态规划
        /// </summary>
        /// <returns></returns>
        public static int ClimbStairs3(int n)
        {
            // 定义状态     创建一个数组用来保存历史数据
            //int[] arry = new int[n];

            // 找到转移关系式  找出所有可能的情况 有两种情况 每次可以 爬 1个台阶 或者 每次可以爬 2个台阶
            //arry[n] = arry[n - 1] + arry[n - 2];

            // 找出初始条件
            if (n <= 1)
            {
                return n;
            }
            int[] dp = new int[n + 1];  // +1 是因为 数组下标从0 开始 比如 n=3 数组下标只到 2 所以 +1 才能继续计算
            dp[1] = 1;
            dp[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }

        /// <summary>
        /// 非递归
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs2(int n)
        {
            if (n <= 1)
            {
                return 1;
            }
            int[] tempArray = new int[n + 1];
            tempArray[1] = 1;
            tempArray[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                tempArray[i] = tempArray[i - 1] + tempArray[i - 2];
            }
            return tempArray[n];
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs1(int n)
        {
            int res = Fibonacci(1, 1, 1);
            return res;
            if (n <= 1)
            {
                return 1;
            }

            return ClimbStairs1(n - 1) + ClimbStairs1(n - 2);
        }
        /// <summary>
        /// 尾递归
        /// </summary>
        /// <param name="n">n-1</param>
        /// <param name="a">b</param>
        /// <param name="b">a+b</param>
        /// <returns></returns>
        public static int Fibonacci(int n, int a, int b)
        {
            if (n <= 1)
                return b;
            return Fibonacci(n - 1, b, a + b);
        }

        #endregion

        #region 合并两个有序数组
        //给你两个有序整数数组 nums1 和 nums2，请你将 nums2 合并到 nums1中，使 nums1 成为一个有序数组。
        //初始化 nums1 和 nums2 的元素数量分别为 m 和 n 。你可以假设 nums1 的空间大小等于 m + n，这样它就有足够的空间保存来自 nums2 的元素
        //输入：nums1 = [1,2,3,0,0,0], m = 3, nums2 = [2,5,6], n = 3
        //输出：[1,2,2,3,5,6] 数组  双指针  排序

        /// <summary>
        /// 数组
        /// </summary>
        public static void Merge1()
        {
            int[] nums1 = { -1, 0, 0, 3, 3, 3, 0, 0, 0 }, nums2 = { 1, 2, 2 };
            int m = 6, n = 3;
            int index = 0;
            int[] arrys = new int[m + n];
            for (int i = 0; i < m; i++)
            {
                if (nums1[i] != 0)
                {
                    arrys[index++] = nums1[i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (nums2[i] != 0)
                {
                    arrys[index++] = nums2[i];
                }
            }
            for (int i = 0; i < arrys.Length; i++)
            {
                int tempIndex = i;
                for (int j = i + 1; j < arrys.Length; j++)
                {
                    if (arrys[i] > arrys[j])
                    {
                        tempIndex = j;
                    }
                    if (i != tempIndex)
                    {
                        int temp = arrys[i];
                        arrys[i] = arrys[tempIndex];
                        arrys[tempIndex] = temp;
                        tempIndex = i;
                    }
                }

            }
            Array.Copy(arrys, nums1, arrys.Length);
        }
        /// <summary>
        /// 双指针
        /// </summary>
        public static void Merge()
        {
            int[] nums1 = { -1, 0, 0, 3, 3, 3, 0, 0, 0 }, nums2 = { 1, 2, 2 };
            int m = 6, n = 3;
            if (m + n == 1)
            {
                return;
            }
            int left = 0, right = 0;
            while (right <= n)
            {
                if (nums1[left] == 0)
                {
                    nums1[left++] = nums2[right++];
                }
                else
                {
                    left++;
                }
            }
            Array.Sort(nums1);
        }


        #endregion

        #region 环形链表
        public static void Demo08()
        {
            //给定一个链表，判断链表中是否有环。
            //如果链表中有某个节点，可以通过连续跟踪 next 指针再次到达，则链表中存在环。 
            //为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。
            //如果 pos 是 - 1，则在该链表中没有环。注意：pos 不作为参数进行传递，仅仅是为了标识链表的实际情况。
            //如果链表中存在环，则返回 true 。 否则，返回 false 。
            //进阶：
            //你能用 O(1)（即，常量）内存解决此问题吗？
            //输入：head = [3, 2, 0, -4], pos = 1
            //输出：true
            //解释：链表中有一个环，其尾部连接到第二个节点。

            int[] array = { 3, 2, 0, -4 };
            ListNode listNode = new ListNode();
            ListNode head = listNode;

            for (int i = 0; i < array.Length; i++)
            {
                listNode.next = new ListNode(array[i]);
                listNode = listNode.next;
            }
            bool res = HasCycle1(head.next);
        }

        public static bool HasCycle1(ListNode head)
        {
            HashSet<ListNode> hs = new HashSet<ListNode>();

            while (head != null)
            {
                if (!hs.Add(head))
                {
                    return true;
                }
                head = head.next;
            }
            return false;
        }

        /// <summary>
        /// 快慢指针
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            if (head == null)
            {
                return false;
            }
            ListNode fastNode = head;
            ListNode slowNode = head;
            while (fastNode != null && fastNode.next != null)
            {
                fastNode = fastNode.next.next;
                slowNode = slowNode.next;
                if (fastNode == slowNode)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 回文链表
        public static void Demo07()
        {
            //请判断一个链表是否为回文链表。
            //输入: 1->2      输入: 1->2->2->1
            //输出: false     输出: true
            //进阶：   栈   递归  链表  双指针
            //你能否用 O(n) 时间复杂度和 O(1) 空间复杂度解决此题？
            //List<int> listInt = new List<int>();
            int[] arry = { 1, 2 };
            ListNode node = new ListNode();
            ListNode head = node;
            for (int i = 0; i < arry.Length; i++)
            {
                node.next = new ListNode(arry[i]);
                node = node.next;
            }

            //ListNode = head.next;
            bool res3 = IsPalindrome3(head.next);

            bool res2 = IsPalindrome2(head.next);

            bool res1 = IsPalindrome1(head.next);

            //bool res = IsPalindrome(head.next);

            //Stack<int> stack = new Stack<int>();
        }
        public static bool isPalindrome(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }

            ListNode fast = head;
            ListNode slow = head;
            ListNode pre = head;
            ListNode prepre = null;
            while (fast != null && fast.next != null)
            {
                pre = slow; // 1 2 2 1 => 1 2 1

                slow = slow.next;   //  1 2 1 => 2 1
                fast = fast.next.next;  // 2 1 => null

                pre.next = prepre;  // 1 => 1 1
                prepre = pre;   // 1 => 1 1
                                // 取 slow 和 pre
            }
            if (fast != null)
            {
                slow = slow.next;
            }
            while (pre != null && slow != null)
            {
                if (pre.val != slow.val)
                {
                    return false;
                }
                pre = pre.next;
                slow = slow.next;
            }
            return true;
        }

        /// <summary>
        /// 快慢指针
        /// </summary>
        /// <returns></returns>
        public static bool IsPalindrome3(ListNode head) // 1 2 2 1
        {
            {
                ListNode fastNode = head;
                ListNode slowNode = head;   // 拿到后半部分节点

                while (fastNode != null && fastNode.next != null)
                {
                    fastNode = fastNode.next.next;
                    slowNode = slowNode.next;
                }
                // 将后半部分节点反转
                ListNode preNode = null;
                while (slowNode != null)
                {
                    ListNode tempNode = slowNode.next;
                    slowNode.next = preNode;
                    preNode = slowNode;
                    slowNode = tempNode;
                }
                // 与头结点作对比
                while (preNode != null)
                {
                    if (head.val != preNode.val)
                    {
                        return false;
                    }
                    preNode = preNode.next;
                    head = head.next;
                }

                return true;
            }
            {
                ListNode fastNode = head;
                ListNode slowNode = head;

                ListNode preNode1 = head;
                ListNode preNode2 = null;
                while (fastNode != null && fastNode.next != null)
                {
                    // 拿到 慢指针链表的数据
                    preNode1 = slowNode;    // 1 2 2 1  => 2 2 1
                                            // 快指针先走两倍
                    fastNode = fastNode.next.next;  // 2 1  => null
                                                    // 慢指针走一倍
                    slowNode = slowNode.next;   // 2 2 1 => 2 1
                                                // 拿到第一个头结点
                    preNode1.next = preNode2;   // 1 => 2 1
                                                // 将第一个头结点给到 preNode2
                    preNode2 = preNode1;    // 1 => 2 1

                }
                if (fastNode != null)
                {
                    slowNode = slowNode.next;
                }
                while (slowNode != null && preNode1 != null)
                {
                    if (slowNode.val != preNode1.val)
                    {
                        return false;
                    }
                    slowNode = slowNode.next;
                    preNode1 = preNode1.next;
                }
                return true;
            }
        }
        public static ListNode Revers(ListNode head)
        {
            ListNode node = null;
            while (head != null)
            {
                ListNode tempNode = head.next;
                head.next = node;
                node = head;
                head = tempNode;
            }
            return node;
        }
        public static ListNode ListNode;
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool IsPalindrome2(ListNode head)
        {
            if (head != null)
            {
                if (!IsPalindrome2(head.next))
                {
                    return false;
                }
                if (ListNode.val != head.val)
                {
                    return false;
                }
                ListNode = ListNode.next;
                //head = head.next;
            }
            return true;
        }

        /// <summary>
        /// 栈
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns> 
        public static bool IsPalindrome1(ListNode head)
        {
            // 创建 栈 集合 由于是 后进先出
            Stack<int> stack = new Stack<int>();
            // 创建存储 元数据的临时节点
            ListNode tempNode = head;
            // 将节点依次压入 栈 中
            while (tempNode != null)
            {
                stack.Push(tempNode.val);
                tempNode = tempNode.next;
            }
            // 当节点不等于 null
            while (head != null)
            {
                // 依次出栈 跟 头结点 做比较
                if (head.val != stack.Pop())
                {
                    return false;
                }
                // 将下个节点赋给 头结点
                head = head.next;
            }
            return true;
        }

        /// <summary>
        /// 双指针
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool IsPalindrome(ListNode head)
        {
            List<int> listInt = new List<int>();

            // 依次将每个节点的值取出来
            while (head != null)
            {
                listInt.Add(head.val);
                head = head.next;
            }
            // 从集合的第一个元素 和 集合的最后一个元素开始匹配
            int left = 0, right = listInt.Count - 1;

            while (left < right)
            {
                // 如果集合前面的元素和后面的元素不匹配 返回 false
                if (!listInt[left].Equals(listInt[right]))
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }
        #endregion

        #region 合并两个有序链表
        public static ListNode MergeTwoLists1(ListNode l1, ListNode l2) // 1 2 3
        {
            if (l1 == null || l2 == null)
                return l1 ?? l2;

            ListNode node = new ListNode();
            ListNode listNode = node;
            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    node.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    node.next = l2;
                    l2 = l2.next;
                }
                node = node.next;
            }
            node.next = l1 == null ? l2 : l1;
            //node = node.next;
            return node;
        }
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2) // 1 2 3
        {
            if (l1 == null || l2 == null)
                return l1 ?? l2;

            if (l1.val <= l2.val)
            {
                l1.next = MergeTwoLists(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = MergeTwoLists(l1, l2.next);
                return l2;
            }

            //ListNode node = MergeTwoLists(l1.next, l2);

            //return node;
        }
        public static ListNode1 MergeTwoListsDg(ListNode1 l1, ListNode1 l2)
        {
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            ListNode1 listNode = new ListNode1();
            ListNode1 node1 = listNode;

            if (l1.val <= l2.val)
            {
                listNode.next = l1;
                l1 = MergeTwoListsDg(l1.next, l2);
            }
            else
            {
                listNode.next = l2;
                l2 = MergeTwoListsDg(l1, l2.next);
            }

            //while (l1 != null && l2 != null)
            //{
            //    if (l1.val <= l2.val)
            //    {
            //        listNode.next = l1;
            //        l1 = MergeTwoListsDg(l1.next, l2);
            //    }
            //    else
            //    {
            //        listNode.next = l2;
            //        l2 = MergeTwoListsDg(l1, l2.next);
            //    }
            //    listNode = listNode.next;
            //}

            node1.next = l1 == null ? l2 : l1;
            return listNode;
        }
        public static ListNode1 MergeTwoLists1(ListNode1 l1, ListNode1 l2)
        {

            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;

            ListNode1 listNode = new ListNode1();
            ListNode1 newNode = listNode;
            // listNode  newNode 两个 可以随意存储  随便存储 在哪个链表中都可以
            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    newNode.next = l1;//new ListNode1(l1.val);
                    l1 = l1.next;
                }
                else
                {
                    newNode.next = l2;//new ListNode1(l2.val);
                    l2 = l2.next;
                }
                newNode = newNode.next;
            }

            //l1 == null ? l2 : l1;
            // 当有一个链表为空时 另一个链表中还有数据 则需要吧不为空的链表数组挂上去
            newNode.next = l1 == null ? l2 : l1;
            return listNode.next;
        }
        public static void Demo05()
        {
            //将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 
            //输入：l1 = [1, 2, 4], l2 = [1, 3, 4]
            //输出：[1,1,2,3,4,4]  递归 链表
            int[] l1 = { 1, 3, 5 }, l2 = { 1, 3, 4 };

            ListNode listNode1 = new ListNode();
            ListNode node1 = listNode1;
            for (int i = 0; i < l1.Length; i++)
            {
                node1.next = new ListNode(l1[i]);
                node1 = node1.next;
            }

            ListNode listNode2 = new ListNode(l2[0]);
            ListNode node2 = listNode2;
            for (int i = 1; i < l2.Length; i++)
            {
                listNode2.next = new ListNode(l2[i]);
                listNode2 = listNode2.next;
            }

            var node = MergeTwoLists1(listNode1.next, node2);
        }
        #endregion

        #region 反转链表
        public static ListNode reverse1(ListNode head)
        {
            ListNode node = null;
            while (head != null)
            {
                ListNode next = head.next;
                head.next = node;
                node = head;
                head = next;
            }
            return node;
        }
        //反转链表
        public static ListNode1 reverse(ListNode1 head) // 123 => 2 3
        {
            ListNode1 prev = null;  // null => 1 =>2 1
            while (head != null)
            {
                ListNode1 next = head.next; //  2 3 => 3 => null
                head.next = prev;   // null => 2 1 => 3 2 1
                prev = head;    // 1 => 2 1 => 3 2 1 
                head = next;    // 2 3 => 3 => null
            }
            return prev;
        }

        public static ListNode1 ReverseList(ListNode1 head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            // return listNode 每次递归返回 1个节点 5 | 5 -> 4|5-> 4 -> 3|5 -> 4-> 3-> 2
            // head.next: 4 5 | head.next: 3  4|head.next: 2 3 |head.next: 1 2 
            ListNode1 listNode = ReverseList(head.next);
            // 通过 head.next 获取下个节点  5 | 4 | 3 | 2
            ListNode1 node1 = head.next;
            // node1: 5 -> 4 5 | 4-> 3 4 | 3-> 2 3 | 2-> 1 2
            node1.next = head;
            //  head.next: 4  5->null |node1:5 4  | node1:4 3 | node1:3 2 | node1:2 1
            head.next = null;

            return listNode;
        }
        public static void Demo04()
        {
            //反转链表
            //给你单链表的头节点 head ，请你反转链表，并返回反转后的链表。

            //输入：head = [1, 2, 3, 4, 5]
            //输出：[5,4,3,2,1]
            int[] head = { 1, 2, 3 };
            ListNode1 listNode1 = new ListNode1(head[0]);
            ListNode1 listNode = listNode1;

            for (int i = 1; i < head.Length; i++)
            {
                listNode1.next = new ListNode1(head[i]);
                // 存储头部的 下一个节点
                listNode1 = listNode1.next;
            }
            var node1 = reverse(listNode);
            var node = ReverseList(listNode);
        }
        #endregion

        #region 删除链表的倒数第N个节点
        //给你一个链表，删除链表的倒数第 n 个结点，并且返回链表的头结点。
        //进阶：你能尝试使用一趟扫描实现吗？
        //输入：head = [1,2,3,4,5], n = 2
        //输出：[1,2,3,5]  双指针
        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode fastNode = head;
            ListNode slowNode = head;

            for (int i = 0; i < n; i++)
            {
                fastNode = fastNode.next;
            }
            if (fastNode == null)
            {
                return slowNode;
            }

            while (fastNode.next != null)
            {
                fastNode = fastNode.next;
                slowNode = slowNode.next;
            }
            // 相当于 改了 head 的 下个节点 4 变成 5
            slowNode.next = slowNode.next.next;
            return head;
        }
        public static void Demo06()
        {
            int[] head = { 1, 2, 3, 4, 5 };  //, 3, 4, 5
            ListNode listNode1 = new ListNode();
            ListNode headNode = listNode1;
            for (int i = 0; i < head.Length; i++)
            {
                listNode1.next = new ListNode(head[i]);
                listNode1 = listNode1.next;
            }
            var node = RemoveNthFromEnd(headNode.next, 2);
        }
        #endregion
        public static void Demo03()
        {
            {
                //删除链表的倒数第N个节点
                //给你一个链表，删除链表的 倒数 第 n 个结点，并且返回链表的头结点。
                //进阶：你能尝试使用一趟扫描实现吗？
                //输入：head = [1, 2, 3, 4, 5], n = 2
                //输出：[1,2,3,5] 双指针

                int[] head = { 1, 2, 3, 4, 5 };  //, 3, 4, 5
                ListNode1 listNode1 = new ListNode1(head[0]);
                ListNode1 headNode = listNode1;
                for (int i = 1; i < head.Length; i++)
                {
                    listNode1.next = new ListNode1(head[i]);
                    listNode1 = listNode1.next;
                }

                int n = 2;
                ListNode1 fastNode = headNode;
                ListNode1 slowNode = headNode;

                for (int i = 0; i < n; i++)
                {
                    fastNode = fastNode.next;
                }

                // 如果 fastNode 头结点 为空 返回 head 的下个节点
                if (fastNode == null)
                {
                    //return head.next;
                }

                while (fastNode.next != null)
                {
                    fastNode = fastNode.next;
                    slowNode = slowNode.next;
                }
                // 3 4 5 -> 将 当前节点的下个节点 4 替换成  当前节点的下 下 个节点 5  
                slowNode.next = slowNode.next.next; // 3 5

                // 为什么 控制 head 链表的 是 slowNode 慢指针链表
            }

            {
                //删除链表的倒数第N个节点
                //给你一个链表，删除链表的 倒数 第 n 个结点，并且返回链表的头结点。
                //进阶：你能尝试使用一趟扫描实现吗？
                //输入：head = [1, 2, 3, 4, 5], n = 2
                //输出：[1,2,3,5]

                int[] head = { 1, 2, 3, 4, 5 };
                // 存储头结点
                ListNode1 listNode = new ListNode1(head[0]);
                // 存储头结点的下一个节点
                ListNode1 newNode = listNode;
                for (int i = 1; i < head.Length; i++)
                {
                    listNode.next = new ListNode1(head[i]);
                    listNode = listNode.next;
                }

                ListNode1 newNode1 = RemoveNthFromEnd(newNode);

                int count = 1, n = 5;
                if (count == n)
                {
                    newNode1 = newNode1.next;
                }
                if (newNode1 == null)
                {

                }

                ListNode1 newNode2 = new ListNode1(newNode1.val);
                count++;
                newNode1 = newNode1.next;
                ListNode1 newNode3 = newNode2;

                while (newNode1 != null)
                {
                    if (count == n)
                    {
                        newNode1 = newNode1.next;
                    }
                    else
                    {
                        newNode2.next = new ListNode1(newNode1.val);
                        newNode2 = newNode2.next;
                        newNode1 = newNode1.next;
                    }
                    count++;
                }

                //ListNode node = null;
                //while (true)
                //{
                //    node = newNode.next;
                //}
                var node = RemoveNthFromEnd(newNode);
                //public ListNode RemoveNthFromEnd(ListNode head, int n) {
            }
        }
        public static void Demo02()
        {
            // 非静态方法 可以使用 静态方法
            // 静态方法 不能使用非静态方法
            //Demo01();

            //ListNode listNode = new ListNode(1);
            int[] head = { 4, 5, 1, 9 };
            ListNode listNode = new ListNode(4);
            {

                ListNode newNode = listNode;
                for (int i = 1; i < head.Length; i++)
                {
                    listNode.next = new ListNode(head[i]);
                    listNode = listNode.next;
                }

                while (true)
                {
                    Console.WriteLine(newNode.val);
                    //newNode.next.val;
                }
            }

            {
                // 链表数据结构
                ListNode NewNode = listNode;
                for (int i = 2; i < 10; i++)
                {
                    listNode.next = new ListNode(i);
                    listNode = listNode.next;
                }

                while (NewNode != null)
                {
                    Console.WriteLine(NewNode.val);
                    NewNode = NewNode.next;
                }

                //listNode.next.next = new ListNode(3);
            }
        }
        public static void Demo01()
        {
            {
                int[] res = { 0, 1, 0, 3, 12 };

                int left = 0, right = 1;
                while (left < res.Length && right < res.Length)
                {
                    int temp = 0;
                    if (res[left] != 0)
                    {
                        left++;
                        right++;
                    }
                    else if (res[right] != 0)
                    {
                        temp = res[left];
                        res[left] = res[right];
                        res[right] = temp;
                    }
                    else
                    {
                        right++;
                    }

                    //if (res[left] <= 0 && res[right] > 0)
                    //{
                    //    temp = res[left];
                    //    res[left] = res[right];
                    //    res[right] = temp;
                    //}
                    //else if (res[left] <= 0 && res[right] <= 0)
                    //{
                    //    right++;
                    //}

                    //left++;
                    //right++;

                }
                //0,1,0,3,12
            }

            {
                //最长公共前缀
                //编写一个函数来查找字符串数组中的最长公共前缀。
                //如果不存在公共前缀，返回空字符串 ""。

                //输入：strs = ["flower", "flow", "flight"]
                //输出："fl"

                string[] strs = { "cir", "car" };

                /// 思路二  那第一个字符串 的单个字符 跟 每个 字符串的 单个字符做对比
                //string res = "";
                StringBuilder stringBuilders = new StringBuilder();
                for (int i = 0; i < strs[0].Length; i++)
                {
                    for (int j = 1; j < strs.Length; j++)
                    {
                        // 与数组里面 每个字符串 单个字符作比较 如果 不相等==》 返回 为空 也返回
                        if (i >= strs[j].Length || !strs[0][i].Equals(strs[j][i]))
                        {
                            //return stringBuilder.ToString();
                            //return res;
                        }
                    }
                    //res += strs[0][i];
                    stringBuilders.Append(strs[0][i]);
                }
                //return res;
                // 思路一 替换数组索引 一次对比
                for (int i = 0; i < strs.Length - 1; i++)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    //string res = "";
                    for (int j = 0; j < strs[i].Length && j < strs[i + 1].Length; j++)
                    {
                        // 字符串 1 和 字符串 2 作比较
                        if (strs[i][j] != strs[i + 1][j])
                        {
                            break;
                            //res += strs[i][j];
                        }
                        stringBuilder.Append(strs[i][j]);
                    }
                    // 把相同的值给到字符串 2 替换索引
                    strs[i + 1] = stringBuilder.ToString();
                }
                //return strs[strs.Length-1]
            }

            {
                //外观数列
                //给定一个正整数 n ，输出外观数列的第 n 项。
                //「外观数列」是一个整数序列，从数字 1 开始，序列中的每一项都是对前一项的描述。
                //你可以将其视作是由递归公式定义的数字字符串序列：
                //countAndSay(1) = "1"
                //countAndSay(n) 是对 countAndSay(n-1) 的描述，然后转换成另一个数字字符串。
                //前五项如下：
                //1.     1
                //2.     11
                //3.     21
                //4.     1211
                //5.     111221
                //第一项是数字 1
                //描述前一项，这个数是 1 即 “ 一 个 1 ”，记作 "11"
                //描述前一项，这个数是 11 即 “ 二 个 1 ” ，记作 "21"
                //描述前一项，这个数是 21 即 “ 一 个 2 + 一 个 1 ” ，记作 "1211"
                //描述前一项，这个数是 1211 即 “ 一 个 1 + 一 个 2 + 二 个 1 ” ，记作 "111221"

                //输入：n = 1
                //输出："1"
                //解释：这是一个基本样例。
                string res = CountAndSay(4);
            }
        }


        /// <summary>
        /// 反转链表
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode1 RemoveNthFromEnd(ListNode1 head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            // 递归反转子链表  这里不包括 头结点
            ListNode1 node = RemoveNthFromEnd(head.next);// 5   head 4 5
                                                         // 通过head.next 获取节点 2
            ListNode1 t1 = head.next;    //t1: 5
                                         // 让 5 指向 头部 4 5 以此类推 但是有一个 问题 节点 是 5 4 5 
            t1.next = head; // t1:5 ->  4 5 
                            // 所以 要将 头部 的 下个节点 设置 为 null
            head.next = null; // 4 (5=null) 动态之后 | t1:5 -> 4 -> null
            return node;
        }

        public static string CountAndSay(int n)
        {
            if (n == 1)
                return n.ToString();

            string res = CountAndSay(n - 1);    //1211
            StringBuilder stringBuilder = new StringBuilder();
            int index = 0;
            for (int i = 1; i < res.Length + 1; i++)
            {
                if (i == res.Length)
                {
                    stringBuilder.Append(i - index).Append(res[index]);
                }
                else if (res[index] != res[i])
                {
                    stringBuilder.Append(i - index).Append(res[index]);
                    index = i;
                }
            }
            return stringBuilder.ToString();
        }

        public void DeleteNode(ListNode node)
        {
            // 请编写一个函数，使其可以删除某个链表中给定的（非末尾）节点。传入函数的唯一参数为 要被删除的节点 。

            //  node.val 要删除的节点是已知的
            // 将删除的节点的下一个节点的值给到当前删除的节点
            node.val = node.next.val;
            // 将下个节点连接到当前删除的节点
            node.next = node.next.next;
        }

        public void AddNode(int num)
        {
            ListNode listNode = new ListNode(1);
            //ListNode1 listNode1 = new ListNode1();
        }


    }
    public class ListNode1
    {
        public int val;
        public ListNode1 next;
        public ListNode1(int val = 0, ListNode1 next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x = 0, ListNode next = null) { val = x; this.next = next; }
    }

    public class Node
    {
        int date;
        Node next;
    }
}
