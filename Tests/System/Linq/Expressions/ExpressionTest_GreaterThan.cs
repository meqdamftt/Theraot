﻿#if LESSTHAN_NET35
extern alias nunitlinq;
#endif

//
// ExpressionTest_GreaterThan.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//
// (C) 2008 Novell, Inc. (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Linq.Expressions;
using NUnit.Framework;

#if TARGETS_NETCORE || TARGETS_NETSTANDARD
using System.Reflection;

#endif

namespace MonoTests.System.Linq.Expressions
{
    [TestFixture]
    public class ExpressionTestGreaterThan
    {
        private struct Slot
        {
            public readonly int Value;

            public Slot(int val)
            {
                Value = val;
            }

            public static bool operator >(Slot a, Slot b)
            {
                return a.Value > b.Value;
            }

            public static bool operator <(Slot a, Slot b)
            {
                return a.Value < b.Value;
            }
        }

        private enum Foo
        {
            Bar,
            Baz
        }

        [Test]
        public void Arg1Null()
        {
            Assert.Throws<ArgumentNullException>(() => Expression.GreaterThan(null, Expression.Constant(1)));
        }

        [Test]
        public void Arg2Null()
        {
            Assert.Throws<ArgumentNullException>(() => Expression.GreaterThan(Expression.Constant(1), null));
        }

        [Test]
        public void Boolean()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.GreaterThan(Expression.Constant(true), Expression.Constant(false)));
        }

        [Test]
        public void Double()
        {
            var expr = Expression.GreaterThan(Expression.Constant(2.0), Expression.Constant(1.0));
            Assert.AreEqual(ExpressionType.GreaterThan, expr.NodeType);
            Assert.AreEqual(typeof(bool), expr.Type);
            Assert.IsNull(expr.Method);
            Assert.AreEqual("(2 > 1)", expr.ToString());
        }

        [Test]
        public void EnumGreaterThan()
        {
            Assert.Throws<InvalidOperationException>
            (
                () =>
                {
                    Expression.GreaterThan
                    (
                        Foo.Bar.ToConstant(),
                        Foo.Baz.ToConstant()
                    );
                }
            );
        }

        [Test]
        public void Integer()
        {
            var expr = Expression.GreaterThan(Expression.Constant(2), Expression.Constant(1));
            Assert.AreEqual(ExpressionType.GreaterThan, expr.NodeType);
            Assert.AreEqual(typeof(bool), expr.Type);
            Assert.IsNull(expr.Method);
            Assert.AreEqual("(2 > 1)", expr.ToString());
        }

        [Test]
        public void MismatchedTypes()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.GreaterThan(Expression.Constant(new OpClass()), Expression.Constant(true)));
        }

        [Test]
        public void NoOperatorClass()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.GreaterThan(Expression.Constant(new NoOpClass()), Expression.Constant(new NoOpClass())));
        }

        [Test]
        public void NullableInt32GreaterThan()
        {
            var l = Expression.Parameter(typeof(int?), "l");
            var r = Expression.Parameter(typeof(int?), "r");

            var compiled = Expression.Lambda<Func<int?, int?, bool>>
            (
                Expression.GreaterThan(l, r), l, r
            ).Compile();

            Assert.IsFalse(compiled(null, null));
            Assert.IsFalse(compiled(null, 1));
            Assert.IsFalse(compiled(null, -1));
            Assert.IsFalse(compiled(1, null));
            Assert.IsFalse(compiled(-1, null));
            Assert.IsFalse(compiled(1, 2));
            Assert.IsTrue(compiled(2, 1));
            Assert.IsFalse(compiled(1, 1));
        }

        [Test]
        public void NullableInt32GreaterThanLiftedToNull()
        {
            var l = Expression.Parameter(typeof(int?), "l");
            var r = Expression.Parameter(typeof(int?), "r");

            var compiled = Expression.Lambda<Func<int?, int?, bool?>>
            (
                Expression.GreaterThan(l, r, true, null), l, r
            ).Compile();

            Assert.AreEqual(null, compiled(null, null));
            Assert.AreEqual(null, compiled(null, 1));
            Assert.AreEqual(null, compiled(null, -1));
            Assert.AreEqual(null, compiled(1, null));
            Assert.AreEqual(null, compiled(-1, null));
            Assert.AreEqual((bool?)false, compiled(1, 2));
            Assert.AreEqual((bool?)true, compiled(2, 1));
            Assert.AreEqual((bool?)false, compiled(1, 1));
        }

        [Test]
        public void StringS()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.GreaterThan(Expression.Constant(""), Expression.Constant("")));
        }

        [Test]
        public void TestCompiled()
        {
            var a = Expression.Parameter(typeof(int), "a");
            var b = Expression.Parameter(typeof(int), "b");

            var p = Expression.GreaterThan(a, b);

            var lambda = Expression.Lambda<Func<int, int, bool>>
            (
                p, a, b
            );

            var compiled = lambda.Compile();
            Assert.AreEqual(true, compiled(10, 1), "tc1");
            Assert.AreEqual(true, compiled(1, 0), "tc2");
            Assert.AreEqual(true, compiled(int.MinValue + 1, int.MinValue), "tc3");
            Assert.AreEqual(false, compiled(-1, 0), "tc4");
            Assert.AreEqual(false, compiled(0, int.MaxValue), "tc5");
        }

        [Test]
        public void UserDefinedClass()
        {
            var method = typeof(OpClass).GetMethod("op_GreaterThan");

            Assert.IsNotNull(method);

            var expr = Expression.GreaterThan(Expression.Constant(new OpClass()), Expression.Constant(new OpClass()));
            Assert.AreEqual(ExpressionType.GreaterThan, expr.NodeType);
            Assert.AreEqual(typeof(bool), expr.Type);
            Assert.AreEqual(method, expr.Method);
            Assert.AreEqual("(value(MonoTests.System.Linq.Expressions.OpClass) > value(MonoTests.System.Linq.Expressions.OpClass))", expr.ToString());
        }

        [Test]
        public void UserDefinedGreaterThanLifted()
        {
            var l = Expression.Parameter(typeof(Slot?), "l");
            var r = Expression.Parameter(typeof(Slot?), "r");

            var node = Expression.GreaterThan(l, r);
            Assert.IsTrue(node.IsLifted);
            Assert.IsFalse(node.IsLiftedToNull);
            Assert.AreEqual(typeof(bool), node.Type);
            Assert.IsNotNull(node.Method);

            var compiled = Expression.Lambda<Func<Slot?, Slot?, bool>>(node, l, r).Compile();

            Assert.AreEqual(true, compiled(new Slot(1), new Slot(0)));
            Assert.AreEqual(false, compiled(new Slot(-1), new Slot(1)));
            Assert.AreEqual(false, compiled(new Slot(1), new Slot(1)));
            Assert.AreEqual(false, compiled(null, new Slot(1)));
            Assert.AreEqual(false, compiled(new Slot(1), null));
            Assert.AreEqual(false, compiled(null, null));
        }

        [Test]
        public void UserDefinedGreaterThanLiftedToNull()
        {
            var l = Expression.Parameter(typeof(Slot?), "l");
            var r = Expression.Parameter(typeof(Slot?), "r");

            var node = Expression.GreaterThan(l, r, true, null);
            Assert.IsTrue(node.IsLifted);
            Assert.IsTrue(node.IsLiftedToNull);
            Assert.AreEqual(typeof(bool?), node.Type);
            Assert.IsNotNull(node.Method);

            var compiled = Expression.Lambda<Func<Slot?, Slot?, bool?>>(node, l, r).Compile();

            Assert.AreEqual(true, compiled(new Slot(1), new Slot(0)));
            Assert.AreEqual(false, compiled(new Slot(-1), new Slot(1)));
            Assert.AreEqual(false, compiled(new Slot(1), new Slot(1)));
            Assert.AreEqual(null, compiled(null, new Slot(1)));
            Assert.AreEqual(null, compiled(new Slot(1), null));
            Assert.AreEqual(null, compiled(null, null));
        }
    }
}