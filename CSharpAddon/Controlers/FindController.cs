namespace VoiceControl
{
    public class FindController : INamedCommandController
    {
        public string Name => "Find";




        public void Build(IBuilder builder)
        {
            RegexForCSharp regex = new RegexForCSharp();
            LineFinder lineFine = new LineFinder();
            builder.Commands.AddCommand("Function", () => lineFine.FindLine(regex.FindFunctionDefinitions));
            builder.Commands.AddCommand("class", () => lineFine.FindLine(regex.FindClasseDefinitions));
        }

    }
}
