// File: MsfsPlugin/MsfsPlugin/encoder/COMEncoder.cs
// COM split encoders only (INT/DEC) for COM1/COM2.
// Encoder press = FTS1 (for COM1) / FTS2 (for COM2)
// Group: RADIO_COM

namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Globalization;
    using Loupedeck.MsfsPlugin.tools;

    internal static class ComFormat
    {
        public static string ToIntPart(long hz)
        {
            if (hz <= 0) return "000.";
            var whole = Math.Truncate(hz / 1_000_000f);
            return $"{whole}.";
        }

        public static string ToDecPart(long hz)
        {
            if (hz <= 0) return "000";
            var mhz  = hz / 1_000_000f;
            var frac = Math.Round(mhz - Math.Truncate(mhz), 3).ToString(CultureInfo.InvariantCulture);
            return (frac.Length > 2 ? frac.Substring(2) : frac).PadRight(3, '0');
        }
    }

    // ===== COM1 INT =====
    internal sealed class Com1IntEncoder : DefaultEncoder
    {
        private readonly Binding _stby;
        private readonly Binding _swap;      // << new
        private const int StepHz = 1_000_000;

        public Com1IntEncoder() : base("COM1 INT", "COM1 Standby integer part", "RADIO_COM", true, 118_000_000, 136_975_000, StepHz)
        {
            _stby = Bind(BindingKeys.COM1_STBY);
            _swap = Bind(BindingKeys.COM1_RADIO_SWAP); // << new
        }

        protected override string GetDisplayValue() => $"{ComFormat.ToIntPart(_stby.ControllerValue)}";
        protected override long GetValue() => _stby.ControllerValue;
        protected override void SetValue(long newValue) => _stby.SetControllerValue(newValue);

        // Press encoder = FTS1
        protected override void RunCommand(string actionParameter) => _swap.SetControllerValue(1);
    }

    // ===== COM1 DEC =====
    internal sealed class Com1DecEncoder : DefaultEncoder
    {
        private readonly Binding _stby;
        private readonly Binding _swap;      // << new
        private const int StepHz = 5_000;

        public Com1DecEncoder() : base("COM1 DEC", "COM1 Standby decimal part", "RADIO_COM", true, 118_000_000, 136_975_000, StepHz)
        {
            _stby = Bind(BindingKeys.COM1_STBY);
            _swap = Bind(BindingKeys.COM1_RADIO_SWAP); // << new
        }

        protected override string GetDisplayValue() => $"{ComFormat.ToDecPart(_stby.ControllerValue)}";
        protected override long GetValue() => _stby.ControllerValue;
        protected override void SetValue(long newValue) => _stby.SetControllerValue(newValue);

        // Press encoder = FTS1
        protected override void RunCommand(string actionParameter) => _swap.SetControllerValue(1);
    }

    // ===== COM2 INT =====
    internal sealed class Com2IntEncoder : DefaultEncoder
    {
        private readonly Binding _stby;
        private readonly Binding _swap;      // << new
        private const int StepHz = 1_000_000;

        public Com2IntEncoder() : base("COM2 INT", "COM2 Standby integer part", "RADIO_COM", true, 118_000_000, 136_975_000, StepHz)
        {
            _stby = Bind(BindingKeys.COM2_STBY);
            _swap = Bind(BindingKeys.COM2_RADIO_SWAP); // << new
        }

        protected override string GetDisplayValue() => $"{ComFormat.ToIntPart(_stby.ControllerValue)}";
        protected override long GetValue() => _stby.ControllerValue;
        protected override void SetValue(long newValue) => _stby.SetControllerValue(newValue);

        // Press encoder = FTS2
        protected override void RunCommand(string actionParameter) => _swap.SetControllerValue(1);
    }

    // ===== COM2 DEC =====
    internal sealed class Com2DecEncoder : DefaultEncoder
    {
        private readonly Binding _stby;
        private readonly Binding _swap;      // << new
        private const int StepHz = 5_000;

        public Com2DecEncoder() : base("COM2 DEC", "COM2 Standby decimal part", "RADIO_COM", true, 118_000_000, 136_975_000, StepHz)
        {
            _stby = Bind(BindingKeys.COM2_STBY);
            _swap = Bind(BindingKeys.COM2_RADIO_SWAP); // << new
        }

        protected override string GetDisplayValue() => $"{ComFormat.ToDecPart(_stby.ControllerValue)}";
        protected override long GetValue() => _stby.ControllerValue;
        protected override void SetValue(long newValue) => _stby.SetControllerValue(newValue);

        // Press encoder = FTS2
        protected override void RunCommand(string actionParameter) => _swap.SetControllerValue(1);
    }
}
