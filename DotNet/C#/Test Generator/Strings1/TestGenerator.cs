using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TestConditions;

namespace Strings
{
    /// <summary>
    /// A simple test generator using Reflection and Custom attributes for generating code for use with NUnit.
    /// Small modifications to the class can provide functionality equivalent to Test Unit generator from Visual Studio,
    /// or similar to what ZenTest is doing for Ruby classes.
    /// A test class skeleton can be generated for any class, but if one provides code required to set up parameter values
    /// for a particular method under test (in the form of PreCondition attributes as shown in FileUtility), and assertions
    /// or expectations in PostCondition attributes, the test generator can generate a fully functional test (in this alpha 
    /// version, possibly with minor additions for covering methods generated for class properties).
    /// </summary>
    class TestGenerator
    {
        public Type TypeToTest { get; set; }
        public Assembly assembly { get; set; }
        public MethodInfo[] staticMethods;
        public MethodInfo[] instanceMethods;
        public ConstructorInfo[] constructors;
        public string TestText { get; set; }

         public TestGenerator(Type type)
        {
            TypeToTest = type;
            assembly = type.Module.Assembly;
        }

        public void GetMethods()
        {
            staticMethods = TypeToTest.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            instanceMethods = TypeToTest.GetMethods((BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
            constructors = TypeToTest.GetConstructors((BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public));
        }

        public void ProcessClass()
        {
            GetMethods();
            GenerateTestClassHeader();
            GenerateClassPreConditions();
            ProcessConstructors();
            ProcessStaticMethods();
            ProcessInstanceMethods();
            GenerateClassFooter();
            GenerateNameSpaceFooter();
        }

        public void GenerateClassFooter()
        {
            TestText += "  }\n";
        }
        public void GenerateNameSpaceFooter()
        {
            TestText += "}\n";
        }

        private void GenerateTestClassHeader()
        {
            string result = "using System;\n";
            result += "using System.Collections.Generic;\n";
            result += "using System.Linq;\n";
            result += "using System.Text;\n";
            result += "using System.IO;\n";
            result += "using NUnit.Framework;\n";
            result += String.Format("using {0};\n", TypeToTest.Namespace);
            result += String.Format("namespace Test{0}\n", TypeToTest.Name);
            result += "{\n";
            result += "  [TestFixture]\n";
            result += String.Format("  class {0}Test : AssertionHelper\n", TypeToTest.Name);
            result += "  {\n";
            TestText += result;
        }

        private void GenerateClassPreConditions()
        {
            object[] ca = TypeToTest.GetCustomAttributes(false);
            string preConditions = "";
            IEnumerable<object> preConds = ca.Where(attr => attr.GetType() == typeof(PreCondition));
            preConds = preConds.OrderBy(attr => ((PostCondition)attr).number);
            foreach (System.Attribute attr in preConds)
            {
                if (attr.GetType() == typeof(PreCondition))
                {
                    preConditions += "     " + ((PreCondition)attr).Condition + "\n";
                }
            }
            TestText += preConditions;
        }

        public void ProcessStaticMethods()
        {
            foreach (MethodInfo mi in staticMethods)
            {
                TestText += GenerateMethodTest(mi);
            }
        }

        public void ProcessConstructors()
        {
            foreach (ConstructorInfo mi in constructors)
            {
                TestText += GenerateMethodTest(mi);
            }
        }

        public void ProcessInstanceMethods()
        {
            foreach (MethodInfo mi in instanceMethods)
            {
                TestText += GenerateMethodTest(mi);
            }
        }

        public string GeneratePreConditions(MemberInfo mi)
        {
            object[] ma = mi.GetCustomAttributes(false);
            string preConditions = "";
            IEnumerable<object> preConds = ma.Where(attr => attr.GetType() == typeof(PreCondition));
            preConds = preConds.OrderBy(attr => ((PostCondition)attr).number);
            foreach (System.Attribute attr in preConds)
            {
                if (attr.GetType() == typeof(PreCondition))
                {
                    preConditions += "          " + ((PreCondition)attr).Condition + "\n";
                }
            }
            return preConditions;
        }

        public string GeneratePostConditions(MemberInfo mi)
        {
            object[] ma = mi.GetCustomAttributes(false);
            string postConditions = "";
            foreach (System.Attribute attr in ma)
            {
                if (attr.GetType() == typeof(PostCondition))
                {
                    postConditions += "          " + ((PostCondition)attr).Condition + "\n";
                }
            }

            return postConditions;
        }



        public string GenerateConstructorTest(ConstructorInfo mi)
        {
            string result = "";
            object[] classAttributes = TypeToTest.GetCustomAttributes(true);
            IEnumerable<object> preConditions = classAttributes.Where(attr => attr.GetType() == typeof(PreCondition));
            PreCondition preCondition = preConditions.Select(pc => (PreCondition)pc).Where(pre => pre.ConstructorVariable != null).First();

            int index = 0;
            result += GenerateTestMethodHeader(mi, index);
            result += GeneratePreConditions(mi);
            result += GenerateConstructorCall(mi, preCondition.ConstructorVariable) + "\n";
            result += GeneratePostConditions(mi);
            result += GenerateTestMethodFooter();
            return result;
        }

        private string GenerateConstructorCall(ConstructorInfo mi, string constructorVariable)
        {
            string result;
            string methodName = mi.ReflectedType.Name;
            string returnType = mi.ReflectedType.Name;
            string signature = String.Format("{0}(", methodName);
            ParameterInfo[] pis = mi.GetParameters();
            IEnumerable<string> piNames = pis.Select(pi => pi.Name);
            string paramList = piNames.Aggregate((s, pi) => s + ", " + pi);
            result = String.Format("          {0} actual = new {1}{2});", returnType, signature, paramList);
            return result;
        }

        public string GenerateMethodTest(MemberInfo mi)
        {
            string result = "";
            if (TypeToTest.IsSubclassOf(mi.ReflectedType))
                return result;
            object[] classAttributes = TypeToTest.GetCustomAttributes(true);
            IEnumerable<object> preConditions = classAttributes.Where(attr => attr.GetType() == typeof(PreCondition));
            PreCondition preCondition = preConditions.Select(pc => (PreCondition)pc).Where(pre => pre.ConstructorVariable != null).First();

            int index = 0;
            result += GenerateTestMethodHeader(mi, index);
            result += GeneratePreConditions(mi);
            if (mi.GetType() == typeof(ConstructorInfo) || mi.GetType().BaseType == typeof(ConstructorInfo))
                result += GenerateConstructorCall((ConstructorInfo)mi, preCondition.ConstructorVariable) + "\n";
            else
                result += GenerateMethodCall((MethodInfo)mi, preCondition.ConstructorVariable) + "\n";
            result += GeneratePostConditions(mi);
            result += GenerateTestMethodFooter();
            return result;
        }

        public string GenerateTestMethodHeader(object mi, int index)
        {
            string result = "     [Test]\n";
            if(mi.GetType() == typeof(ConstructorInfo) || mi.GetType().BaseType == typeof(ConstructorInfo))
                result += String.Format("     public void {0}()\n", ((ConstructorInfo)mi).ReflectedType.Name) + "     {\n";
            else
                result += String.Format("     public void {0}()\n", ((MethodInfo)mi).Name) + "     {\n";
            return result;
        }

        public string GenerateTestMethodFooter()
        {
            return "     }\n";
        }

        public string GenerateMethodCall(MethodInfo mi, string constructorVariable)
        {
            string result;
            string methodName = mi.Name;
            string returnType = mi.ReturnType.Name;
            bool isConstructor = mi.IsConstructor;
            string signature = String.Format("{0}(", methodName);
            ParameterInfo [] pis = mi.GetParameters();
            IEnumerable<string> piNames = pis.Select(pi => pi.Name);
            string paramList = "";
            if (piNames.Count() > 0)
                paramList = piNames.Aggregate((s, pi) => s + ", " + pi);
            if (returnType != "Void" || isConstructor)
                result = String.Format("          {0} actual = {1}.{2}{3});", returnType, constructorVariable, signature, paramList);
            else
                result = String.Format("          {0}.{1}{2});", constructorVariable, signature, paramList);
            return result;
        }

    }
}
