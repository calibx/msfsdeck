namespace Loupedeck.MsfsPlugin
{
    using System;
       
    using Loupedeck.MsfsPlugin.msfs;

    public class Binding
    {
        private Int64 _MSFSPreviousValue;
        private Int64 _ControllerPreviousValue;
        public Int64 ControllerValue { get; set; }
        public BindingKeys Key { get; set; }
        public Int64 MsfsValue { get; set; }
        public Boolean ControllerChanged { get; set; }
        public Boolean MSFSChanged { get; set; }

        public Binding(BindingKeys Key)
        {
            this.Key = Key;
        }
        public Boolean HasMSFSChanged() => this.MSFSChanged;
        public void SetMsfsValue(Int64 newValue)
        {
            this.MSFSChanged = !this._MSFSPreviousValue.Equals(this.MsfsValue);
            this._MSFSPreviousValue = this.MsfsValue;
            this.MsfsValue = newValue;
        }
        public void SetControllerValue(Int64 newValue)
        {
            this.ControllerChanged = true;
            this._ControllerPreviousValue = this.ControllerValue;
            this.ControllerValue = newValue;
            SimConnectDAO.Instance.Connect();
        }
        public void Reset()
        {
            this.ControllerValue = this.MsfsValue;
            this._MSFSPreviousValue = this.MsfsValue;
            this._ControllerPreviousValue = this.MsfsValue;
            this.ControllerChanged = false;
            this.MSFSChanged = false;
        }
        public void ResetController()
        {
            this.MsfsValue = this.ControllerValue;
            this._MSFSPreviousValue = this.ControllerValue;
            this._ControllerPreviousValue = this.ControllerValue;
            this.ControllerChanged = false;
            this.MSFSChanged = false;
        }

    }
}
