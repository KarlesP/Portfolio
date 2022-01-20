using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using Strings;
namespace TestFileUtility
{
  [TestFixture]
  class FileUtilityTest : AssertionHelper
  {
     private string path = @"C:\TestFiles\test2.txt;"
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
          FileUtility actual = new FileUtility(path);
          Expect(path.Split('\\').Last(), Is.EqualTo(fu.fileName));
          Expect(path, Is.EqualTo(fu.path));
     }
     [Test]
     public void get_path()
     {
          String actual = fu.get_path();
     }
     [Test]
     public void set_path()
     {
          fu.set_path(value);
     }
     [Test]
     public void get_fileName()
     {
          String actual = fu.get_fileName();
     }
     [Test]
     public void set_fileName()
     {
          fu.set_fileName(value);
     }
     [Test]
     public void GetIndexOfStringInFile()
     {
          string strToAppend = expected;
          fu.AppendStringToSpaceDelimitedFile(strToAppend);
          string strToFind = "delimited";
          Int32 actual = fu.GetIndexOfStringInFile(strToFind);
          Expect(actual, Is.EqualTo(2));
     }
     [Test]
     public void ReadString()
     {
          String actual = fu.ReadString();
     }
     [Test]
     public void WriteString()
     {
          fu.WriteString(str);
     }
     [Test]
     public void Clear()
     {
          fu.Clear();
     }
     [Test]
     public void AppendString()
     {
          fu.AppendString(strToAppend);
     }
     [Test]
     public void AppendString()
     {
          fu.AppendString(strToAppend, Delimiter);
     }
     [Test]
     public void AppendHashToDelimitedFile()
     {
          fu.AppendHashToDelimitedFile(hashToAppend, Delimiter);
     }
     [Test]
     public void AppendStringToSpaceDelimitedFile()
     {
          fu.AppendStringToSpaceDelimitedFile(strToAppend);
     }
     [Test]
     public void AppendStringToTabDelimitedFile()
     {
          fu.AppendStringToTabDelimitedFile(strToAppend);
     }
     [Test]
     public void AppendHashToSpaceDelimitedFile()
     {
          fu.AppendHashToSpaceDelimitedFile(hashToAppend);
     }
     [Test]
     public void AppendHashToTabDelimitedFile()
     {
          fu.AppendHashToTabDelimitedFile(hashToAppend);
     }
     [Test]
     public void GetIndexesOfString()
     {
          String actual = fu.GetIndexesOfString(str);
     }
     [Test]
     public void ToString()
     {
          String actual = fu.ToString();
     }
     [Test]
     public void Equals()
     {
          Boolean actual = fu.Equals(obj);
     }
     [Test]
     public void GetHashCode()
     {
          Int32 actual = fu.GetHashCode();
     }
     [Test]
     public void GetType()
     {
          Type actual = fu.GetType();
     }
     [Test]
     public void Finalize()
     {
          fu.Finalize();
     }
     [Test]
     public void MemberwiseClone()
     {
          Object actual = fu.MemberwiseClone();
     }
  }
}
