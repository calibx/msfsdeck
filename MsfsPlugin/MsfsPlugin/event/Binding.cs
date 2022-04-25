namespace Loupedeck.MsfsPlugin
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Binding
{
        private String _MSFSPreviousValue;
        private String _ControllerPreviousValue;
        public String ControllerValue { get; set; }
        public BindingKeys Key { get;}
        public String MsfsValue { get; set; }
        public Boolean ControllerChanged { get; set; }
        private Boolean MSFSChanged { get; set; }

        public Boolean HasMSFSChanged() => this.MSFSChanged;


        public Binding(BindingKeys key) => this.Key = key;
        public void SetMsfsValue(String newValue)
        {
            this.MSFSChanged = this._MSFSPreviousValue != null && !this._MSFSPreviousValue.Equals(this.MsfsValue);
            this._MSFSPreviousValue = this.MsfsValue;
            this.MsfsValue = newValue;
        }
        public void SetControllerValue(String newValue)
        {
            this.ControllerChanged = true;//this._ControllerPreviousValue!= null && !this._ControllerPreviousValue.Equals(newValue);
            this._ControllerPreviousValue = this.ControllerValue;
            this.ControllerValue = newValue;
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
