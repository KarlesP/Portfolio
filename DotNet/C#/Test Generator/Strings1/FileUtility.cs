using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TestConditions;

namespace Strings
{
    /// <summary>
    /// This is the class implementing sample functionality to be tested.
    /// The PreCondition and PostCondition attributes are added to provide meta information for 
    /// a simple test generator I added to the exercise. These attributes allow to generate code 
    /// similar or identical to the manually writted NUnit test file StringsTest.cs
    /// Due to lack of time, I added only a few pre-and post conditions, to allow to generate Init method for test setup,
    /// the constructor and GetIndexOfStringInFile method of the class. An example of using test generator is provided in
    /// the TestGeneratorTest.cs file.
    /// </summary>
    [PreCondition(1,"private string path = @\"C:\\TestFiles\\test2.txt;\"")]
    [PreCondition(2,"private FileUtility fu;")] 
    [PreCondition(3,"private string expected;")] 
    [PreCondition(4,"[SetUp]")] 
    [PreCondition(5,"public void Init()")] 
    [PreCondition(6,"{")] 
    [PreCondition(7,"   expected = \"A delimited text\";")]  
    [PreCondition(8,"   fu = new FileUtility(path);")] 
    [PreCondition(9,"   fu.Clear();")] 
    [PreCondition(10,"}")]
    [PreCondition(11,"", ConstructorVariable = "fu")] 
    class FileUtility
    {
        /// <summary>
        /// Full path to a file
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// File name without directory path
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// Constructor for FileUtility class
        /// </summary>
        /// <param name="path">Full path to a file</param>
        [PostCondition("Expect(path, Is.EqualTo(fu.path));")]
        [PostCondition("Expect(path.Split('\\\\').Last(), Is.EqualTo(fu.fileName));")]
        public FileUtility(string path)
        {
            this.path = path;
            this.fileName = path.Split('\\').Last();
        }

        /// <summary>
        /// GetIndexOfStringInFile searches for a string in the file.
        /// </summary>
        /// <param name="strToFind">String to search for in the file.</param>
        /// <returns>An integer index of the string found. If not found, returns -1.</returns>
        [PreCondition(1,"string strToAppend = expected;")]
        [PreCondition(2,"fu.AppendStringToSpaceDelimitedFile(strToAppend);")]
        [PreCondition(3,"string strToFind = \"delimited\";")]
        [PostCondition("Expect(actual, Is.EqualTo(2));")]
        public int GetIndexOfStringInFile(string strToFind)
        {
            StreamReader sr = File.OpenText(path);
            int index = sr.ReadToEnd().IndexOf(strToFind);
            sr.Close();
            return index;
        }

        /// <summary>
        /// Read a file to a string 
        /// </summary>
        public string ReadString()
        {
            StreamReader sr = File.OpenText(path);
            string s = sr.ReadToEnd();
            sr.Close();
            return s;
        }

        /// <summary>
        /// Writes a string to the file
        /// </summary>
        /// <param name="strToAppend">String to append to the file</param>
        public void WriteString(string str)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(str);
            sw.Close();
        }

        /// <summary>
        /// Creates or overrides an empty text file
        /// </summary>
        /// <param name="strToAppend">String to append to the file</param>
        public void Clear()
        {
            FileStream fs = File.Create(path);
            fs.Close();
        }

        /// <summary>
        /// Appends a string to the file
        /// </summary>
        /// <param name="strToAppend">String to append to the file</param>
        public void AppendString(string strToAppend)
        {
            StreamWriter sw = File.AppendText(path);
            sw.Write(strToAppend);
            sw.Close();
        }

        /// <summary>
        /// Appends a string with a delimiter to the file
        /// </summary>
        /// <param name="strToAppend">String to append to the file</param>
        /// <param name="Delimiter">Delimiter to append at the end of strToAppend.</param>
        public void AppendString(string strToAppend, char Delimiter)
        { 
            AppendString(strToAppend + Delimiter);
        }

        /// <summary>
        /// Appends strings from a HashSet<string>, separating them with Delimiter
        /// </summary>
        /// <param name="strToAppend">String to append to the file</param>
        /// <param name="Delimiter">Delimiter to append at the end of strToAppend.</param>
        public void AppendHashToDelimitedFile(HashSet<string> hashToAppend, char Delimiter)
        {
            AppendString(hashToAppend.Aggregate((temp, next) => temp + next + Delimiter));
        }

        /// <summary>
        /// Appends a string with a space delimiter to the file.
        /// </summary>
        /// <param name="strToAppend">String to append to the file.</param>
        public void AppendStringToSpaceDelimitedFile(string strToAppend)
        {
            AppendString(strToAppend + " ");
        }

        /// <summary>
        /// Appends a string with a tab delimiter to the file.
        /// </summary>
        /// <param name="strToAppend">String to append to the file.</param>
        public void AppendStringToTabDelimitedFile(string strToAppend)
        {
            AppendString(strToAppend + "\t");
        }

        /// <summary>
        /// Appends strings from a HashSet<string>, separating them with spaces
        /// </summary>
        /// <param name="hashToAppend">HashSet<string> containing strings to be appended</param>
        public void AppendHashToSpaceDelimitedFile(HashSet<string> hashToAppend)
        {
            AppendHashToDelimitedFile(hashToAppend, ' ');
        }

        /// <summary>
        /// Appends strings from a HashSet<string>, separating them with tabs
        /// </summary>
        /// <param name="hashToAppend">HashSet<string> containing strings to be appended</param>
        public void AppendHashToTabDelimitedFile(HashSet<string> hashToAppend)
        {
            AppendHashToDelimitedFile(hashToAppend, '\t');
        }

        public string GetIndexesOfString(string str)
        {
            StreamReader sr = File.OpenText(path);
            string s = sr.ReadToEnd();
            sr.Close();
            string result = "";
            int index = s.IndexOf(str);
            if (index > -1)
                result = index.ToString();
            else
                return "";
            int len = str.Length;
            index = s.IndexOf(str, index + len);
            while (index > -1 && index < s.Length - 1) 
            {
                result += "," + index.ToString();
                index = s.IndexOf(str, index + len);
            } 
            return result;
        }


    }
}
