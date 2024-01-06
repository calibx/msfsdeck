namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class PushbackInput : DefaultInput
    {
        public PushbackInput() : base("Pushback", "Pushback", "Misc")
        {
            bindings.Add(Register(BindingKeys.PUSHBACK_STATE));
            bindings.Add(Register(BindingKeys.PUSHBACK_ATTACHED));
            bindings.Add(Register(BindingKeys.PUSHBACK_CONTROLLER, 3));
        }

        protected override void ChangeValue()
        {
            if (bindings[1].MsfsValue == 1)
            {
                bindings[2].SetControllerValue((bindings[2].ControllerValue + 1) % 4);
            }
            else
            {
                bindings[2].SetControllerValue(bindings[2].ControllerValue == 0 ? 3 : 0);
            }
        }
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (bindings[0].MsfsValue == 0)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(0));
                }
                else if (bindings[1].MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(2));
                }
                else if (bindings[2].ControllerValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(4));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(3));
                }

                switch (bindings[2].ControllerValue)
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
