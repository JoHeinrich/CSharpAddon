using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceControl
{

    public class MemberController : ProtectInformationController, ICommandController
    {
        public MemberController(IValueCollection globalState) : base(globalState)
        {
        }
        
        public void Build(ICommandBuilder builder)
        {
            if (Information != null)
            {
                foreach (var varname in Information.UsedMembers/*.OrderBy(x=>x)*/)
                {
                    builder.AddCommand(varname, () => SendKeys.SendWait(varname));
                }
            }
        }

    }
}
