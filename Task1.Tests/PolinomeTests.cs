using System.Collections;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class PolinomeTests
    {
        private static Polinome source1 = 
            new Polinome(1.2, 6.2345, 541.001, 2.1484, -0.05, 0.1, -2.11);
        private static Polinome source2 =
            new Polinome(3.8, 15.333679, -2.145);
        private static Polinome expectedSum = 
            new Polinome(1.2, 6.2345, 541.001, 2.1484, 3.75, 15.433679, -4.255);
        private static Polinome expectedDif1 =
            new Polinome(1.2, 6.2345, 541.001, 2.1484, -3.85, -15.233679, 0.035);
        private static Polinome expectedDif2 =
            new Polinome(-1.2, -6.2345, -541.001, -2.1484, 3.85, 15.233679, -0.035);
        private static Polinome expectedMul =
            new Polinome(4.56, 42.0915148, 2148.8276217255, 8290.326590179, 
                -1127.6942690364, -4.99500195, -6.3773821, -32.56856269, 4.52595);

        [Test, TestCaseSource(typeof(PolinomeSumTest), nameof(PolinomeSumTest.TestCasesSum))]
        public Polinome SumTests(Polinome p1, Polinome p2)
        {
            return p1 + p2;
        }

        [Test, TestCaseSource(typeof(PolinomeDifTest), nameof(PolinomeDifTest.TestCasesDif))]
        public Polinome DifTests(Polinome p1, Polinome p2)
        {
            return p1 - p2;
        }

        [Test, TestCaseSource(typeof(PolinomeMulTest), nameof(PolinomeMulTest.TestCasesMul))]
        public Polinome MulTests(Polinome p1, Polinome p2)
        {
            return p1 * p2;
        }

        public class PolinomeSumTest
        {
            public static IEnumerable TestCasesSum
            {
                get
                {
                    yield return new TestCaseData(source1, source2).Returns(expectedSum);
                    yield return new TestCaseData(source2, source1).Returns(expectedSum);
                }
            }
        }

        public class PolinomeDifTest
        {
            public static IEnumerable TestCasesDif
            {
                get
                {
                    yield return new TestCaseData(source1, source2).Returns(expectedDif1);
                    yield return new TestCaseData(source2, source1).Returns(expectedDif2);
                }
            }
        }
        public class PolinomeMulTest
        {
            public static IEnumerable TestCasesMul
            {
                get
                {
                    yield return new TestCaseData(source1, source2).Returns(expectedMul);
                    yield return new TestCaseData(source2, source1).Returns(expectedMul);
                }
            }
        }
    }
}
