// File: MsfsPlugin/MsfsPlugin/input/radios/COMInputs.cs
// Button tiles for COM full-value displays (COM1/COM2 Active & Standby) and FTS (swap).
// Group: RADIO_COM

namespace Loupedeck.MsfsPlugin
{
    using System.Globalization;
    using Loupedeck.MsfsPlugin.input; // DefaultInput
    using Loupedeck.MsfsPlugin.tools; // Binding, BindingKeys, BitmapBuilder, ImageTool, etc.

    internal static class ComUi
    {
        public static string ToMHz3(long hz)
            => hz <= 0 ? "000.000" : (hz / 1_000_000.0).ToString("000.000", CultureInfo.InvariantCulture);

        public static BitmapImage Label(PluginImageSize size, string line1, string line2)
        {
            using (var b = new BitmapBuilder(size))
            {
                b.DrawText($"{line1}\n{line2}");
                return b.ToImage();
            }
        }
    }

    // ===== Displays (no action on press) =====

    internal sealed class Com1ActiveDisplayBtn : DefaultInput
    {
        private readonly Binding _act;
        public Com1ActiveDisplayBtn() : base("COM1 ACT (disp)", "COM1 Active frequency (display)", "RADIO_COM")
            => _act = Bind(BindingKeys.COM1_ACTIVE_FREQUENCY);

        protected override BitmapImage GetImage(PluginImageSize s)
            => ComUi.Label(s, "ACTIVE", ComUi.ToMHz3(_act.ControllerValue));
        protected override void ChangeValue() { /* display-only */ }
    }

    internal sealed class Com1StandbyDisplayBtn : DefaultInput
    {
        private readonly Binding _stby;
        public Com1StandbyDisplayBtn() : base("COM1 STBY (disp)", "COM1 Standby frequency (display)", "RADIO_COM")
            => _stby = Bind(BindingKeys.COM1_STBY);

        protected override BitmapImage GetImage(PluginImageSize s)
            => ComUi.Label(s, "STANDBY", ComUi.ToMHz3(_stby.ControllerValue));
        protected override void ChangeValue() { }
    }

    internal sealed class Com2ActiveDisplayBtn : DefaultInput
    {
        private readonly Binding _act;
        public Com2ActiveDisplayBtn() : base("COM2 ACT (disp)", "COM2 Active frequency (display)", "RADIO_COM")
            => _act = Bind(BindingKeys.COM2_ACTIVE_FREQUENCY);

        protected override BitmapImage GetImage(PluginImageSize s)
            => ComUi.Label(s, "ACTIVE", ComUi.ToMHz3(_act.ControllerValue));
        protected override void ChangeValue() { }
    }

    internal sealed class Com2StandbyDisplayBtn : DefaultInput
    {
        private readonly Binding _stby;
        public Com2StandbyDisplayBtn() : base("COM2 STBY (disp)", "COM2 Standby frequency (display)", "RADIO_COM")
            => _stby = Bind(BindingKeys.COM2_STBY);

        protected override BitmapImage GetImage(PluginImageSize s)
            => ComUi.Label(s, "STANDBY", ComUi.ToMHz3(_stby.ControllerValue));
        protected override void ChangeValue() { }
    }

    // ===== FTS (Frequency Transfer Switch) =====

    internal sealed class Com1FtsButton : DefaultInput
    {
        private readonly Binding _swap;
        public Com1FtsButton() : base("FTS1", "COM1 Frequency Transfer", "RADIO_COM")
            => _swap = Bind(BindingKeys.COM1_RADIO_SWAP);

        protected override void ChangeValue() => _swap.SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize s) => ComUi.Label(s, "COM1", "FTS");
    }

    internal sealed class Com2FtsButton : DefaultInput
    {
        private readonly Binding _swap;
        public Com2FtsButton() : base("FTS2", "COM2 Frequency Transfer", "RADIO_COM")
            => _swap = Bind(BindingKeys.COM2_RADIO_SWAP);

        protected override void ChangeValue() => _swap.SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize s) => ComUi.Label(s, "COM2", "FTS");
    }
}
