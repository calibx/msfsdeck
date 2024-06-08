namespace Loupedeck.MsfsPlugin
{
    using Loupedeck.MsfsPlugin.tools;

    internal static class BindingExtensions
    {
        public static void ToggleControllerValue(this Binding binding) => binding.SetControllerValue(ConvertTool.GetToggledValue(binding.ControllerValue));
    }
}
