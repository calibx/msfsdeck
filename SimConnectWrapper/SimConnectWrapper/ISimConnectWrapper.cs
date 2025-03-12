using System;
using static SimConnectWrapper.DataTransferTypes;

namespace SimConnectWrapper
{
    public interface ISimConnectWrapper
    {
        void Connect();

        bool IsConnected();

        void send(Enum eventName, UInt32 value);

        void register(Enum eventName, string key);

        string getString(string key);

        long getLong(string key);

        double getDouble(string key);

        void Disconnect(bool unloading = false);
    }
}