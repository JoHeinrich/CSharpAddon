namespace VoiceControl
{
    public class FindController : ICommandController
    {

        public void Build(ICommandBuilder builder)
        {
            RegexForCSharp regex = new RegexForCSharp();
            LineFinder lineFine = new LineFinder();
            builder.AddCommand("Function", () => lineFine.FindLine(regex.FindFunctionDefinitions));
            builder.AddCommand("class", () => lineFine.FindLine(regex.FindClassDefinitions));
        }

    }
}
