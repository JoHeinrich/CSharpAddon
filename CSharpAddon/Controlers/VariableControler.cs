using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class VariableController : ProtectInformationController, INamedCommandController
    {
        public string Name => "variable";

        public void Build(IBuilder builder)
        {

            if (Information != null)
            {
                foreach (var varname in Information.Variables/*.OrderBy(x=>x)*/)
                {
                    builder.Commands.AddCommand(varname, () => SendKeys.SendWait(varname));
                    //Console.WriteLine(varname);
                }
            }
        }


    }
}
