namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;
    class SpoilerEncoder : DefaultEncoder
    {
        public SpoilerEncoder() : base("Spoiler", "Spoiler position", "Nav", true, -1, 10, 1) { }
        protected override String GetDisplayValue() => this.GetValue() == -1 ? "Arm" : this.GetValue().ToString();
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => MsfsData.Instance.CurrentSpoiler;
        protected override void SetValue(Int64 newValue) => MsfsData.Instance.CurrentSpoiler = (Int32)newValue;
    }
}
