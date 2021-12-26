namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.MsfsPlugin.input;

    class RPMInput : DefaultInput
    {
        public RPMInput() : base("RPM", "Display RPM percent", "Misc") { }

        protected override void ChangeValue() { }
        protected override String GetValue() => MsfsData.Instance.Rpm.ToString() + " % RPM";
    }
}

