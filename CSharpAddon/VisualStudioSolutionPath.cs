using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoiceControl;

namespace CSharpAddon
{
    public class Settings
    {
        private readonly ISettings settings;
        private readonly IPaths paths;
        ISetting<List<string>> projectDirectories;
        public List<string> ProjectDirectories => projectDirectories.Value;
        public void AddDirectory(string path)
        {
            ProjectDirectories.Add(path);
            projectDirectories.Value = ProjectDirectories;
        }
        public Settings(ISettings settings, IPaths paths)
        {
            this.settings = settings;
            this.paths = paths;
            List<string> defaultprojectDirectories = new List<string>
                {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual Studio Projects"),
                    paths.GetPath("Addons"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "source","repos")
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
                    if(Directory.Exists(path))
                    {
                        foreach (var solutionPath in Directory.EnumerateFiles(path, extension, SearchOption.AllDirectories))
                        {
                            var solutionName = Path.GetFileNameWithoutExtension(solutionPath);
                            found[solutionName] = solutionPath;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Solutionsearch directory does not exist: "+path);
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
    public class NotInPath : ICheckSolve
    {
        ISettings Settings;
        IPaths paths;
        public string SolutionName { get; set; }

        public NotInPath(ISettings settings, IPaths paths)
        {
            Settings = settings;
            this.paths = paths;
        }

        public string Error => $"Could not find {SolutionName} solution in directory";

        public string AvailableAction => "click to locate solution";

        public bool Check()
        {
            var info = new PathFinder(new Solutions(new Settings(Settings, paths)));
            return !string.IsNullOrEmpty(info.FindPath()) ;
        }

        public string OpenDialog()
        {
            string selectedPath = string.Empty;
            var t = new Thread((ThreadStart)(() => {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    InitialDirectory = @"C:\",
                    Title = $"Locate {SolutionName} Solution",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "sln",
                    Filter = "Solution (*.sln)|*.sln",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true

                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = dialog.FileName;
                }
            }));

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            return selectedPath;

        }
        public string Solve()
        {
            var dir = OpenDialog();
            if (string.IsNullOrEmpty(dir)) return string.Empty;
            new Settings(Settings, paths).AddDirectory(Path.GetDirectoryName(dir));
            return string.Empty;
        }
    }
    public class PathFinder
    {
        private readonly Solutions solutions;

        public PathFinder(Solutions solutions)
        {
            this.solutions = solutions;

        }

        public string SolutionName
        {
            get
            {
                var title = WindowInformation.GetActiveWindow();

                var match = Regex.Match(title, @"([^\(]+)(\((.)+\))* - Microsoft Visual Studio");
                if (!match.Success) return string.Empty;
                var solutionName = match.Groups[1].Value.Trim();
                return solutionName;
            }
        }

        public string FindPath()
        {
            var solutionName = SolutionName;
            if (solutions.Available.ContainsKey(solutionName))
            {
                return solutions.Available[solutionName];
            }
            else
            {
  
            }
            return string.Empty;
        }
    }
    public class VisualStudioSolutionPath : IInformationSource
    {
        private readonly PathFinder pathFinder;
        NotInPath notInPath;
        //VoiceControl - Microsoft Visual Studio
        //"Voice Control (Running) - Microsoft Visual Studio "
        bool firtShow = true;
        public VisualStudioSolutionPath(PathFinder pathFinder, NotInPath notInPath)
        {
            this.pathFinder = pathFinder;
            this.notInPath = notInPath;
        }
        public string Name => "VisualStudioSolutionPath";
        public string Value
        {
            get
            {
                var path = pathFinder.FindPath();
                var title = WindowInformation.GetActiveWindow();
                if (string.IsNullOrEmpty(path) && title.Contains("Microsoft Visual Studio")&& firtShow)
                {
                    notInPath.SolutionName = pathFinder.SolutionName;
                    //Task.Run(notInPath.Solve);
                    firtShow = false;
                }
                return path;
            }

        }
    }
}
