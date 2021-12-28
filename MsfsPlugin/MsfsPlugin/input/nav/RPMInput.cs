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
        public RPMInput() : base("RPM/N1", "Display RPM for or N1", "Misc") { }

        protected override void ChangeValue() { }
        protected override String GetValue() {
            String result = "";
            if (MsfsData.Instance.Rpm != 0)
            {
                result = "RPM\n" + MsfsData.Instance.Rpm.ToString();
            } else
            {
                result = "E1 " + MsfsData.Instance.E1N1.ToString() + "\nE2 " + MsfsData.Instance.E2N1.ToString() + "\nE3 " + MsfsData.Instance.E3N1.ToString() + "\nE4 " + MsfsData.Instance.E4N1.ToString();
            }
            return result;
        }
    }
}

