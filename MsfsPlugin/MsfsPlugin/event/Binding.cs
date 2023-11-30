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
            MSFSPreviousValue = MsfsValue;
            MsfsValue = newValue;
            MSFSChanged = !MSFSPreviousValue.Equals(MsfsValue);
        }

        public void SetControllerValue(long newValue)
        {
            ControllerPreviousValue = ControllerValue;
            ControllerValue = newValue;
            ControllerChanged = true;
            SimConnectDAO.Instance.Connect();
        }

        public void Reset()
        {
            if (!ControllerChanged)
            {
                ControllerChanged = false;
                MSFSChanged = false;
                ControllerValue = MsfsValue;
                MSFSPreviousValue = MsfsValue;
                ControllerPreviousValue = MsfsValue;
            }
        }

        public void ResetController()
        {
            ControllerChanged = false;
            MSFSChanged = false;
            MsfsValue = ControllerValue;
            MSFSPreviousValue = ControllerValue;
            ControllerPreviousValue = ControllerValue;
        }

        private long MSFSPreviousValue;
        private long ControllerPreviousValue;
    }
}
