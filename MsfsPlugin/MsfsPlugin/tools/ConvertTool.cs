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

        public static String IntToCOMStatus(Int64 comStatus)
        {
            String type;
            switch (comStatus)
            {
                case -1:
                    type = "INVALID";
                    break;
                case 0:
                    type = "OK";
                    break;
                case 1:
                    type = "NOT EXIST";
                    break;
                case 2:
                    type = "NO ELEC";
                    break;
                case 3:
                    type = "FAILED";
                    break;
                default:
                    type = "Unknown";
                    break;
            }
            return type;
        }
        public static String IntToCOMType(Int64 comType)
        {
            String type;
            switch (comType)
            {
                case 0:
                    type = "ATIS";
                    break;
                case 1:
                    type = "UNICOM";
                    break;
                case 2:
                    type = "CTAF";
                    break;
                case 3:
                    type = "GROUND";
                    break;
                case 4:
                    type = "TOWER";
                    break;
                case 5:
                    type = "CLR";
                    break;
                case 6:
                    type = "APPR";
                    break;
                case 7:
                    type = "DEP";
                    break;
                case 8:
                    type = "FSS";
                    break;
                case 9:
                    type = "AWOS";
                    break;
                default:
                    type = "Unknow";
                    break;
            }
            return type;
        }
    }
}
