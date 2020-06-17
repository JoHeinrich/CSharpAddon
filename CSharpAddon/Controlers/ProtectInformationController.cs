using System;

namespace VoiceControl
{
    public abstract class ProtectInformationController:  IDoesChange, IUsesGlobalState
    {
        IValueCollection globalState;
        public IValueCollection GlobalState
        {
            get => globalState;
            set { globalState = value; GlobalState.Changed += GlobalState_Changed; }
        }

        string currentPath;
        ProjectInformation projectInformation;
        public ProjectInformation Information => projectInformation;



        public event Action<object> Changed;
        private void ProjectInformation_Changed()
        {
            Changed?.Invoke(this);
        }

        private void GlobalState_Changed(string arg1, string path)
        {
            if (arg1 != "VisualStudioSolutionPath" || path == "") return;
            if (path == currentPath) return;
            currentPath = path;
            if (projectInformation != null)
            {
                projectInformation.Changed -= ProjectInformation_Changed;
            }
            try
            {
                projectInformation = ProjectInformationManager.Get(currentPath);
                projectInformation.Changed += ProjectInformation_Changed;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            Changed?.Invoke(this);
        }

       
    }
}
