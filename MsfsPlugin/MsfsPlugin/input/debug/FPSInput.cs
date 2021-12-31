namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;

    class FPSInput : DefaultInput
    {
        public FPSInput() : base("FPS", "Display current FPS", "Debug") { }
        protected override String GetValue() => MsfsData.Instance.Fps + "\nFPS";
    }
}

