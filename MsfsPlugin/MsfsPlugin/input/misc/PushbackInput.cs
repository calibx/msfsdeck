namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;

    class PushbackInput : DefaultInput
    {
        public PushbackInput() : base("Pushback", "Pushback", "Misc")
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PUSHBACK_STATE)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PUSHBACK_ATTACHED)));
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.PUSHBACK_CONTROLLER)));
            this._bindings[2].ControllerValue = 3;
        }
        protected override void ChangeValue()
        {
            if (this._bindings[1].MsfsValue == 1)
            {
                this._bindings[2].SetControllerValue((this._bindings[2].ControllerValue + 1) % 4);
            }
            else
            {
                this._bindings[2].SetControllerValue(this._bindings[2].ControllerValue == 0 ? 3 : 0);
            }
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (this._bindings[0].MsfsValue == 0)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageDisableResourcePath));
                }
                else if (this._bindings[1].MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOnResourcePath));
                }
                else if (this._bindings[2].ControllerValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageOffResourcePath));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(this._imageWaitResourcePath));
                }

                switch (this._bindings[2].ControllerValue)
                {
                    case 0:
                        bitmapBuilder.DrawText("Pushback straight");
                        break;
                    case 1:
                        bitmapBuilder.DrawText("Pushback left");
                        break;
                    case 2:
                        bitmapBuilder.DrawText("Pushback right");
                        break;
                    case 3:
                        bitmapBuilder.DrawText("Pushback");
                        break;
                }
                return bitmapBuilder.ToImage();
            }
        }

    }
}


