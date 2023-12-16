namespace Loupedeck.MsfsPlugin.tools
{
    internal class DecimalValueAdjuster
    {
        public DecimalValueAdjuster(int minInt, int maxInt, int minDecimal, int maxDecimal, int stepDecimal, int representationValueDivider)
        {
            this.minInt = minInt;
            this.maxInt = maxInt;
            this.minDecimal = minDecimal;
            this.maxDecimal = maxDecimal;
            this.stepDecimal = stepDecimal;
            intDivider = maxDecimal + stepDecimal;
            valueDivider = representationValueDivider / intDivider;
        }

        public long IncrIntValue(long presentValue, int ticks)
        {
            var actualValue = presentValue / valueDivider;
            var intValue = actualValue / intDivider;       // Truncate decimal part away
            var decimals = actualValue % intDivider;

            return EncodeValues(ConvertTool.ApplyAdjustment(intValue, ticks, minInt, maxInt, 1, true), decimals);
        }

        public long IncrDecimalValue(long presentValue, int ticks)
        {
            var actualValue = presentValue / valueDivider;
            var intValue = actualValue / intDivider;       // Truncate decimal part away
            var decimals = actualValue % intDivider;

            if (intValue == 0 && decimals == 0)
            {
                intValue = minInt;   // Recover if an illegal value crept into the system
            }

            return EncodeValues(intValue, ConvertTool.ApplyAdjustment(decimals, ticks, minDecimal, maxDecimal, stepDecimal, true));
        }

        long EncodeValues(long intValue, long decimalValue) =>
            (intValue * intDivider + decimalValue) * valueDivider;

        readonly int minInt;
        readonly int maxInt;
        readonly int minDecimal;
        readonly int maxDecimal;
        readonly int stepDecimal;
        readonly int valueDivider;
        readonly int intDivider;
    }
}
