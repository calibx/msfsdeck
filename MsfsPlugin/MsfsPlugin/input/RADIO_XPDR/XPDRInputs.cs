// File: MsfsPlugin/MsfsPlugin/input/RADIO_XPDR/XPDRInputs.cs
// Transponder keypad + IDENT via MobiFlight presets (if available). No encoder; direct buttons.

namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;  // DefaultInput
    using Loupedeck.MsfsPlugin.tools;  // BitmapBuilder, MobiSend

    internal static class XpdrPresets
    {
        public const string KB0   = "IFLY_737MAX_XPDR_KB_0";
        public const string KB1   = "IFLY_737MAX_XPDR_KB_1";
        public const string KB2   = "IFLY_737MAX_XPDR_KB_2";
        public const string KB3   = "IFLY_737MAX_XPDR_KB_3";
        public const string KB4   = "IFLY_737MAX_XPDR_KB_4";
        public const string KB5   = "IFLY_737MAX_XPDR_KB_5";
        public const string KB6   = "IFLY_737MAX_XPDR_KB_6";
        public const string KB7   = "IFLY_737MAX_XPDR_KB_7";
        public const string KB8   = "IFLY_737MAX_XPDR_KB_8";
        public const string KB9   = "IFLY_737MAX_XPDR_KB_9";
        public const string KBCLR = "IFLY_737MAX_XPDR_KB_CLR";
        public const string IDENT = "IFLY_737MAX_XPDR_IDENT";
    }

    internal static class XpdrTile
    {
        public static BitmapImage Label(PluginImageSize size, string text)
        {
            using (var b = new BitmapBuilder(size))
            {
                b.DrawText(text);
                return b.ToImage();
            }
        }
    }

    abstract class XpdrKeyBase : DefaultInput
    {
        private readonly string _label; private readonly string _preset;
        protected XpdrKeyBase(string label, string preset)
            : base($"XPDR {label}", "Transponder Key", "RADIO_XPDR")
        { _label = label; _preset = preset; }

        protected override void ChangeValue() => MobiSend.Send(_preset);
        protected override BitmapImage GetImage(PluginImageSize s) => XpdrTile.Label(s, _label);
    }

    class XpdrK0 : XpdrKeyBase { public XpdrK0() : base("0", XpdrPresets.KB0) { } }
    class XpdrK1 : XpdrKeyBase { public XpdrK1() : base("1", XpdrPresets.KB1) { } }
    class XpdrK2 : XpdrKeyBase { public XpdrK2() : base("2", XpdrPresets.KB2) { } }
    class XpdrK3 : XpdrKeyBase { public XpdrK3() : base("3", XpdrPresets.KB3) { } }
    class XpdrK4 : XpdrKeyBase { public XpdrK4() : base("4", XpdrPresets.KB4) { } }
    class XpdrK5 : XpdrKeyBase { public XpdrK5() : base("5", XpdrPresets.KB5) { } }
    class XpdrK6 : XpdrKeyBase { public XpdrK6() : base("6", XpdrPresets.KB6) { } }
    class XpdrK7 : XpdrKeyBase { public XpdrK7() : base("7", XpdrPresets.KB7) { } }
    class XpdrK8 : XpdrKeyBase { public XpdrK8() : base("8", XpdrPresets.KB8) { } }
    class XpdrK9 : XpdrKeyBase { public XpdrK9() : base("9", XpdrPresets.KB9) { } }
    class XpdrClr : XpdrKeyBase { public XpdrClr() : base("CLR", XpdrPresets.KBCLR) { } }
    class XpdrIdent : XpdrKeyBase { public XpdrIdent() : base("IDENT", XpdrPresets.IDENT) { } }
}
