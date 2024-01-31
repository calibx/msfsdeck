namespace Loupedeck.MsfsPlugin
{
    using System.Linq;

    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class Binding
    {
        public long ControllerValue { get; private set; }

        public BindingKeys Key { get; }

        public long MsfsValue { get; private set; }

        public bool GetBool() => ConvertTool.getBoolean(MsfsValue);

        public bool ControllerChanged { get; private set; }

        public bool MSFSChanged { get; set; }

        public Binding(BindingKeys key, long? value = null)
        {
            Key = key;
            if (value.HasValue)
                ControllerValue = value.Value;
        }

        public bool HasMSFSChanged() => MSFSChanged;

        public void SetMsfsValue(long newValue)
        {
            if (MsfsValue == newValue)
                return;

            if (newValue == ControllerPreviousValue)
            {
                // Ignore delayed change. Yes, this is not foolproof because the setting could be several
                // changes away, but at least it avoids most of the occurrences where the value flips back
                // and forth due to a delayed value coming from MSFS.
                if (DoTrace)
                    DebugTracing.Trace($"Ignoring delayed change for key '{Key}' to '{newValue}'");
                // Only ignore once (if the value is resent to us, we accept it):
                ControllerPreviousValue = long.MinValue;
                return;
            }

            else if (ControllerChanged)
            {
                // Ignore a change from MSFS if we are in process of sending another value to it.
                if (DoTrace)
                    DebugTracing.Trace($"Ignoring change for key '{Key}' to '{newValue}' since we have a new value about to be sent.");
                return;
            }

            if (DoTrace)
                DebugTracing.Trace($"MSFS value for key '{Key}' changed from '{MsfsValue}' to '{newValue}'");

            MsfsValue = newValue;
            MSFSChanged = true;
        }

        public void SetControllerValue(long newValue)
        {
            SetControllerValueCalled = true;
            ControllerChanged = true;   // We need to do this even if the value is unchanged - e.g. NAV frequency swapping will only work with this
            if (DoTrace)
                DebugTracing.Trace($"Change {Key} from '{ControllerValue}' to '{newValue}'");

            ControllerPreviousValue = ControllerValue;
            ControllerValue = newValue;
            SimConnectDAO.Instance.Connect();
        }

        public void Reset()
        {
            if (DoTrace)
            {
                DebugTracing.Trace($"Key {Key}. Changing ControllerValue from '{ControllerValue}' to '{MsfsValue}'.");
            }
            ControllerValue = MsfsValue;
            ControllerChanged = false;
            MSFSChanged = false;
            SetControllerValueCalled = false;
        }

        public void ResetController()
        {
            DebugTracing.Trace($"Key {Key}. Changing MsfsValue from '{MsfsValue}' to '{ControllerValue}'.");
            MsfsValue = ControllerValue;
            ControllerChanged = false;
            SetControllerValueCalled = false;
        }

        private long ControllerPreviousValue = long.MinValue;
        private bool SetControllerValueCalled = false;
        private bool DoTrace => DebugTracing.TracingEnabled && (SetControllerValueCalled || KeysToTrace.Contains(Key));

        // For debugging: add keys to trace even if they are not manipulated by calling SetControllerValue():
        private readonly BindingKeys[] KeysToTrace = { };
    }
}
