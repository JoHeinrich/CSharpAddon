namespace VoiceControl
{
    public class MembersController : ProjectInformationController, IListController
    {
        public MembersController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(IListBuilder builder)
        {
            builder.Add(Information?.UsedMembers);
        }
    }
}
