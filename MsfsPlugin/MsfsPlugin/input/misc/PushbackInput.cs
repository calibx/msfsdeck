namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class PushbackInput : DefaultInput
    {
        public PushbackInput() : base("Pushback", "Pushback", "Misc")
        {
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.PUSHBACK_STATE));
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.PUSHBACK_ATTACHED));
            _bindings.Add(MsfsData.Instance.Register(BindingKeys.PUSHBACK_CONTROLLER, 3));
        }
        protected override void ChangeValue()
        {
            if (_bindings[1].MsfsValue == 1)
            {
                _bindings[2].SetControllerValue((_bindings[2].ControllerValue + 1) % 4);
            }
            else
            {
                _bindings[2].SetControllerValue(_bindings[2].ControllerValue == 0 ? 3 : 0);
            }
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (_bindings[0].MsfsValue == 0)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(0));
                }
                else if (_bindings[1].MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(2));
                }
                else if (_bindings[2].ControllerValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(4));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(3));
                }

                switch (_bindings[2].ControllerValue)
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


