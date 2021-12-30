namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Misc", true, -1, 10, 1) { }
        protected override String GetDisplayValue() => this.GetValue() == -1 ? "Auto" : this.GetValue().ToString();
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int32 GetValue() => MsfsData.Instance.CurrentSpoiler;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentSpoiler = newValue;
    }
}
