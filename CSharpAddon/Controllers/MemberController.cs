using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class MemberController : ProjectInformationController, ICommandController
    {
        public MemberController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {
            //builder.AddCommand("<a,CSharpAddon.List.Members>", x => SendKeys.SendWait(x));
            builder.AddCommand("small <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FirstLetterSmall(x)));
            builder.AddCommand("big <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FormatClass(x)));
        }

    }
}
