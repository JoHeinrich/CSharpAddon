using System.Linq;

namespace VoiceControl
{
    public class AttributesController : ProjectInformationController, IListController
    {
        public AttributesController(IValueCollection globalState) : base(globalState)
        {

        }

        public void Build(IListBuilder builder)
        {
            builder.Add(Information?.UsedAttributes.Select(x => x.Split('.').Last()));
        }
    }
}
