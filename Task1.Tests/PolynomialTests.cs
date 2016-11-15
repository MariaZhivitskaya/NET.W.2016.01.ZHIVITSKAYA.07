using System.Collections;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        private static readonly Polynomial Source1 = 
            new Polynomial(1.2, 6.2345, 541.001, 2.1484, -0.05, 0.1, -2.11);
        private static readonly Polynomial Source2 =
            new Polynomial(3.8, 15.333679, -2.145);
        private static readonly Polynomial ExpectedSum = 
            new Polynomial(1.2, 6.2345, 541.001, 2.1484, 3.75, 15.433679, -4.255);
        private static readonly Polynomial ExpectedDif1 =
            new Polynomial(1.2, 6.2345, 541.001, 2.1484, -3.85, -15.233679, 0.035);
        private static readonly Polynomial ExpectedDif2 =
            new Polynomial(-1.2, -6.2345, -541.001, -2.1484, 3.85, 15.233679, -0.035);
        private static readonly Polynomial ExpectedMul =
            new Polynomial(4.56, 42.0915148, 2148.8276217255, 8290.326590179, 
                -1127.6942690364, -4.99500195, -6.3773821, -32.56856269, 4.52595);

        [Test, TestCaseSource(typeof(PolynomialSumTest), nameof(PolynomialSumTest.TestCasesSum))]
        public Polynomial SumTests(Polynomial p1, Polynomial p2) => p1 + p2;

        [Test, TestCaseSource(typeof(PolynomialDifTest), nameof(PolynomialDifTest.TestCasesDif))]
        public Polynomial DifTests(Polynomial p1, Polynomial p2) => p1 - p2;

        [Test, TestCaseSource(typeof(PolynomialMulTest), nameof(PolynomialMulTest.TestCasesMul))]
        public Polynomial MulTests(Polynomial p1, Polynomial p2) => p1 * p2;

        public class PolynomialSumTest
        {
            public static IEnumerable TestCasesSum
            {
                get
                {
                    yield return new TestCaseData(Source1, Source2).Returns(ExpectedSum);
                    yield return new TestCaseData(Source2, Source1).Returns(ExpectedSum);
                }
            }
        }

        public class PolynomialDifTest
        {
            public static IEnumerable TestCasesDif
            {
                get
                {
                    yield return new TestCaseData(Source1, Source2).Returns(ExpectedDif1);
                    yield return new TestCaseData(Source2, Source1).Returns(ExpectedDif2);
                }
            }
        }
        public class PolynomialMulTest
        {
            public static IEnumerable TestCasesMul
            {
                get
                {
                    yield return new TestCaseData(Source1, Source2).Returns(ExpectedMul);
                    yield return new TestCaseData(Source2, Source1).Returns(ExpectedMul);
                }
            }
        }
    }
}
