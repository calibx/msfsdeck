// File: MsfsPlugin/MsfsPlugin/encoder/XPDR_display.cs
// Display-only transponder tile. Group: RADIO_XPDR.
// Shows the 4-digit code from XpdrUi (updated by keypad inputs).

namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.tools;

    internal sealed class XpdrDisplayTile : DefaultEncoder
    {
        public XpdrDisplayTile()
            : base("XPDR", "Transponder Code (display)", "RADIO_XPDR", true, 0, 1, 1) { }

        protected override string GetDisplayValue() => $"XPDR\n{XpdrUi.Code}";

        // Read-only tile
        protected override long GetValue() => 0;
        protected override void SetValue(long newValue) { }
        protected override void RunCommand(string actionParameter) { }
    }
}
