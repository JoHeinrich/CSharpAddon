using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class MemberController : ProtectInformationController, INamedCommandController
    {
        public string Name => "Member";

        public void Build(IBuilder builder)
        {
            builder.Settings.RepetitionCount = 10;
            if (Information != null)
            {
                foreach (var varname in Information.UsedMembers/*.OrderBy(x=>x)*/)
                {
                    builder.Commands.AddCommand(varname, () => SendKeys.SendWait(varname));
                    //Console.WriteLine(varname);
                }
            }
        }


    }
}
