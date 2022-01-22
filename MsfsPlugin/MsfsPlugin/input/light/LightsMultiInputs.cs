namespace Loupedeck.MsfsPlugin.folder
{
    using System;
    
    public class LightsMultiInputs : PluginDynamicCommand, Notifiable
    {
        public LightsMultiInputs() : base()
        {

            this.AddParameter("Navigation", "Navigation light", "Lights");
            this.AddParameter("Beacon", "Beacon light", "Lights");
            this.AddParameter("Landing", "Landing light", "Lights");
            this.AddParameter("Taxi", "Taxi light", "Lights");
            this.AddParameter("Strobes", "Strobes light", "Lights");
            this.AddParameter("Instruments", "Instruments light", "Lights");
            this.AddParameter("Recognition", "Recognition light", "Lights");
            this.AddParameter("Wing", "Wing light", "Lights");
            this.AddParameter("Logo", "Logo light", "Lights");
            this.AddParameter("Cabin", "Cabin light", "Lights");

            MsfsData.Instance.Register(this);

        }

        protected override String GetCommandDisplayName(String actionParameter, PluginImageSize imageSize)
        {
            var ret = "";
            switch (actionParameter)
            {
                case "Navigation":
                    ret += MsfsData.Instance.NavigationLight ? "Disable" : "Enable";
                    break;
                case "Beacon":
                    ret += MsfsData.Instance.BeaconLight ? "Disable" : "Enable";
                    break;
                case "Landing":
                    ret += MsfsData.Instance.LandingLight ? "Disable" : "Enable";
                    break;
                case "Taxi": 
                    ret += MsfsData.Instance.TaxiLight ? "Disable" : "Enable";
                    break;
                case "Strobes":
                    ret += MsfsData.Instance.StrobesLight ? "Disable" : "Enable";
                    break;
                case "Instruments":
                    ret += MsfsData.Instance.InstrumentsLight ? "Disable" : "Enable";
                    break;
                case "Recognition":
                    ret += MsfsData.Instance.RecognitionLight ? "Disable" : "Enable";
                    break;
                case "Wing":
                    ret += MsfsData.Instance.WingLight ? "Disable" : "Enable";
                    break;
                case "Logo":
                    ret += MsfsData.Instance.LogoLight ? "Disable" : "Enable";
                    break;
                case "Cabin":
                    ret += MsfsData.Instance.CabinLight ? "Disable" : "Enable";
                    break;
            }
            ret += " " + actionParameter;
            return ret;
        }
        protected override void RunCommand(String actionParameter)
        {
            switch (actionParameter)
            {
                case "Navigation":
                    MsfsData.Instance.NavigationLight = !MsfsData.Instance.NavigationLight;
                    break;
                case "Beacon":
                    MsfsData.Instance.BeaconLight = !MsfsData.Instance.BeaconLight;
                    break;
                case "Landing":
                    MsfsData.Instance.LandingLight = !MsfsData.Instance.LandingLight;
                    break;
                case "Taxi":
                    MsfsData.Instance.TaxiLight = !MsfsData.Instance.TaxiLight;
                    break;
                case "Strobes":
                    MsfsData.Instance.StrobesLight = !MsfsData.Instance.StrobesLight;
                    break;
                case "Instruments":
                    MsfsData.Instance.InstrumentsLight = !MsfsData.Instance.InstrumentsLight;
                    break;
                case "Recognition":
                    MsfsData.Instance.RecognitionLight = !MsfsData.Instance.RecognitionLight;
                    break;
                case "Wing":
                    MsfsData.Instance.WingLight = !MsfsData.Instance.WingLight;
                    break;
                case "Logo":
                    MsfsData.Instance.LogoLight = !MsfsData.Instance.LogoLight;
                    break;
                case "Cabin":
                    MsfsData.Instance.CabinLight = !MsfsData.Instance.CabinLight;
                    break;
            }
        }
        public void Notify() => this.ActionImageChanged();
    }

}
