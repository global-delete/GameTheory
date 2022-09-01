using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    public class Vector
    {
        public double[] item;
        public int size = 0;

        public static Vector operator *(double scalar, Vector vector)
        {
            return vector.Multiplay(scalar);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return vector1.Addition(vector2);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return vector1.Substraction(vector2);
        }

        public static Random rnd = new Random();
        public static Vector GetRandom(int size)
        {
            Vector rn = new Vector(size);
            for (int i = 0; i < size; i++)
                rn.item[i] = rnd.NextDouble() - 0.5;
            rn.Normalize();
            return rn;
        }

        public Vector(int size)
        {
            this.size = size;
            item = new double[size];
            for (int i = 0; i < size; i++)
                item[i] = 0;
        }

        public Vector(double[] a)
        {
            this.size = a.Length;
            item = new double[size];
            for (int i = 0; i < size; i++)
                item[i] = a[i];
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= size) return default(double);
                else return item[index];
            }
            set
            {
                if (index >= 0 && index < size)
                    item[index] = value;
            }
        }

        public bool SetElement(int id, double a) //устанавливает значение по индексу
        {
            if (id < 0 || id > size) return false;
            item[id] = a;
            return true;
        }

        public double GetElement(int id) //получает элемент по индексу
        {
            if (id < 0 || id > size) return default(double);
            return item[id];
        }

        public int GetSize() //получает размер вектора
        {
            return size;
        }

        public Vector Copy()
        {
            Vector copy = new Vector(item);
            return copy;
        }

        public override string ToString()
        {
            string s = "(";
            for (int i = 0; i < size - 1; i++)
                s += item[i].ToString() + ", ";
            s += item[size - 1].ToString() + ")";
            return s;
        }

        public Vector Multiplay(double a) //умножение вектора на число
        {
            Vector temp = new Vector(size);
            for (int i = 0; i < size; i++)
                temp[i] = item[i] * a;
            return temp;
        }

        public Vector MultiplayInt(int a) //умножение вектора на число
        {
            Vector temp = new Vector(size);
            for (int i = 0; i < size; i++)
                temp[i] = item[i] * a;
            return temp;
        }

        public Vector Addition(Vector a) //сложение векторов
        {
            if (size == a.size)
            {
                Vector temp = new Vector(size);
                for (int i = 0; i < size; i++)
                    temp.item[i] = item[i] + a.item[i];
                return temp;
            }
            Vector o = new Vector(0);
            return o;

        }

        public Vector Substraction(Vector a) //вычитание векторов
        {
            Vector temp = new Vector(size);
            if (size == a.size)
                for (int i = 0; i < size; i++)
                    temp.item[i] = item[i] - a.item[i];

            return temp;
        }

        public double ScalarMultiplay(Vector a) //скалярное произведение векторов
        {
            double temp = 0;
            if (size == a.size)
                for (int i = 0; i < size; i++)
                    temp = temp + item[i] * a.item[i];

            return temp;
        }

        public double EvklidNorma() //евклидова норма (длина вектора)
        {
            double temp = 0;
            for (int i = 0; i < size; i++)
                temp += item[i] * item[i];
            return Math.Sqrt(temp);
        }

        public double MaxNorma() //норма, вычисляющая максимум по i
        {
            double temp = 0;
            for (int i = 0; i < size; i++)
                if (Math.Abs(item[i]) > temp)
                    temp = item[i];
            return temp;
        }

        public double Norma() //третий вид нормы
        {
            double temp = 0;
            for (int i = 0; i < size; i++)
                temp = temp + Math.Abs(item[i]);
            return temp;
        }

        public Vector Normalize() //нормализация вектора
        {
            Vector temp = new Vector(size);
            double a = EvklidNorma();
            for (int i = 0; i < size; i++)
                if (a != 0) temp.item[i] = item[i] / a;
                else temp.item[i] = item[i];

            return temp;
        }

        public void Clear()
        {
            for (var i = 0; i < size; ++i)
                item[i] = 0;
        }

        public Vector UnarMinus() //унарный минус
        {
            Vector temp = new Vector(size);
            for (int i = 0; i < size; i++)
                temp[i] = item[i] * (-1);
            return temp;
        }
        public double GetMinimalItem()
        {
            int min = 0;
            for (int i = 1; i < size; i++)
            {
                if (item[i] < item[min])
                {
                    min = i;
                }
            }
            return item[min];
        }
        public double GetMaximalItem()
        {
            int max = 0;
            for (int i = 1; i < size; i++)
            {
                if (item[i] > item[max])
                {
                    max = i;
                }
            }
            return item[max];
        }
        public int Dominance(Vector vector)
        {
            int d = 0;
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                if (item[i] < vector.item[i])
                {
                    k++;
                }
                else
                {
                    k--;
                }
            }
            if (k == size)
            {
                d = 2;
            }
            if (k == -size)
            {
                d = 1;
            }
            return d;
        }
        public void view()
        {
            string s = "(";
            for (int i = 0; i < size - 1; i++)
                s += item[i].ToString() + ", ";
            s += item[size - 1].ToString() + ")";
            Console.WriteLine(s);
        }
    }
}
