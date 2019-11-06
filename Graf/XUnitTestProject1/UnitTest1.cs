using System;
using Xunit;
using Graf_App;
using System.Collections.Generic;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void BuildSpain_Test_AloneVertex()//проверка корректности работы при одной вершине
        {
            List<Graf> grafs3 = new List<Graf>();
            List<Graf> grafs2 = new List<Graf>();
            grafs3.Add(new Graf(1, 2, 3, 2, 1));
            grafs2 = grafs3;
            Graf graf = new Graf(0, 0, 0, 0, 0);
            graf.sets = new int[10];
            for (int i = 1; i < 10; i++)
                graf.sets[i] = i;
            graf.BuildSpanningTree(2, grafs3);
            Assert.Equal(grafs3[0].edges.distance, grafs2[0].edges.distance);
        }
        [Fact]
        public void SortByDistance_Test_MoreVertex()//проверка корректности сортировки
        {
            List<Graf> grafs3 = new List<Graf>();
            List<Graf> grafs2 = new List<Graf>();
            grafs3.Add(new Graf(1, 2, 3, 2, 1));
            grafs3.Add(new Graf(1, 4, 5, 2, 1));
            grafs3.Add(new Graf(1, 3, 2, 2, 1));
            Graf graf = new Graf(0, 0, 0, 0, 0);
            graf.sets = new int[10];
            for (int i = 1; i < 10; i++)
                graf.sets[i] = i;
            int result = 2;
            graf.BuildSpanningTree(2, grafs3);
            Assert.Equal(grafs3[1].edges.distance,result);
            Assert.Equal(grafs3[2].edges.distance,5);
        }

        [Fact]
        public void AddVertex_Test ()
        {
            List<Graf> grafsResult = new List<Graf>();
            grafsResult.Add(new Graf(1, 5, 2, 4, 2));
            grafsResult.Add (new Graf(2, 5, 2, 4, 2));
            List<Graf> grafs = new List<Graf>();
            Graf graf = new Graf(0,0,0,0,0);
            grafs.Add(graf.AddVertex(1, 5, 2, 4, 2));
            grafs.Add(graf.AddVertex(2, 5, 2, 4, 2));
            Assert.Equal(grafsResult[0].edges.U, grafs[0].edges.U);
            Assert.Equal(grafsResult[0].edges.V, grafs[0].edges.V);
            Assert.Equal(grafsResult[1].edges.U, grafs[1].edges.U);
            Assert.Equal(grafsResult[1].edges.V, grafs[1].edges.V);
            Assert.Equal(grafsResult[0].edges.distance, grafs[0].edges.distance);
            Assert.Equal(grafsResult[0].edges.edgesCount, grafs[0].edges.edgesCount);
            Assert.Equal(grafsResult[0].edges.verticlesCount, grafs[0].edges.verticlesCount);
            Assert.Equal(grafsResult[1].edges.distance, grafs[1].edges.distance);
            Assert.Equal(grafsResult[1].edges.edgesCount, grafs[1].edges.edgesCount);
            Assert.Equal(grafsResult[1].edges.verticlesCount, grafs[1].edges.verticlesCount);
        }
    }
}
