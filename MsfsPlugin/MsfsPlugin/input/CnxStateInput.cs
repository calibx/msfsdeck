namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class CnxStateInput : PluginDynamicCommand, Notifiable
    {
        public CnxStateInput() : base("Connect", "Display FSUIPC connexion state", "Debug")
        {
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return MsfsData.Instance.state ? "Disconnect" : "Connect";
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
            if (MsfsData.Instance.state)
            {
                SimulatorDAO.Disconnect();
            }
            else
            {
                SimulatorDAO.Initialise();
            }

        }
    }
}

