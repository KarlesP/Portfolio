using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace Strings
{

    [TestFixture]
    class TestGeneratorTest : AssertionHelper
    {
        TestGenerator tg;
        [SetUp]
        public void Init()
        {
            tg = new TestGenerator(typeof(FileUtility));

        }

        [Test]
        public void ProcessClass()
        {
            tg.ProcessClass();
            string path = @"C:\TestFiles\FileUtilityTest.cs";
            FileUtility fu = new FileUtility(path);
            fu.WriteString(tg.TestText);
        }

        [Test]
        public void TestGenerator()
        {
            Expect(tg.assembly, Is.Not.Null);
            Expect(tg.assembly, Is.EqualTo(typeof(FileUtility).Module.Assembly));
            Expect(tg.TypeToTest, Is.EqualTo(typeof(FileUtility)));
        }

        [Test]
        public void GetMethods()
        {
            tg.GetMethods();
            Expect(tg.staticMethods, Is.Empty);
            Expect(tg.instanceMethods, Is.Not.Empty);
            Expect(tg.constructors, Is.Not.Empty);
        }

        [Test]
        public void GenerateMethodCall()
        {
            tg.GetMethods();
            MethodInfo mi = tg.TypeToTest.GetMethod("GetIndexOfStringInFile");
            string expected = "          Int32 actual = fu.GetIndexOfStringInFile(strToFind);";
            string actual = tg.GenerateMethodCall(mi, "fu");
            Expect(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GenerateMethodTest()
        {
            tg.GetMethods();
            MethodInfo mi = tg.TypeToTest.GetMethod("GetIndexOfStringInFile");
            string actual = tg.GenerateMethodTest(mi);
            string path = @"C:\TestFiles\testmethodgen.txt";
            FileUtility fu = new FileUtility(path);
            fu.WriteString(actual);
            //Expect(actual, Is.EqualTo("public void GetIndexOfStringInFile()"));
            Expect(actual.IndexOf("[Test]"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("public void GetIndexOfStringInFile()"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("{"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("string strToAppend = expected;"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("fu.AppendStringToSpaceDelimitedFile(strToAppend);"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("string strToFind = \"delimited\";"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("Int32 actual = fu.GetIndexOfStringInFile(strToFind);"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("Expect(actual, Is.EqualTo(2));"), Is.Not.EqualTo(-1));
            Expect(actual.IndexOf("}"), Is.Not.EqualTo(-1));
        }

    }
}
