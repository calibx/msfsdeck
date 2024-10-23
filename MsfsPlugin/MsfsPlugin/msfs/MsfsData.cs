namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin;

    class MsfsData
    {
        private readonly List<INotifiable> notifiables = new List<INotifiable>();

        public Dictionary<BindingKeys, Binding> bindings = new Dictionary<BindingKeys, Binding>();

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());
        public static MsfsData Instance => lazy.Value;
        public MsfsPlugin plugin { get; set; }
        public bool DEBUG { get; set; }
        public string AircraftName { get; set; }
        public string DebugValue1 { get; set; }
        public string DebugValue2 { get; set; }
        public string DebugValue3 { get; set; }

        private MsfsData()
        {
            DEBUG = false;
        }

        public void Register(INotifiable notif) => notifiables.Add(notif);

        public Binding Register(BindingKeys key, long? value = null)
        {
            if (!bindings.ContainsKey(key))
            {
                bindings.Add(key, new Binding(key, value));
            }
            return bindings[key];
        }

        public void Changed()
        {
            lock (this)
            {
                plugin.OnActionImageChanged(null, null, true);
                foreach (INotifiable notifiable in notifiables)
                {
                    notifiable.Notify();
                }

            }
        }
    }
}

