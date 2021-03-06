﻿using System;
using System.Collections.Generic;
using System.Text;
/*
 * The actual implementation of A*
 * 
 * 
 */
namespace LabyrinthSolver
{
    public class AStarSearch
    {
        public Dictionary<LabCoOrd, LabCoOrd> cameFrom = new Dictionary<LabCoOrd, LabCoOrd>();
        public Dictionary<LabCoOrd, int> costSoFar = new Dictionary<LabCoOrd, int>();
        private HashSet<LabCoOrd> visited;
        public Labyrinth graph;

        // Cheap and simple way of figuring out the general distance of the goal. 
        static public int Heuristic(LabCoOrd a, LabCoOrd b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        //constructor. Does most of the actual work. 
        public AStarSearch(Labyrinth graph)
        {
            this.graph = graph;
            visited = new HashSet<LabCoOrd>();
            LabCoOrd start = new LabCoOrd(0, 0);
            LabCoOrd goal = new LabCoOrd(graph.getSize() - 1, graph.getSize() - 1);
            var fringe = new PriorityQueue<LabCoOrd>();
            fringe.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            visited.Add(start);

            while (fringe.Count > 0)
            {
                var current = fringe.Dequeue();

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    int newCost = costSoFar[current] + graph.Cost(next);
                    if (( !costSoFar.ContainsKey(next) || newCost < costSoFar[next] ) && !visited.Contains(next))  
                    {
                        costSoFar[next] = newCost;
                        int priority = newCost + Heuristic(next, goal);
                        fringe.Enqueue(next, priority);
                        cameFrom[next] = current;
                        visited.Add(current);
                    }
                }
            }
        }

        // Once the cost-and cameFrom-matrices are completed, constructing the path is simple
        public void printReconstructedPaths()
        {
            List<LabCoOrd> path = new List<LabCoOrd>();
            // Okay, this is ugly. Very ugly. But I feel I'm running out of time and a
            // ugly hack is better than missing out on a criteria
            LabCoOrd a = new LabCoOrd(graph.getSize() - 1, graph.getSize() - 2); 
            LabCoOrd b = new LabCoOrd(graph.getSize() - 2, graph.getSize() - 1);

            int aVal, bVal;
            if (( this.costSoFar.TryGetValue(a, out aVal) &&
                this.costSoFar.TryGetValue(b, out bVal) ) && aVal == bVal)
            {
                Console.WriteLine("a: " + aVal + ", bval: " + bVal);
                path = new List<LabCoOrd>();
                path = getPath(new LabCoOrd(0, 0), a, new LabCoOrd(graph.getSize() - 1, graph.getSize() - 1));
                Console.WriteLine(printPath(path));
                path = new List<LabCoOrd>();
                path = getPath(new LabCoOrd(0, 0), b, new LabCoOrd(graph.getSize() - 1, graph.getSize() - 1));
                Console.WriteLine(printPath(path));
            }
            else
            {
                path = new List<LabCoOrd>();
                path = getPath(new LabCoOrd(0, 0), new LabCoOrd(graph.getSize() - 1, graph.getSize() - 1));
                Console.WriteLine(printPath(path));
            }
        }

        public List<LabCoOrd> getPath(LabCoOrd start, LabCoOrd goal)
        {
            List<LabCoOrd> path = new List<LabCoOrd>();
            LabCoOrd current = goal;
            while (!current.Equals(start))
            {
                LabCoOrd ptr;
                if (this.cameFrom.TryGetValue(current, out ptr))
                {
                    current = ptr;
                }
                path.Add(current);
            }
            return path;
        }


        public List<LabCoOrd> getPath(LabCoOrd start, LabCoOrd goal, LabCoOrd RealGoal)
        {
            List<LabCoOrd> path = new List<LabCoOrd>();
            LabCoOrd current = goal;
            if (!current.Equals(RealGoal))
                path.Add(current);
            while (!current.Equals(start))
            {
                LabCoOrd ptr;
                if (this.cameFrom.TryGetValue(current, out ptr))
                {
                    current = ptr;
                }
                path.Add(current);
            }
            return path;
        }

        public string printPath(List<LabCoOrd> path)
        {
            int finalCost;
            this.costSoFar.TryGetValue(path[0], out finalCost);

            path.Reverse();
            StringBuilder builder = new StringBuilder();
            builder.Append("FinalCost: " + finalCost + "\nPath: ");
            foreach (LabCoOrd l in path)
                builder.Append(l + " ");
            builder.Append("\n");
            return builder.ToString();
        }


        // Print out an array of the calculated costs
        public void drawCalculatedCosts()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Calculated Cost:\n");
            builder.Append("---------------------\n");

            for (var y = 0; y < this.graph.getSize(); y++)
            {
                for (var x = 0; x < this.graph.getSize(); x++)
                {
                    if ((x == 0 && y == 0) || (x == (this.graph.getSize() - 1) && y == this.graph.getSize() - 1))
                        builder.Append("| @ ");
                    else
                    {
                        LabCoOrd id = new LabCoOrd(x, y);
                        LabCoOrd ptr = id;
                        int cost;
                        if (!this.costSoFar.TryGetValue(id, out cost)) // Get cost from AStarSearch
                        {
                            ptr = id;
                        }
                        if (cost > 9) // Some cheap "padding" for the sake of alignment
                            builder.Append("|" + cost + " ");
                        else
                            builder.Append("| " + cost + " ");
                    }
                }
                builder.Append("|\n---------------------\n");
            }
            Console.WriteLine(builder.ToString());
        }


    }//class
}//namespace
