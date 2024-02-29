﻿namespace Loupedeck.MsfsPlugin.input
{
    using System.Collections.Generic;

    public abstract class DefaultInput : PluginDynamicCommand, INotifiable
    {
        protected readonly List<Binding> bindings = new List<Binding>();
        protected readonly Binding bindingCnx = Register(BindingKeys.CONNECTION);
        protected static Binding Register(BindingKeys key, long? value = null) => MsfsData.Instance.Register(key, value);

        protected DefaultInput(string name, string desc, string category) : base(name, desc, category)
        {
            MsfsData.Instance.Register(this);
        }

        protected DefaultInput()
        {
            MsfsData.Instance.Register(this);
        }

        public void Notify()
        {
            foreach (Binding binding in bindings)
            {
                if (binding.HasMSFSChanged())
                {
                    binding.Reset();
                }
            }
        }

        protected override string GetCommandDisplayName(string actionParameter, PluginImageSize imageSize) => GetValue();
        protected override BitmapImage GetCommandImage(string actionParameter, PluginImageSize imageSize) => GetImage(imageSize);
        protected override void RunCommand(string actionParameter) => ChangeValue();
        protected virtual string GetValue() => null;
        protected virtual BitmapImage GetImage(PluginImageSize imageSize) => null;
        protected virtual void ChangeValue() { }
    }
}
