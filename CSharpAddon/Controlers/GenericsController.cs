using System.Windows.Forms;

namespace VoiceControl
{
    public class GenericsController : ProtectInformationController, INamedCommandController
    {
        public string Name => "Generic";

        public void Build(IBuilder builder)
        {
            builder.Settings.RepetitionCount = 10;
            if (Information != null)
            {
                foreach (var varname in Information.UsedGenerics/*.OrderBy(x=>x)*/)
                {
                    builder.Commands.AddCommand(varname, () => SendKeys.SendWait(varname));
                    //Console.WriteLine(varname);
                }
            }
        }


    }
}
