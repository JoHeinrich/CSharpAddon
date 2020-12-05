using System.Collections.Generic;

namespace VoiceControl
{
    public class ProjectInformationManager
    {
        const string path = @"C:\Users\laise\Documents\Visual Studio Projects\VoiceControl";
        private static ProjectInformation active;
        static Dictionary<string, ProjectInformation> projectInformations = new Dictionary<string, ProjectInformation>();
        public static ProjectInformation Get(string path)
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
}
