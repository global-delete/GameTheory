using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    class Program
    {
        static void Main(string[] args)
        {

            Matrix winsMatrix = new Matrix(new double[,]
            {
                {4, 2, 3, -1 },
                {-4, 0, -2, 2 }
            });

            Solver solver= new Solver(winsMatrix);

            
            solver.MatrixGameSolution();

            Console.ReadKey();
        }
    }
}
