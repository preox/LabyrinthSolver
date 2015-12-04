using System;
using System.Collections.Generic;
/* 
 * Queue what handles the "Frontier"/"Fringe"-portion of A* algorithm. 
 * 
 */
namespace LabyrinthSolver
{
    public class PriorityQueue<T>
    {

        private List<Tuple<T, int>> elements = new List<Tuple<T, int>>();

        public int Count
        {
            get { return elements.Count; }
        }

        // Place in queue
        public void Enqueue(T item, int priority)
        {
            elements.Add(Tuple.Create(item, priority));
        }

        //dequeue the object of 
        public T Dequeue()
        {
            int bestIndex = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }
            T bestItem = elements[bestIndex].Item1;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    } // Class PriorityQueue
} // namespace
