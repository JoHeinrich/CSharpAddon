using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace VoiceControl
{
    public class ProjectInformationManager
    {
        const string path = @"C:\Users\laise\Documents\Visual Studio Projects\VoiceControl";
        private static ProjectInformation active;
        static Dictionary<string, ProjectInformation> projectInformations = new Dictionary<string, ProjectInformation>();
        public static  ProjectInformation Get (string path)
        {
            if (!projectInformations.ContainsKey(path))
            {
                projectInformations[path] = new ProjectInformation(path);
            }
            return projectInformations[path];
        }
        public static ProjectInformation Active
        {
            get
            {
                if (active == null) active = new ProjectInformation(path);
                return active;
            }
        }
    }
    public class ProjectWatcher
    {
        public event Action Changed;

        public ProjectWatcher(IEnumerable <string >paths, string fileType)
        {
            foreach (var path in paths)
            {
                ObservePath(path,  fileType); 
            }
        }
        public void ObservePath(string path,string fileType)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;
            watcher.Filter = fileType;
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Changed;
            watcher.Deleted += Watcher_Changed;
            watcher.Renamed+= Watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.FullPath);
            Changed?.Invoke();
        }
    }
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
            projectWatcher.Changed += () => Changed?.Invoke();


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
    public class ProjectInformation : IProjectInformation
    {
        private string path;
        const string fileType = "*.cs";
        RegexForCSharp regexForCSharp = new RegexForCSharp();
        Dictionary<string, IFileInformation> fileData = new Dictionary<string, IFileInformation>();
        ProjectWatcher projectWatcher;
        public ProjectInformation(string path)
        {
            this.path = Path.GetDirectoryName(path);
            projectWatcher = new ProjectWatcher(new HashSet<string>(Files.Select(x => Path.GetDirectoryName(x))), fileType);
            projectWatcher.Changed += () => Changed?.Invoke();


        }


        public event Action Changed;

        public List<string> Files => Directory.EnumerateFiles(path, fileType, SearchOption.AllDirectories).Where(x => !(x.Contains("obj") || x.Contains(".vs"))).ToList();
        public List<string> FileNames => Files.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
        public IFileInformation GetFileData(string path)
        {
            lock (fileData)
            {

                if (fileData.TryGetValue(path, out IFileInformation value)) return value;
                try
                {
                    string data = File.ReadAllText(path);
                    IFileInformation fileInformation = new FileInformation(path);
                    fileData[path] = fileInformation;
                    return fileInformation;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not load file " + path);
                    return null;
                }

            }


        }

        public HashSet<string> ForEachFile(Func<IFileInformation, IEnumerable<string>> func)
        {
            List<string> found = new List<string>();
            Files.ForEach(file =>
            {
                var data = GetFileData(file);
                if (data != null )found.AddRange(func(data));
            });
            return new HashSet<string>(found);
        }
        public IEnumerable<string> Variables => ForEachFile(x=>x.DefinedVariables);
        public IEnumerable<string> Classes => ForEachFile(x => x.DefinedClasses);
        public IEnumerable<string> Functions => ForEachFile(x => x.DefinedFunctions);
        public IEnumerable<string> UsedTypes => ForEachFile(x => x.UsedTypes);

        public IEnumerable<string> DefinedClasses { get; }
        public IEnumerable<string> DefinedFunctions { get; }
        public IEnumerable<string> DefinedVariables { get; }
        public IEnumerable<string> UsedFunctions { get; }
        public IEnumerable<string> UsedGenerics => ForEachFile(x => x.UsedGenerics);
        public IEnumerable<string> UsedMembers => ForEachFile(x => x.UsedMembers);
    }
}
