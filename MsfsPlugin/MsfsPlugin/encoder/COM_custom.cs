// File: MsfsPlugin/MsfsPlugin/encoder/COM_full_displays.cs
// Full-value COM1/COM2 Active & Standby display tiles (no split), group: RADIO_COM.

namespace Loupedeck.MsfsPlugin
{
    using System.Globalization;
    using Loupedeck.MsfsPlugin.tools;

    internal static class ComFmt
    {
        public static string MHz3(long hz)
            => hz <= 0 ? "000.000" : (hz / 1_000_000.0).ToString("000.000", CultureInfo.InvariantCulture);
    }

    // --- COM1 ---
    internal sealed class Com1ActiveFull : DefaultEncoder
    {
        private readonly Binding _a;
        public Com1ActiveFull() : base("COM1 ACT (full)", "COM1 Active frequency", "RADIO_COM", true, 0, 1, 1)
            => _a = Bind(BindingKeys.COM1_ACTIVE_FREQUENCY);

        protected override string GetDisplayValue() => $"COM1\n{ComFmt.MHz3(_a.ControllerValue)}";
        protected override long GetValue() => 0; protected override void SetValue(long _) { } protected override void RunCommand(string _) { }
    }

    internal sealed class Com1StandbyFull : DefaultEncoder
    {
        private readonly Binding _s;
        public Com1StandbyFull() : base("COM1 STBY (full)", "COM1 Standby frequency", "RADIO_COM", true, 0, 1, 1)
            => _s = Bind(BindingKeys.COM1_STBY);

        protected override string GetDisplayValue() => $"COM1\n{ComFmt.MHz3(_s.ControllerValue)}";
        protected override long GetValue() => 0; protected override void SetValue(long _) { } protected override void RunCommand(string _) { }
    }

    // --- COM2 ---
    internal sealed class Com2ActiveFull : DefaultEncoder
    {
        private readonly Binding _a;
        public Com2ActiveFull() : base("COM2 ACT (full)", "COM2 Active frequency", "RADIO_COM", true, 0, 1, 1)
            => _a = Bind(BindingKeys.COM2_ACTIVE_FREQUENCY);

        protected override string GetDisplayValue() => $"COM2\n{ComFmt.MHz3(_a.ControllerValue)}";
        protected override long GetValue() => 0; protected override void SetValue(long _) { } protected override void RunCommand(string _) { }
    }

    internal sealed class Com2StandbyFull : DefaultEncoder
    {
        private readonly Binding _s;
        public Com2StandbyFull() : base("COM2 STBY (full)", "COM2 Standby frequency", "RADIO_COM", true, 0, 1, 1)
            => _s = Bind(BindingKeys.COM2_STBY);

        protected override string GetDisplayValue() => $"COM2\n{ComFmt.MHz3(_s.ControllerValue)}";
        protected override long GetValue() => 0; protected override void SetValue(long _) { } protected override void RunCommand(string _) { }
    }
}
