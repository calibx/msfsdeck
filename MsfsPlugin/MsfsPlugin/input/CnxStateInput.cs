namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class CnxStateInput : PluginDynamicCommand, Notifiable
    {
        public CnxStateInput() : base("Connection", "Display FSUIPC connexion state", "Debug")
        {
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            MsfsData.Instance.ValuesDisplayed = true;
            if (MsfsData.Instance.Connected)
            {
                return "Connected";
            }
            else
            {
                return MsfsData.Instance.TryConnect ? "Trying to connect" : "Disconnected";
            }
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
        }
    }
}

