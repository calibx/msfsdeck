namespace Loupedeck.MsfsPlugin.tools
{
    using System;

    public class ConvertTool
    {

        public static Int64 ApplyAdjustment(Int64 value, Int32 ticks, Int32 min, Int32 max, Int32 step, Boolean cycle = false)
        {
            value += ticks * step;
            if (value < min)
            {
                value = cycle ? max : min;
            }
            else if (value > max)
            {
                value = cycle ? min : max;
            }
            return value;
        }
    }
}
