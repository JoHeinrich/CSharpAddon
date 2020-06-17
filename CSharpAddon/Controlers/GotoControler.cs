using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VoiceControl
{
    //public class FileController : ProtectInformationController, INamedCommandController
    //{
    //    public string Name => "File";

    //    public void Build(IBuilder builder)
    //    {
    //        builder.Commands.AddCommand(Information.FileNames, value => SendKeys.SendWait("^{t}f " + value + "{Enter}"));
    //    }
    //}
    //public class ClassController : ProtectInformationController, INamedCommandController
    //{
    //    public string Name => "class";

    //    public void Build(IBuilder builder)
    //    {
    //        builder.Commands.AddCommand(Information.Classes, value => SendKeys.SendWait("^{t}t " + value + "{Enter}"));
    //    }
    //}
    //public class FunctionController : ProtectInformationController, INamedCommandController
    //{
    //    public string Name => "function";

    //    public void Build(IBuilder builder)
    //    {
    //        builder.Commands.AddCommand(Information.Functions, value => SendKeys.SendWait("^{t}m " + value + "{Enter}"));
    //    }
    //}

    public class GotoController : ProtectInformationController, INamedCommandController
    {


        public string Name => "goto";

        public void Build(IBuilder builder)
        {
            builder.Commands.AddCommand("line number()", v => SendKeys.SendWait(@"^{t}:" + v + "{Enter}"));
            CreateCommandForEachEntry("file", builder, "^{t}f ", Information.FileNames, "{Enter}");
            CreateCommandForEachEntry("class", builder, "^{t}t ", Information.Classes, "{Enter}");
            CreateCommandForEachEntry("function", builder, "^{t}m ", Information.Functions, "{Enter}");

        }
        public void CreateCommandForEachEntry(string pre, IBuilder builder, string before, IEnumerable<string> entries, string after)
        {
            builder.Commands.AddCommand(entries, value => SendKeys.SendWait(before + value + after), pre);
            
        }
    }
}
