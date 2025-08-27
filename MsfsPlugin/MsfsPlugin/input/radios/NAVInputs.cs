// File: MsfsPlugin/MsfsPlugin/input/radios/NAVInputs.cs
// NAV1/NAV2 keypad + FTS via MobiFlight presets (if available), with safe fallback.
// Group: RADIO_COM

namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;   // DefaultInput
    using Loupedeck.MsfsPlugin.tools;   // MsfsData, Binding, BindingKeys, BitmapBuilder, MobiSend
    using System;

    internal static class NavPresets
    {
        // --- Replace these with the *exact* MobiFlight preset names you have ---
        // NAV1
        public const string NAV1_TFR = "IFLY_737MAX_NAV1_TFR";
        public const string NAV1_MODE_UP = "IFLY_737MAX_NAV1_MODE_UP";
        public const string NAV1_MODE_DN = "IFLY_737MAX_NAV1_MODE_DOWN";
        public const string NAV1_TEST = "IFLY_737MAX_NAV1_TEST";
        public const string NAV1_K0 = "IFLY_737MAX_NAV1_KB_0";
        public const string NAV1_K1 = "IFLY_737MAX_NAV1_KB_1";
        public const string NAV1_K2 = "IFLY_737MAX_NAV1_KB_2";
        public const string NAV1_K3 = "IFLY_737MAX_NAV1_KB_3";
        public const string NAV1_K4 = "IFLY_737MAX_NAV1_KB_4";
        public const string NAV1_K5 = "IFLY_737MAX_NAV1_KB_5";
        public const string NAV1_K6 = "IFLY_737MAX_NAV1_KB_6";
        public const string NAV1_K7 = "IFLY_737MAX_NAV1_KB_7";
        public const string NAV1_K8 = "IFLY_737MAX_NAV1_KB_8";
        public const string NAV1_K9 = "IFLY_737MAX_NAV1_KB_9";
        public const string NAV1_CLR = "IFLY_737MAX_NAV1_KB_CLR";

        // NAV2
        public const string NAV2_TFR = "IFLY_737MAX_NAV2_TFR";
        public const string NAV2_MODE_UP = "IFLY_737MAX_NAV2_MODE_UP";
        public const string NAV2_MODE_DN = "IFLY_737MAX_NAV2_MODE_DOWN";
        public const string NAV2_TEST = "IFLY_737MAX_NAV2_TEST";
        public const string NAV2_K0 = "IFLY_737MAX_NAV2_KB_0";
        public const string NAV2_K1 = "IFLY_737MAX_NAV2_KB_1";
        public const string NAV2_K2 = "IFLY_737MAX_NAV2_KB_2";
        public const string NAV2_K3 = "IFLY_737MAX_NAV2_KB_3";
        public const string NAV2_K4 = "IFLY_737MAX_NAV2_KB_4";
        public const string NAV2_K5 = "IFLY_737MAX_NAV2_KB_5";
        public const string NAV2_K6 = "IFLY_737MAX_NAV2_KB_6";
        public const string NAV2_K7 = "IFLY_737MAX_NAV2_KB_7";
        public const string NAV2_K8 = "IFLY_737MAX_NAV2_KB_8";
        public const string NAV2_K9 = "IFLY_737MAX_NAV2_KB_9";
        public const string NAV2_CLR = "IFLY_737MAX_NAV2_KB_CLR";
    }

    internal static class NavTileMini
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

    // Simple local buffer (only used if MobiSend.Send fails)
    internal static class NavBuffer
    {
        private static string _n1 = "";
        private static string _n2 = "";

        public static void Push(int idx, char d)
        {
            if (d < '0' || d > '9') return;
            if (idx == 1) { _n1 = (_n1 + d); if (_n1.Length > 5) _n1 = _n1[^5..]; }
            else if (idx == 2) { _n2 = (_n2 + d); if (_n2.Length > 5) _n2 = _n2[^5..]; }
        }

        public static void Clear(int idx) { if (idx == 1) _n1 = ""; else if (idx == 2) _n2 = ""; }

        public static long ToHz(int idx)
        {
            var s = idx == 1 ? _n1 : _n2;
            if (string.IsNullOrEmpty(s) || s.Length < 3) return 0;

            var whole = s[..^2];
            var dec = s[^2..];

            if (!int.TryParse(whole, out var w) || !int.TryParse(dec, out var d)) return 0;

            var mhz = w + d / 100.0;
            long hz = (long)(mhz * 1_000_000.0);

            if (hz < 108_000_000) hz = 108_000_000;
            if (hz > 117_950_000) hz = 117_950_000;
            return hz;
        }
    }

    // ===== NAV1 =====

    class Nav1TransferBtn : DefaultInput
    {
        private readonly Binding _stby;
        private readonly Binding _swap; // best-effort

        public Nav1TransferBtn() : base("NAV1 FTS", "NAV1 Transfer", "RADIO_NAV")
        {
            _stby = MsfsData.Instance.Register(BindingKeys.NAV1_STBY_FREQUENCY);
            // try a few likely keys; if none exist, _swap stays null, fallback still works by just setting stby
            _swap = TryAny("NAV1_RADIO_SWAP", "NAV1_SWAP", "NAV1_TFR");
        }

        protected override void ChangeValue()
        {
            if (!MobiSend.Send(NavPresets.NAV1_TFR))
            {
                long hz = NavBuffer.ToHz(1);
                if (hz > 0) _stby.SetControllerValue(hz);
                _swap?.SetControllerValue(1);
                NavBuffer.Clear(1);
            }
        }

        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "FTS1");

        private static Binding TryAny(params string[] names)
        {
            foreach (var n in names)
                if (Enum.TryParse(typeof(BindingKeys), n, out var keyObj))
                    try { return MsfsData.Instance.Register((BindingKeys)keyObj); } catch { }
            return null;
        }
    }

    class Nav1ModeUpBtn : DefaultInput
    {
        public Nav1ModeUpBtn() : base("NAV1 MODE ▲", "NAV1 Mode Up", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV1_MODE_UP);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "MODE ▲");
    }

    class Nav1ModeDownBtn : DefaultInput
    {
        public Nav1ModeDownBtn() : base("NAV1 MODE ▼", "NAV1 Mode Down", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV1_MODE_DN);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "MODE ▼");
    }

    class Nav1TestBtn : DefaultInput
    {
        public Nav1TestBtn() : base("NAV1 TEST", "NAV1 Test", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV1_TEST);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "TEST1");
    }

    abstract class Nav1KeyBase : DefaultInput
    {
        private readonly string _preset; private readonly string _label;
        protected Nav1KeyBase(string label, string preset) : base($"NAV1 K {label}", "NAV1 Key", "RADIO_NAV")
        { _label = label; _preset = preset; }

        protected override void ChangeValue()
        {
            if (!MobiSend.Send(_preset))
                NavBuffer.Push(1, _label[0]); // fallback
        }
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, $"K {_label}");
    }

    class Nav1K0 : Nav1KeyBase { public Nav1K0() : base("0", NavPresets.NAV1_K0) { } }
    class Nav1K1 : Nav1KeyBase { public Nav1K1() : base("1", NavPresets.NAV1_K1) { } }
    class Nav1K2 : Nav1KeyBase { public Nav1K2() : base("2", NavPresets.NAV1_K2) { } }
    class Nav1K3 : Nav1KeyBase { public Nav1K3() : base("3", NavPresets.NAV1_K3) { } }
    class Nav1K4 : Nav1KeyBase { public Nav1K4() : base("4", NavPresets.NAV1_K4) { } }
    class Nav1K5 : Nav1KeyBase { public Nav1K5() : base("5", NavPresets.NAV1_K5) { } }
    class Nav1K6 : Nav1KeyBase { public Nav1K6() : base("6", NavPresets.NAV1_K6) { } }
    class Nav1K7 : Nav1KeyBase { public Nav1K7() : base("7", NavPresets.NAV1_K7) { } }
    class Nav1K8 : Nav1KeyBase { public Nav1K8() : base("8", NavPresets.NAV1_K8) { } }
    class Nav1K9 : Nav1KeyBase { public Nav1K9() : base("9", NavPresets.NAV1_K9) { } }

    class Nav1KClr : DefaultInput
    {
        public Nav1KClr() : base("NAV1 K CLR", "NAV1 Key CLR", "RADIO_NAV") { }
        protected override void ChangeValue()
        {
            if (!MobiSend.Send(NavPresets.NAV1_CLR))
                NavBuffer.Clear(1);
        }
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "CLR1");
    }

    // ===== NAV2 =====

    class Nav2TransferBtn : DefaultInput
    {
        private readonly Binding _stby;
        private readonly Binding _swap;

        public Nav2TransferBtn() : base("NAV2 FTS", "NAV2 Transfer", "RADIO_NAV")
        {
            _stby = MsfsData.Instance.Register(BindingKeys.NAV2_STBY_FREQUENCY);
            _swap = TryAny("NAV2_RADIO_SWAP", "NAV2_SWAP", "NAV2_TFR");
        }

        protected override void ChangeValue()
        {
            if (!MobiSend.Send(NavPresets.NAV2_TFR))
            {
                long hz = NavBuffer.ToHz(2);
                if (hz > 0) _stby.SetControllerValue(hz);
                _swap?.SetControllerValue(1);
                NavBuffer.Clear(2);
            }
        }

        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "FTS2");

        private static Binding TryAny(params string[] names)
        {
            foreach (var n in names)
                if (Enum.TryParse(typeof(BindingKeys), n, out var keyObj))
                    try { return MsfsData.Instance.Register((BindingKeys)keyObj); } catch { }
            return null;
        }
    }

    class Nav2ModeUpBtn : DefaultInput
    {
        public Nav2ModeUpBtn() : base("NAV2 MODE ▲", "NAV2 Mode Up", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV2_MODE_UP);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "MODE ▲");
    }

    class Nav2ModeDownBtn : DefaultInput
    {
        public Nav2ModeDownBtn() : base("NAV2 MODE ▼", "NAV2 Mode Down", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV2_MODE_DN);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "MODE ▼");
    }

    class Nav2TestBtn : DefaultInput
    {
        public Nav2TestBtn() : base("NAV2 TEST", "NAV2 Test", "RADIO_NAV") { }
        protected override void ChangeValue() => MobiSend.Send(NavPresets.NAV2_TEST);
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "TEST2");
    }

    abstract class Nav2KeyBase : DefaultInput
    {
        private readonly string _preset; private readonly string _label;
        protected Nav2KeyBase(string label, string preset) : base($"NAV2 K {label}", "NAV2 Key", "RADIO_NAV")
        { _label = label; _preset = preset; }

        protected override void ChangeValue()
        {
            if (!MobiSend.Send(_preset))
                NavBuffer.Push(2, _label[0]); // fallback buffer
        }
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, $"K {_label}");
    }

    class Nav2K0 : Nav2KeyBase { public Nav2K0() : base("0", NavPresets.NAV2_K0) { } }
    class Nav2K1 : Nav2KeyBase { public Nav2K1() : base("1", NavPresets.NAV2_K1) { } }
    class Nav2K2 : Nav2KeyBase { public Nav2K2() : base("2", NavPresets.NAV2_K2) { } }
    class Nav2K3 : Nav2KeyBase { public Nav2K3() : base("3", NavPresets.NAV2_K3) { } }
    class Nav2K4 : Nav2KeyBase { public Nav2K4() : base("4", NavPresets.NAV2_K4) { } }
    class Nav2K5 : Nav2KeyBase { public Nav2K5() : base("5", NavPresets.NAV2_K5) { } }
    class Nav2K6 : Nav2KeyBase { public Nav2K6() : base("6", NavPresets.NAV2_K6) { } }
    class Nav2K7 : Nav2KeyBase { public Nav2K7() : base("7", NavPresets.NAV2_K7) { } }
    class Nav2K8 : Nav2KeyBase { public Nav2K8() : base("8", NavPresets.NAV2_K8) { } }
    class Nav2K9 : Nav2KeyBase { public Nav2K9() : base("9", NavPresets.NAV2_K9) { } }

    class Nav2KClr : DefaultInput
    {
        public Nav2KClr() : base("NAV2 K CLR", "NAV2 Key CLR", "RADIO_NAV") { }
        protected override void ChangeValue()
        {
            if (!MobiSend.Send(NavPresets.NAV2_CLR))
                NavBuffer.Clear(2);
        }
        protected override BitmapImage GetImage(PluginImageSize s) => NavTileMini.Label(s, "CLR2");
    }
}
