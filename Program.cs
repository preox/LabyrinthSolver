using System;

/*
 * Labyrinth Solver
 * 
 * A project designed to solve a specific task: 
 * 
 * Given a square grid (labyrinth) with a cost of each room/node, 
 * find minimum cost path from top left corner to bottom right corner
 * 
 * Allowed directions are: left, right, up and down. No bishops allowed :)
 * 
 * 
 * So solve this, I choose to implement the A*-algorithm. https://en.wikipedia.org/wiki/A*_search_algorithm
 * 
 * I first looked into using Dijkstra’s for some time, until I stumbled upon A*. It 
 * is widley used in game and gametheory and seemed appropriate as well as a fun challange. 
 * 
 * (From wiki)
      "A* uses a best-first search and finds a least-cost path from a given initial node 
      to one goal node (out of one or more possible goals). As A* traverses the graph,
      it builds up a tree of partial paths. The leaves of this tree (called the open set or fringe) 
      are stored in a priority queue that orders the leaf nodes by a cost function, 
      which combines a heuristic estimate of the cost to reach a goal and the distance 
      traveled from the initial node. Specifically, the cost function is"
 * 
 * 
 * So I've built is as such: 
    * Construct a labyrinth
    * calculate costs and steps (done in AStarSearch.cs)
    * reconstruct path backwards, given the calculations done in above step
    * Print to screen
 * 
 * 
 * 
 * 
 * 
 */
namespace LabyrinthSolver
{
    public class Program
    {
        static void Main()
        {
            // Make new grid. Given a size, and random values. 
            var grid = new Labyrinth(5, 0, 10);

            // for testing purposes, we'r setting the node-costs to the values given in task. 
            /*grid.costs = new int[,] {
                { 1,3,2,5,9}, 
                { 6,5,1,3,3}, 
                { 4,2,1,4,5}, 
                { 8,2,8,4,1}, 
                { 7,1,2,2,3}, 
            };*/
            
            Console.WriteLine("Starting Labyrinth:\n" + grid);

            // Run A*
            var astar = new AStarSearch(grid);

            // Print results
            astar.drawCalculatedCosts();
            astar.printReconstructedPath();

        }
    } // Class program
} // Namespace
