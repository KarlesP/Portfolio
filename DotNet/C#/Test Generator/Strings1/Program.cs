using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Strings
{
    class Program
    {

        /// From the description of the Code Exercise:
        /// The following is a class that allows a user to save strings to a file and then later retrieve the first index of a specified string.  
        /// Currently the class can create and read space-delimited and tab-delimited strings.  
        /// A user wants the class to be extended to (1) append all strings from a HashSet<string> to a file and 
        /// (2) return a comma separated list of every index where the string appears in the file. 
        /// We always leave code better than we find it, so create automated unit tests, add the desired functionality, 
        /// and refactor the code to make it better.
        /// Please see the implementation in FileUtility class and NUnit test for it in StringsTest.cs

        public static void Main(string[] args)
        {
            string path = @"C:\TestFiles\test.txt";
            Console.WriteLine(path.Split('\\').Last());
            HashSet<string> hash = new HashSet<string>("apple juice,orange juice,mango juice".Split(','));
            FileUtility fu = new FileUtility(path);
            switch( args[0])
            {
                case "s":
                    fu.AppendStringToSpaceDelimitedFile("Space delimited text");
                    Console.WriteLine( fu.GetIndexOfStringInFile("delimited"));
                    break;
                case "t":
                    fu.AppendStringToTabDelimitedFile("Tab delimited text");
                    Console.WriteLine(fu.GetIndexOfStringInFile("delimited"));
                    break;
                case "hs":
                    fu.AppendHashToSpaceDelimitedFile(hash);
                    Console.WriteLine(fu.GetIndexesOfString("juice"));
                    break;
                case "ht":
                    fu.AppendHashToTabDelimitedFile(hash);
                    Console.WriteLine(fu.GetIndexesOfString("juice"));
                    break;
                default:
                    break;
            }
            Console.WriteLine( fu.GetIndexOfStringInFile("delimited"));
        }
    }
}
