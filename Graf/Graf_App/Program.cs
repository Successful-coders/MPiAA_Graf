using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;

namespace Graf_App
{

    public class Edge
    {
        public int U = -1;
        public int V = -1;
        public int distance = -1;

        public int edgesCount = -1;
        public int verticlesCount = -1;
        public int cost = 0;
    }
    public class Graf
    {
        //static int MAX = 33000 ;
        public Edge edges = new Edge();
       // private int[,] tree = new int[MAX, 3];
        public double Cost { get; private set; }

        public int[] sets;
        public Graf(int first, int second, int distance, int vert, int edg)
        {
            edges.edgesCount = edg;
            edges.verticlesCount = vert;
            edges.U = first;
            edges.V = second;
            edges.distance = distance;
        }
        public Graf AddVertex(int first, int second, int distance, int vert, int edg)
        {
            Graf graf = new Graf(first, second,distance,vert,edg);
            return graf;
        }
        private int Find(int vertex)//возвращается называние вершины
        {
            return (sets[vertex]);
        }
        private void Join(int v1, int v2)//
        {
            if (v1 < v2)
                sets[v2] = v1;
            else
                sets[v1] = v2;
        }

        public void BuildSpanningTree(int k , List<Graf> grafs)//построение минимального остовного дерева
        {
            if (grafs.Count > 2 )
            {
                Edge temp;
                for (int i = 1; i < k; i++)
                {
                    for (int j = 1; j <= k - i; j++)
                    {
                        if (grafs[j].edges.distance > grafs[j + 1].edges.distance)
                        {
                            temp = grafs[j].edges;
                            grafs[j].edges = grafs[j + 1].edges;
                            grafs[j + 1].edges = temp;
                        }
                    }
                }

                Cost = 0;
                int t = 1;
                for (int i = 1; i <= k; i++)
                {
                    for (i = 1; i < k; i++)
                        if (Find((grafs[i].edges.U)) != Find((grafs[i].edges.V)))
                        {
                            //tree[t, 1] = grafs[i].edges.U;
                            //tree[t, 2] = grafs[i].edges.V;
                            //Cost += grafs[i].edges.distance;
                            this.Join(grafs[i].edges.U, grafs[i].edges.V);
                            t++;
                        }
                }
            }
        }
        //public void DisplayInfo(int k )//вывод вершин, которые создают минимальный остов
        //{
        //    Console.WriteLine("The Edges of the Minimum Spanning Tree are:");
        //    for (int i = 1; i < k; i++)
        //        Console.WriteLine(tree[i, 1] + " - " + tree[i, 2]);
        //}
    }

    class Program
    {
        static int MAX = 10;

        static List<Graf> ReadAddFile(string filename)//чтение из файла и создание графа
        {
            List<Graf> grafs = new List<Graf>();
            grafs.Add(null);
            int i = 0, vert = -1, edg = -1;
            foreach (string line in File.ReadLines(filename))
            {
                if (i == 0)
                {
                    vert = Convert.ToInt32(line);
                    i++;
                }
                else
                {
                    if (i == 1)
                    {
                        edg = Convert.ToInt32(line);
                        i++;
                    }
                    else
                    {
                        string[] parts = line.Split('-');
                        grafs.Add(new Graf(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), vert, edg));

                    }
                }
            }
            return grafs;
        }
        static void CreateGraf(string filename)
        {

            string writeLine = "";
            Random rnd = new Random();
            StreamWriter sw;
            FileInfo fi = new FileInfo(filename); // информация о файле 
            sw = fi.CreateText(); // или поток для записи 

            int vertex =  (rnd.Next(10, MAX));
            int edge = rnd.Next(vertex, MAX);
            int firstTop;
            int secondTop;
            int distance;
            sw.WriteLine(vertex.ToString());
            sw.WriteLine(edge.ToString());
            for (int i = 0; i < edge; i++)
            {
                firstTop = rnd.Next(1,vertex);
                secondTop = rnd.Next(1,vertex);
                distance = rnd.Next(10, 100);
                writeLine = firstTop.ToString() + "-" + secondTop.ToString() + "-" + distance.ToString();
                sw.WriteLine(writeLine);
            }
            sw.Close();

        }
        static List<Graf> AddVertex (Graf graf, List<Graf> grafs)
        {
            int vertCount = grafs[grafs.Count - 1].edges.verticlesCount;
            int edgCount = grafs[grafs.Count - 1].edges.edgesCount;
            vertCount++;
            edgCount++;
            grafs.Add(graf.AddVertex(1, 2, 3, vertCount, edgCount));
            return grafs;
        }
        static void Main(string[] args)
        {
            CreateGraf("input.txt");
            List<Graf> grafs = ReadAddFile("input.txt");
            Graf graf = new Graf(0, 0, 0, 0, 0);
            int vertCount = grafs.Count - 1;
            int edgCount = grafs.Count - 1;

            Console.WriteLine("Vert " + vertCount);
            Console.WriteLine("edge " + edgCount);

            graf.sets = new int[vertCount];
            for (int i = 1; i < vertCount; i++)
                graf.sets[i] = i;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            graf.BuildSpanningTree(vertCount, grafs);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            
            //graf.DisplayInfo(vertCount);
           // Console.WriteLine("Cost: " + graf.Cost);
            //List<Graf> grafs3 = new List<Graf>();
            //List<Graf> grafs2 = new List<Graf>();
            //grafs3.Add(new Graf(1, 2, 3, 2, 1));
            //grafs3.Add(new Graf(1, 3, 2, 2, 1));
            //Graf graf = new Graf(0, 0, 0, 0, 0);
            //graf.sets = new int[10];
            //for (int i = 1; i < 10; i++)
            //    graf.sets[i] = i;
            //int result = 2;
            //graf.BuildSpanningTree(2, grafs3);
        }
    }
}

