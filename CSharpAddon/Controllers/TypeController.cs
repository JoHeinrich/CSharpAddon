using System.Windows.Forms;

namespace VoiceControl
{
    public class TypeController : ProtectInformationController, ICommandController
    {
        public TypeController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {

            if (Information != null)
            {
                foreach (var typename in Information.UsedTypes)
                {
                    builder.AddCommand(typename, () => SendKeys.SendWait(typename));
                }
            }
        }


    }
}
