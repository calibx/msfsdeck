namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class HeadingEncoder : DefaultEncoder
    {

        public HeadingEncoder() : base("Head", "heading of the AP", "Nav", true, 0, 360, 1) { }
        protected override String GetDisplayValue() => "[" + this.GetValue().ToString() + "]\n" + MsfsData.Instance.CurrentHeading;
        protected override void RunCommand(String actionParameter) => this.SetValue(MsfsData.Instance.CurrentHeading);
        protected override Int32 GetValue() => MsfsData.Instance.CurrentAPHeading;
        protected override Int32 SetValue(Int32 newValue) => MsfsData.Instance.CurrentAPHeading = newValue;
    }
}
