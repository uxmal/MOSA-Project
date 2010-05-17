﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Fröhlich (aka grover, <mailto:sharpos@michaelruck.de>)
 *  
 */

namespace Test.Mosa.Runtime.CompilerFramework.CIL
{
    using MbUnit.Framework;

    [TestFixture]
    [Importance(Importance.Critical)]
    [Category(@"Basic types")]
    [Description(@"Tests support for the basic type System.String")]
    public class StringFixture : CodeDomTestRunner
    {
        private static string CreateTestCode(string value)
        {
            return @"
                public class TestClass
                {
                    public static string valueA = @""" + value + @""";
                    public static string valueB = @""" + value + @""";

                    public static bool LengthMustMatch()
                    {
                        return " + value.Length + @" == valueA.Length;
                    }
                }

            "
            + Code.ObjectClassDefinition
            + Code.NoStdLibDefinitions;
        }

        private delegate bool B_V();

        [Test]
        public void MustProperlyCompileLdstrAndLengthMustMatch()
        {
            this.CodeSource = CreateTestCode(@"Foo");
            this.DoNotReferenceMsCorlib = true;

            Assert.IsTrue((bool)Run<B_V>("", "TestClass", "LengthMustMatch"));
        }
    }
}
