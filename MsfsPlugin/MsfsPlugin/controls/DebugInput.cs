namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class DebugInput : PluginDynamicCommand
    {
        public DebugInput() : base("Etat", "Affiche l etat de connexion", "Debug")
        {
        }
        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            return MsfsData.Instance.state;
        }

        protected override void RunCommand(String actionParameter)
        {
           // SimulatorDAO.Initialise();
        }
    }
}

