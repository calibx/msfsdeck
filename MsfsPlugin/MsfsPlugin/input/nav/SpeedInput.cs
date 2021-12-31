namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class SpeedInput : DefaultInput
    {
        public SpeedInput() : base("Speed", "Display current and AP speed", "Nav") { }
        protected override String GetValue() => "Speed\n" + MsfsData.Instance.CurrentSpeed + "\n[" + MsfsData.Instance.CurrentAPSpeed + "]";
        protected override void ChangeValue() => MsfsData.Instance.CurrentAPSpeed = (Int32)(Math.Round(MsfsData.Instance.CurrentSpeed / 100d, 0) * 100);
    }
}

