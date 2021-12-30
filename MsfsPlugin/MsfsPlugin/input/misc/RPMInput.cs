namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class RPMInput : DefaultInput
    {
        public RPMInput() : base("RPM/N1", "Display RPM for or N1", "Misc") { }
        protected override void ChangeValue() { }
        protected override String GetValue()
        {
            var result = MsfsData.Instance.Rpm != 0
                ? "RPM\n" + MsfsData.Instance.Rpm.ToString()
                : MsfsData.Instance.NumberOfEngines == 4
                    ? MsfsData.Instance.E1N1.ToString() + "\n" + MsfsData.Instance.E2N1.ToString() + "\n" + MsfsData.Instance.E3N1.ToString() + "\n" + MsfsData.Instance.E4N1.ToString()
                    : MsfsData.Instance.NumberOfEngines == 2
                                    ? MsfsData.Instance.E1N1.ToString() + "\n" + MsfsData.Instance.E2N1.ToString()
                                    : MsfsData.Instance.E1N1.ToString();
            return result;
        }
    }
}

