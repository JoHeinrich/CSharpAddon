﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoiceControl
{
    public class GenericsController : ProtectInformationController, IListController
    {
        public GenericsController(IValueCollection globalState) : base(globalState)
        {
        }


        public void Build(IListBuilder builder)
        {
            builder.Add(Information?.UsedGenerics);
        }
    }

    public class GenericController : ProtectInformationController, ICommandController
    {
        public GenericController(IValueCollection globalState) : base(globalState)
        {
        }

        public void Build(ICommandBuilder builder)
        {
            builder.AddCommand("<g,CSharpAddon.List.Generics>of<t,CSharpAddon.List.Types>", x => SendKeys.SendWait(x.Get(0) + "<" + x.Get(1) + ">"));
        }


    }
}
