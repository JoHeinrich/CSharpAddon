using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VoiceControl;

namespace CSharpAddon
{
    public class Settings
    {
        private readonly ISettings settings;
        private readonly IPaths paths;
        ISetting<List<string>> projectDirectories;
        public List<string> ProjectDirectories => projectDirectories.Value;

        public Settings(ISettings settings, IPaths paths)
        {
            this.settings = settings;
            this.paths = paths;
            List<string> defaultprojectDirectories = new List<string>
                {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual Studio Projects"),
                    paths.GetPath("Addons"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "source","repos")
                };
            projectDirectories = settings.Create("SolutionDirectories", defaultprojectDirectories);

        }
    }
    public class Solutions : IListController
    {
        private readonly Settings settings;

        public Solutions(Settings settings)
        {
            this.settings = settings;
        }

        public Dictionary<string, string> Available
        {
            get
            {
                Dictionary<string, string> found = new Dictionary<string, string>();
                var extension = "*.sln";
                foreach (var path in settings.ProjectDirectories)
                {
                    foreach (var solutionPath in Directory.EnumerateFiles(path, extension, SearchOption.AllDirectories))
                    {
                        var solutionName = Path.GetFileNameWithoutExtension(solutionPath);
                        found[solutionName] = solutionPath;
                    }
                }
                return found;
            }
        }
        public List<string> Build()
        {
            return Available.Keys.ToList();
        }

        public void Build(IListBuilder builder)
        {
            builder.Add(Available.Keys);
        }
    }
    public class VisualStudioSolutionPath : IInformationSource
    {
        private readonly Solutions solutions;

        //VoiceControl - Microsoft Visual Studio
        //"Voice Control (Running) - Microsoft Visual Studio "

        public VisualStudioSolutionPath(Solutions solutions)
        {
            this.solutions = solutions;
        }
        public string Name => "VisualStudioSolutionPath";
        public string Value
        {
            get
            {
                var title = WindowInformation.GetActiveWindow();

                var match = Regex.Match(title, @"((\w| )+)(\((\w| )+\))* - Microsoft Visual Studio");
                if (!match.Success) return string.Empty;
                var solutionName = match.Groups[1].Value.Trim();
                if (solutions.Available.ContainsKey(solutionName))
                    return solutions.Available[solutionName];

                return string.Empty;
            }

        }
    }
}
