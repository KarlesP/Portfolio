using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;


namespace Strings
{

    /// <summary>
    /// This is a manually written NUnit test class for FileUtility class I have written.
    /// </summary>
    [TestFixture]
    class StringsTest : AssertionHelper
    {
        private string path = @"C:\TestFiles\test2.txt";
        private FileUtility fu;
        private string expected;

        [SetUp]
        public void Init()
        {
            expected = "A delimited text"; 
            fu = new FileUtility(path);
            fu.Clear();
        }

        [Test]
        public void FileUtility()
        {
            Expect(path, Is.EqualTo(fu.path));
            Expect(path.Split('\\').Last(), Is.EqualTo(fu.fileName));
        }

        [Test]
        public void ReadString()
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(expected);
            sw.Close();
            string actual = fu.ReadString();
            Expect(expected, Is.EqualTo(actual));
        }

        [Test]
        public void WriteString()
        {
            fu.WriteString(expected);
            StreamReader sr = File.OpenText(path);
            string actual = sr.ReadToEnd();
            sr.Close();
            Expect(expected, Is.EqualTo(actual));
        }

        [Test]
        public void Clear()
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(expected);
            sw.Close();
            fu.Clear();
            string actual = fu.ReadString();
            Expect("", Is.EqualTo(actual));
        }

        [Test]
        public void AppendString()
        {
            fu.AppendString(expected);
            string actual = fu.ReadString();
            Expect(expected, Is.EqualTo(actual));
        }

        [Test]
        public void AppendStringWithDelimiter()
        {
            fu.AppendString(expected, ' ');
            string actual = fu.ReadString();
            Expect(expected + " ", Is.EqualTo(actual));
        }

        [Test]
        public void AppendStringTabDelimited()
        {
            string strToAppend = expected;
            fu.AppendStringToTabDelimitedFile(strToAppend);
            string actual = fu.ReadString();
            Expect(expected + "\t", Is.EqualTo(actual));
        }

        [Test]
        public void AppendStringSpaceDelimited()
        {
            string strToAppend = expected; 
            fu.AppendStringToSpaceDelimitedFile(strToAppend);
            string actual = fu.ReadString();
            Expect(actual, Is.EqualTo(expected + " "));
        }

        [Test]
        public void GetIndexOfString()
        {
            string strToAppend = expected;
            fu.AppendStringToSpaceDelimitedFile(strToAppend);
            string strToFind = "delimited";
            Int32 actual = fu.GetIndexOfStringInFile(strToFind);
            Expect(actual, Is.EqualTo(2));
        }

        [Test]
        public void GetIndexesOfString()
        {
            string exp = "apple juice orange juice mango juice";
                        
            fu.WriteString(exp);
            Assert.AreEqual("6,19,31" , fu.GetIndexesOfString("juice"));
        }

    }
}
