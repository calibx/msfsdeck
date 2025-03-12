namespace Loupedeck.MsfsPlugin.input
{
    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    public abstract class DefaultInput : PluginDynamicCommand, INotifiable
    {
        protected DefaultInput(string name, string desc, string category) : base(name, desc, category)
        {
            entity = new CommonEntity();
            MsfsData.Instance.Register(this);
        }

        protected DefaultInput()
        {
            entity = new CommonEntity();
            MsfsData.Instance.Register(this);
        }

        protected static Binding Register(BindingKeys key, long? value = null) => MsfsData.Instance.Register(key, value);   //>> Can be removed when all inputs declare individual bindings

        protected Binding Bind(BindingKeys key, long? value = null) => entity.Bind(key, value);

        public void Notify(bool force)
        {
            var refresh = false;
            foreach (Binding binding in entity.bindings)
            {
                
                if (binding.HasMSFSChanged() || force)
                {
                    binding.Reset();
                    refresh = true;
                }
            }
            if (refresh)
            {
                DebugTracing.Trace("input " + this.ToString());
                ActionImageChanged();
            }
        }

        protected override string GetCommandDisplayName(string actionParameter, PluginImageSize imageSize) => GetValue();
        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize) => GetImage(imageSize);
        protected override void RunCommand(string actionParameter) => ChangeValue();
        protected virtual string GetValue() => null;
        protected virtual BitmapImage GetImage(PluginImageSize imageSize) => null;
        protected virtual void ChangeValue() { }

        readonly CommonEntity entity;
    }
}
