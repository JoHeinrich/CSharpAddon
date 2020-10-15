using System.Windows.Forms;

namespace VoiceControl
{
    public class DefineController : ICommandController
    {
        public string Name => "Define";

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("Small text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatVariable(v)));
            builder.AddCommand("big text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)));
            builder.AddCommand("class text(en-us)", v => SendKeys.SendWait("class "+TextFomatter.FormatClass(v)+ "{{}{enter}"));
            builder.AddCommand("Local text(en-us)", v => SendKeys.SendWait("var " + TextFomatter.FormatVariable(v)+"="));
            builder.AddCommand("function text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)+ "{(}{)}{{}{enter}"));

        }
  
    }
}
