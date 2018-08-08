﻿using Advanced.Algorithms.DataStructures;
using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using System;
using System.Collections.Generic;

namespace Advanced.Algorithms.Graph
{ 
    /// <summary>
    /// A Prims algorithm implementation.
    /// </summary>
    public class Prims<T, W> where W : IComparable
    {
        /// <summary>
        /// Find Minimum Spanning Tree of given weighted graph.
        /// </summary>
        /// <returns>List of MST edges</returns>
        public List<MSTEdge<T, W>>
            FindMinimumSpanningTree(WeightedGraph<T, W> graph)
        {
            var edges = new List<MSTEdge<T, W>>();

            //gather all unique edges
            dfs(graph, graph.ReferenceVertex,
                new BMinHeap<MSTEdge<T, W>>(),
                new HashSet<T>(),
                edges);

            return edges;
        }

        /// <summary>
        /// Do DFS to pick smallest weight neighbour edges 
        /// of current spanning tree one by one.
        /// </summary>
        /// <param name="spanTreeNeighbours"> Use Fibornacci Min Heap to pick smallest edge neighbour </param>
        /// <param name="spanTreeEdges">result MST edges</param>
        private void dfs(WeightedGraph<T, W> graph, WeightedGraphVertex<T, W> currentVertex, 
            BMinHeap<MSTEdge<T, W>> spanTreeNeighbours, HashSet<T> spanTreeVertices, 
            List<MSTEdge<T, W>> spanTreeEdges)
        {
            while (true)
            {
                //add all edges to Fibornacci Heap
                //So that we can pick the min edge in next step
                foreach (var edge in currentVertex.Edges)
                {
                    spanTreeNeighbours.Insert(new MSTEdge<T, W>(currentVertex.Value, edge.Key.Value, edge.Value));
                }

                //pick min edge
                var minNeighbourEdge = spanTreeNeighbours.ExtractMin();

                //skip edges already in MST
                while (spanTreeVertices.Contains(minNeighbourEdge.Source) && spanTreeVertices.Contains(minNeighbourEdge.Destination))
                {
                    minNeighbourEdge = spanTreeNeighbours.ExtractMin();

                    //if no more neighbours to explore 
                    //time to end exploring
                    if (spanTreeNeighbours.Count == 0)
                    {
                        return;
                    }
                }

                //keep track of visited vertices
                //do not duplicate vertex
                if (!spanTreeVertices.Contains(minNeighbourEdge.Source))
                {
                    spanTreeVertices.Add(minNeighbourEdge.Source);
                }

                //Destination vertex will never be a duplicate
                //since this is an unexplored Vertex
                spanTreeVertices.Add(minNeighbourEdge.Destination);

                //add edge to result
                spanTreeEdges.Add(minNeighbourEdge);

                //now explore the destination vertex
                var graph1 = graph;
                currentVertex = graph1.Vertices[minNeighbourEdge.Destination];
            }
        }
    }
}
