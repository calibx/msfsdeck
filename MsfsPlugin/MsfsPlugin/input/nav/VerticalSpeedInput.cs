namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class VerticalSpeedInput : DefaultInput
    {
        public VerticalSpeedInput() : base("VS", "Display current and AP vertical speed", "Nav") { }
        protected override String GetValue() => "VS\n" + MsfsData.Instance.CurrentVerticalSpeed + "\n[" + MsfsData.Instance.CurrentAPVerticalSpeedState + "]";
        protected override void ChangeValue() => MsfsData.Instance.CurrentAPVerticalSpeed = (Int16)(Math.Round(MsfsData.Instance.CurrentVerticalSpeed / 100d, 0) * 100);
    }
}

