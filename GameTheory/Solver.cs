using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    public delegate double DelFunction(double x);

    class Solver
    {
        public Matrix MatrixOfWinnings;

        Vector VextorValuePoint;

        Matrix MatrixFunction;

                                    //вектор минимальных выигрышей
        Vector VectorMinimumWins;

                                    //вектор максимальных выигрышей
        Vector VectorMaximumWins;

        public double LowLimit = 0;

        public double HighLimit = 0;

        public List<double> SaddlePoints;

        public Solver(Matrix matrixOfWinnings)
        {
                                                     //создаем матрицу выигрыша
            MatrixOfWinnings = new Matrix(matrixOfWinnings);

                                    //создаем вектор минимальных выигрышей 
            VectorMinimumWins = new Vector(MatrixOfWinnings.n);

                                                         //создаем вектор максимальных выигрышей
            VectorMaximumWins = new Vector(MatrixOfWinnings.m);

                                                 //создаем коллекцию седловых точек
            SaddlePoints = new List<double>();
        }

        public void MatrixGameSolution()
        {
            Console.WriteLine("Матрица выигрышей: ");
            MatrixOfWinnings.View();

            GetVectorsWins();
            Console.WriteLine("\nВектор минимальных выигрышей: ");
            VectorMinimumWins.view();

            Console.WriteLine("\nВектор максимальных выигрышей: ");
            VectorMaximumWins.view();

            GetGameLimits();
            Console.WriteLine("\nНижняя граница игры: {0}", LowLimit);
            Console.WriteLine("\nВерхняя граница игры: {0}", HighLimit);

            if (IsSaddlePoint() == true)
            {
                GetSaddlePoints();
                ViewSaddlePoints();
            }
            else
            {
                Console.WriteLine("\nСедловых точек нет!" +
                    "\nЗадача не решается чистой стратегией!");

                DeletingRows();
                DeletingColumns();
                Matrix[] matrix;
                var res = GetOptimalMixedStrategy(out matrix);

                if (res == null)
                {
                    Console.WriteLine("\nНеверная входная матрица!" +
                    "\nНе получится решить смешанными стратегиями!");
                }

                Console.WriteLine();

                if (res != null)
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        Console.WriteLine("p0 = {0} p1 = {1} v = {2}", Math.Round(res[i][0], 3), Math.Round(res[i][1], 3), Math.Round(res[i][4], 3));
                        Console.WriteLine("q0 = {0} q1 = {1} v = {2}", Math.Round(res[i][2], 3), Math.Round(res[i][3], 3), Math.Round(res[i][4], 3));

                        Console.WriteLine("\nСоответствующая матрица:");

                        matrix[i].View();
                        //matrixхъ

                        Console.WriteLine();
                    }
                }
            }
        }
        static double Function(double coefficient1, double coefficient2, double valueOfPoint)
        {
            return (coefficient1 * valueOfPoint) + (coefficient2 * (1 - valueOfPoint));
        }
        public void GetGameLimits()
        {
                                                                                                //вектор минимальных элементов по строкам
            Vector vectorMinimums = new Vector(MatrixOfWinnings.n);

                                                                                                         //проходимся по строкам матрицы
            for (int indexRow = 0; indexRow < MatrixOfWinnings.n; indexRow++)
            {
                                                                                                          //заполняем вектор минимальных элементов по строкам
                vectorMinimums[indexRow] = MatrixOfWinnings.GetRow(indexRow).GetMinimalItem();
            }

                                                                                                                                //нижняя граница игры
            LowLimit = vectorMinimums.GetMaximalItem();

            Vector vectorMaximums = new Vector(MatrixOfWinnings.m);

                                                                                                                          //проходимся по столбцам матрицы
            for (int indexColumn = 0; indexColumn < MatrixOfWinnings.m; indexColumn++)
            {
                                                                                                             //заполняем вектор максимальных элементов по столбцам
                vectorMaximums[indexColumn] = MatrixOfWinnings.GetColumn(indexColumn).GetMaximalItem();
            }

                                                                                                                             //верхняя граница игры
            HighLimit = VectorMaximumWins.GetMinimalItem();
        }

        public bool IsSaddlePoint()
        {
            if (LowLimit == HighLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetVectorsWins()
        {
                                                                                                                             //проходимся по строкам матрицы
            for (int indexRow = 0; indexRow < MatrixOfWinnings.n; indexRow++)
            {
                                                                                               //заполняем вектор минимальных выигрышей 
                VectorMinimumWins[indexRow] = MatrixOfWinnings.GetRow(indexRow).GetMinimalItem();
            }

                                                                                                  //проходимся по столбцам матрицы
            for (int indexColumn = 0; indexColumn < MatrixOfWinnings.m; indexColumn++)
            {
                                                                                                     //заполняем вектор максимальных выигрышей 
                VectorMaximumWins[indexColumn] = MatrixOfWinnings.GetColumn(indexColumn).GetMaximalItem();
            }
        }
        public void GetSaddlePoints()
        {                               
                                                 //если нижняя граница равна верхней границе
            if (LowLimit == HighLimit)
            {
                                                                                 //проходимся по вектору минимальных выигрышей
                for (int indexItem = 0; indexItem < VectorMinimumWins.GetSize(); indexItem++)
                {
                                                                                 //если текущий элемент равен нижней границе
                    if (LowLimit == VectorMinimumWins.GetElement(indexItem))
                    {
                                                                          //добавляем седловую точку в коллекцию
                        SaddlePoints.Add(VectorMinimumWins.GetElement(indexItem));
                    }
                }

                                                                      //проходимся по вектору максимальных выигрышей
                for (int indexItem = 0; indexItem < VectorMaximumWins.GetSize(); indexItem++)
                {
                                                                               //если текущий элемент равен верхней границе игры
                    if (HighLimit == VectorMaximumWins.GetElement(indexItem))
                    {
                                                                                               //добавляем седловую точку в коллекцию
                        SaddlePoints.Add(VectorMaximumWins.GetElement(indexItem));
                    }
                }
            }
        }
        public void ViewSaddlePoints()
        {
            Console.WriteLine("Вывод седловых точек:");

                                                                                     //проходимся по элементам коллекции
            for (int indexItem = 0; indexItem < SaddlePoints.Count; indexItem++)
            {
                Console.WriteLine("V{0} = {1}", indexItem + 1, SaddlePoints[indexItem]);
            }
        }
        public void DeletingRows()
        {
                                                                              //проходимся по строкам
            for (int indexRow = 0; indexRow < MatrixOfWinnings.n - 1; indexRow++)
            {
                for (int indexRow2 = 1; indexRow2 < MatrixOfWinnings.n; indexRow2++)
                {
                                                                           //если индекс первой строки не равен индексу второй строки
                    if (indexRow != indexRow2)
                    {
                                                                   //если стратегия по первой строке заведомо хуже чем стратегия по второй строке
                        if (MatrixOfWinnings.GetRow(indexRow).Dominance(MatrixOfWinnings.GetRow(indexRow2)) == 2)
                        {
                                                                   //удаляем из матрицы первую строку
                            MatrixOfWinnings = MatrixOfWinnings.DeleteRow(indexRow);

                                                              //сбрасываем индекс первой строки
                            indexRow = 0;
                            Console.WriteLine();
                            MatrixOfWinnings.View();
                            break;
                        }
                        //иначе если стратегия по первой строке заведомо лучше чем стратегия по второй строке
                        else if (MatrixOfWinnings.GetRow(indexRow).Dominance(MatrixOfWinnings.GetRow(indexRow2)) == 1)
                        {
                            //удаляем из матрицы вторую строку
                            MatrixOfWinnings = MatrixOfWinnings.DeleteRow(indexRow2);

                            //уменьшаем индекс второй строки
                            indexRow2--;
                            Console.WriteLine();
                            MatrixOfWinnings.View();
                            break;
                        }
                    }
                }
            }
        }
        public void DeletingColumns()
        {
            //проходимся по столбцам
            for (int indexColumn = 0; indexColumn < MatrixOfWinnings.m - 1; indexColumn++)
            {
                for (int indexColumn2 = 1; indexColumn2 < MatrixOfWinnings.m; indexColumn2++)
                {
                    //если индекс первого столбца не равен индексу второго столбца
                    if (indexColumn != indexColumn2)
                    {
                        //если стратегия по первому столбцу заведомо хуже чем стратегия по второму столбцу
                        if (MatrixOfWinnings.GetColumn(indexColumn).Dominance(MatrixOfWinnings.GetColumn(indexColumn2)) == 2)
                        {
                            //удаляем из матрицы первый столбец
                            MatrixOfWinnings = MatrixOfWinnings.DeleteColumn(indexColumn);

                            //сбрасываем индекс первому столбцу
                            indexColumn = 0;

                            Console.WriteLine();
                            MatrixOfWinnings.View();

                            break;
                        }
                        //иначе если стратегия по первому столбцу заведомо лучше чем стратегия по второму столбцу
                        else if (MatrixOfWinnings.GetColumn(indexColumn).Dominance(MatrixOfWinnings.GetColumn(indexColumn2)) == 1)
                        {
                            //удаляем из матрицы второй столбец
                            MatrixOfWinnings = MatrixOfWinnings.DeleteColumn(indexColumn2);

                            //уменьшаем индекс второй столбец
                            indexColumn2--;

                            Console.WriteLine();
                            MatrixOfWinnings.View();



                            break;
                        }
                    }
                }
            }
        }
        public Vector[] GetOptimalMixedStrategy(out Matrix[] mat)
        {
            if (MatrixOfWinnings.n != 2 || MatrixOfWinnings.m < 2)
            {
                mat = null;
                return null;
            }

            var Ans = new List<Vector>();
            var AnsMatrixs = new List<Matrix>();

            Vector min = null;
            Matrix minMatrix = null;
            double minV = double.MaxValue;

            for (int i = 0; i < MatrixOfWinnings.m - 1; i++)
            {
                for (int j = i + 1; j < MatrixOfWinnings.m; j++)
                {
                    var matrix = new Matrix(2, 2);

                    matrix.SetColumn(MatrixOfWinnings.GetColumn(i), 0);
                    matrix.SetColumn(MatrixOfWinnings.GetColumn(j), 1);

                    var cur = Calculate2x2(matrix);

                    Ans.Add(cur);
                    AnsMatrixs.Add(matrix);

                    if (minV >= cur[4])
                    {
                        minV = cur[4];
                        min = cur;
                        minMatrix = matrix;
                    }
                }
            }


            mat = new Matrix[] { minMatrix };
            return new Vector[] { min };
            //mat = AnsMatrixs.ToArray();
            //return Ans.ToArray();
        }

        Vector Calculate2x2(Matrix matrix)
        {
            var ans = new Vector(5);

            //p
            ans[0] = (matrix[1, 1] - matrix[1, 0]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);

            ans[1] = (matrix[0, 0] - matrix[0, 1]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);

            //q
            ans[2] = (matrix[1, 1] - matrix[0, 1]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);

            ans[3] = (matrix[0, 0] - matrix[1, 0]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);

            //v
            ans[4] = (matrix[1, 1] * matrix[0, 0] - matrix[0, 1] * matrix[1, 0]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);

            return ans;
        }
    }
}

