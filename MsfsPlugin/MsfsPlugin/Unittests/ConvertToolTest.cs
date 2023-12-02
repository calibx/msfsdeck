namespace Loupedeck.MsfsPlugin.Unittests
{
    using Loupedeck.MsfsPlugin.tools;

    using NUnit.Framework;

    [TestFixture]
    internal class ConvertToolTest
    {
        [Test]
        public void TestHdg()
        {
            const int min = 1;
            const int max = 360;
            const int step = 1;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(358), Is.EqualTo(359));
            Assert.That(Adjustment(359), Is.EqualTo(360));
            Assert.That(Adjustment(360), Is.EqualTo(1));
            Assert.That(Adjustment(1), Is.EqualTo(2));

            Assert.That(Adjustment(10, 20), Is.EqualTo(30));
            Assert.That(Adjustment(340, 20), Is.EqualTo(360));
            Assert.That(Adjustment(350, 20), Is.EqualTo(10));
            Assert.That(Adjustment(50, 0), Is.EqualTo(50));
            Assert.That(Adjustment(50, 360), Is.EqualTo(50));
            Assert.That(Adjustment(50, 720), Is.EqualTo(50));
            Assert.That(Adjustment(50, 750), Is.EqualTo(80));

            Assert.That(Adjustment(2, -1), Is.EqualTo(1));
            Assert.That(Adjustment(1, -1), Is.EqualTo(360));
            Assert.That(Adjustment(360, -1), Is.EqualTo(359));

            Assert.That(Adjustment(50, -50), Is.EqualTo(360));
            Assert.That(Adjustment(10, -360), Is.EqualTo(10));
            Assert.That(Adjustment(10, -720), Is.EqualTo(10));
        }

        [Test]
        public void TestCourse()
        {
            const int min = 0;
            const int max = 359;
            const int step = 1;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(358), Is.EqualTo(359));
            Assert.That(Adjustment(359), Is.EqualTo(0));
            Assert.That(Adjustment(0), Is.EqualTo(1));
            Assert.That(Adjustment(1), Is.EqualTo(2));

            Assert.That(Adjustment(10, 20), Is.EqualTo(30));
            Assert.That(Adjustment(339, 20), Is.EqualTo(359));
            Assert.That(Adjustment(345, 20), Is.EqualTo(5));
            Assert.That(Adjustment(0, 359), Is.EqualTo(359));
            Assert.That(Adjustment(0, 360), Is.EqualTo(0));
            Assert.That(Adjustment(50, 360), Is.EqualTo(50));
            Assert.That(Adjustment(50, 720), Is.EqualTo(50));
            Assert.That(Adjustment(50, 750), Is.EqualTo(80));

            Assert.That(Adjustment(2, -1), Is.EqualTo(1));
            Assert.That(Adjustment(1, -1), Is.EqualTo(0));
            Assert.That(Adjustment(0, -1), Is.EqualTo(359));

            Assert.That(Adjustment(50, -50), Is.EqualTo(0));
            Assert.That(Adjustment(50, -51), Is.EqualTo(359));
            Assert.That(Adjustment(10, -360), Is.EqualTo(10));
            Assert.That(Adjustment(10, -720), Is.EqualTo(10));
        }

        [Test]
        public void TestNavIntegers()
        {
            const int min = 108;
            const int max = 117;
            const int step = 1;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(115), Is.EqualTo(116));
            Assert.That(Adjustment(116), Is.EqualTo(117));
            Assert.That(Adjustment(117), Is.EqualTo(108));
            Assert.That(Adjustment(108), Is.EqualTo(109));

            Assert.That(Adjustment(109, -4), Is.EqualTo(115));
        }

        [Test]
        public void TestNavDecimals()
        {
            const int min = 0;
            const int max = 95;
            const int step = 5;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(90), Is.EqualTo(95));
            Assert.That(Adjustment(95), Is.EqualTo(0));
            Assert.That(Adjustment(0), Is.EqualTo(5));

            Assert.That(Adjustment(15, -5), Is.EqualTo(90));
        }

        [Test]
        public void TestComIntegers()
        {
            const int min = 118;
            const int max = 136;
            const int step = 1;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(135), Is.EqualTo(136));
            Assert.That(Adjustment(136), Is.EqualTo(118));
            Assert.That(Adjustment(118), Is.EqualTo(119));

            Assert.That(Adjustment(120, -3), Is.EqualTo(136));
        }

        [Test]
        public void TestComDecimals()
        {
            const int min = 0;
            const int max = 995;
            const int step = 5;
            const bool cycle = true;

            long Adjustment(long value, int ticks = 1) => ConvertTool.ApplyAdjustment(value, ticks, min, max, step, cycle);

            Assert.That(Adjustment(990), Is.EqualTo(995));
            Assert.That(Adjustment(995), Is.EqualTo(0));
            Assert.That(Adjustment(0), Is.EqualTo(5));

            Assert.That(Adjustment(15, -6), Is.EqualTo(985));
        }
    }
}
