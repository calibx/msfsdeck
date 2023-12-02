namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.msfs;

    public class Binding
    {
        public long ControllerValue { get; set; }

        public BindingKeys Key { get; set; }

        public long MsfsValue { get; set; }

        public bool ControllerChanged { get; set; }

        public bool MSFSChanged { get; set; }

        public Binding(BindingKeys key) => Key = key;

        public bool HasMSFSChanged() => MSFSChanged;

        public void SetMsfsValue(long newValue)
        {
            MSFSChanged = !MSFSPreviousValue.Equals(MsfsValue);
            MSFSPreviousValue = MsfsValue;
            MsfsValue = newValue;
        }
        public void SetControllerValue(long newValue)
        {
            ControllerChanged = true;
            ControllerPreviousValue = ControllerValue;
            ControllerValue = newValue;
            SimConnectDAO.Instance.Connect();
        }
        public void Reset()
        {
            ControllerValue = MsfsValue;
            MSFSPreviousValue = MsfsValue;
            ControllerPreviousValue = MsfsValue;
            ControllerChanged = false;
            MSFSChanged = false;
        }
        public void ResetController()
        {
            MsfsValue = ControllerValue;
            MSFSPreviousValue = ControllerValue;
            ControllerPreviousValue = ControllerValue;
            ControllerChanged = false;
            MSFSChanged = false;
        }

        private long MSFSPreviousValue;
        private long ControllerPreviousValue;
    }
}
