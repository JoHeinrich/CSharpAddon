using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceControl;

using System.Threading;
using CSharpAddon;
using System.IO;

namespace TestProgram
{
    class Program
    {
          
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.Load("CSharpAddon");
            var info = new ProjectInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\code\CSharpAddon\CSharpAddon.sln");
            string members = string.Join("\n", info.UsedMembers);
            File.WriteAllText("members.txt",members);
            File.WriteAllText("types.txt", string.Join("\n", info.UsedTypes));
            File.WriteAllText("files.txt", string.Join("\n", info.Files));
            //Thread.Sleep(2000);
            //LineFinder finder = new LineFinder();
            //finder.AddLineBelow(5, "Hello");
            ////FileInformation.AddLineBelow();
            //Console.WriteLine("");
            //LineFinder find = new LineFinder();
            //finder.AddLineBelow(find, "Hello");
            //FileInformation.AddLineBelow();
            //FileInformation.AddLineBelow();
            //FileInformation.AddLineBelow();
            //finder.AddLineBelow(find);
            //Console.WriteLine("");

            ////Console.WriteLine(line);Load
            ////Console.WriteLine(LineFinder);
            ////LineFinder findLine=newlineFine.ResourceManager
            ////var c=new ControllerTestProgram();
            ////InformationManager informationManager = new InformationManager(c.Values);
            //LineFinder find = new LineFinder();
            //LineFinder findLine = new LineFinder();
            //LineFinder find = new LineFinder();
            Console.WriteLine("Completed");
            //LineFinder findLine = new LineFinder();
            //AppDomain.CurrentDomain.Load("CSharpAddOn");
            //finder.AddLineBelow(AppDomain.CurrentDomain.Load)
            //while (true) { Thread.Sleep()}
            //finder.AddLineBelow(5, "");
            
            //Console.WriteLine("Ready");
            //while (true) { Thread.Sleep(2000); }
        }
        static void main()
        {
            
        }
    }
}
