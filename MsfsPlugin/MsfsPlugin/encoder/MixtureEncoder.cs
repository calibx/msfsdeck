﻿namespace Loupedeck.MsfsPlugin
{
    using System;

    using Loupedeck.MsfsPlugin.encoder;

    class MixtureEncoder : DefaultEncoder
    {
        public MixtureEncoder() : base("Mixture", "Mixture encoder for the 4 engines", "Nav", true, 0, 100, 1)
        {
            var bind = new Binding(BindingKeys.MIXTURE);
            this._bindings.Add(bind);
            MsfsData.Instance.Register(bind);
        }
        protected override void RunCommand(String actionParameter) => this.SetValue(0);
        protected override Int64 GetValue() => this._bindings[0].ControllerValue;
        protected override void SetValue(Int64 newValue) => this._bindings[0].SetControllerValue(newValue);
    }
}
