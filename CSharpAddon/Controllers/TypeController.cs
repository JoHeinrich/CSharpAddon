using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoiceControl
{
    public class TypesController : ProtectInformationController, IListController
    {
        List<string> defaultTypes;
        public TypesController(IValueCollection globalState) : base(globalState)
        {
            defaultTypes = new List<string>
            {
                "int","long","void","char","float","byte","bool","double","short","string"
            };
        }
        public void Build(IListBuilder builder)
        {
            builder.Add(defaultTypes);
            builder.Add(Information?.UsedTypes);
        }

    }

    public class TypeController : ProtectInformationController, ICommandController
    {
        public TypeController(IValueCollection globalState) : base(globalState)
        {
            List<string> sting = new List<string>();

        }

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("CSharpAddon.List.Types", x => SendKeys.SendWait(x));
        }


    }
}
