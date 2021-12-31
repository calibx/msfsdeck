namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.input;
    class DebugInput : DefaultInput
    {

        public DebugInput() : base("Debug", "Display debugged value ", "Debug"){}
        protected override String GetValue() => "Debug\n" + MsfsData.Instance.DebugValue;

    }
}

