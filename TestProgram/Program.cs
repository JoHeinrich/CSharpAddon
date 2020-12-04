using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceControl;

using System.Threading;
using CSharpAddon;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.Load("CSharpAddon");
            NotInPath.OpenDialog();
            //var c=new ControllerTestProgram();
            //InformationManager informationManager = new InformationManager(c.Values);

            //Console.WriteLine("Ready");
            while (true) { Thread.Sleep(2000); }
            
        }
    }
}
