using System;
using System.Globalization;
using CesiumLanguageWriter;
using NUnit.Framework;

namespace CesiumLanguageWriterTests
{
    /// <summary>
    /// Tests the <see cref="Cartesian3"/> type.
    /// </summary>
    [TestFixture]
    public class TestCartesian
    {
        /// <summary>
        /// Tests that initialization of and access to the type's values works correctly.
        /// </summary>
        [Test]
        public void TestHoldValue()
        {
            Cartesian3 test = new Cartesian3(1.0, 2.0, 3.0);
            Assert.AreEqual(1.0, test.X);
            Assert.AreEqual(2.0, test.Y);
            Assert.AreEqual(3.0, test.Z);
        }

        /// <summary>
        /// Tests initialization from an array of 3 coordinates works correctly.
        /// </summary>
        [Test]
        public void TestFromArray()
        {
            double[] values = { 2.0, 3.0, 6.0 };

            Cartesian3 test = new Cartesian3(values);
            Assert.AreEqual(values.Length, test.Length);
            Assert.AreEqual(test.X, test[0]);
            Assert.AreEqual(test.Y, test[1]);
            Assert.AreEqual(test.Z, test[2]);
        }

        /// <summary>
        /// Tests implicit conversion from <see cref="UnitCartesian3"/> coordinates.
        /// </summary>
        [Test]
        public void TestConversionFromUnitCartesian()
        {
            UnitCartesian3 unit = new UnitCartesian3(1.0, 1.0, 1.0);
            Cartesian3 test = unit;
            Assert.AreEqual(unit.X, test.X);
            Assert.AreEqual(unit.Y, test.Y);
            Assert.AreEqual(unit.Z, test.Z);
        }

        /// <summary>
        /// Tests the equality and inequality methods and operators.
        /// </summary>
        [Test]
        public void TestEquality()
        {
            Cartesian3 first = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 second = new Cartesian3(1.0, 2.0, 3.0);
            Assert.AreEqual(first, second);
            Assert.AreEqual(second, first);
            Assert.IsTrue(first == second);
            Assert.IsTrue(second == first);
            Assert.IsFalse(first != second);
            Assert.IsFalse(second != first);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(second.Equals(first));

            second = new Cartesian3(0.0, 2.0, 3.0);
            Assert.AreNotEqual(first, second);
            Assert.AreNotEqual(second, first);
            Assert.IsFalse(first == second);
            Assert.IsFalse(second == first);
            Assert.IsTrue(first != second);
            Assert.IsTrue(second != first);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));

            second = new Cartesian3(1.0, 0.0, 3.0);
            Assert.AreNotEqual(first, second);
            Assert.AreNotEqual(second, first);
            Assert.IsFalse(first == second);
            Assert.IsFalse(second == first);
            Assert.IsTrue(first != second);
            Assert.IsTrue(second != first);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));

            second = new Cartesian3(1.0, 2.0, 0.0);
            Assert.AreNotEqual(first, second);
            Assert.AreNotEqual(second, first);
            Assert.IsFalse(first == second);
            Assert.IsFalse(second == first);
            Assert.IsTrue(first != second);
            Assert.IsTrue(second != first);
            Assert.IsFalse(first.Equals(second));
            Assert.IsFalse(second.Equals(first));
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.EqualsEpsilon"/> method.
        /// </summary>
        [Test]
        public void TestEqualsEpsilon()
        {
            Cartesian3 first = new Cartesian3(1e-1, 1e-2, 1e-3);
            Cartesian3 second = new Cartesian3(1.1e-1, 1.1e-2, 1.1e-3);
            Assert.IsTrue(second.EqualsEpsilon(first, 1e-1));
            Assert.IsTrue(second.EqualsEpsilon(first, 1e-2));
            Assert.IsFalse(second.EqualsEpsilon(first, 1e-3));
            Assert.IsFalse(second.EqualsEpsilon(first, 1e-4));
            Assert.IsFalse(second.EqualsEpsilon(first, 1e-5));
        }
        
        /// <summary>
        /// Tests the <see cref="Cartesian3.Magnitude"/> property.
        /// </summary>
        [Test]
        public void TestMagnitude()
        {
            Cartesian3 test = new Cartesian3(2.0, 3.0, 6.0);
            Assert.AreEqual(7.0, test.Magnitude);
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.Normalize()"/> method.
        /// </summary>
        [Test]
        public void TestNormalize()
        {
            Cartesian3 test = new Cartesian3(2.0, 3.0, 6.0);
            UnitCartesian3 unit = test.Normalize();
            Assert.AreEqual(2.0 / 7.0, unit.X);
            Assert.AreEqual(3.0 / 7.0, unit.Y);
            Assert.AreEqual(6.0 / 7.0, unit.Z);
        }

        /// <summary>
        /// Tests that normalization of a <see cref="Cartesian3"/> with zero magnitude
        /// produces an <see cref="UnsupportedCaseException"/>.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void TestNormalizeOfZeroMagnitude()
        {
            Cartesian3 test = Cartesian3.Zero;
            UnitCartesian3 unit = test.Normalize();
        }

        /// <summary>
        /// Tests that normalization of a <see cref="Cartesian3"/> with infinite magnitude
        /// produces an <see cref="NotFiniteNumberException"/>.
        /// </summary>
        [Test]
        [ExpectedException(typeof(NotFiniteNumberException))]
        public void TestNormalizeOfInfiniteMagnitude()
        {
            Cartesian3 test = new Cartesian3(Double.PositiveInfinity, Double.PositiveInfinity, Double.PositiveInfinity);
            UnitCartesian3 unit = test.Normalize();
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.HasZeroMagnitude"/> method.
        /// </summary>
        [Test]
        public void TestHasZeroMagnitude()
        {
            Assert.IsTrue(Cartesian3.Zero.HasZeroMagnitude);
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.IsUndefined"/> method.
        /// </summary>
        [Test]
        public void TestIsUndefined()
        {
            Assert.IsFalse(new Cartesian3(1.0, 1.0, 1.0).IsUndefined);
            Assert.IsTrue(Cartesian3.Undefined.IsUndefined);
            Assert.IsTrue(new Cartesian3(Double.NaN, 1.0, 1.0).IsUndefined);
            Assert.IsTrue(new Cartesian3(1.0, Double.NaN, 1.0).IsUndefined);
            Assert.IsTrue(new Cartesian3(1.0, 1.0, Double.NaN).IsUndefined);
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.Invert"/> method.
        /// </summary>
        [Test]
        public void TestInvert()
        {
            Cartesian3 Cartesian3 = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 inverted = Cartesian3.Invert();
            Assert.AreEqual(-1.0, inverted.X);
            Assert.AreEqual(-2.0, inverted.Y);
            Assert.AreEqual(-3.0, inverted.Z);
        }

        /// <summary>
        /// Tests negation of a set of coordinates.
        /// </summary>
        [Test]
        public void TestNegation()
        {
            Cartesian3 c = -new Cartesian3(1.0, 2.0, 3.0);
            Assert.AreEqual(-1.0, c.X);
            Assert.AreEqual(-2.0, c.Y);
            Assert.AreEqual(-3.0, c.Z);
        }

        /// <summary>
        /// Tests the addition methods and operators.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            Cartesian3 original = new Cartesian3(10.0, 20.0, 30.0);
            Cartesian3 toAdd = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 result = original + toAdd;
            Assert.AreEqual(11.0, result.X);
            Assert.AreEqual(22.0, result.Y);
            Assert.AreEqual(33.0, result.Z);

            result = original.Add(toAdd);
            Assert.AreEqual(11.0, result.X);
            Assert.AreEqual(22.0, result.Y);
            Assert.AreEqual(33.0, result.Z);
        }

        /// <summary>
        /// Tests the subtraction methods and operators.
        /// </summary>
        [Test]
        public void TestSubtract()
        {
            Cartesian3 original = new Cartesian3(10.0, 20.0, 30.0);
            Cartesian3 toSubtract = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 result = original - toSubtract;
            Assert.AreEqual(9.0, result.X);
            Assert.AreEqual(18.0, result.Y);
            Assert.AreEqual(27.0, result.Z);

            result = original.Subtract(toSubtract);
            Assert.AreEqual(9.0, result.X);
            Assert.AreEqual(18.0, result.Y);
            Assert.AreEqual(27.0, result.Z);
        }

        /// <summary>
        /// Tests the multiplication methods and operators.
        /// </summary>
        [Test]
        public void TestMultiply()
        {
            Cartesian3 original = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 multiplied = original * 5.0;
            Assert.AreEqual(5.0, multiplied.X);
            Assert.AreEqual(10.0, multiplied.Y);
            Assert.AreEqual(15.0, multiplied.Z);

            multiplied = 5.0 * original;
            Assert.AreEqual(5.0, multiplied.X);
            Assert.AreEqual(10.0, multiplied.Y);
            Assert.AreEqual(15.0, multiplied.Z);

            multiplied = original.Multiply(5.0);
            Assert.AreEqual(5.0, multiplied.X);
            Assert.AreEqual(10.0, multiplied.Y);
            Assert.AreEqual(15.0, multiplied.Z);
        }

        /// <summary>
        /// Tests the multiplication methods and operators.
        /// </summary>
        [Test]
        public void TestDivide()
        {
            Cartesian3 original = new Cartesian3(2.0, 4.0, 6.0);
            Cartesian3 result = original / 2.0;
            Assert.AreEqual(1.0, result.X);
            Assert.AreEqual(2.0, result.Y);
            Assert.AreEqual(3.0, result.Z);

            result = original.Divide(2.0);
            Assert.AreEqual(1.0, result.X);
            Assert.AreEqual(2.0, result.Y);
            Assert.AreEqual(3.0, result.Z);
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.Dot"/> method.
        /// </summary>
        [Test]
        public void TestDotProduct()
        {
            Cartesian3 first = new Cartesian3(1.0, 3.0, -2.0);
            Cartesian3 second = new Cartesian3(4.0, -2.0, -1.0);
            Assert.AreEqual(0, first.Dot(second));
            Assert.AreEqual(0, second.Dot(first));
        }

        /// <summary>
        /// Tests the <see cref="Cartesian3.Cross"/> method.
        /// </summary>
        [Test]
        public void TestCrossProduct()
        {
            double angle = Math.PI / 4.0;
            double cos = Math.Cos(angle / 2.0);
            double sin = Math.Sin(angle / 2.0);

            double a = cos * cos - sin * sin / 3.0;
            double b = 2.0 * (sin * sin + sin * cos * Math.Sqrt(3.0)) / 3.0;
            double c = 2.0 * (sin * sin - sin * cos * Math.Sqrt(3.0)) / 3.0;

            // The three vectors here are the orthonormal set obtained by rotating
            // the x-axis, y-axis, and z-axis through an angle of 45 degrees about
            // the (1,1,1) vector.
            Cartesian3 first = new Cartesian3(a, b, c);
            Cartesian3 second = new Cartesian3(c, a, b);
            Cartesian3 third = new Cartesian3(b, c, a);
            Cartesian3 result = first.Cross(second);

            Assert.AreEqual(third.X, result.X, Constants.Epsilon14);
            Assert.AreEqual(third.Y, result.Y, Constants.Epsilon14);
            Assert.AreEqual(third.Z, result.Z, Constants.Epsilon14);
        }

        /// <summary>
        /// Tests rotation by a <see cref="UnitQuaternion"/>.
        /// </summary>
        [Test]
        public void TestRotateByUnitQuaternion()
        {
            double angle = Math.PI / 3.0;  // half angle of 120 degree rotation
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            Cartesian3 axis = (new Cartesian3(1.0, 1.0, 1.0)).Normalize(); // unit vector along [1,1,1]

            double w = cos;
            double x = sin * axis.X;
            double y = sin * axis.Y;
            double z = sin * axis.Z;

            // The original vector is along the x-axis.
            Cartesian3 original = new Cartesian3(1.0, 0.0, 0.0);

            // The rotated vector is along the z-axis.
            Cartesian3 rotated = original.Rotate(new UnitQuaternion(w, x, y, z));
            Assert.AreEqual(0.0, rotated.X, Constants.Epsilon15);
            Assert.AreEqual(0.0, rotated.Y, Constants.Epsilon15);
            Assert.AreEqual(1.0, rotated.Z, Constants.Epsilon15);
        }

        /// <summary>
        /// Tests rotation by an <see cref="Matrix3By3"/>.
        /// </summary>
        [Test]
        public void TestRotateByMatrix3By3()
        {
            double angle = Math.PI / 3.0;  // half angle of 120 degree rotation
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            Cartesian3 axis = (new Cartesian3(1.0, 1.0, 1.0)).Normalize(); // unit vector along [1,1,1]

            double w = cos;
            double x = sin * axis.X;
            double y = sin * axis.Y;
            double z = sin * axis.Z;

            // The original vector is along the x-axis.
            Cartesian3 original = new Cartesian3(1.0, 0.0, 0.0);

            // The rotated vector is along the z-axis.
            Cartesian3 rotated = original.Rotate(new Matrix3By3(new UnitQuaternion(w, x, y, z)));
            Assert.AreEqual(0.0, rotated.X, Constants.Epsilon15);
            Assert.AreEqual(0.0, rotated.Y, Constants.Epsilon15);
            Assert.AreEqual(1.0, rotated.Z, Constants.Epsilon15);
        }

        /// <summary>
        /// Tests that Cartesian3.GetHashCode returns something at least reasonably random.
        /// </summary>
        [Test]
        public void TestGetHashCode()
        {
            Cartesian3 object1 = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 object2 = new Cartesian3(1.0, 2.0, 3.0);
            Cartesian3 object3 = new Cartesian3(1.0, 2.0, 3.1);
            Assert.AreEqual(object1.GetHashCode(), object2.GetHashCode());
            Assert.AreNotEqual(object1.GetHashCode(), object3.GetHashCode());
        }

        /// <summary>
        /// Tests that construction from a null array of doubles throws the correct exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInitializationFromNull()
        {
            double[] array = null;
            Cartesian3 first = new Cartesian3(array, 0);
        }

        /// <summary>
        /// Tests that construction from an array of doubles with an incorrect length throws the correct exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInitializationFromBadArray()
        {
            double[] array = new double[2];
            Cartesian3 first = new Cartesian3(array, 0);
        }

        /// <summary>
        /// Tests to ensure that an invalid index throws the anticipated exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexTooHigh()
        {
            Cartesian3 first = new Cartesian3(1.0, 2.0, 3.0);
            double bad = first[3];
        }

        /// <summary>
        /// Tests ToString method
        /// </summary>
        [Test]
        public void TestToString()
        {
            double val1 = 1.1;
            double val2 = 2.1;
            double val3 = 3.1;
            string sep = ", ";
            String result = val1.ToString(CultureInfo.CurrentCulture) + sep + val2.ToString(CultureInfo.CurrentCulture) + sep +
                            val3.ToString(CultureInfo.CurrentCulture);
            Cartesian3 test = new Cartesian3(val1, val2, val3);
            Assert.AreEqual(result, test.ToString());
        }
    }
}