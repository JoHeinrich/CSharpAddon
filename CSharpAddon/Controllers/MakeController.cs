using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VoiceControl;
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

        public static Func<string,List<string>> SelectTypeFinder(IVaraibles x)
        {
            RegexForCSharp regex = new RegexForCSharp();

            if (x.IsAvailable("type"))
            {
                string type = x.Get("type");

                switch (type)
                {
                    case "function":
                        return regex.FindFunctionDefinitions;
    
                    case "class":
                        return regex.FindDefinitions;
  
                    default:
                        return regex.FindAccessModifiable;
  
                }
            }
            return regex.FindAccessModifiable;
        }
        public void Build(ICommandBuilder builder)
        {

            builder.AddCommand("<type,[function|class]><access,CSharpAddon.List.AccessModifiers>", (IVaraibles x) =>
            {
                Func<string, List<string>> finder = regex.FindAccessModifiable; 

                if (x.IsAvailable("type"))
                {
                    string type = x.Get("type");

                    switch (type)
                    {
                        case "function":
                            finder = regex.FindFunctionDefinitions;
                            break;
                        case "class":
                            finder = regex.FindDefinitions;
                            break;
                        default:
                            finder = regex.FindAccessModifiable;
                            break;
                    }
                }
                string access = x.Get("access");

                ChangeAccessModifier(finder, access + " ");
            });

        }
    }
}
