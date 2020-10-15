using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VoiceControl
{
    public class RegexForCSharp
    {

        public HashSet<string> Find(string input, string pattern)
        {
            HashSet<string> found = new HashSet<string>();

            var match = Regex.Match(input, pattern);
            while (match.Success)
            {
                var varname = match.Groups[1].Value;
                found.Add(varname);
                match = match.NextMatch();
            }
            return found;
        }

        public HashSet<string> Find(string input, string pattern, string group)
        {
            HashSet<string> found = new HashSet<string>();
            var groupPattern = pattern.Replace(group, "(" + group + ")");
            var match = Regex.Match(input, groupPattern);
            while (match.Success)
            {
                var varname = match.Groups[1].Value;
                found.Add(varname);
                match = match.NextMatch();
            }
            return found;
        }

        public List<string> FindVariables(string input)
        {
            var normalDefinition = Find(input, "(?!using|return)[ |{(}][a-zA-Z<>]+ ([a-zA-Z1-9]+) *(=|;|in )");
            var arrowDefinition = Find(input, "([a-z1-9]+) *=>");
            var funkDefinition = Find(input, @"[\(|,]" + WhiteSpace + TemplateType + WhiteSpace + MemberName + OptionalWhiteSpace + @"[\)|,]");

            return new List<string>(normalDefinition.Concat(arrowDefinition).Concat(funkDefinition));
        }

        private string MemberName = "[a-zA-Z1-9]+";
        private string VariableName = "[a-z1-9][a-zA-Z1-9]*";
        //private string TypeName = "[a-zA-Z1-9]+";
        private string TemplateType = "[a-zA-Z1-9<>]+";
        private string WhiteSpace = "[\n|\r| ]+";
        private string OptionalWhiteSpace = "[\n|\r| ]*";
        private string ArgumentStart = @"\(";
        private string ArgumentEnd = @"\)";
        private string Repeat(string value) => "(?:" + value + ")*";
        private string Optional(string value) => "(?:" + value + ")?";
        private string SpeciallWordRemover = "(?!using|return|new)";

        private string FunctionParameterDefinition => OptionalWhiteSpace + TemplateType + WhiteSpace + MemberName + OptionalWhiteSpace;
        private string FunctionsArgumentsDefinition => Repeat(FunctionParameterDefinition + "," + OptionalWhiteSpace) + Optional(FunctionParameterDefinition);
        private string FunctionDefinition =>SpeciallWordRemover+ TemplateType+WhiteSpace+ MemberName + OptionalWhiteSpace + ArgumentStart + FunctionsArgumentsDefinition + ArgumentEnd+OptionalWhiteSpace+"[;|{]";


        public List<string> FindUsedTypes(string input)
        {
            var normalDefinition = Find(input, "(?!using|return)[ |{(}]([a-zA-Z<>]+) [a-zA-Z1-9]+ *(=|;|in )");
            var templateInnerDefinition = Find(input, "(?!using|return)[ |{(}]([a-zA-Z<>]+) [a-zA-Z1-9]+ *(=|;|in )");
            //var templateDefinition = Find(input, "(?!using|return)[ |{(}]([a-zA-Z<>]+) [a-zA-Z1-9]+ *(=|;|in )");
            var funkDefinition = Find(input, @"[\(|,][\n|\r| ]*([a-zA-Z<>]+) [a-z1-9]+ *[\)|,]");

            return new List<string>(normalDefinition.Concat(funkDefinition));
        }

        public List<string> FindClasseDefinitions(string input)
        {
            return Find(input, "class ([A-z0-9]+)").ToList();
        }

        public List<string> FindFunctionDefinitions(string input)
        {
            //return Find(input, FunctionDefinition, MemberName).ToList();
            var unfiltered = Find(input, WhiteSpace+SpeciallWordRemover+TemplateType+WhiteSpace+MemberName+OptionalWhiteSpace+ArgumentStart,MemberName);
            return unfiltered/*.Where(x => !x.Contains("return")).Where(x => !x.Contains("new"))*/.ToList();
            //"(?![a-zA-Z<>])+ ([a-z1-9]+) *\("
        }

        public List<string> AccessModifier(string input)
        {
            return Find(input, "(public|private|protected) *").ToList();
        }
    }
}
