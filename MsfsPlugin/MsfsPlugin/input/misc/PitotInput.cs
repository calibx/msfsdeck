namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PitotInput : DefaultInput
    {
        public PitotInput() : base("Pitot", "Pitot heating", "Misc") { }
        protected override String GetValue() => MsfsData.Instance.CurrentPitot ? "Disable Pitot" : "Enable Pitot";
        protected override void ChangeValue() => MsfsData.Instance.CurrentPitot = !MsfsData.Instance.CurrentPitot;
    }
}

