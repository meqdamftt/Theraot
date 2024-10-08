﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Tests.ExpressionCompiler.Binary
{
    public static class BinarySubtractTests
    {
        #region Test methods

        [Test]
        public static void CheckByteSubtractTest()
        {
            byte[] array = new byte[] { 0, 1, byte.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyByteSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckSByteSubtractTest()
        {
            sbyte[] array = new sbyte[] { 0, 1, -1, sbyte.MinValue, sbyte.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifySByteSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckUShortSubtractTest()
        {
            ushort[] array = new ushort[] { 0, 1, ushort.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyUShortSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckShortSubtractTest()
        {
            short[] array = new short[] { 0, 1, -1, short.MinValue, short.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyShortSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckUIntSubtractTest()
        {
            uint[] array = new uint[] { 0, 1, uint.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyUIntSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckIntSubtractTest()
        {
            int[] array = new int[] { 0, 1, -1, int.MinValue, int.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyIntSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckULongSubtractTest()
        {
            ulong[] array = new ulong[] { 0, 1, ulong.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyULongSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckLongSubtractTest()
        {
            long[] array = new long[] { 0, 1, -1, long.MinValue, long.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyLongSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckFloatSubtractTest()
        {
            float[] array = new float[] { 0, 1, -1, float.MinValue, float.MaxValue, float.Epsilon, float.NegativeInfinity, float.PositiveInfinity, float.NaN };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyFloatSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckDoubleSubtractTest()
        {
            double[] array = new double[] { 0, 1, -1, double.MinValue, double.MaxValue, double.Epsilon, double.NegativeInfinity, double.PositiveInfinity, double.NaN };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyDoubleSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckDecimalSubtractTest()
        {
            decimal[] array = new decimal[] { decimal.Zero, decimal.One, decimal.MinusOne, decimal.MinValue, decimal.MaxValue };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyDecimalSubtract(array[i], array[j]);
                }
            }
        }

        [Test]
        public static void CheckCharSubtractTest()
        {
            char[] array = new char[] { '\0', '\b', 'A', '\uffff' };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    VerifyCharSubtract(array[i], array[j]);
                }
            }
        }

        #endregion

        #region Test verifiers

        private static void VerifyByteSubtract(byte a, byte b)
        {
            bool failed = false;
            try
            {
                Expression<Func<byte>> e =
                    Expression.Lambda<Func<byte>>(
                        Expression.Subtract(
                            Expression.Constant(a, typeof(byte)),
                            Expression.Constant(b, typeof(byte))),
                        Enumerable.Empty<ParameterExpression>());
            }
            catch (InvalidOperationException)
            {
                // this is expected
                failed = true;
            }

            Assert.True(failed);
        }

        private static void VerifySByteSubtract(sbyte a, sbyte b)
        {
            bool failed = false;
            try
            {
                Expression<Func<sbyte>> e =
                    Expression.Lambda<Func<sbyte>>(
                        Expression.Subtract(
                            Expression.Constant(a, typeof(sbyte)),
                            Expression.Constant(b, typeof(sbyte))),
                        Enumerable.Empty<ParameterExpression>());
            }
            catch (InvalidOperationException)
            {
                // this is expected
                failed = true;
            }

            Assert.True(failed);
        }

        private static void VerifyUShortSubtract(ushort a, ushort b)
        {
            Expression<Func<ushort>> e =
                Expression.Lambda<Func<ushort>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(ushort)),
                        Expression.Constant(b, typeof(ushort))),
                    Enumerable.Empty<ParameterExpression>());
            Func<ushort> f = e.Compile();

            // add with expression tree
            ushort etResult = default(ushort);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            ushort csResult = default(ushort);
            Exception csException = null;
            try
            {
                csResult = (ushort)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyShortSubtract(short a, short b)
        {
            Expression<Func<short>> e =
                Expression.Lambda<Func<short>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(short)),
                        Expression.Constant(b, typeof(short))),
                    Enumerable.Empty<ParameterExpression>());
            Func<short> f = e.Compile();

            // add with expression tree
            short etResult = default(short);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            short csResult = default(short);
            Exception csException = null;
            try
            {
                csResult = (short)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyUIntSubtract(uint a, uint b)
        {
            Expression<Func<uint>> e =
                Expression.Lambda<Func<uint>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(uint)),
                        Expression.Constant(b, typeof(uint))),
                    Enumerable.Empty<ParameterExpression>());
            Func<uint> f = e.Compile();

            // add with expression tree
            uint etResult = default(uint);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            uint csResult = default(uint);
            Exception csException = null;
            try
            {
                csResult = (uint)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyIntSubtract(int a, int b)
        {
            Expression<Func<int>> e =
                Expression.Lambda<Func<int>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(int)),
                        Expression.Constant(b, typeof(int))),
                    Enumerable.Empty<ParameterExpression>());
            Func<int> f = e.Compile();

            // add with expression tree
            int etResult = default(int);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            int csResult = default(int);
            Exception csException = null;
            try
            {
                csResult = (int)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyULongSubtract(ulong a, ulong b)
        {
            Expression<Func<ulong>> e =
                Expression.Lambda<Func<ulong>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(ulong)),
                        Expression.Constant(b, typeof(ulong))),
                    Enumerable.Empty<ParameterExpression>());
            Func<ulong> f = e.Compile();

            // add with expression tree
            ulong etResult = default(ulong);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            ulong csResult = default(ulong);
            Exception csException = null;
            try
            {
                csResult = (ulong)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyLongSubtract(long a, long b)
        {
            Expression<Func<long>> e =
                Expression.Lambda<Func<long>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(long)),
                        Expression.Constant(b, typeof(long))),
                    Enumerable.Empty<ParameterExpression>());
            Func<long> f = e.Compile();

            // add with expression tree
            long etResult = default(long);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            long csResult = default(long);
            Exception csException = null;
            try
            {
                csResult = (long)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyFloatSubtract(float a, float b)
        {
            Expression<Func<float>> e =
                Expression.Lambda<Func<float>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(float)),
                        Expression.Constant(b, typeof(float))),
                    Enumerable.Empty<ParameterExpression>());
            Func<float> f = e.Compile();

            // add with expression tree
            float etResult = default(float);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            float csResult = default(float);
            Exception csException = null;
            try
            {
                csResult = (float)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyDoubleSubtract(double a, double b)
        {
            Expression<Func<double>> e =
                Expression.Lambda<Func<double>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(double)),
                        Expression.Constant(b, typeof(double))),
                    Enumerable.Empty<ParameterExpression>());
            Func<double> f = e.Compile();

            // add with expression tree
            double etResult = default(double);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            double csResult = default(double);
            Exception csException = null;
            try
            {
                csResult = (double)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyDecimalSubtract(decimal a, decimal b)
        {
            Expression<Func<decimal>> e =
                Expression.Lambda<Func<decimal>>(
                    Expression.Subtract(
                        Expression.Constant(a, typeof(decimal)),
                        Expression.Constant(b, typeof(decimal))),
                    Enumerable.Empty<ParameterExpression>());
            Func<decimal> f = e.Compile();

            // add with expression tree
            decimal etResult = default(decimal);
            Exception etException = null;
            try
            {
                etResult = f();
            }
            catch (Exception ex)
            {
                etException = ex;
            }

            // add with real IL
            decimal csResult = default(decimal);
            Exception csException = null;
            try
            {
                csResult = (decimal)(a - b);
            }
            catch (Exception ex)
            {
                csException = ex;
            }

            // either both should have failed the same way or they should both produce the same result
            if (etException != null || csException != null)
            {
                Assert.NotNull(etException);
                Assert.NotNull(csException);
                Assert.AreEqual(csException.GetType(), etException.GetType());
            }
            else
            {
                Assert.AreEqual(csResult, etResult);
            }
        }

        private static void VerifyCharSubtract(char a, char b)
        {
            bool failed = false;
            try
            {
                Expression<Func<char>> e =
                    Expression.Lambda<Func<char>>(
                        Expression.Subtract(
                            Expression.Constant(a, typeof(char)),
                            Expression.Constant(b, typeof(char))),
                        Enumerable.Empty<ParameterExpression>());
            }
            catch (InvalidOperationException)
            {
                // this is expected
                failed = true;
            }

            Assert.True(failed);
        }

        #endregion
    }
}
