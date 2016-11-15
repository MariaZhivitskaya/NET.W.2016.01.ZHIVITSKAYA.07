using System;
using System.Globalization;

namespace Task1
{
    /// <summary>
    /// Represents a polynomial.
    /// </summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        private double[] _coefficients = { };
        private static double _eps;

        public double Eps => _eps;

        public int Power { get; }

        /// <summary>
        /// A property for coefficients.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if there are 
        /// no coefficients.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if
        /// the precision of coefficients is less than epsilon.</exception>
        public double[] Coefficients
        {
            get { return _coefficients; }
            private set
            {
                if (value.Length < 1)
                    throw new ArgumentException("Wrong number of parameters!");

                _coefficients = new double[value.Length];
                value.CopyTo(_coefficients, 0);
            }
        }

        /// <summary>
        /// Gets the value of epsilon from the App.config file.
        /// </summary>
        static Polynomial()
        {
            _eps = double.Parse(System.Configuration.ConfigurationManager.AppSettings["epsilon"]);
        }

        /// <summary>
        /// Creates the polynomial according to specified coefficients.
        /// </summary>
        /// <param name="coefficients">Coefficients.</param>
        public Polynomial(params double[] coefficients)
        {
            Coefficients = coefficients;
            Power = Coefficients.Length - 1;
        }

        /// <summary>
        /// Clones the polynomial.
        /// </summary>
        /// <returns>Returns the clone of the polynomial.</returns>
        object ICloneable.Clone() => Clone();

        /// <summary>
        /// Clones the polynomial.
        /// </summary>
        /// <returns>Returns the clone of the polynomial.</returns>
        public Polynomial Clone() => new Polynomial(Coefficients);

        /// <summary>
        /// Gets the hash-code of the polynomial.
        /// </summary>
        /// <returns>Returns the hash-code of the polynomial.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Coefficients.GetHashCode()) * 397) ^ Power;
            }
        }

        /// <summary>
        /// Compares polynomials for equality.
        /// </summary>
        /// <param name="obj">The polynomial for the comparison.</param>
        /// <returns>Returns the result of the comparison.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if(obj.GetType() != GetType())
                return false;

            return Equals((Polynomial)obj);
        }

        /// <summary>
        /// Compares polynomials for equality.
        /// </summary>
        /// <param name="other">The polynomial for the comparison.</param>
        /// <returns>Returns the result of the comparison.</returns>
        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Coefficients.Length != other.Coefficients.Length)
                return false;

            for (int i = 0; i < Coefficients.Length; i++)
                if (Math.Abs(Coefficients[i] - other.Coefficients[i]) > Eps)
                    return false;

            return true;
        }

        /// <summary>
        /// Compares polynomials for equality.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the result of the comparison.</returns>
        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (ReferenceEquals(p1, null))
                return false;

            return p1.Equals(p2);
        }

        /// <summary>
        /// Compares polynomials for inequality.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the result of the comparison.</returns>
        public static bool operator !=(Polynomial p1, Polynomial p2) =>
            !(p1 == p2);

        /// <summary>
        /// Realizes the unary operator -.
        /// </summary>
        /// <param name="p">The polynomial.</param>
        /// <returns>Returns the opposite polynomial.</returns>
        public static Polynomial operator -(Polynomial p)
        {
            var newCoefficients = new double[p.Power + 1];

            for (int i = 0; i < newCoefficients.Length; i++)
                newCoefficients[i] = -p.Coefficients[i];

            return new Polynomial(newCoefficients);
        }

        /// <summary>
        /// Realizes the binary operator +.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the sum of two polynomials.</returns>
        public static Polynomial operator +(Polynomial p1, Polynomial p2) =>
            SumLogic(p1, p2);

        /// <summary>
        /// Realizes the binary operator -.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the difference of two polynomials.</returns>
        public static Polynomial operator -(Polynomial p1, Polynomial p2) =>
            DifferenceLogic(p1, p2);

        /// <summary>
        /// Realizes the binary operator *.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the product of two polynomials.</returns>
        public static Polynomial operator *(Polynomial p1, Polynomial p2) =>
            MultiplicationLogic(p1, p2);

        /// <summary>
        /// Counts the sum of two polynomials.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the sum of two polynomials.</returns>
        public static Polynomial Add(Polynomial p1, Polynomial p2) =>
            SumLogic(p1, p2);

        /// <summary>
        /// Counts the difference of two polynomials.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the difference of two polynomials.</returns>
        public static Polynomial Subtract(Polynomial p1, Polynomial p2) =>
            DifferenceLogic(p1, p2);

        /// <summary>
        /// Counts the difference of two polynomials.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the difference of two polynomials.</returns>
        public static Polynomial Multiply(Polynomial p1, Polynomial p2) =>
            MultiplicationLogic(p1, p2);

        /// <summary>
        /// Counts the sum of two polynomials.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if
        /// the left-hand or the right-hand polynomial is null.</exception>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the sum of two polynomials.</returns>
        private static Polynomial SumLogic(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null))
                throw new ArgumentNullException("The first polynomial is null!");

            if (ReferenceEquals(p2, null))
                throw new ArgumentNullException("The second polynomial is null!");

            int minDegree = Math.Min(p1.Power, p2.Power);

            var polynomial = p1.Power >= p2.Power ? p1.Clone() : p2.Clone();

            for (int i = polynomial.Power - minDegree, j = 0; i < polynomial.Power + 1; i++)
            {
                polynomial.Coefficients[i] += p1.Power < p2.Power ?
                   p1.Coefficients[j++] : p2.Coefficients[j++];
            }

            return polynomial;
        }

        /// <summary>
        /// Counts the difference of two polynomials.
        /// </summary>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the difference of two polynomials.</returns>
        private static Polynomial DifferenceLogic(Polynomial p1, Polynomial p2) =>
             p1 + (-p2);

        /// <summary>
        /// Counts the product of two polynomials.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if
        /// the left-hand or the right-hand polynomial is null.</exception>
        /// <param name="p1">The left-hand polynomial.</param>
        /// <param name="p2">The right-hand polynomial.</param>
        /// <returns>Returns the product of two polynomials.</returns>
        private static Polynomial MultiplicationLogic(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null))
                throw new ArgumentNullException("The first polynomial is null!");

            if (ReferenceEquals(p2, null))
                throw new ArgumentNullException("The second polynomial is null!");

            var coefficients = new double[p1.Power + p2.Power + 1];

            for (int i = 0; i < coefficients.Length; i++)
                coefficients[i] =
                    Coefficient(p1.Coefficients, p2.Coefficients, coefficients.Length - 1 - i);

            return new Polynomial(coefficients);
        }

        /// <summary>
        /// Counts the coefficient of the specified degree.
        /// </summary>
        /// <param name="coefficients1">Coefficients of the first polynomial.</param>
        /// <param name="coefficients2">Coefficients of the second polynomial.</param>
        /// <param name="power">The degree.</param>
        /// <returns>Returns the coefficient.</returns>
        private static double Coefficient(double[] coefficients1, double[] coefficients2, int power)
        {
            double sum = 0;

            for (int i = 0; i < coefficients1.Length; i++)
                for (int j = 0; j < coefficients2.Length; j++)
                    if (i + j == (coefficients1.Length - 1) + (coefficients2.Length - 1) - power)
                        sum += coefficients1[i]*coefficients2[j];

            return sum;
        }
    }
}