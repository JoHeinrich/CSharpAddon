using System.Collections.Generic;

namespace VoiceControl
{
    public class AccessModifiers : IListController
    {
        public void Build(IListBuilder builder)
        {
            builder.Add(new List<string> { "public", "private", "protected", "internal" });
        }
    }
}
