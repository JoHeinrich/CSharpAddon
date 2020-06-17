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
   
    public class VisualStudioSolutionPath : IInformationSource
    {
        //VoiceControl - Microsoft Visual Studio
        //"Voice Control (Running) - Microsoft Visual Studio "

        public string Name { get => "VisualStudioSolutionPath"; }
        public string Value
        {
            get
            {
                var title = WindowInformation.GetActiveWindow();
                List<string> projectDirectories = new List<string>
                {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Visual Studio Projects"),
                    //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"VoiceControl\Addons"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"EasyVoiceCode\Addons")
                };
                var match = Regex.Match(title, @"((\w| )+)(\(\w+\))* - Microsoft Visual Studio");
                if (!match.Success) return string.Empty;
                var solutionName = match.Groups[1].Value.Trim();
                var withExtension = solutionName + ".sln";
                foreach (var path in projectDirectories)
                {

                    foreach (var project in Directory.EnumerateFiles(path, withExtension, SearchOption.AllDirectories))
                    {
                        return project;
                    }

                }

                return string.Empty;
            }

        }
    }
}
