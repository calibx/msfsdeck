namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.Loader;
    using System.Text;
    using System.Threading.Tasks;

    internal class SimConnectWrapper : ISimConnectWrapper
    {
        private readonly object simWrapper;
        private static readonly Lazy<SimConnectWrapper> lazy = new Lazy<SimConnectWrapper>(() => new SimConnectWrapper());

        public static SimConnectWrapper Instance => lazy.Value;

        public SimConnectWrapper()
        {
            var context = new AssemblyLoadContext("MSFSContext");
            var pathWithEnv = @"%USERPROFILE%\AppData\Local\Logi\LogiPluginService\Plugins\MsfsPlugin";
            var filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            var assembly = context.LoadFromAssemblyPath(filePath + "\\SimConnectWrapper.dll");
            context.LoadFromAssemblyPath(filePath + "\\Microsoft.FlightSimulator.SimConnect.dll");

            Type assemblyType = assembly.GetType("SimConnectWrapper.SimConnectWrapper");

            if (assemblyType != null)
            {
                var argTypes = Array.Empty<Type>();
                ConstructorInfo cInfo = assemblyType.GetConstructor(argTypes);
                simWrapper = cInfo.Invoke(null);
            }
        }

        public void Connect() => simWrapper.InvokeMethod("Connect");
        public void Disconnect(bool unloading = false) => simWrapper.InvokeMethod("Disconnect");
        public void send(Enum eventName, uint value) => simWrapper.InvokeMethod("send", [eventName, value]);
        public void register(Enum eventName, string key) => simWrapper.InvokeMethod("register", [eventName, key]);
        public bool IsConnected() => (bool)simWrapper.InvokeMethod("IsConnected");
        public string getString(string key) => (string)simWrapper.InvokeMethod("getString", [key]);
        public long getLong(string key) => (long)simWrapper.InvokeMethod("getLong", [key]);
        public double getDouble(string key) => (double)simWrapper.InvokeMethod("getDouble", [key]);
    }
}
