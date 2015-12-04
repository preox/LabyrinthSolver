using System;
using System.Collections.Generic;
using System.Text;
/*
 * Labyrinth 
 * Class than represents a labyrinth. Will build according to specs
 * given in constructor. Will randomize numbers within given parameters. 
 * 
 * Is also capable of retrieving a list of valid coordinates, return the cost for a given coordinate
 * as well as a overridden tostring for those "pretty" printouts. 
 * 
 */
namespace LabyrinthSolver
{
    public class Labyrinth 
    {

        public int[,] costs;

        public int size;

        public int getSize()
        {
            return size;
        }


        // constructor. Build a somewhat random cost-matrix
        public Labyrinth(int size, int randMin, int randMax)
        {
            this.size = size;
            costs = new int[size, size];
            Random rnd = new Random();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                { 
                    costs[y,x] = rnd.Next(randMin,randMax);
                }
            
            }
        }// constructor


        // get cost
        public int Cost(LabCoOrd pos)
        {
            return costs[pos.y, pos.x];
        }

        // To be used for checking neighbours
        public static LabCoOrd[] DIRECTIONS = new[]
        {
            new LabCoOrd(1, 0),
            new LabCoOrd(0, -1),
            new LabCoOrd(-1, 0),
            new LabCoOrd(0, 1)
        };

        // validate coordinate
        public bool isValidCoordinate(LabCoOrd coord)
        {
            if (coord.x < 0 || coord.y < 0 || coord.x >= this.size || coord.y >= this.size)
            {
                return false;
            }
            return true;
        }

        // get a list of neighbours. 
        public List<LabCoOrd> Neighbors(LabCoOrd id)
        {
            List<LabCoOrd> neigh = new List<LabCoOrd>();
            foreach (var dir in DIRECTIONS)
            {
                LabCoOrd next = new LabCoOrd(id.x + dir.x, id.y + dir.y);
                if (isValidCoordinate(next))
                {
                    neigh.Add(next);
                }
            }
            return neigh;
        }

        //Prints a pretty board. Mark start and end positions. 
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("---------------------\n");
            for (int y = 0; y < this.size; y++)
            {
                for (int x = 0; x < this.size; x++)
                {
                    if ((x == 0 && y == 0) || ((x == this.size - 1 && y == this.size - 1))) // Mark start and end
                        builder.Append("|{" + costs[y, x] + "}");
                    else
                        builder.Append("| " + costs[y, x] + " ");
                }
                builder.Append("|\n---------------------\n");
            }
            return builder.ToString();
        } // tostring. 

    }//class
}//namespace
