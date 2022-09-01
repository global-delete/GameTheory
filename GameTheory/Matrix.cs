using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    class Matrix
    {
        public int n, m;//начальное кол-во строк и столбцов   

        public double[,] items;//двумерная матрица

        //конструктор с указанием количества строк и столбцов
        public Matrix(int nn, int mm)
        {
            n = nn;
            m = mm;
            items = new double[n, m];//создаем матрицу
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    items[i, j] = 0;
        }
        //конструктор с указанием двумерной матрицы
        public Matrix(double[,] mm)
        {
            //определяем количество строк и столбцов для создаваемой матрицы
            n = mm.GetLength(0);
            m = mm.GetLength(1);
            //создаем матрицу и заполняем ее значениями из указанной матрицы
            items = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    items[i, j] = mm[i, j];
        }
        public Matrix(Matrix mx)
        {
            n = mx.items.GetLength(0);
            m = mx.items.GetLength(1);
            items = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    items[i, j] = mx.items[i, j];
        }
        //индексация матрицы
        public double this[int nn, int mm]
        {
            //возвращение значения
            get
            {
                //если индекс матрицы выходит за пределы
                if (nn < 0 || nn >= n || mm < 0 || mm >= m)
                    return default(double);
                else return items[nn, mm];
            }
            //задание значения
            set
            {
                //если индекс матрицы выходит за пределы
                if (nn >= 0 && mm >= 0 && nn < n && mm < m)
                    items[nn, mm] = value; //задаем значение матрицы по индексу
            }
        }
        //получение строки в виде вектора
        public Vector GetRow(int nn)
        {
            //если индекс строки не фыходит за пределы матрицы
            if (nn < 0 || nn >= n)
                return null;
            else
            {
                //создаем вектор
                Vector vn = new Vector(m);
                for (int j = 0; j < m; j++)
                    vn[j] = items[nn, j];//заполняем вектор
                return vn;
            }
        }
        //получение столбца в виде вектора
        public Vector GetColumn(int mm)
        {
            //если индекс столбца не выходит за пределы
            if (mm < 0 || mm >= m)
                return null;
            else
            {
                //создаем вектор
                Vector vm = new Vector(n);
                for (int i = 0; i < n; i++) //получаем вектор
                    vm[i] = items[i, mm];
                return vm;
            }
        }

        //возвращение количества строк матрицы 
        public int GetRow()
        {
            return n;
        }

        //возвращение количества столбцов матрицы
        public int GetColumn()
        {
            return m;
        }
        //задаем строку, в аргументах вектор и индекс строки
        public void SetRow(Vector vn, int nn)
        {
            //если количество столбцов не равно размеру вектора
            //или если индекс строки выходит за пределы границы матрицы
            if ((m != vn.GetSize()) || nn < 0 || nn >= n)
                return;
            //перезаписываем строку матрицы значениями ивектора
            for (int j = 0; j < m; j++)
                items[nn, j] = vn[j];
        }

        //задаем столбец, в аргументах вектор и интекс столбца
        public void SetColumn(Vector vm, int mm)
        {
            //если количество строк не равно размеру вектора
            //или если индекс столбца выходит за пределы матрицы
            if ((n != vm.GetSize()) || mm < 0 || mm >= m)
                return;
            //перезаписываем столбец значениями из вектора
            for (int i = 0; i < n; i++)
                items[i, mm] = vm[i];
        }

        public /*override*/ new string ToString()
        {
            string s = "{";
            for (int i = 0; i < n - 1; i++)
            {
                Vector v = GetRow(i);
                s += v.ToString() + ",";
            }
            Vector vv = GetRow(n - 1);
            s += vv.ToString() + "}";
            return s;
        }

        public Matrix Addition(Matrix a) //сложение матриц
        {
            if (n == a.n && m == a.m)
            {
                Matrix c = new Matrix(n, m);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        c.items[i, j] = items[i, j] + a.items[i, j];
                return c;
            }
            Matrix o = new Matrix(0, 0);
            return (o);
        }

        public Matrix Substraction(Matrix a) //вычитание матриц
        {
            if (n == a.n && m == a.m)
            {
                Matrix c = new Matrix(n, m);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        c.items[i, j] = items[i, j] - a.items[i, j];
                return c;
            }
            Matrix o = new Matrix(0, 0);
            return (o);
        }

        public Matrix Multiplay(double a) //умножение матрицы на число
        {
            Matrix c = new Matrix(n, m);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    c.items[i, j] = items[i, j] * a;
            return c;
        }

        public Matrix MultiplayMatrix(Matrix a) //умножение матрицы на матрицу
        {
            if (m == a.n)
            {
                Matrix c = new Matrix(n, a.m);

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < a.m; j++)
                        for (int k = 0; k < m; k++)
                            c.items[i, j] += items[i, k] * a.items[k, j];
                return c;
            }
            Matrix o = new Matrix(0, 0);
            return o;
        }

        public Vector MultiplayVector(Vector b) //умножение матрицы на вектор
        {
            if (m != b.GetSize()) return null;
            Vector c = new Vector(n);
            for (int i = 0; i < n; i++)
                c.SetElement(i, GetRow(i).ScalarMultiplay(b));
            return c;
        }

        public Matrix EdinichMatrix(int m) // единичная матрица
        {
            Matrix c = new Matrix(m, m);

            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    if (i == j) c.items[i, j] = 1;
                    else c.items[i, j] = 0;

            return c;
        }

        public double EvklidNorma() //евклидова норма
        {
            double s = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    s += items[i, j] * items[i, j];
            return Math.Sqrt(s);
        }

        public double MaxNormaI() //норма, максимальная по i
        {
            double max = 0, s = 0;
            for (int i = 0; i < n; i++)
            {
                s = 0;
                for (int j = 0; j < m; j++)
                    s += Math.Abs(items[i, j]);
                if (s > max) max = s;
            }
            return max;
        }

        public double MaxNormaJ() //норма, максимальная по j
        {
            double max = 0, s = 0;
            for (int j = 0; j < m; j++)
            {
                s = 0;
                for (int i = 0; i < n; i++)
                    s += Math.Abs(items[i, j]);
                if (s > max) max = s;
            }
            return max;
        }

        public Matrix Transponir(Matrix r) //транспонированная матрица
        {
            Matrix c = new Matrix(r.n, r.m);

            for (int i = 0; i < c.n; i++)
                for (int j = 0; j < c.m; j++)
                    c.items[i, j] = r.items[j, i];
            return c;
        }

        public void SwapRow(int nn, int mm)
        {
            if (nn < 0 || mm < 0 || nn > n || mm > n) return;
            if (nn == mm) return;
            Vector d1 = GetRow(nn);
            Vector d2 = GetRow(mm);
            SetRow(d1, mm);
            SetRow(d2, nn);
        }

        public void SwapColumn(int nn, int mm)
        {
            if (nn < 0 || mm < 0 || nn > m || mm > m) return;
            if (nn == mm) return;
            Vector d1 = GetColumn(nn);
            Vector d2 = GetColumn(mm);
            SetColumn(d1, mm);
            SetColumn(d2, nn);
        }

        public Vector SolveTreygUP(Vector b)
        {
            if (m != n || n != b.GetSize())
                return null;
            for (int i = 0; i < n; i++)
            {
                if (items[i, i] == 0) return null;
                for (int j = 0; j < i; j++)
                    if (items[i, j] != 0) return null;
            }
            Vector x = new Vector(n);
            x[n - 1] = b.GetElement(n - 1) / items[n - 1, n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                double s = 0;
                for (int k = i + 1; k < n; k++)
                    s += items[i, k] * x[k];
                x[i] = (b[i] - s) / items[i, i];
            }
            return x;
        }

        public Vector SolveTreygDOWN(Vector b)
        {
            if (m != n || n != b.GetSize())
                return null;
            for (int i = 0; i < n; i++)
            {
                if (items[i, i] == 0)
                    return null;
                for (int j = i + 1; j < m; j++)
                    if (items[i, j] != 0)
                        return null;
            }
            Vector x = new Vector(n);
            x[0] = b.GetElement(0) / items[0, 0];
            for (int i = 1; i < n; i++)
            {
                double s = 0;
                for (int k = 1; k < i - 1; k++)
                    s += items[i, k] * x[k];
                x[i] = (b[i] - s) / items[i, i];
            }
            return x;
        }

        public Vector MGauss(Vector b)
        {
            double max;
            int indmax;
            for (int i = 0; i < b.size; i++)
            {
                max = Math.Abs(items[i, i]);
                indmax = i;
                for (int j = i; j < b.size; j++)
                    if (Math.Abs(items[j, i]) > max)
                    {
                        max = Math.Abs(items[j, i]);
                        indmax = j;
                    }

                if (indmax != i)
                {
                    SwapRow(i, indmax);
                    double temp = b[i];
                    b[i] = b[indmax];
                    b[indmax] = temp;
                }

                double x;
                for (int z = 1; z < b.size; z++)
                    for (int j = z; j < b.size; j++)
                    {
                        x = items[j, z - 1] / items[z - 1, z - 1];
                        for (int k = 0; k < b.size; k++)
                            items[j, k] = items[j, k] - x * items[z - 1, k];
                        b[j] = b[j] - x * b[z - 1];
                    }
            }

            for (int q = b.size - 1; q >= 0; q--)
            {
                for (int j = q + 1; j < b.size; j++)
                    b[q] -= items[q, j] * b[j];
                b[q] = b[q] / items[q, q];
            }
            return b;
        }

        public Vector MOrtogonalize(Vector b)
        {
            Matrix R = new Matrix(n, n);
            Matrix T = new Matrix(n, n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    R[i, j] = 0;
                    if (i == j) T[i, j] = 1;
                    else T[i, j] = 0;
                }
            Vector v = new Vector(n);
            R.SetColumn(GetColumn(0), 0);
            for (int k = 1; k < n; ++k)
            {
                v.Clear();
                for (int i = 0; i < k; ++i)
                {
                    T[i, k] = (GetColumn(k).ScalarMultiplay(R.GetColumn(i))) / R.GetColumn(i).ScalarMultiplay(R.GetColumn(i));
                    v = v.Substraction(R.GetColumn(i).Multiplay(T[i, k]));
                    //v = R.GetColumn(i).Multiplay(T[i, k]);
                }
                R.SetColumn(GetColumn(k).Addition(v), k);
                //R.SetColumn(GetColumn(k).Substraction(v), k);
            }

            Matrix D = R.Transponir(R).MultiplayMatrix(R);
            Vector x = new Vector(n);

            var tReverese = T.Reverse(T);

            //x = T.Reverse(T).MultiplayMatrix(D.Reverse(D)).MultiplayMatrix(R.Transponir(R)).MultiplayVector(b);

            Matrix b1 = new Matrix(b.size, b.size);
            b1.SetRow(b, 0);
            b1.SetRow(b.Substraction(GetRow(n - 1).Multiplay(R.GetRow(n - 1).ScalarMultiplay(b) / R.GetRow(n - 1).ScalarMultiplay(GetRow(n - 1)))), 1);
            for (int i = 2; i < n; i++)
            {
                //b1.SetRow(b1.GetRow(i - 1)
            }
            for (int i = 0; i < n; i++)
            {
                if (i > 1)
                {
                    b1.SetRow(b1.GetRow(i - 1).Substraction(GetRow(n - i + 1).Multiplay(x[n - i + 1])), i);
                }
                x[i] = R.GetRow(i).ScalarMultiplay(b1.GetRow(n - i)) / R.GetRow(i).ScalarMultiplay(GetRow(i));
            }
            return x;
        }

        public Vector PoslPribl(double eps, Vector b)
        {
            Transform(this, b);
            Vector xt = new Vector(n);
            for (int i = 0; i < b.size; i++)
                xt[i] = b[i];

            Vector xs = new Vector(n);
            Vector ras = new Vector(n);

            if (EvklidNorma() < 1)
            {
                do
                {
                    xs = this.MultiplayVector(xt);
                    xs = xs.Addition(b);
                    ras = xs.Substraction(xt);
                    xt = xs.Copy();
                }
                while (ras.EvklidNorma() > eps);

            }
            return xs;
        }

        public void Transform(Matrix a, Vector b)
        {
            double max;
            int indmax;
            for (int i = 0; i < b.size; i++)
            {
                max = Math.Abs(a.items[i, i]);
                indmax = i;
                for (int j = i; j < b.size; j++)
                    if (Math.Abs(a.items[j, i]) > max)
                    {
                        max = Math.Abs(a.items[j, i]);
                        indmax = j;
                    }

                if (indmax != i)
                {
                    SwapRow(i, indmax);
                    double temp = b[i];
                    b[i] = b[indmax];
                    b[indmax] = temp;
                }

                if (max == 0) break;
            }

            for (int i = 0; i < n; i++)
            {
                b[i] = b[i] / a.items[i, i];
                for (int j = 0; j < n; j++)
                    if (i != j)
                        a.items[i, j] = (-1) * a.items[i, j] / a.items[i, i];
                a.items[i, i] = 0;
            }

        }

        public Vector Progon(Vector b)
        {
            Vector aa = new Vector(n);
            Vector bb = new Vector(n);

            aa[1] = -items[0, 1] / items[0, 0];
            bb[1] = b[0] / items[0, 0];

            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 1; j < n - 1; j++)
                {
                    aa[i + 1] = -items[i, j + 1] / (items[i, i] + items[i, j - 1] * aa[i]);
                    bb[i + 1] = (-items[i, j - 1] * bb[i] + b[i]) / (items[i, i] + items[i, j - 1] * aa[i]);
                }
            }

            Vector x = new Vector(n);
            x[n - 1] = (-items[n - 1, n - 2] * bb[n - 1] + b[n - 1]) / (items[n - 1, n - 1] + items[n - 1, n - 2] * aa[n - 1]);
            for (int i = n - 2; i >= 0; i--)
                x[i] = aa[i + 1] * x[i + 1] + bb[i + 1];

            return x;
        }

        public Vector MNK(Vector x, Vector y, int k)
        {
            Vector b = new Vector(k + 1);

            if (x.size == y.size)
            {
                for (int i = 0; i < k + 1; i++)
                    for (int j = 0; j < k + 1; j++)
                    {
                        items[i, j] = 0;
                        for (int kk = 0; kk < x.size; kk++)
                            items[i, j] += Math.Pow(x[kk], i + j);
                    }

                for (int i = 0; i < k + 1; i++)
                {
                    b[i] = 0;
                    for (int kk = 0; kk < x.size; kk++)
                        b[i] += Math.Pow(x[kk], i) * y[kk];
                }


                MGauss(b);
            }
            return b;
        }



        public double Determinant(Matrix r) // определитель матрицы
        {
            double det = 0;
            if (r.n != r.m)
                return double.NaN;

            if (r.n == 1)
                det = r.items[0, 0];

            else if (r.n == 2)
                det = r.items[0, 0] * r.items[1, 1] - r.items[0, 1] * r.items[1, 0];

            else if (r.n > 2)
            {
                for (int i = 0; i < r.m; i++)
                {
                    Matrix c = new Matrix(r.n - 1, r.m - 1);
                    det += Math.Pow(-1, i + 2) * r.items[0, i] * Determinant(Minor(r, 0, i));
                }
            }
            return det;
        }
        //возвращаем дополнительный минор (матрица)
        //в аргументах матрица, индексы строки и столбца, которые надо вычеркнуть
        public Matrix Minor(Matrix r, int mm, int nn) // минор матрицы
        {
            //создаем новую матрицу для нахождения дополнительного минора
            Matrix c = new Matrix(r.n - 1, r.m - 1);
            //перебор элемента по строке
            for (int i = 0, q = 0; q < c.n; i++, q++)
                for (int j = 0, p = 0; p < c.m; j++, p++)
                { //если встретили ненужную строку

                    if (i == mm) i++;//пропускаем эту строку

                    //если встретили ненужный столбец
                    if (j == nn) j++;//пропускаем столбец
                    c.items[q, p] = r.items[i, j];//заполняем вспомогательную матрицу
                }
            return c;
        }

        public Matrix AlgebrDop(Matrix r) // алгебраическое дополнение
        {
            Matrix c = new Matrix(r.n, r.m);
            for (int i = 1; i <= r.n; i++)
                for (int j = 1; j <= r.m; j++)
                    c.items[i - 1, j - 1] = Math.Pow(-1, i + j) * Determinant(Minor(r, i - 1, j - 1));

            return c;
        }

        public Matrix Reverse(Matrix r) //обратная матрица
        {
            if (Determinant(r) == 0)
            {
                Matrix nan = new Matrix(0, 0);
                return nan;
            }
            Matrix c;
            c = Transponir(AlgebrDop(r).Multiplay((1 / Determinant(r))));

            return c;
        }
        public Matrix DeleteRow(int row)
        {
            Matrix mx = new Matrix(this);
            mx.n--;
            bool b = false;
            for (int i = 0; i < mx.n; i++)
            {
                if (i == row)
                {
                    b = true;
                }
                for (int j = 0; j < m; j++)
                {
                    if (b == true)
                    {
                        mx.items[i, j] = items[i + 1, j];
                    }
                    else
                    {
                        mx.items[i, j] = items[i, j];
                    }
                }
                b = false;
            }
            return mx;
        }
        public Matrix DeleteColumn(int column)
        {
            Matrix mx = new Matrix(this);
            mx.m--;
            bool b = false;
            for (int i = 0; i < n; i++)
            {                
                for (int j = 0; j < mx.m; j++)
                {
                    if (j == column)
                    {
                        b = true;
                    }
                    if (b == true)
                    {
                        mx.items[i, j] = items[i, j + 1];
                    }
                    else
                    {
                        mx.items[i, j] = items[i, j];
                    }
                }
                b = false;
            }
            return mx;
        }
        //public Matrix DeleteColumn(int column)
        //{
        //    //m--;
        //    bool b = false;
        //    for (int i = 0; i < n; i++)
        //    {
        //        for (int j = 0; j < m; j++)
        //        {
        //            if (j == column)
        //            {
        //                if (b == true)
        //                {
        //                    items[i, j] = items[i, j + 1];
        //                }
        //        }
        //        b = false;
        //    }
        //}
        public void View()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write("{0,-2}", items[i,j] + "  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void view()
        {
            string s = "{";
            for (int i = 0; i < n - 1; i++)
            {
                Vector v = GetRow(i);
                s += v.ToString() + ",";
            }
            Vector vv = GetRow(n - 1);
            s += vv.ToString() + "}";
            Console.Write(s);
        }
    }
}
