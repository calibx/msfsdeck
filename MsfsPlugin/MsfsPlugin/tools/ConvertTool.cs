namespace Loupedeck.MsfsPlugin.tools
{
    public static class ConvertTool
    {
        public static long getToggledValue(long value) => value == 0 ? 1 : 0;

        public static long ApplyAdjustment(long value, int ticks, int min, int max, int step, bool cycle = false)
        {
            //Debug.WriteLine($"ConvertTool.ApplyAdjustment({value}, {ticks}, {min}, {max}, {step}, {cycle})");
            value += ticks * step;
            if (value < min)
            {
                value = cycle ? value + max - min + 1 : min;
            }
            else if (value > max)
            {
                value = cycle ? value - max + min - 1 : max;
            }
            return value;
        }

        public static string IntToCOMStatus(long comStatus)
        {
            string type;
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

        public static string IntToCOMType(long comType)
        {
            string type;
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
