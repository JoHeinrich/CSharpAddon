using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceControl
{
    public class MakeController : INamedCommandController
    {
        public string Name => "Make";

        RegexForCSharp regex = new RegexForCSharp();


        public void ChangeAccessModifier(Func<string, List<string>> action,string value)
        {
            LineFinder lineFine = new LineFinder();

            if (lineFine.FindLine(action))
            {
                if (!lineFine.FindWord(regex.AccessModifier))
                {
                    SendKeys.SendWait("{left}");
                    SendKeys.SendWait("{HOME}");

                }
                
                SendKeys.SendWait(value);

            }

        }
        public void Build(IBuilder builder)
        {

            builder.Commands.AddCommand("Function Public", () => ChangeAccessModifier(regex.FindFunctionDefinitions,"public "));
            builder.Commands.AddCommand("Function private", () => ChangeAccessModifier(regex.FindFunctionDefinitions, "private "));
            builder.Commands.AddCommand("Function protected", () => ChangeAccessModifier(regex.FindFunctionDefinitions, "protected "));
            builder.Commands.AddCommand("class public", () => ChangeAccessModifier(regex.FindClasseDefinitions, "public "));
            builder.Commands.AddCommand("class private", () => ChangeAccessModifier(regex.FindClasseDefinitions, "private "));
            builder.Commands.AddCommand("class protected", () => ChangeAccessModifier(regex.FindClasseDefinitions, "protected "));
        }
    }
}
