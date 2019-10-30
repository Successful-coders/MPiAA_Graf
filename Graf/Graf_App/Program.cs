using System;
using System.Collections.Generic;
using System.IO;

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
        public Edge edges = new Edge();
        private int[,] tree = new int[100, 3];
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

        public void BuildSpanningTree(List<Graf> grafs, int k)//построение минимального остовного дерева
        {
            Cost = 0;
            int t = 1;
            for (int i = 1; i <= k; i++)
            {
                for (i = 1; i < k; i++)
                    if (Find((grafs[i].edges.U)) != Find((grafs[i].edges.V)))
                    {
                        tree[t, 1] = grafs[i].edges.U;
                        tree[t, 2] = grafs[i].edges.V;
                        Cost += grafs[i].edges.distance;
                        this.Join(grafs[i].edges.U, grafs[i].edges.V);
                        t++;
                    }
            }
        }
        public void DisplayInfo(int k )//вывод вершин, которые создают минимальный остов
        {
            Console.WriteLine("The Edges of the Minimum Spanning Tree are:");
            for (int i = 1; i < k; i++)
                Console.WriteLine(tree[i, 1] + " - " + tree[i, 2]);
        }
    }

    class Program
    {
        static void SortByDistance(int k, List<Graf> grafs)//сортировка по весам ребер
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
        }
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
  
        static void Main(string[] args)
        {
            int MAX = 100;
            List<Graf> grafs = ReadAddFile("input.txt");

            int vertCount = grafs[1].edges.verticlesCount;
            int edgCount = grafs[1].edges.edgesCount;

            Graf graf = new Graf(0,0,0,0,0);
            graf.sets = new int[MAX];
            for (int i = 1; i <= vertCount; i++)
                graf.sets[i] = i;

            SortByDistance(vertCount, grafs);
            graf.BuildSpanningTree(grafs, vertCount);
            graf.DisplayInfo(vertCount);
            Console.WriteLine("Cost: " + graf.Cost);
        }
    }
}

