using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{
    public class AccessModifiers : IListController
    {
        public void Build(IListBuilder builder)
        {
            builder.Add(new List<string> { "public", "private", "protected", "internal" });
        }
    }

    public class MembersController : ProtectInformationController, IListController
    {
        public MembersController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(IListBuilder builder)
        {
            builder.Add(Information?.UsedMembers);
        }
    }

    public class MemberController : ProtectInformationController, ICommandController
    {
        public MemberController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<a,CSharpAddon.List.Members>", x => SendKeys.SendWait(x));
            builder.AddCommand("small <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FormatVariable(x)));
            builder.AddCommand("big <a,CSharpAddon.List.Members>", x => SendKeys.SendWait(TextFomatter.FormatClass(x)));
        }

    }

    public class CombinationController : ICommandController
    {
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<t,CSharpAddon.List.Types><m,CSharpAddon.List.Members>", x => SendKeys.SendWait(x.Get("t") + " " + TextFomatter.FormatVariable(x.Get("m"))));
            builder.AddCommand("new [<CSharpAddon.Command.Type>|<CSharpAddon.Command.Generic>]", () => SendKeys.SendWait("new"));
        }
    }
}
