using System.Windows.Forms;

namespace VoiceControl
{
    public class GenericsController : ProtectInformationController, ICommandController
    {
        public GenericsController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {
            if (Information != null)
            {
                foreach (var varname in Information.UsedGenerics/*.OrderBy(x=>x)*/)
                {
                    builder.AddCommand(varname, () => SendKeys.SendWait(varname));
                    //Console.WriteLine(varname);
                }
            }
        }


    }
}
