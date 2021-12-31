namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class HeadingInput : DefaultInput
    {
        public HeadingInput() : base("Heading", "Display current and AP heading", "Nav") { }
        protected override String GetValue() => "Head\n" + MsfsData.Instance.CurrentHeading + "\n[" + MsfsData.Instance.CurrentAPHeading + "]";
        protected override void ChangeValue() => MsfsData.Instance.CurrentAPHeading = MsfsData.Instance.CurrentHeading;

    }
}

