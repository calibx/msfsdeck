namespace Loupedeck.MsfsPlugin
{

    using Loupedeck.MsfsPlugin.input;
    using Loupedeck.MsfsPlugin.tools;
    class SpoilerArmInput : DefaultInput
    {
        public SpoilerArmInput() : base("SpoilerArm", "Spoiler Arm", "Misc")
        {
            this._bindings.Add(MsfsData.Instance.Register(new Binding(BindingKeys.SPOILERS_ARM)));
        }
        protected override void ChangeValue() => this._bindings[0].SetControllerValue(1);
        protected override BitmapImage GetImage(PluginImageSize imageSize)
        {
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.SetBackgroundImage(ImageTool.GetOnOffImage(this._bindings[0].ControllerValue));
                bitmapBuilder.DrawText("Spoiler Arm");
                return bitmapBuilder.ToImage();
            }
        }
    }
}

