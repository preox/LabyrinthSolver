/*
 * LabCoOrd
 * A struct that reflects a labyrinth coordinate. 
 * y and x
 * As well as an overridden tostring-method. . 
 *
 * Why struct and not class? 
 * Small. Very small. Just two ints. 
 */
namespace LabyrinthSolver
{
    public struct LabCoOrd
    {

        public int x, y;
        
        //constructor
        public LabCoOrd(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        // print object..
        public override string ToString()
        {
            return string.Format("[y:{0},x:{1}]", this.y, this.x);
        }
    }//class
}//namespace
