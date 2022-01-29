namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class RPMInput : DefaultInput
    {
        public RPMInput() : base("RPM/N1", "Display RPM for or N1", "Misc") { }
        protected override String GetValue()
        {
            String result;
            switch (MsfsData.Instance.EngineType)
            {
                case 0:
                    result = "RPM\n" + MsfsData.Instance.Rpm.ToString();
                    break;
                case 1:
                case 5:
                    result = "N1\n" + (MsfsData.Instance.NumberOfEngines == 4
                    ? MsfsData.Instance.E1N1.ToString() + "\n" + MsfsData.Instance.E2N1.ToString() + "\n" + MsfsData.Instance.E3N1.ToString() + "\n" + MsfsData.Instance.E4N1.ToString()
                    : MsfsData.Instance.NumberOfEngines == 2
                                    ? MsfsData.Instance.E1N1.ToString() + "\n" + MsfsData.Instance.E2N1.ToString()
                                    : MsfsData.Instance.E1N1.ToString());
                    break;
                default:
                    result = "N/A";
                    break;
            }
            return result;
        }
    }
}

