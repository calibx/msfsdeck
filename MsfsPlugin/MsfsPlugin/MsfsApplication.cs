namespace Loupedeck.MsfsPlugin
{
    using System;

    // This class can be used to connect the Loupedeck plugin to an application.

    public class MsfsApplication : ClientApplication
    {
        public MsfsApplication()
        {
        }

        // This method can be used to link the plugin to a Windows application.
        public override string[] GetProcessOrBundleNames() => ["FlightSimulator","FlightSimulator2024"];


        // This method can be used to check whether the application is installed or not.
        public override ClientApplicationStatus GetApplicationStatus() => ClientApplicationStatus.Unknown;
    }
}
