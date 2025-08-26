namespace Loupedeck.MsfsPlugin.tools
{
    // Shared UI buffer for the transponder code so the display tile can show
    // what you typed on the keypad immediately (0000–7777, octal).
    internal static class XpdrUi
    {
        private static string _code = "0000";
        public static string Code => _code;

        public static void Push(char d)
        {
            if (d < '0' || d > '7') return;     // octal only
            _code = (_code + d).PadLeft(5, '0');
            _code = _code.Substring(_code.Length - 4);
        }
        public static void Clear() => _code = "0000";
        public static void Set(string fourDigits)
        {
            if (string.IsNullOrEmpty(fourDigits)) { _code = "0000"; return; }
            var s = fourDigits.Trim();
            if (s.Length != 4) s = s.PadLeft(4, '0');
            foreach (var c in s) if (c < '0' || c > '7') return; // keep last valid
            _code = s;
        }
    }
}
