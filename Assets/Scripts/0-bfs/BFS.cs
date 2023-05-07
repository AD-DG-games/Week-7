﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public class BFS {
    public static void FindPath<NodeType>(
            IGraph<NodeType> graph, 
            NodeType startNode, NodeType endNode, 
            List<NodeType> outputPath, int maxiterations=1000)
    {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int i; for (i = 0; i < maxiterations; ++i) { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0) {
                break;
            } else {
                NodeType searchFocus = openQueue.Dequeue();

                if (searchFocus.Equals(endNode)) {
                    // We found the target -- now construct the path:
                    outputPath.Add(endNode);
                    while (previous.ContainsKey(searchFocus)) {
                        searchFocus = previous[searchFocus];
                        outputPath.Add(searchFocus);
                    }
                    outputPath.Reverse();
                    break;
                } else {
                    // We did not found the target yet -- develop new nodes.
                    foreach (var neighbor in graph.Neighbors(searchFocus)) {
                        if (openSet.Contains(neighbor)) {
                            continue;
                        }
                        openQueue.Enqueue(neighbor);
                        openSet.Add(neighbor);
                        previous[neighbor] = searchFocus;
                    }
                }
            }
        }
    }
    private static void FindSpace<NodeType>(IGraph<NodeType> graph, NodeType startNode, List<NodeType> space, int maxiterations)
    {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> openSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int i; for (i = 0; i < maxiterations; ++i)
        { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0)
            {
                break;
            }
            else
            {
                NodeType searchFocus = openQueue.Dequeue();

                if (openSet.Count >= maxiterations)
                {
                    foreach (var node in openSet)
                    {
                        space.Add(node);
                    }
                    return;
                    // We found the target -- now construct the path:
                    space.Add(searchFocus);
                    while (previous.ContainsKey(searchFocus))
                    {
                        searchFocus = previous[searchFocus];
                        space.Add(searchFocus);
                    }
                    space.Reverse();
                    break;
                }
                else
                {
                    // We did not found the target yet -- develop new nodes.
                    foreach (var neighbor in graph.Neighbors(searchFocus))
                    {
                        if (openSet.Contains(neighbor))
                        {
                            continue;
                        }
                        openQueue.Enqueue(neighbor);
                        openSet.Add(neighbor);
                        previous[neighbor] = searchFocus;
                    }
                }
            }
        }
    }
    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode, int maxiterations=1000) {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, maxiterations);
        return path;
    }
    public static List<NodeType> GetSpace<NodeType>(IGraph<NodeType> graph, NodeType startNode, int maxiterations = 1000)
    {
        List<NodeType> space = new List<NodeType>();
        FindSpace(graph, startNode, space, maxiterations);
        return space;
    }
}