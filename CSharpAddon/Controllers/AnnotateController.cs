using System.Windows.Forms;

namespace VoiceControl
{
    public class AnnotateController : ICommandController
    {
        RegexForCSharp regex = new RegexForCSharp();
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<type,[function|class]><attribute,CSharpAddon.List.Attributes>", (IVaraibles x)=>
            {
                string attribute = x.Get("attribute");
                LineFinder lineFinder = new LineFinder();
                if(lineFinder.FindLine(MakeController.SelectTypeFinder(x)))
                {
                    //SendKeys.SendWait("{END}");
                    //SendKeys.SendWait(", ");
                    SendKeys.SendWait("^({Enter})[" + attribute+"]{LEFT}");
                }
            });
        }
    }
}
