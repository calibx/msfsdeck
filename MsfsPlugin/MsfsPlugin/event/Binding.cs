namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public class Binding
    {
        public long ControllerValue { get; private set; }

        public BindingKeys Key { get; }

        public long MsfsValue { get; private set; }

        public bool ControllerChanged { get; private set; }

        public bool MSFSChanged { get; set; }   //>> Not good that this can be set from outside

        public Binding(BindingKeys key, long? value = null)
        {
            Key = key;
            if (value.HasValue)
                ControllerValue = value.Value;
        }

        public bool HasMSFSChanged() => MSFSChanged;

        //>> Strange that the following methods do not lock, since they can be called from different threads concurrently

        public void SetMsfsValue(long newValue)
        {
            MSFSChanged = MsfsValue != newValue;

            if (!MSFSChanged)
                return;

            if (newValue == ControllerPreviousValue)
            {
                // Ignore delayed change. Yes, this is not foolproof because the setting could be several
                // changes away, but at least it avoids most of the occurrences where the value flips back
                // and forth due to a delayed value coming from MSFS.

                if (DoTrace)
                    DebugTracing.Trace($"Ignoring delayed change to {newValue}");
                return;
            }

            else if (ControllerChanged)
            {
                // Ignore a change from MSFS if we are in process of sending another value to it.

                if (DoTrace)
                    DebugTracing.Trace($"Ignoring change to {newValue} since we have a new value about to be sent.");
                return;
            }

            if (DoTrace)
                DebugTracing.Trace($"MSFS value for key {Key} changed from '{MsfsValue}' to '{newValue}'");

            //>>MSFSPreviousValue = MsfsValue;
            MsfsValue = newValue;
        }

        public void SetControllerValue(long newValue)
        {
            SetControllerValueCalled = true;
            ControllerChanged = ControllerValue != newValue;
            if (ControllerChanged)
            {
                if (DoTrace)
                    DebugTracing.Trace($"Change {Key} from '{ControllerValue}' to '{newValue}'");

                ControllerPreviousValue = ControllerValue;
                ControllerValue = newValue;
                SimConnectDAO.Instance.Connect();
            }
        }

        public void Reset()
        {
            if (DoTrace)
            {
                DebugTracing.Trace($"Key {Key}");
            }
            ControllerValue = MsfsValue;
            ControllerChanged = false;
            MSFSChanged = false;
            if (MsfsValue == ControllerValue)
                SetControllerValueCalled = false;
        }

        public void ResetController()
        {
            DebugTracing.Trace($"Key {Key}");
            MsfsValue = ControllerValue;
            ControllerChanged = false;
            SetControllerValueCalled = false;
        }

        //>>private long MSFSPreviousValue = long.MinValue;
        private long ControllerPreviousValue = long.MinValue;
        private bool SetControllerValueCalled = false;
        private bool DoTrace => DebugTracing.tracingEnabled && SetControllerValueCalled;
    }
}
