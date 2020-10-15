using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoiceControl;
using System.Linq;
using System.Collections.Generic;

namespace WindowsAddonsTest
{
    [TestClass]
    public class UnitTest1
    {
        RegexForCSharp regex = new RegexForCSharp();
        public void Check(Func<string, List<string>> func, string input, List<string> result)
        {
            var found = func(input);
            Assert.AreEqual(result.Count, found.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i], found[i]);
            }
        }


        [DataTestMethod]
        [DataRow(" void TestMethod1(){", "TestMethod1")]
        [DataRow(" int  TestMethod1();", "TestMethod1")]
        [DataRow(" void TestMethod1();", "TestMethod1")]
        [DataRow(" void TestMethod1(){", "TestMethod1")]
       
        [DataRow(" void TestMethod1(string test);", "TestMethod1")]
        [DataRow(" void TestMethod1(string test,int t);", "TestMethod1")]
        [DataRow(" void TestMethod1(List<string> test);", "TestMethod1")]
        [DataRow("public void Build(ICommandBuilder builder);", "Build")]
        public void TestFunction(string input, string result)
        {
            Check(regex.FindFunctionDefinitions, input,new List<string> { result });
        }

        [DataTestMethod]
        [DataRow("TestMethod1()")]
        [DataRow("return TestMethod1()")]
        [DataRow("using TestMethod1")]
        [DataRow(" int  TestMethod1{" )]
        [DataRow(" voidTestMethod1();" )]
        [DataRow("TestMethod1=" )]
        [DataRow("=void TestMethod1()")]
        [DataRow("(void TestMethod1()")]
        [DataRow("                found.Add(varname);")]
        [DataRow("CSharpProjectInformation cSharpProjectInformation = new CSharpProjectInformation();")]

        public void TestWrongFunction(string input)
        {
            Check(regex.FindFunctionDefinitions, input, new List<string> {  });
        }

        [TestMethod]
        public void TestVariable()
        {
            RegexForCSharp regex = new RegexForCSharp();
            var found = regex.FindVariables("Files.ForEach(file =>            {                found.AddRange(func(File.ReadAllText(file)));            }); ");
            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("file", found[0]);
        }
  
        [TestMethod]
        public void TestFunction4()
        {
            RegexForCSharp regex = new RegexForCSharp();
            var found = regex.FindFunctionDefinitions("CSharpProjectInformation cSharpProjectInformation = new CSharpProjectInformation();");
            Assert.AreEqual(0, found.Count);
        }

        [TestMethod]
        public void TestVariableSo()
        {

            ProjectInformation info = ProjectInformationManager.Active;
            foreach (var item in info.Variables)
            {
                Console.WriteLine(item);

            }

            //Assert.IsTrue(cSharpProjectInformation.DefinedFunctions.Contains("Build"));

        }
    }
}
