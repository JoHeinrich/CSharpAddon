using System.Windows.Forms;

namespace VoiceControl
{
    public class DefineController : ICommandController
    {
        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("Small text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatVariable(v)));
            builder.AddCommand("big text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)));
            builder.AddCommand("class text(en-us)", v => SendKeys.SendWait("class " + TextFomatter.FormatClass(v) + "{{}{enter}"));
            builder.AddCommand("Local text(en-us)", v => SendKeys.SendWait("var " + TextFomatter.FormatVariable(v) + "="));
            //builder.AddCommand("function text(en-us)", v => SendKeys.SendWait(TextFomatter.FormatClass(v)+ "{(}{)}{{}{enter}"));

            builder.AddCommand("function(<CSharpAddon.Command.Type>)text()", v => SendKeys.SendWait(TextFomatter.FormatClass(v) + "{(}{)}{{}{enter}"));

            builder.AddCommand("<a,CSharpAddon.List.AccessModifiers>class text()", x => SendKeys.SendWait(x.Get(0) + " class " + TextFomatter.FormatClass(x.Get(1)) + "{{}{enter}"));

            //builder.AddCommand("<a,CSharpAddon.List.AccessModifiers>class text()", (i, x) => SendKeys.SendWait(x[0] + " class " + TextFomatter.FormatClass(x[1]) + "{{}{enter}"));
            //builder.AddCommand("<a,CSharpAddon.List.AccessModifiers>class text() with <t,CSharpAddon.List.Types>", (i, x) => SendKeys.SendWait(x[0] + " class " + TextFomatter.FormatClass(x[1]) + ":" + x[2] + "{{}{enter}"));

        }

    }
}
