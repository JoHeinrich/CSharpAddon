using System.Windows.Forms;

namespace VoiceControl
{
    public class DefineController : INamedCommandController
    {
        public string Name => "Define";

        public void Build(IBuilder builder)
        {
            builder.Commands.AddStringCommand("Small text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatVariable(v)));
            builder.Commands.AddStringCommand("big text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)));
            builder.Commands.AddStringCommand("class text(en-us)", v => SendKeys.SendWait("class "+TextFomatter.FormatClass(v)+ "{{}{enter}"));
            builder.Commands.AddStringCommand("Local text(en-us)", v => SendKeys.SendWait("var " + TextFomatter.FormatVariable(v)+"="));
            builder.Commands.AddStringCommand("function text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)+ "{(}{)}{{}{enter}"));

        }
  
    }
}
