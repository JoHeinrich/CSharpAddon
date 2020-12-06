using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class VariableController : ProjectInformationController, ICommandController
    {
        public VariableController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {

            if (Information != null)
            {
                foreach (var varname in Information.Variables/*.OrderBy(x=>x)*/)
                {
                    builder.AddCommand(varname, () => SendKeys.SendWait(varname));
                }
            }
        }


    }
}
