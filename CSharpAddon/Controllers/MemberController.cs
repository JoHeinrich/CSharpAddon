using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class MemberController : ICommandController
    {
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<a,CSharpAddon.List.Members>", x => SendKeys.SendWait(x));
            builder.AddCommand("small <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FirstLetterSmall(x)));
            builder.AddCommand("big <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FormatClass(x)));
            builder.AddCommand("<t1,CSharpAddon.List.Members> dot <t2,CSharpAddon.List.Members>[ dot <t3,CSharpAddon.List.Members>]", x => SendKeys.SendWait(x.Get("t1") + "." + x.Get("t2") + (x.IsAvailable("t3") ? ("." + x.Get("t3")) : "")));

        }

    }
}
