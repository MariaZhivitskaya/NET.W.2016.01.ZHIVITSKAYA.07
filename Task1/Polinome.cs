using System;

namespace Task1
{
    public class Polinome
    {
        private readonly double[] arr;
        private readonly int degree;

        public Polinome(params double[] data)
        {
            if (data == null)
            {
                throw new NullReferenceException("NULL data!");
            }

            if (data.Length < 1)
            {
                throw new ArgumentException("Wrong number of parameters!");
            }

            degree = data.Length - 1;
            arr = new double[degree + 1];

            data.CopyTo(arr, 0);
        }

        public Polinome(int degree)
        {
            if (degree < 0)
            {
                throw new ArgumentOutOfRangeException("Wrong polinome degree!");
            }

            this.degree = degree;
            arr = new double[degree + 1];
        }

        public Polinome(Polinome p)
        {
            degree = p.degree;
            p.arr.CopyTo(arr, 0);
        }

        public override bool Equals(object obj)
        {
            var p = obj as Polinome;

            if (degree != p?.degree)
            {
                return false;
            }

            for (int i = 0; i < degree + 1; i++)
            {
                if (Math.Abs(arr[i] - (p.arr[i])) > Math.Pow(10, -6))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Equals(Polinome p)
        {
            if (p == null)
            {
                return false;
            }

            for (int i = 0; i < degree + 1; i++)
            {
                if (!arr[i].Equals(p.arr[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return degree * 0x00010000 + 13;
        }

        public static Polinome operator -(Polinome p)
        {
            var newArr = new double[p.degree + 1];

            for (int i = 0; i < p.degree + 1; i++)
            {
                newArr[i] = -p.arr[i];
            }

            return new Polinome(newArr);
        }


        public static Polinome operator +(Polinome p1, Polinome p2)
        {
            int maxDegree = p1.degree > p2.degree ? p1.degree : p2.degree;
            int minDegree = p1.degree < p2.degree ? p1.degree : p2.degree;
            var greatestPolinome = maxDegree == p1.degree ? p1 : p2;
            var lesserPolinome = minDegree == p1.degree ? p1 : p2;
            var newPolinome = greatestPolinome;

            for (int i = newPolinome.degree, j = minDegree; i > minDegree + 1; i--, j--)
            {
                greatestPolinome.arr[i] += lesserPolinome.arr[j];
            }

            return newPolinome;
        }

        public static Polinome operator -(Polinome p1, Polinome p2)
        {
            int maxDegree = p1.degree > p2.degree ? p1.degree : p2.degree;
            int minDegree = p1.degree < p2.degree ? p1.degree : p2.degree;
            var greatestPolinome = maxDegree == p1.degree ? p1 : -p2;
            var lesserPolinome = minDegree == p1.degree ? p1 : -p2;
            var newPolinome = greatestPolinome;

            for (int i = newPolinome.degree, j = minDegree; i > minDegree + 1; i--, j--)
            {
                greatestPolinome.arr[i] += lesserPolinome.arr[j];
            }

            return newPolinome;
        }

        public static Polinome operator *(Polinome p1, Polinome p2)
        {
            var newPolinome = new Polinome(p1.degree + p2.degree);

            for (int i = 0; i < newPolinome.degree + 1; i++)
            {
                newPolinome.arr[i] = Coefficient(p1.arr, p2.arr, newPolinome.degree - i);
            }

            return newPolinome;
        }

        /// <summary>
        /// Counts a coefficient of the specified degree.
        /// </summary>
        /// <param name="arr1">Coefficients of the first polinome.</param>
        /// <param name="arr2">Coefficients of the second polinome.</param>
        /// <param name="degree">A degree.</param>
        /// <returns>Return a coefficient.</returns>
        private static double Coefficient(double[] arr1, double[] arr2, int degree)
        {
            double sum = 0;

            for (int i = 0; i < arr1.Length; i++)
            {
                for (int j = 0; j < arr2.Length; j++)
                {
                    if (i + j == (arr1.Length - 1) + (arr2.Length - 1) - degree)
                    {
                        sum += arr1[i]*arr2[j];
                    }
                }
            }

            return sum;
        }
    }
}
