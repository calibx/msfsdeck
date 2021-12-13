namespace Loupedeck.MsfsPlugin
{
    using System;

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using FSUIPC;


    class SimulatorDAO
    {
        private static readonly Offset<Int32> verticalSpeed = new Offset<Int32>(0x02C8);

        private static readonly Offset<Double> compass = new Offset<Double>(0x02CC);
        private static readonly Offset<Int32> fps = new Offset<Int32>(0x0274);
        private static readonly Offset<Int32> altitude = new Offset<Int32>(0x0574);
        private static readonly Offset<Int32> speed = new Offset<Int32>(0x02BC);

        private static readonly Offset<Int16> verticalSpeedAP = new Offset<Int16>(0x07F2);
        private static readonly Offset<Int16> compassAP = new Offset<Int16>(0x07CC);
        private static readonly Offset<Int32> altitudeAP = new Offset<Int32>(0x07D4);
        private static readonly Offset<Int32> speedAP = new Offset<Int32>(0x07E2);

        private static readonly Offset<Int32> apSwitch = new Offset<Int32>(0x07BC);
        private static readonly Offset<Int32> apAltHoldSwitch = new Offset<Int32>(0x07D0);
        private static readonly Offset<Int32> apHeadHoldSwitch = new Offset<Int32>(0x07C8);
        private static readonly Offset<Int32> apVSHoldSwitch = new Offset<Int32>(0x07EC);
        private static readonly Offset<Int32> apNavHoldSwitch = new Offset<Int32>(0x07C4);
        private static readonly Offset<Int32> apSpeedHoldSwitch = new Offset<Int32>(0x07DC);

        private static readonly Offset<Int32> apNextWPID = new Offset<Int32>(0x616C);
        private static readonly Offset<Int32> apNextWPETE = new Offset<Int32>(0x60E4);
        private static readonly Offset<Double> apNextWPDist = new Offset<Double>(0x6048);
        private static readonly Offset<Double> apNextWPHeading = new Offset<Double>(0x6050);

        private static readonly Offset<Int32> parkingBrakes = new Offset<Int32>(0x0BC8);
        private static readonly Offset<Int32> spoiler = new Offset<Int32>(0x0BD0);

        private static readonly Offset<Byte> gearOverSpeed = new Offset<Byte>(0x0B4F);
        private static readonly Offset<Int32> gearHandle = new Offset<Int32>(0x0BE8);
        private static readonly Offset<Int32> gearFront = new Offset<Int32>(0x0BEC);
        private static readonly Offset<Int32> gearLeft = new Offset<Int32>(0x0BF0);
        private static readonly Offset<Int32> gearRight = new Offset<Int32>(0x0BF4);

        private static readonly Timer timer = new System.Timers.Timer();

        public static void Initialise()
        {
            lock (timer)
            {
                if (!MsfsData.Instance.Connected && !MsfsData.Instance.TryConnect)
                {
                    try
                    {
                        MsfsData.Instance.TryConnect = true;
                        FSUIPCConnection.Open();
                    }
                    catch (FSUIPCException)
                    {
                    }
                    timer.Interval = 500;
                    timer.Enabled = true;
                    timer.Elapsed += refresh;

                }
            }
        }

        public static void Disconnect()
        {
            FSUIPCConnection.Close();
            timer.Enabled = false;
            MsfsData.Instance.Connected = false;
            MsfsData.Instance.TryConnect = false;
            MsfsData.Instance.changed();
        }


        public static void refresh(Object source, EventArgs e)
        {
            try
            {
                if (FSUIPCConnection.IsOpen)
                {
                    lock (timer)
                    {
                        FSUIPCConnection.Process();
                        if (MsfsData.Instance.SetToMSFS)
                        {
                            verticalSpeedAP.Value = (Int16)MsfsData.Instance.CurrentAPVerticalSpeed;
                            compassAP.Value = (Int16)(MsfsData.Instance.CurrentAPHeading * 182);
                            altitudeAP.Value = (Int32)(MsfsData.Instance.CurrentAPAltitude * 65536 / 3.28);
                            speedAP.Value = (Int32)MsfsData.Instance.CurrentAPSpeed;
                            apSwitch.Value = (Int32)MsfsData.Instance.ApSwitch;
                            apAltHoldSwitch.Value = (Int32)MsfsData.Instance.ApAltHoldSwitch;
                            apNavHoldSwitch.Value = (Int32)MsfsData.Instance.ApNavHoldSwitch;
                            apVSHoldSwitch.Value = (Int32)MsfsData.Instance.ApVSHoldSwitch;
                            apHeadHoldSwitch.Value = (Int32)MsfsData.Instance.ApHeadHoldSwitch;
                            apSpeedHoldSwitch.Value = (Int32)MsfsData.Instance.ApSpeedHoldSwitch;
                            parkingBrakes.Value = (Int32)MsfsData.Instance.CurrentBrakes;
                            gearHandle.Value = (Int32)MsfsData.Instance.CurrentGearHandle;
                            spoiler.Value = getSpoiler(MsfsData.Instance.CurrentSpoiler);
                            MsfsData.Instance.SetToMSFS = false;
                        }
                        else
                        {
                            MsfsData.Instance.CurrentAPVerticalSpeedFromMSFS = (Int32)verticalSpeedAP.Value;
                            MsfsData.Instance.CurrentAPSpeedFromMSFS = (Int32)speedAP.Value;
                            MsfsData.Instance.CurrentAPHeadingFromMSFS = compassAP.Value / 182;
                            if (MsfsData.Instance.CurrentAPHeading <= 0)
                            {
                                MsfsData.Instance.CurrentAPHeadingFromMSFS += 360;
                            }
                            MsfsData.Instance.CurrentAPAltitudeFromMSFS = (Int32)Math.Round(altitudeAP.Value / 65536 * 3.28 / 10.0) * 10;
                            MsfsData.Instance.ApSwitchFromMSFS = apSwitch.Value;
                            MsfsData.Instance.CurrentBrakesFromMSFS = parkingBrakes.Value;
                            MsfsData.Instance.CurrentGearHandleFromMSFS = gearHandle.Value;
                            MsfsData.Instance.ApAltHoldSwitchFromMSFS = apAltHoldSwitch.Value;
                            MsfsData.Instance.ApNavHoldSwitchFromMSFS = apNavHoldSwitch.Value;
                            MsfsData.Instance.ApVSHoldSwitchFromMSFS = apVSHoldSwitch.Value;
                            MsfsData.Instance.ApHeadHoldSwitchFromMSFS = apHeadHoldSwitch.Value;
                            MsfsData.Instance.ApSpeedHoldSwitchFromMSFS = apSpeedHoldSwitch.Value;
                            MsfsData.Instance.CurrentSpoilerFromMSFS = getSpoilerFromMSFS(spoiler.Value);
                        }

                        MsfsData.Instance.CurrentHeading = (Int32)compass.Value;
                        MsfsData.Instance.CurrentSpeed = (Int32)speed.Value / 128;
                        MsfsData.Instance.CurrentVerticalSpeed = (Int32)(verticalSpeed.Value * 60 * 3.28084 / 256);
                        MsfsData.Instance.CurrentAltitude = (Int32)(altitude.Value * 3.28);
                        MsfsData.Instance.Fps = 32768 / (fps.Value + 1);
                        MsfsData.Instance.GearOverSpeed = gearOverSpeed.Value;
                        MsfsData.Instance.GearLeft = gearLeft.Value;
                        MsfsData.Instance.GearFront = gearFront.Value;
                        MsfsData.Instance.GearRight = gearRight.Value;
                        MsfsData.Instance.ApNextWPDist = apNextWPDist.Value * 0.00053996d;
                        MsfsData.Instance.ApNextWPETE = apNextWPETE.Value;
                        MsfsData.Instance.ApNextWPHeading = (apNextWPHeading.Value * 57.29);
                        MsfsData.Instance.ApNextWPID = apNextWPID.Value;
                    }
                }
                else
                {
                    MsfsData.Instance.TryConnect = true;
                    MsfsData.Instance.Connected = false;
                    FSUIPCConnection.Open();
                }
            }
            catch (FSUIPCException)
            {
                MsfsData.Instance.Connected = false;
                timer.Interval = 2000;
            }
            if (FSUIPCConnection.IsOpen)
            {
                MsfsData.Instance.Connected = true;
                MsfsData.Instance.TryConnect = false;
                timer.Interval = 500;

            }
        }

        private static Int32 getSpoilerFromMSFS(Int32 value)
        {
            var result = 0;
            if (value == 4800)
            { result = 1; } else
            {
                result = 1 + (value - 5620 / (16383 - 5620));
            }
            return result;
        } 
        private static Int32 getSpoiler(Int32 currentSpoiler)
        {
            var result = 0;
            if (currentSpoiler == 1)
            {
                result = 4800;
            } else
            {
                result = 5620 + (currentSpoiler - 1) * (16383 - 5620) / 100;
            }
            return result;
        }
    }
}
