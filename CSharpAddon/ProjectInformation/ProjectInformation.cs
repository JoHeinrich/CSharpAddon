using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace VoiceControl
{
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
            projectWatcher = new ProjectWatcher(new HashSet<string>(AllFiles.Select(x => Path.GetDirectoryName(x))), fileType);
            projectWatcher.Changed += x => { Invalidate(x); Changed?.Invoke(); };


        }


        public event Action Changed;
        public List<string> AllFiles => Directory.EnumerateFiles(path, fileType, SearchOption.AllDirectories).ToList();
        public List<string> Files => AllFiles.Where(x => !(x.Contains("obj") || x.Contains(".vs"))).ToList();
        public List<string> FileNames => Files.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();

        public void Invalidate(string path)
        {
            lock (fileData)
            {
                if (fileData.ContainsKey(path))
                {
                    fileData.Remove(path);
                }
            }
        }
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
            AllFiles.ForEach(file =>
            {
                var data = GetFileData(file);
                if (data != null) found.AddRange(func(data));
            });
            return new HashSet<string>(found);
        }
        public IEnumerable<string> Variables => ForEachFile(x => x.DefinedVariables);
        public IEnumerable<string> Classes => ForEachFile(x => x.DefinedClasses);
        public IEnumerable<string> Functions => ForEachFile(x => x.DefinedFunctions);
        public IEnumerable<string> UsedTypes => ForEachFile(x => x.UsedTypes);

        public IEnumerable<string> DefinedClasses { get; } = new List<string>();
        public IEnumerable<string> DefinedFunctions { get; } = new List<string>();
        public IEnumerable<string> DefinedVariables { get; } = new List<string>();
        public IEnumerable<string> UsedFunctions { get; } = new List<string>();
        public IEnumerable<string> UsedGenerics => ForEachFile(x => x.UsedGenerics);
        public IEnumerable<string> UsedMembers => ForEachFile(x => x.UsedMembers);
    }
}
