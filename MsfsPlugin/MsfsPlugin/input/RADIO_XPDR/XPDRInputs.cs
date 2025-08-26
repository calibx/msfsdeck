namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;  // DefaultInput
    using Loupedeck.MsfsPlugin.tools;  // MobiSend, XpdrUi
    using System;

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

    // IDENT
    class XpdrIdent : DefaultInput
    {
        public XpdrIdent() : base("XPDR IDENT", "Transponder IDENT", "RADIO_XPDR") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","XPDR","VC_TRANSPONDER_IDENT_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => XpdrTile.Label(s, "IDENT");
    }

    // Digits 0..7 + CLR
    abstract class XpdrKeyBase : DefaultInput
    {
        private readonly string evt;
        private readonly char? digit; // null for CLR
        private readonly string label;

        protected XpdrKeyBase(string label, string evt, char? digit)
            : base($"XPDR {label}", "Transponder Key", "RADIO_XPDR")
        { this.label = label; this.evt = evt; this.digit = digit; }

        protected override void ChangeValue()
        {
            if (digit.HasValue) XpdrUi.Push(digit.Value); else XpdrUi.Clear();
            MobiSend.Send("IFLY","737MAX","XPDR", evt);
        }

        protected override BitmapImage GetImage(PluginImageSize s) => XpdrTile.Label(s, label);
    }

    class XpdrK0 : XpdrKeyBase { public XpdrK0() : base("0","VC_TRANSPONDER_KB_0_SW",'0'){} }
    class XpdrK1 : XpdrKeyBase { public XpdrK1() : base("1","VC_TRANSPONDER_KB_1_SW",'1'){} }
    class XpdrK2 : XpdrKeyBase { public XpdrK2() : base("2","VC_TRANSPONDER_KB_2_SW",'2'){} }
    class XpdrK3 : XpdrKeyBase { public XpdrK3() : base("3","VC_TRANSPONDER_KB_3_SW",'3'){} }
    class XpdrK4 : XpdrKeyBase { public XpdrK4() : base("4","VC_TRANSPONDER_KB_4_SW",'4'){} }
    class XpdrK5 : XpdrKeyBase { public XpdrK5() : base("5","VC_TRANSPONDER_KB_5_SW",'5'){} }
    class XpdrK6 : XpdrKeyBase { public XpdrK6() : base("6","VC_TRANSPONDER_KB_6_SW",'6'){} }
    class XpdrK7 : XpdrKeyBase { public XpdrK7() : base("7","VC_TRANSPONDER_KB_7_SW",'7'){} }
    class XpdrClr: XpdrKeyBase { public XpdrClr(): base("CLR","VC_TRANSPONDER_KB_CLR_SW", null){} }
}
