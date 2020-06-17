using System.Windows.Forms;

namespace VoiceControl
{
    public class TypeController : ProtectInformationController, INamedCommandController
    {
        public string Name => "type";

        public void Build(IBuilder builder)
        {

            if (Information != null)
            {
                foreach (var typename in Information.UsedTypes)
                {
                    builder.Commands.AddCommand(typename, () => SendKeys.SendWait(typename));
                    //Console.WriteLine(varname);
                }
            }
        }


    }
}
