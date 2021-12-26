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
        private static readonly Offset<Int16> zoom = new Offset<Int16>(0x832C);

        private static readonly Offset<Int32> altitude = new Offset<Int32>(0x0574);
        private static readonly Offset<Int32> speed = new Offset<Int32>(0x02BC);
        private static readonly Offset<Int16> throttle1 = new Offset<Int16>(0x088C);
        private static readonly Offset<Int16> throttle2 = new Offset<Int16>(0x0924);
        private static readonly Offset<Int16> throttle3 = new Offset<Int16>(0x09BC);
        private static readonly Offset<Int16> throttle4 = new Offset<Int16>(0x0A54);
        private static readonly Offset<Int16> throttleLower = new Offset<Int16>(0x333A);

        private static readonly Offset<Int16> mixture1 = new Offset<Int16>(0x0890);
        private static readonly Offset<Int16> mixture2 = new Offset<Int16>(0x0928);
        private static readonly Offset<Int16> mixture3 = new Offset<Int16>(0x09C0);
        private static readonly Offset<Int16> mixture4 = new Offset<Int16>(0x0A58);

        private static readonly Offset<Int16> verticalSpeedAP = new Offset<Int16>(0x07F2);
        private static readonly Offset<Int16> compassAP = new Offset<Int16>(0x07CC);
        private static readonly Offset<Int32> altitudeAP = new Offset<Int32>(0x07D4);
        private static readonly Offset<Int32> speedAP = new Offset<Int32>(0x07E2);

        private static readonly Offset<Int32> apSwitch = new Offset<Int32>(0x07BC);
        private static readonly Offset<Int32> apThrottleSwitch = new Offset<Int32>(0x0810);
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
        private static readonly Offset<Int32> spoilerArm = new Offset<Int32>(0x0BD0);
        private static readonly Offset<Int32> spoilerPosition = new Offset<Int32>(0x0BD4);
        private static readonly Offset<Int16> aileronTrim = new Offset<Int16>(0x0C02);
        private static readonly Offset<Int16> rudderTrim = new Offset<Int16>(0x0C04);

        private static readonly Offset<Byte> gearOverSpeed = new Offset<Byte>(0x0B4F);
        private static readonly Offset<Int32> gearHandle = new Offset<Int32>(0x0BE8);
        private static readonly Offset<Int32> gearFront = new Offset<Int32>(0x0BEC);
        private static readonly Offset<Int32> gearLeft = new Offset<Int32>(0x0BF0);
        private static readonly Offset<Int32> gearRight = new Offset<Int32>(0x0BF4);

        private static readonly Offset<Int16> maxFlap = new Offset<Int16>(0x3BF8);
        private static readonly Offset<Int16> currentFlap = new Offset<Int16>(0x3BFA);

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
                    MsfsData.Instance.RefreshRate = 500;
                    timer.Interval = MsfsData.Instance.RefreshRate;
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
            timer.Interval = MsfsData.Instance.RefreshRate;
            try
            {
                if (FSUIPCConnection.IsOpen)
                {
                    lock (timer)
                    {
                        FSUIPCConnection.Process();
                        MsfsData.Instance.DebugValue = throttleLower.Value;
                        if (MsfsData.Instance.SetToMSFS)
                        {
                            verticalSpeedAP.Value = MsfsData.Instance.CurrentAPVerticalSpeed;
                            compassAP.Value = (Int16)(MsfsData.Instance.CurrentAPHeading * 182);
                            altitudeAP.Value = (Int32)(MsfsData.Instance.CurrentAPAltitude * 65536 / 3.28);
                            speedAP.Value = MsfsData.Instance.CurrentAPSpeed;
                            apSwitch.Value = MsfsData.Instance.ApSwitch;
                            apThrottleSwitch.Value = MsfsData.Instance.ApThrottleSwitch;
                            apAltHoldSwitch.Value = MsfsData.Instance.ApAltHoldSwitch;
                            apNavHoldSwitch.Value = MsfsData.Instance.ApNavHoldSwitch;
                            apVSHoldSwitch.Value = MsfsData.Instance.ApVSHoldSwitch;
                            apHeadHoldSwitch.Value = MsfsData.Instance.ApHeadHoldSwitch;
                            apSpeedHoldSwitch.Value = MsfsData.Instance.ApSpeedHoldSwitch;
                            parkingBrakes.Value = MsfsData.Instance.CurrentBrakes;
                            zoom.Value = (Int16)MsfsData.Instance.CurrentZoom;
                            mixture1.Value = (Int16)MsfsData.Instance.CurrentMixture;
                            mixture2.Value = (Int16)MsfsData.Instance.CurrentMixture;
                            mixture3.Value = (Int16)MsfsData.Instance.CurrentMixture;
                            mixture4.Value = (Int16)MsfsData.Instance.CurrentMixture;

                            if (MsfsData.Instance.CurrentThrottle < 0)
                            {
                                throttle1.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 4096d / 100);
                                throttle2.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 4096d / 100);
                                throttle3.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 4096d / 100);
                                throttle4.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 4096d / 100);
                            }
                            else
                            {
                                throttle1.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 16383d / 100);
                                throttle2.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 16383d / 100);
                                throttle3.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 16383d / 100);
                                throttle4.Value = (Int16)Math.Round(MsfsData.Instance.CurrentThrottle * 16383d / 100);
                            }
                            gearHandle.Value = MsfsData.Instance.CurrentGearHandle;
                            spoilerArm.Value = GetSpoiler(MsfsData.Instance.CurrentSpoiler);
                            aileronTrim.Value = MsfsData.Instance.CurrentAileronTrim;
                            rudderTrim.Value = MsfsData.Instance.CurrentRudderTrim;
                            currentFlap.Value = (Int16)(16383 / (maxFlap.Value+1) * MsfsData.Instance.CurrentFlap);
                            MsfsData.Instance.SetToMSFS = false;
                        }
                        else
                        {
                            MsfsData.Instance.CurrentAPVerticalSpeedFromMSFS = verticalSpeedAP.Value;
                            MsfsData.Instance.CurrentAPSpeedFromMSFS = (Int32)speedAP.Value;
                            MsfsData.Instance.CurrentAPHeadingFromMSFS = compassAP.Value / 182;
                            if (MsfsData.Instance.CurrentAPHeading <= 0)
                            {
                                MsfsData.Instance.CurrentAPHeadingFromMSFS += 360;
                            }
                            MsfsData.Instance.CurrentAPAltitudeFromMSFS = (Int32)Math.Round(altitudeAP.Value / 65536 * 3.28 / 10.0) * 10;
                            MsfsData.Instance.ApSwitchFromMSFS = apSwitch.Value;
                            MsfsData.Instance.ApThrottleSwitchFromMSFS = apThrottleSwitch.Value;
                            MsfsData.Instance.CurrentBrakesFromMSFS = parkingBrakes.Value;
                            MsfsData.Instance.CurrentThrottleFromMSFS = MsfsData.Instance.CurrentThrottle < 0 ? (Int16)(throttle1.Value * 100 / 4096) : (Int16)(throttle1.Value * 100 / 16383);
                            MsfsData.Instance.ThrottleLowerFromMSFS = throttleLower.Value;
                            MsfsData.Instance.CurrentGearHandleFromMSFS = gearHandle.Value;
                            MsfsData.Instance.ApAltHoldSwitchFromMSFS = apAltHoldSwitch.Value;
                            MsfsData.Instance.ApNavHoldSwitchFromMSFS = apNavHoldSwitch.Value;
                            MsfsData.Instance.ApVSHoldSwitchFromMSFS = apVSHoldSwitch.Value;
                            MsfsData.Instance.ApHeadHoldSwitchFromMSFS = apHeadHoldSwitch.Value;
                            MsfsData.Instance.ApSpeedHoldSwitchFromMSFS = apSpeedHoldSwitch.Value;
                            MsfsData.Instance.CurrentSpoilerFromMSFS = GetSpoilerFromMSFS(spoilerPosition.Value, spoilerArm.Value);
                            MsfsData.Instance.CurrentRudderTrimFromMSFS = rudderTrim.Value;
                            MsfsData.Instance.CurrentAileronTrimFromMSFS = aileronTrim.Value;
                            MsfsData.Instance.CurrentZoom = zoom.Value;
                            MsfsData.Instance.CurrentMixture = mixture1.Value;
                            MsfsData.Instance.CurrentFlap = (Int32)Math.Round(currentFlap.Value * (maxFlap.Value + 1) / 16383d);
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
                        MsfsData.Instance.ApNextWPHeading = apNextWPHeading.Value * 57.29;
                        MsfsData.Instance.ApNextWPID = apNextWPID.Value;
                        MsfsData.Instance.MaxFlap = maxFlap.Value + 1;

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
                MsfsData.Instance.RefreshRate = 2000;
            }
            if (FSUIPCConnection.IsOpen)
            {
                MsfsData.Instance.Connected = true;
                MsfsData.Instance.TryConnect = false;
                MsfsData.Instance.RefreshRate = 500;

            }
        }

        private static Int32 GetSpoilerFromMSFS(Int32 msfsValue, Int32 armValue) => armValue == 4800 ? -1 : msfsValue == 0 ? 0 : (Int32)Math.Round((Double)msfsValue / 16383 * 10);
        private static Int32 GetSpoiler(Int32 currentSpoiler) => currentSpoiler == 0 ? 0 : currentSpoiler == -1 ? 4800 : 5800 + currentSpoiler * (16383 - 5800) / 10;
    }
}
