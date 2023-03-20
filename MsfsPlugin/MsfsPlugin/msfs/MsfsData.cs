namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    class MsfsData
    {
        private readonly List<Notifiable> notifiables = new List<Notifiable>();

        public Dictionary<BindingKeys, Binding> bindings = new Dictionary<BindingKeys, Binding>();

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());
        public static MsfsData Instance => lazy.Value;
        public MSFSPlugin plugin { get; set; }
        public Boolean DEBUG { get; set; }
        public UInt64 MessageId { get; set; }
        public String AircraftName { get; set; }
        public String DebugValue1 { get; set; }
        public String DebugValue2 { get; set; }
        public String DebugValue3 { get; set; }
        private MsfsData()
        {
            this.MessageId = 0;
        }

        public void Register(Notifiable notif) => this.notifiables.Add(notif);

        public Binding Register(Binding binding)
        {
            if (!this.bindings.ContainsKey(binding.Key))
            {
                this.bindings.Add(binding.Key, binding);
            }
            return this.bindings[binding.Key];
        }

        public void Changed()
        {
            lock (this)
            {
                this.MessageId++;
                this.plugin.OnActionImageChanged(null, null, true);
                foreach (Notifiable notifiable in this.notifiables)
                {
                    notifiable.Notify();
                }

            }
        }
    }
}

