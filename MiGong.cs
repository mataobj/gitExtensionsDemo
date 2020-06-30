using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SortDictionary
{
    class Item
    {
        public String Name { get; set; }
        public Int32 Rank { get; set; }
        public Int32 SubRank { get; set; }
    }

    class Pos
    {
        public Pos(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public Int32 X
        {
            get { return m_x; }
            set { m_x = value; }
        }
        public Int32 Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        private Int32 m_x = 0;
        private Int32 m_y = 0;
    }

    class Program
    {
        static void init()
        {
            MG = new int[M, N] {
                { 0,1,0,0,0,0,0},
                { 0,1,0,0,0,1,1},
                { 0,1,1,0,0,1,1},
                { 0,0,0,1,0,0,0},
                { 0,1,0,0,0,1,0}
            };

            DIR = new Pos[4] {
                new Pos(0, -1), // 上
                new Pos(-1, 0), // 左
                new Pos(0, 1),  // 下
                new Pos(1, 0),  // 右
            };
        }

        static void print()
        {
            for(int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(MG[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        static bool FindPath()
        {
            Queue<Pos> path = new Queue<Pos>();
            if (MG[0, 0] != 0)
                return false;

            path.Enqueue(new Pos(0, 0));
            while(path.Count > 0)
            {
                var topPos = path.Dequeue();
                MG[topPos.X, topPos.Y] = 9; // 标志已经走过了

                if (topPos.X == M-1 && topPos.Y == N-1)
                {
                    // 走到终点了
                    return true;
                }

                bool findone = false;
                for (int i = 0; i < 4; i++)
                {
                    Int32 newX = topPos.X + DIR[i].X;
                    Int32 newY = topPos.Y + DIR[i].Y;

                    if (newX >= 0 && newX < M && newY >= 0 && newY < N)
                    {
                        // 只判断合法的
                        if (MG[newX, newY] == 0)
                        {
                            findone = true;
                            // 可以走还没有走的格子, 入队
                            path.Enqueue(new Pos(newX, newY));
                        }
                    }
                }

                if (!findone)
                {
                    // 这个点后面没有通路了
                    MG[topPos.X, topPos.Y] = 0;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            init();

            Console.WriteLine("before: ");
            print();

            Console.WriteLine("begin findPath... ");
            if (FindPath())
            {
                Console.WriteLine("find Path!");
            }
            else
            {
                Console.WriteLine("cannot find Path...");
            }

            Console.WriteLine("after: ");
            print();
        }

        private static Int32[,] MG;
        private static Pos[] DIR;
        public const Int32 M = 5;
        public const Int32 N = 7;
    }
}
