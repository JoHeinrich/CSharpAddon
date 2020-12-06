using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VoiceControl
{

    public class GotoController : ProjectInformationController, ICommandController
    {
        public GotoController(IValueCollection globalState) : base(globalState)
        {

        }

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("line number()", (int v) => SendKeys.SendWait(@"^{t}:" + v + "{Enter}"));
            if (Information == null) return;
            CreateCommandForEachEntry("file", builder, "^{t}f ", Information.FileNames, "{Enter}");
            CreateCommandForEachEntry("class", builder, "^{t}t ", Information.Classes, "{Enter}");
            CreateCommandForEachEntry("function", builder, "^{t}m ", Information.Functions, "{Enter}");
        }
        public void CreateCommandForEachEntry(string pre, ICommandBuilder builder, string before, IEnumerable<string> entries, string after)
        {
            builder.AddCommand(entries, value => SendKeys.SendWait(before + value + after), pre);
        }
    }
}
