namespace Loupedeck.MsfsPlugin
{
    using System;

    public class MsfsApplication : ClientApplication
    {
        public MsfsApplication()
        {

        }

        protected override String GetProcessName() => "FSUIPC7";

        protected override String GetBundleName() => "";
    }
}