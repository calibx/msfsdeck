// File: MsfsPlugin/MsfsPlugin/encoder/NAV_displays.cs
// NAV1/NAV2 Active & Standby display tiles (2 decimals), grouped under RADIO_NAV.
// Uses ControllerValue so it updates immediately. No MobiFlight required.

namespace Loupedeck.MsfsPlugin
{
    using System.Globalization;
    using Loupedeck.MsfsPlugin.tools;

    internal static class NavDisplayFormat
    {
        public static string ToMHz2(long hz)
            => hz <= 0 ? "000.00" : (hz / 1_000_000.0).ToString("000.00", CultureInfo.InvariantCulture);
    }

    // ===== NAV1 =====
    internal sealed class Nav1ActiveDisplay : DefaultEncoder
    {
        private readonly Binding _active;
        public Nav1ActiveDisplay()
            : base("NAV1 ACT", "NAV1 Active (display)", "RADIO_NAV", true, 0, 1, 1)
            => _active = Bind(BindingKeys.NAV1_ACTIVE_FREQUENCY);

        protected override string GetDisplayValue()
            => $"ACT\n{NavDisplayFormat.ToMHz2(_active.ControllerValue)}";

        protected override long GetValue() => 0;
        protected override void SetValue(long _) { }
        protected override void RunCommand(string _) { }
    }

    internal sealed class Nav1StandbyDisplay : DefaultEncoder
    {
        private readonly Binding _stby;
        public Nav1StandbyDisplay()
            : base("NAV1 STBY", "NAV1 Standby (display)", "RADIO_NAV", true, 0, 1, 1)
            => _stby = Bind(BindingKeys.NAV1_STBY_FREQUENCY);

        protected override string GetDisplayValue()
            => $"STBY\n{NavDisplayFormat.ToMHz2(_stby.ControllerValue)}";

        protected override long GetValue() => 0;
        protected override void SetValue(long _) { }
        protected override void RunCommand(string _) { }
    }

    // ===== NAV2 =====
    internal sealed class Nav2ActiveDisplay : DefaultEncoder
    {
        private readonly Binding _active;
        public Nav2ActiveDisplay()
            : base("NAV2 ACT", "NAV2 Active (display)", "RADIO_NAV", true, 0, 1, 1)
            => _active = Bind(BindingKeys.NAV2_ACTIVE_FREQUENCY);

        protected override string GetDisplayValue()
            => $"ACT\n{NavDisplayFormat.ToMHz2(_active.ControllerValue)}";

        protected override long GetValue() => 0;
        protected override void SetValue(long _) { }
        protected override void RunCommand(string _) { }
    }

    internal sealed class Nav2StandbyDisplay : DefaultEncoder
    {
        private readonly Binding _stby;
        public Nav2StandbyDisplay()
            : base("NAV2 STBY", "NAV2 Standby (display)", "RADIO_NAV", true, 0, 1, 1)
            => _stby = Bind(BindingKeys.NAV2_STBY_FREQUENCY);

        protected override string GetDisplayValue()
            => $"STBY\n{NavDisplayFormat.ToMHz2(_stby.ControllerValue)}";

        protected override long GetValue() => 0;
        protected override void SetValue(long _) { }
        protected override void RunCommand(string _) { }
    }
}
