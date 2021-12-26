namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.input;

    class MasterInput : DefaultInput
    {
        public MasterInput() : base("Master switch", "Master Switch", "Misc") { }

        protected override String GetValue() => MsfsData.Instance.MasterSwitch ? "Master Off" : "Master On";
        protected override void ChangeValue() => MsfsData.Instance.MasterSwitch = !MsfsData.Instance.MasterSwitch;
    }
}

