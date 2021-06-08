using System.Collections.Generic;
using System.IO;
using System;
using VoiceControl;
using System.Threading.Tasks;

namespace VoiceControl
{
    public class ProjectWatcher
    {
        public event Action<string> Changed;

        public ProjectWatcher(IEnumerable<string> paths, string fileType)
        {
            foreach (var path in paths)
            {
                ObservePath(path, fileType);
            }
        }
        public void ObservePath(string path, string fileType)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;
            watcher.Filter = fileType;
            watcher.Changed += Watcher_Changed;
            watcher.Created += Watcher_Changed;
            watcher.Deleted += Watcher_Changed;
            watcher.Renamed += Watcher_Changed;
            
            watcher.EnableRaisingEvents = true;
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Task.Run(() =>
            {
                Console.WriteLine(e.FullPath);
                Changed?.Invoke(e.FullPath);
            });
        }
    }
}
