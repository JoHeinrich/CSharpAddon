using System.Windows.Forms;
using System.Collections.Generic;
namespace VoiceControl
{
    public class CombinationController : ICommandController
    {
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<t,CSharpAddon.List.Types><m,CSharpAddon.List.Members>", x => SendKeys.SendWait(x.Get("t") + " " + TextFomatter.FirstLetterSmall(x.Get("m"))));
            builder.AddCommand("<t,CSharpAddon.Command.Generic><m,CSharpAddon.List.Members>", x => SendKeys.SendWait(/*x.Get("t") + */" " + TextFomatter.FirstLetterSmall(x.Get("m"))));

        }

    }
}
