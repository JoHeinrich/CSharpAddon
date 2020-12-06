using System.Linq;

namespace VoiceControl
{
    public class UseController:ProjectInformationController, ICommandController
    {
        public UseController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<using,CSharpAddon.List.Usings>", (IVaraibles varaibles) =>
            {
                string use = varaibles.Get("using");
                var namespaces = Information?.UsedNamespaces.Where(x => x.EndsWith(use));
                var name = namespaces.FirstOrDefault();
                LineFinder lineFinder = new LineFinder();
                lineFinder.AddLineBelow(1, "using " + name+";");
            });
        }



    }
}
