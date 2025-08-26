namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;  // DefaultInput
    using Loupedeck.MsfsPlugin.tools;  // MobiSend
    using System;

    // ---------- Helpers ----------
    internal static class NavTile
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

    // ================= NAV1 =================

    class Nav1TransferBtn : DefaultInput
    {
        public Nav1TransferBtn() : base("NAV1 FTS", "NAV1 Transfer", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV1","VC_NAV_1_TFR_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "FTS1");
    }

    class Nav1ModeUpBtn : DefaultInput
    {
        public Nav1ModeUpBtn() : base("NAV1 MODE ▲", "NAV1 Mode Up", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV1","VC_NAV_1_MODE_UP_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "MODE ▲");
    }

    class Nav1ModeDownBtn : DefaultInput
    {
        public Nav1ModeDownBtn() : base("NAV1 MODE ▼", "NAV1 Mode Down", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV1","VC_NAV_1_MODE_DOWN_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "MODE ▼");
    }

    class Nav1TestBtn : DefaultInput
    {
        public Nav1TestBtn() : base("NAV1 TEST", "NAV1 Test", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV1","VC_NAV_1_TEST_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "TEST1");
    }

    abstract class Nav1KeyBase : DefaultInput
    {
        private readonly string evt; private readonly string label;
        protected Nav1KeyBase(string label, string evt) : base($"NAV1 K {label}", "NAV1 Key", "RADIO_NAV")
        { this.label = label; this.evt = evt; }

        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV1", evt);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, $"K {label}");
    }

    class Nav1K0 : Nav1KeyBase { public Nav1K0() : base("0","VC_NAV_1_KB_0_SW"){} }
    class Nav1K1 : Nav1KeyBase { public Nav1K1() : base("1","VC_NAV_1_KB_1_SW"){} }
    class Nav1K2 : Nav1KeyBase { public Nav1K2() : base("2","VC_NAV_1_KB_2_SW"){} }
    class Nav1K3 : Nav1KeyBase { public Nav1K3() : base("3","VC_NAV_1_KB_3_SW"){} }
    class Nav1K4 : Nav1KeyBase { public Nav1K4() : base("4","VC_NAV_1_KB_4_SW"){} }
    class Nav1K5 : Nav1KeyBase { public Nav1K5() : base("5","VC_NAV_1_KB_5_SW"){} }
    class Nav1K6 : Nav1KeyBase { public Nav1K6() : base("6","VC_NAV_1_KB_6_SW"){} }
    class Nav1K7 : Nav1KeyBase { public Nav1K7() : base("7","VC_NAV_1_KB_7_SW"){} }
    class Nav1K8 : Nav1KeyBase { public Nav1K8() : base("8","VC_NAV_1_KB_8_SW"){} }
    class Nav1K9 : Nav1KeyBase { public Nav1K9() : base("9","VC_NAV_1_KB_9_SW"){} }
    class Nav1KClr : Nav1KeyBase { public Nav1KClr() : base("CLR","VC_NAV_1_KB_CLR_SW"){} }

    // ================= NAV2 =================

    class Nav2TransferBtn : DefaultInput
    {
        public Nav2TransferBtn() : base("NAV2 FTS", "NAV2 Transfer", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV2","VC_NAV_2_TFR_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "FTS2");
    }

    class Nav2ModeUpBtn : DefaultInput
    {
        public Nav2ModeUpBtn() : base("NAV2 MODE ▲", "NAV2 Mode Up", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV2","VC_NAV_2_MODE_UP_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "MODE ▲");
    }

    class Nav2ModeDownBtn : DefaultInput
    {
        public Nav2ModeDownBtn() : base("NAV2 MODE ▼", "NAV2 Mode Down", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV2","VC_NAV_2_MODE_DOWN_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "MODE ▼");
    }

    class Nav2TestBtn : DefaultInput
    {
        public Nav2TestBtn() : base("NAV2 TEST", "NAV2 Test", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV2","VC_NAV_2_TEST_SW_OBJ");
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, "TEST2");
    }

    abstract class Nav2KeyBase : DefaultInput
    {
        private readonly string evt; private readonly string label;
        protected Nav2KeyBase(string label, string evt) : base($"NAV2 K {label}", "NAV2 Key", "RADIO_NAV")
        { this.label = label; this.evt = evt; }

        protected override void ChangeValue() => MobiSend.Send("IFLY","737MAX","NAV2", evt);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTile.Label(s, $"K {label}");
    }

    class Nav2K0 : Nav2KeyBase { public Nav2K0() : base("0","VC_NAV_2_KB_0_SW"){} }
    class Nav2K1 : Nav2KeyBase { public Nav2K1() : base("1","VC_NAV_2_KB_1_SW"){} }
    class Nav2K2 : Nav2KeyBase { public Nav2K2() : base("2","VC_NAV_2_KB_2_SW"){} }
    class Nav2K3 : Nav2KeyBase { public Nav2K3() : base("3","VC_NAV_2_KB_3_SW"){} }
    class Nav2K4 : Nav2KeyBase { public Nav2K4() : base("4","VC_NAV_2_KB_4_SW"){} }
    class Nav2K5 : Nav2KeyBase { public Nav2K5() : base("5","VC_NAV_2_KB_5_SW"){} }
    class Nav2K6 : Nav2KeyBase { public Nav2K6() : base("6","VC_NAV_2_KB_6_SW"){} }
    class Nav2K7 : Nav2KeyBase { public Nav2K7() : base("7","VC_NAV_2_KB_7_SW"){} }
    class Nav2K8 : Nav2KeyBase { public Nav2K8() : base("8","VC_NAV_2_KB_8_SW"){} }
    class Nav2K9 : Nav2KeyBase { public Nav2K9() : base("9","VC_NAV_2_KB_9_SW"){} }
    class Nav2KClr : Nav2KeyBase { public Nav2KClr() : base("CLR","VC_NAV_2_KB_CLR_SW"){} }
}
