using System.Linq;

namespace VoiceControl
{
    public class UsingsController : ProjectInformationController, IListController
    {
        public UsingsController(IValueCollection globalState) : base(globalState)
        {

        }

        public void Build(IListBuilder builder)
        {
            builder.Add(Information?.UsedNamespaces.Select(x=>x.Split('.').Last()));
        }


    }
}
