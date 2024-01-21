namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    class DebugInput : DefaultInput
    {
        public DebugInput() : base("Debug", "Display debugged value ", "Debug") { }
        protected override string GetValue() => "Debug\n" + MsfsData.Instance.DebugValue1 + "\n" + MsfsData.Instance.DebugValue2 + "\n" + MsfsData.Instance.DebugValue3;
        protected override void ChangeValue() => MsfsData.Instance.DEBUG = !MsfsData.Instance.DEBUG;
    }
}

