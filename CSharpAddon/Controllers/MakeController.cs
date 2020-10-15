using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoiceControl
{
    public class MakeController : ICommandController
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
        public void Build(ICommandBuilder builder)
        {

            builder.AddCommand("Function Public", () => ChangeAccessModifier(regex.FindFunctionDefinitions,"public "));
            builder.AddCommand("Function private", () => ChangeAccessModifier(regex.FindFunctionDefinitions, "private "));
            builder.AddCommand("Function protected", () => ChangeAccessModifier(regex.FindFunctionDefinitions, "protected "));
            builder.AddCommand("class public", () => ChangeAccessModifier(regex.FindClasseDefinitions, "public "));
            builder.AddCommand("class private", () => ChangeAccessModifier(regex.FindClasseDefinitions, "private "));
            builder.AddCommand("class protected", () => ChangeAccessModifier(regex.FindClasseDefinitions, "protected "));
        }
    }
}
