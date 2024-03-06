using System;

namespace Loupedeck.MsfsPlugin.msfs.mobi
{
    /**
     * WasmModuleClientData from Mobiflight https://github.com/MobiFlight/MobiFlight-Connector/blob/main/SimConnectMSFS/WasmModuleClientData.cs
     */

    public class WasmModuleClientData
    {
        public string NAME;
        public Enum AREA_SIMVAR_ID;
        public Enum AREA_COMMAND_ID;
        public Enum AREA_RESPONSE_ID;
        public Enum AREA_STRINGSIMVAR_ID;
        public SIMCONNECT_DEFINE_ID DATA_DEFINITION_ID;
        public uint RESPONSE_OFFSET;
    }
}
