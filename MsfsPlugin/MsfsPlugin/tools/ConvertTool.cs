namespace Loupedeck.MsfsPlugin.tools
{
    using System;

    public static class ConvertTool
    {
        // Conversion factors

        public const double inHg2mbar = 33.86388158;   // Note also that 1 mbar == 1 hPa

        public static uint RoundToUint(double value) => (uint)Round(value, 0);

        public static long RoundToLong(double value) => (long)Round(value, 0);

        public static double Round(double value, int digits) => Math.Round(value, digits, MidpointRounding.AwayFromZero);

        public static bool GetBoolean(long value) => value != 0;

        public static long GetToggledValue(long value) => value == 0 ? 1 : 0;

        public static long ApplyAdjustment(long value, int ticks, int min, int max, int step, bool cycle = false)
        {
            var result = value + ticks * step;
            if (cycle)
            {
                int adjuster = max - min + step;
                while (result > max)
                {
                    result -= adjuster;
                }
                while (result < min)
                {
                    result += adjuster;
                }
            }
            else
            {
                if (result < min)
                {
                    return min;
                }
                else if (result > max)
                {
                    return max;
                }
            }
            return result;
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
