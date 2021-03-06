﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace VoiceControl
{
    public class ProjectInformation : IProjectInformation
    {
        private string path;
        const string fileType = "*.cs";
        RegexForCSharp regexForCSharp = new RegexForCSharp();
        ConcurrentDictionary<string, IFileInformation> fileData = new ConcurrentDictionary<string, IFileInformation>();
        ProjectWatcher projectWatcher;
        public ProjectInformation(string path)
        {
            this.path = Path.GetDirectoryName(path);
            //projectWatcher = new ProjectWatcher(new HashSet<string>(AllFiles.Select(x => Path.GetDirectoryName(x))), fileType);
            //projectWatcher.Changed += x => { LoadFile(x); Changed?.Invoke(); };

            LoadFileData();
            

            
        }
       

        public event Action Changed;
        public List<string> AllFiles => Directory.EnumerateFiles(path, fileType, SearchOption.AllDirectories).ToList();
        public List<string> Files => AllFiles.Where(x => !(x.Contains("obj") || x.Contains(".vs") || x.Contains("Library"))).ToList();
        public List<string> FileNames => Files.Select(x => Path.GetFileNameWithoutExtension(x)).ToList();


        public void LoadFile(string path)
        {
            try
            {
                fileData[path] = new FileInformation(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void LoadFileData()
        {
            foreach (var file in Files)
            {
                LoadFile(file);
            }
            Changed?.Invoke();
        }

        public IFileInformation GetFileData(string path)
        {
            if (fileData.TryGetValue(path, out IFileInformation value))
                return value;
            return null;
        }

        public HashSet<string> ForEachFile(Func<IFileInformation, IEnumerable<string>> func)
        {
            List<string> found = new List<string>();
            Files.ForEach(file =>
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

        public IEnumerable<string> UsedNamespaces => ForEachFile(x => x.UsedNamespaces);
        public IEnumerable<string> UsedAttributes => ForEachFile(x => x.UsedAttributes);
    }
}
