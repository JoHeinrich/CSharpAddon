using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace VoiceControl
{
    public class ProjectRegularExpressionInformation : IProjectInformation
    {
        private string path;
        const string fileType = "*.cs";
        RegexForCSharp regexForCSharp = new RegexForCSharp();
        Dictionary<string, string> fileData = new Dictionary<string, string>();
        ProjectWatcher projectWatcher;
        public ProjectRegularExpressionInformation(string path)
        {
            this.path = Path.GetDirectoryName(path);
            projectWatcher = new ProjectWatcher(new HashSet<string>(Files.Select(x => Path.GetDirectoryName(x))), fileType);
            projectWatcher.Changed += x => Changed?.Invoke();


        }


        public event Action Changed;

        public List<string> Files => Directory.EnumerateFiles(path, fileType, SearchOption.AllDirectories).Where(x => !(x.Contains("obj") || x.Contains(".vs"))).ToList();
        public List<string> FileNames => Files.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
        public string GetFileData(string path)
        {
            if (fileData.TryGetValue(path, out string value)) return value;
            try
            {
                string data = File.ReadAllText(path);
                fileData[path] = data;
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }



        }

        public HashSet<string> ForEachFile(Func<string, List<string>> func)
        {
            List<string> found = new List<string>();
            Files.ForEach(file =>
            {
                found.AddRange(func(File.ReadAllText(file)));
            });
            return new HashSet<string>(found);
        }
        public IEnumerable<string> Variables => ForEachFile(regexForCSharp.FindVariables);
        public IEnumerable<string> Classes => ForEachFile(regexForCSharp.FindClasseDefinitions);
        public IEnumerable<string> Functions => ForEachFile(regexForCSharp.FindFunctionDefinitions);
        public IEnumerable<string> UsedTypes => ForEachFile(regexForCSharp.FindUsedTypes);

        public IEnumerable<string> DefinedClasses { get; }
        public IEnumerable<string> DefinedFunctions { get; }
        public IEnumerable<string> DefinedVariables { get; }
        public IEnumerable<string> UsedFunctions { get; }
        public IEnumerable<string> UsedGenerics { get; }
        public IEnumerable<string> UsedMembers { get; }
    }
}
