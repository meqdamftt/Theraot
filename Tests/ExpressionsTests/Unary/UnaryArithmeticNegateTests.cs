﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Tests.ExpressionCompiler.Unary
{
    public static class UnaryArithmeticNegateTests
    {
        #region Test methods

        [Test]
        public static void CheckUnaryArithmeticNegateByteTest()
        {
            byte[] values = new byte[] { 0, 1, byte.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateByte(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateCharTest()
        {
            char[] values = new char[] { '\0', '\b', 'A', '\uffff' };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateChar(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateDecimalTest()
        {
            decimal[] values = new decimal[] { decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateDecimal(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateDoubleTest()
        {
            double[] values = new double[] { 0, 1, -1, double.MinValue, double.MaxValue, double.Epsilon, double.NegativeInfinity, double.PositiveInfinity, double.NaN };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateDouble(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateFloatTest()
        {
            float[] values = new float[] { 0, 1, -1, float.MinValue, float.MaxValue, float.Epsilon, float.NegativeInfinity, float.PositiveInfinity, float.NaN };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateFloat(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateIntTest()
        {
            int[] values = new int[] { 0, 1, -1, int.MinValue, int.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateInt(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateLongTest()
        {
            long[] values = new long[] { 0, 1, -1, long.MinValue, long.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateLong(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateSByteTest()
        {
            sbyte[] values = new sbyte[] { 0, 1, -1, sbyte.MinValue, sbyte.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateSByte(values[i]);
            }
        }

        [Test]
        public static void CheckUnaryArithmeticNegateShortTest()
        {
            short[] values = new short[] { 0, 1, -1, short.MinValue, short.MaxValue };
            for (int i = 0; i < values.Length; i++)
            {
                VerifyArithmeticNegateShort(values[i]);
            }
        }

        #endregion

        #region Test verifiers

        private static void VerifyArithmeticNegateByte(byte value)
        {
            try
            {
                Expression<Func<byte>> e =
                   Expression.Lambda<Func<byte>>(
                       Expression.Negate(Expression.Constant(value, typeof(byte))),
                       Enumerable.Empty<ParameterExpression>());
                Assert.False(true); // shouldn't get here
            }
            catch (InvalidOperationException)
            {
                // success
            }
        }

        private static void VerifyArithmeticNegateChar(char value)
        {
            try
            {
                Expression<Func<char>> e =
                   Expression.Lambda<Func<char>>(
                       Expression.Negate(Expression.Constant(value, typeof(char))),
                       Enumerable.Empty<ParameterExpression>());
                Assert.False(true); // shouldn't get here
            }
            catch (InvalidOperationException)
            {
                // success
            }
        }

        private static void VerifyArithmeticNegateDecimal(decimal value)
        {
            Expression<Func<decimal>> e =
                Expression.Lambda<Func<decimal>>(
                    Expression.Negate(Expression.Constant(value, typeof(decimal))),
                    Enumerable.Empty<ParameterExpression>());
            Func<decimal> f = e.Compile();
            Assert.AreEqual((decimal)(0 - value), f());
        }

        private static void VerifyArithmeticNegateDouble(double value)
        {
            Expression<Func<double>> e =
                Expression.Lambda<Func<double>>(
                    Expression.Negate(Expression.Constant(value, typeof(double))),
                    Enumerable.Empty<ParameterExpression>());
            Func<double> f = e.Compile();
            Assert.AreEqual((double)(0 - value), f());
        }

        private static void VerifyArithmeticNegateFloat(float value)
        {
            Expression<Func<float>> e =
                Expression.Lambda<Func<float>>(
                    Expression.Negate(Expression.Constant(value, typeof(float))),
                    Enumerable.Empty<ParameterExpression>());
            Func<float> f = e.Compile();
            Assert.AreEqual((float)(0 - value), f());
        }

        private static void VerifyArithmeticNegateInt(int value)
        {
            Expression<Func<int>> e =
                Expression.Lambda<Func<int>>(
                    Expression.Negate(Expression.Constant(value, typeof(int))),
                    Enumerable.Empty<ParameterExpression>());
            Func<int> f = e.Compile();
            Assert.AreEqual((int)(0 - value), f());
        }

        private static void VerifyArithmeticNegateLong(long value)
        {
            Expression<Func<long>> e =
                Expression.Lambda<Func<long>>(
                    Expression.Negate(Expression.Constant(value, typeof(long))),
                    Enumerable.Empty<ParameterExpression>());
            Func<long> f = e.Compile();
            Assert.AreEqual((long)(0 - value), f());
        }

        private static void VerifyArithmeticNegateSByte(sbyte value)
        {
            try
            {
                Expression<Func<sbyte>> e =
                   Expression.Lambda<Func<sbyte>>(
                       Expression.Negate(Expression.Constant(value, typeof(sbyte))),
                       Enumerable.Empty<ParameterExpression>());
                Assert.False(true); // shouldn't get here
            }
            catch (InvalidOperationException)
            {
                // success
            }
        }

        private static void VerifyArithmeticNegateShort(short value)
        {
            Expression<Func<short>> e =
                Expression.Lambda<Func<short>>(
                    Expression.Negate(Expression.Constant(value, typeof(short))),
                    Enumerable.Empty<ParameterExpression>());
            Func<short> f = e.Compile();
            Assert.AreEqual((short)(0 - value), f());
        }

        #endregion
    }
}
