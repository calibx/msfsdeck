namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class CnxStateInput : PluginDynamicCommand, Notifiable
    {
        public CnxStateInput() : base("State", "Display MSFS connexion state", "Debug")
        {
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return MsfsData.Instance.state;
        }

        public void Notify() => this.AdjustmentValueChanged();

        protected override void RunCommand(String actionParameter)
        {
                MsfsData.Instance.state = "Init CNX";
                MsfsData.Instance.changed();
                SimulatorDAO.Initialise();
        }
    }
}

