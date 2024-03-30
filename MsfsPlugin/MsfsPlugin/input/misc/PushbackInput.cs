namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;

    class PushbackInput : DefaultInput
    {
        public PushbackInput() : base("Pushback", "Pushback", "Misc")
        {
            state = Bind(BindingKeys.PUSHBACK_STATE);
            attached = Bind(BindingKeys.PUSHBACK_ATTACHED);
            controller = Bind(BindingKeys.PUSHBACK_CONTROLLER, 3);
        }

        protected override void ChangeValue()
        {
            if (attached.MsfsValue == 1)
            {
                controller.SetControllerValue((controller.ControllerValue + 1) % 4);
            }
            else
            {
                controller.SetControllerValue(controller.ControllerValue == 0 ? 3 : 0);
            }
        }

        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                if (state.MsfsValue == 0)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(0));
                }
                else if (attached.MsfsValue == 1)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(2));
                }
                else if (controller.ControllerValue == 3)
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(4));
                }
                else
                {
                    bitmapBuilder.SetBackgroundImage(ImageTool.GetOnAvailableWaitDisableImage(3));
                }

                switch (controller.ControllerValue)
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

        readonly Binding state;
        readonly Binding attached;
        readonly Binding controller;
    }
}
