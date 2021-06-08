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
            //builder.AddCommand("<t1,CSharpAddon.List.Members> dot <t2,CSharpAddon.List.Members>[ dot <t3,CSharpAddon.List.Members>]", x => SendKeys.SendWait(x.Get("t1") + "." + x.Get("t2") + (x.IsAvailable("t3") ? ("." + x.Get("t3")) : "")));
            builder.AddCommand("<g,CSharp1Addon.List.Generics>of<t,CSharpAddon.List.Types>", x => SendKeys.SendWait(x.Get(0) + "<" + x.Get(1) + ">"));
            builder.AddCommand("<g,CSharpAddon.List.Generics>of<t1,CSharpAddon.List.Types>komma<t2,CSharpAddon.List.Types>", x => SendKeys.SendWait(x.Get(0) + "<" + x.Get(1) + "," + x.Get(2) + ">"));
            builder.AddCommand("new <a,CSharpAddon.List.Types>", (string x) => SendKeys.SendWait("new "+x));
            builder.AddCommand("new <g,CSharpAddon.List.Generics>of<t,CSharpAddon.List.Types>", x => SendKeys.SendWait("new "+ x.Get(0) + "<" + x.Get(1) + ">"));
            builder.AddCommand("array of<t,CSharpAddon.List.Types>", x => SendKeys.SendWait(x.Get(0) +"[]"));
        }

    }
}
