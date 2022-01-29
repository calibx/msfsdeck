namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class PauseInput : DefaultInput
    {
        public PauseInput() : base("Pause", "Pause", "Misc") { }
        protected override String GetValue() => MsfsData.Instance.Pause ? "Paused" : "Pause";
        protected override void ChangeValue() => MsfsData.Instance.Pause = !MsfsData.Instance.Pause;
    }
}

