using System.Windows.Forms;

namespace VoiceControl
{
    public class ExtendController : ICommandController
    {
        RegexForCSharp regex = new RegexForCSharp();
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<type,CSharpAddon.List.Types>", (IVaraibles x) =>
            {
                string type = x.Get("type");
                LineFinder lineFinder = new LineFinder();
                if (lineFinder.FindLine(regex.FindDefinitions))
                {

                    SendKeys.SendWait("{END}");
                    var line = lineFinder.GetLine();
                    SendKeys.SendWait(line.Contains(":")?",":": ");
                    SendKeys.SendWait(type);
                }
            });
        }
    }
}
