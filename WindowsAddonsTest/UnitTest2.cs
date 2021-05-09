using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoiceControl;
namespace WindowsAddonsTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void ConstructorTest()
        {

            FileInformation information = new FileInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\CSharpAddon\ProjectInformation.cs");
        }


        [TestMethod]
        public void ParameterTypes()
        {
            FileInformation information = new FileInformation(@"C:\Users\laise\Documents\EasyVoiceCodeTest2\Addons\CSharpAddon\code\CSharpAddon\WindowsAddonsTest\TestFiles\ParameterTestClass.cs");
            foreach (var item in information.UsedTypes)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(information.UsedTypes.Contains("ParameterType"));
        }
        [TestMethod]
        public void AtLeastRemainingIdentifiers()
        {

            FileInformation information = new FileInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\CSharpAddon\ProjectInformation.cs");
            var recognizedList = information.DefinedClasses.Concat(information.DefinedFunctions.Concat(information.DefinedVariables));
            var singleRecognize = new HashSet<string>(recognizedList);
            var allSingle = new HashSet<string>(information.AllIdentifiers);
            foreach (var item in allSingle)
            {
                singleRecognize.Remove(item);
            }
            Console.WriteLine(allSingle + " missing it identifiers");
            foreach (var item in allSingle)
            {
                Console.WriteLine(item);
            }
        }
        [TestMethod]
        public void ThisProjectInformation()
        {

            ProjectInformation information = new ProjectInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\CSharpAddon.sln");
            foreach (var item in information.Variables.OrderBy(x=>x))
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void WriteProjectInformationToFile()
        {

            ProjectInformation information = new ProjectInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\CSharpAddon.sln");
            string list = string.Join("\n", information.UsedMembers.Select(x=>x.ToLower()));
            File.WriteAllText(@"C:\Users\laise\Desktop\members.txt",list);
            //foreach (var item in information.Variables.OrderBy(x => x))
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
