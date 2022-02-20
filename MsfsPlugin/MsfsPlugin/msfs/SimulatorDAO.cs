namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using FSUIPC;


    class SimulatorDAO
    {
        private static readonly Offset<Int32> verticalSpeed = new Offset<Int32>(0x02C8);

        private static readonly Offset<Double> compass = new Offset<Double>(0x02CC);
        private static readonly Offset<Double> debug1 = new Offset<Double>(0x34A0);
        private static readonly Offset<Double> debug2 = new Offset<Double>(0x0540);
        private static readonly Offset<Double> debug3 = new Offset<Double>(0x0548);

        private static readonly Offset<Int16> pause = new Offset<Int16>(0x0262);
        private static readonly Offset<Int32> fps = new Offset<Int32>(0x0274);

        private static readonly Offset<Int16> barometer = new Offset<Int16>(0x0330);

        private static readonly Offset<Int16> fuelWeightLeft = new Offset<Int16>(0x126C);
        private static readonly Offset<Int16> fuelQuantityLeft = new Offset<Int16>(0x1264);
        private static readonly Offset<Int16> fuelCapacity = new Offset<Int16>(0x1240);
        private static readonly Offset<Double> fuelWeightFlowE1 = new Offset<Double>(0x0918);
        private static readonly Offset<Double> fuelWeightFlowE2 = new Offset<Double>(0x09B0);
        private static readonly Offset<Double> fuelWeightFlowE3 = new Offset<Double>(0x0A48);
        private static readonly Offset<Double> fuelWeightFlowE4 = new Offset<Double>(0x0AE0);


        private static readonly Offset<String> aircraftName = new Offset<String>(0x3D00, 256);
        private static readonly Offset<String> airportsNear = new Offset<String>(0x0658, 120);
        private static readonly Offset<Byte> engineType = new Offset<Byte>(0x0609);
        private static readonly Offset<Double> rpm = new Offset<Double>(0x2400);
        private static readonly Offset<Double> E1N1 = new Offset<Double>(0x2010);
        private static readonly Offset<Double> E2N1 = new Offset<Double>(0x2110);
        private static readonly Offset<Double> E3N1 = new Offset<Double>(0x2210);
        private static readonly Offset<Double> E4N1 = new Offset<Double>(0x2310);
        private static readonly Offset<Int16> numberOfEngines = new Offset<Int16>(0x0AEC);


        private static readonly Offset<Int16> zoom = new Offset<Int16>(0x8336);
        private static readonly Offset<Int16> light = new Offset<Int16>(0x0D0C);
        private static readonly Offset<Int16> pushback = new Offset<Int16>(0x31F0);
        private static readonly Offset<Int16> pushbackState = new Offset<Int16>(0x31F4);
        
        private static readonly Offset<Int32> altitude = new Offset<Int32>(0x3324);
        private static readonly Offset<Int32> speed = new Offset<Int32>(0x02BC);
        private static readonly Offset<Int16> throttle1 = new Offset<Int16>(0x088C);
        private static readonly Offset<Int16> throttle2 = new Offset<Int16>(0x0924);
        private static readonly Offset<Int16> throttle3 = new Offset<Int16>(0x09BC);
        private static readonly Offset<Int16> throttle4 = new Offset<Int16>(0x0A54);
        private static readonly Offset<Int16> throttleLower = new Offset<Int16>(0x333A);

        private static readonly Offset<Int16> propeller1 = new Offset<Int16>(0x088E);
        private static readonly Offset<Int16> propeller2 = new Offset<Int16>(0x0926);
        private static readonly Offset<Int16> propeller3 = new Offset<Int16>(0x09BE);
        private static readonly Offset<Int16> propeller4 = new Offset<Int16>(0x0A56);

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
        private static readonly Offset<Int16> elevatorTrim = new Offset<Int16>(0x0BC0);

        private static readonly Offset<Byte> gearOverSpeed = new Offset<Byte>(0x0B4F);
        private static readonly Offset<Int32> gearHandle = new Offset<Int32>(0x0BE8);
        private static readonly Offset<Int32> gearFront = new Offset<Int32>(0x0BEC);
        private static readonly Offset<Int32> gearLeft = new Offset<Int32>(0x0BF0);
        private static readonly Offset<Int32> gearRight = new Offset<Int32>(0x0BF4);
        private static readonly Offset<Byte> gearRetractable = new Offset<Byte>(0x060C);


        private static readonly Offset<Byte> pitot = new Offset<Byte>(0x029C);
        private static readonly Offset<Int16> masterSwitch = new Offset<Int16>(0x281C);


        private static readonly Offset<Int16> maxFlap = new Offset<Int16>(0x3BF8);
        private static readonly Offset<Int16> currentFlap = new Offset<Int16>(0x0BDC);

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();

        private static readonly List<String> invertedCabinLightAircraftsPatterns = new List<String>() { "Airbus A320 Neo.*", "DA40-NG.*", "Bonanza G36.*", "TBM 930.*", "Kodiak 100.*", "Boeing 787-10.*" };

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
                    MsfsData.Instance.RefreshRate = 100;
                    timer.Interval = MsfsData.Instance.RefreshRate;
                    timer.Enabled = true;
                    timer.Elapsed += Refresh;

                }
            }
        }

        public static void Disconnect()
        {
            FSUIPCConnection.Close();
            timer.Enabled = false;
            MsfsData.Instance.Connected = false;
            MsfsData.Instance.TryConnect = false;
            MsfsData.Instance.Changed();
        }


        public static void Refresh(Object source, EventArgs e)
        {

            try
            {
                if (FSUIPCConnection.IsOpen)
                {
                    lock (timer)
                    {
                        timer.Interval = MsfsData.Instance.RefreshRate;
                        FSUIPCConnection.Process();
                        MsfsData.Instance.DebugValue1 =  (debug1.Value /16).ToString();
                        MsfsData.Instance.DebugValue2 = (debug2.Value / 1.68d).ToString();
                        MsfsData.Instance.DebugValue3 = ((Int32)(debug3.Value / 1.69d)).ToString();

                        if (MsfsData.Instance.SetToMSFS)
                        {
                            pause.Value = (Int16)(MsfsData.Instance.Pause ? 1 : 0);
                            barometer.Value = (Int16)(MsfsData.Instance.Barometer * 16);
                            pushbackState.Value = MsfsData.Instance.Pushback;
                            verticalSpeedAP.Value = (Int16)MsfsData.Instance.CurrentAPVerticalSpeed;
                            compassAP.Value = (Int16)(MsfsData.Instance.CurrentAPHeading * 182);
                            altitudeAP.Value = (Int32)(MsfsData.Instance.CurrentAPAltitude * 65536 / 3.28);
                            speedAP.Value = MsfsData.Instance.CurrentAPSpeed;
                            apSwitch.Value = MsfsData.Instance.ApSwitch ? 1 : 0;
                            apThrottleSwitch.Value = MsfsData.Instance.ApThrottleSwitch ? 1 : 0;
                            apAltHoldSwitch.Value = MsfsData.Instance.ApAltHoldSwitch ? 1 : 0;
                            apNavHoldSwitch.Value = MsfsData.Instance.ApNavHoldSwitch ? 1 : 0;
                            apVSHoldSwitch.Value = MsfsData.Instance.ApVSHoldSwitch ? 1 : 0;
                            apHeadHoldSwitch.Value = MsfsData.Instance.ApHeadHoldSwitch ? 1 : 0;
                            apSpeedHoldSwitch.Value = MsfsData.Instance.ApSpeedHoldSwitch ? 1 : 0;
                            parkingBrakes.Value = MsfsData.Instance.CurrentBrakes ? 32767 : 0;
                            zoom.Value = (Int16)MsfsData.Instance.CurrentZoom;
                            light.Value = GetLights();
                            mixture1.Value = (Int16)Math.Round(MsfsData.Instance.CurrentMixture / 100d * 16383);
                            mixture2.Value = (Int16)Math.Round(MsfsData.Instance.CurrentMixture / 100d * 16383);
                            mixture3.Value = (Int16)Math.Round(MsfsData.Instance.CurrentMixture / 100d * 16383);
                            mixture4.Value = (Int16)Math.Round(MsfsData.Instance.CurrentMixture / 100d * 16383);
                            if (MsfsData.Instance.CurrentPropeller < 0)
                            {
                                propeller1.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 4096d / 100);
                                propeller2.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 4096d / 100);
                                propeller3.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 4096d / 100);
                                propeller4.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 4096d / 100);
                            }
                            else
                            {
                                propeller1.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 16383d / 100);
                                propeller2.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 16383d / 100);
                                propeller3.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 16383d / 100);
                                propeller4.Value = (Int16)Math.Round(MsfsData.Instance.CurrentPropeller * 16383d / 100);
                            }

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
                            aileronTrim.Value = (Int16)Math.Round(MsfsData.Instance.CurrentAileronTrim / 100d * 16383);
                            rudderTrim.Value = (Int16)Math.Round(MsfsData.Instance.CurrentRudderTrim / 100d * 16383);
                            elevatorTrim.Value = (Int16)Math.Round(MsfsData.Instance.CurrentElevatorTrim / 100d * 16383);
                            if (maxFlap.Value != 0)
                            {
                                currentFlap.Value = (Int16)(16383 / maxFlap.Value * MsfsData.Instance.CurrentFlap);
                            }
                            pitot.Value = MsfsData.Instance.CurrentPitot ? (Byte)1 : (Byte)0;
                            masterSwitch.Value = (Int16)(MsfsData.Instance.MasterSwitch ? 1 : 0);
                            MsfsData.Instance.SetToMSFS = false;
                        }
                        else
                        {
                            MsfsData.Instance.BarometerFromMSFS = (Int16)Math.Round(barometer.Value / 16d);
                            MsfsData.Instance.PauseFromMSFS = pause.Value != 0;
                            MsfsData.Instance.PushbackFromMSFS = pushback.Value;
                            MsfsData.Instance.CurrentAPVerticalSpeedFromMSFS = verticalSpeedAP.Value;
                            MsfsData.Instance.CurrentAPSpeedFromMSFS = (Int32)speedAP.Value;
                            MsfsData.Instance.CurrentAPHeadingFromMSFS = compassAP.Value / 182;
                            if (MsfsData.Instance.CurrentAPHeadingFromMSFS <= 0)
                            {
                                MsfsData.Instance.CurrentAPHeadingFromMSFS += 360;
                            }
                            MsfsData.Instance.CurrentAPAltitudeFromMSFS = (Int32)Math.Round(altitudeAP.Value / 65536 * 3.28 / 10.0) * 10;
                            MsfsData.Instance.ApSwitchFromMSFS = apSwitch.Value == 1;
                            MsfsData.Instance.ApThrottleSwitchFromMSFS = apThrottleSwitch.Value == 1;
                            MsfsData.Instance.CurrentBrakesFromMSFS = FSUIPCConnection.ReadLVar("ParkingBrake_Position") == 100;
                            MsfsData.Instance.CurrentThrottleFromMSFS = throttle1.Value < 0 ? (Int16)(throttle1.Value * 100 / 4096) : (Int16)(throttle1.Value * 100 / 16383);
                            MsfsData.Instance.ThrottleLowerFromMSFS = throttleLower.Value;
                            MsfsData.Instance.CurrentGearHandleFromMSFS = gearHandle.Value;
                            MsfsData.Instance.ApAltHoldSwitchFromMSFS = apAltHoldSwitch.Value == 1;
                            MsfsData.Instance.ApNavHoldSwitchFromMSFS = apNavHoldSwitch.Value == 1;
                            MsfsData.Instance.ApVSHoldSwitchFromMSFS = apVSHoldSwitch.Value == 1;
                            MsfsData.Instance.ApHeadHoldSwitchFromMSFS = apHeadHoldSwitch.Value == 1;
                            MsfsData.Instance.ApSpeedHoldSwitchFromMSFS = apSpeedHoldSwitch.Value == 1;
                            MsfsData.Instance.CurrentSpoilerFromMSFS = GetSpoilerFromMSFS(spoilerPosition.Value, spoilerArm.Value);
                            MsfsData.Instance.CurrentRudderTrimFromMSFS = (Int16)Math.Round(rudderTrim.Value / 16383d * 100);
                            MsfsData.Instance.CurrentAileronTrimFromMSFS = (Int16)Math.Round(aileronTrim.Value / 16383d * 100);
                            MsfsData.Instance.CurrentElevatorTrimFromMSFS = (Int16)Math.Round(elevatorTrim.Value / 16383d * 100);
                            MsfsData.Instance.CurrentMixtureFromMSFS = (Int32)Math.Round(mixture1.Value / 16383d * 100);
                            MsfsData.Instance.CurrentPropellerFromMSFS = propeller1.Value < 0 ? (Int16)(propeller1.Value * 100 / 4096) : (Int16)(propeller1.Value * 100 / 16383);
                            MsfsData.Instance.CurrentFlapFromMSFS = (Int32)Math.Round(currentFlap.Value * maxFlap.Value / 16383d);
                            MsfsData.Instance.CurrentPitotFromMSFS = pitot.Value == 1;
                            MsfsData.Instance.MasterSwitchFromMSFS = masterSwitch.Value == 1;
                            GetLightsFromMSFS(light.Value);
                        }

                        MsfsData.Instance.CurrentHeading = (Int32)compass.Value;
                        MsfsData.Instance.AircraftName = aircraftName.Value;
                        MsfsData.Instance.CurrentSpeed = (Int32)speed.Value / 128;
                        MsfsData.Instance.CurrentVerticalSpeed = (Int32)(verticalSpeed.Value * 60 * 3.28084 / 256);
                        MsfsData.Instance.CurrentAltitude = (Int32)altitude.Value;
                        MsfsData.Instance.Fps = 32768 / (fps.Value + 1);
                        MsfsData.Instance.GearOverSpeed = gearOverSpeed.Value;
                        MsfsData.Instance.GearLeft = gearLeft.Value;
                        MsfsData.Instance.GearFront = gearFront.Value;
                        MsfsData.Instance.GearRight = gearRight.Value;
                        MsfsData.Instance.GearRetractable = gearRetractable.Value;
                        MsfsData.Instance.ApNextWPDist = apNextWPDist.Value * 0.00053996d;
                        MsfsData.Instance.ApNextWPETE = apNextWPETE.Value;
                        MsfsData.Instance.ApNextWPHeading = apNextWPHeading.Value * 57.29;
                        MsfsData.Instance.ApNextWPID = apNextWPID.Value;
                        MsfsData.Instance.MaxFlap = maxFlap.Value + 1;
                        MsfsData.Instance.Rpm = (Int16)Math.Round(rpm.Value);
                        MsfsData.Instance.EngineType = engineType.Value;
                        MsfsData.Instance.E1N1 = Math.Round(E1N1.Value, 1);
                        MsfsData.Instance.E2N1 = Math.Round(E2N1.Value, 1);
                        MsfsData.Instance.E3N1 = Math.Round(E3N1.Value, 1);
                        MsfsData.Instance.E4N1 = Math.Round(E4N1.Value, 1);
                        MsfsData.Instance.NumberOfEngines = numberOfEngines.Value;
                        MsfsData.Instance.FuelFlow = (Int32)(fuelWeightFlowE1.Value + fuelWeightFlowE2.Value + fuelWeightFlowE3.Value + fuelWeightFlowE4.Value);
                        MsfsData.Instance.FuelPercent = (Int32)Math.Round(fuelQuantityLeft.Value * 100d / fuelCapacity.Value);
                        MsfsData.Instance.FuelTimeLeft = MsfsData.Instance.FuelFlow != 0 ? (Int32)Math.Round((Double)fuelWeightLeft.Value * 3600 / MsfsData.Instance.FuelFlow) : 0;

                        SendControls();
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
            }
            MsfsData.Instance.Changed();
        }

        private static void SendControls()
        {
            if (MsfsData.Instance.ATC)
            {
                //FSUIPCConnection.SendControlToFS(FsControl.ATC, 0);
                FSUIPCConnection.SendKeyToFS(Keys.Scroll);
                MsfsData.Instance.ATC = false;
            }
            if (MsfsData.Instance.ATC0)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_0, 0);
                MsfsData.Instance.ATC0 = false;
            }
            if (MsfsData.Instance.ATC1)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_1, 0);
                MsfsData.Instance.ATC1 = false;
            }
            if (MsfsData.Instance.ATC2)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_2, 0);
                MsfsData.Instance.ATC2 = false;
            }
            if (MsfsData.Instance.ATC3)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_3, 0);
                MsfsData.Instance.ATC3 = false;
            }
            if (MsfsData.Instance.ATC4)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_4, 0);
                MsfsData.Instance.ATC4 = false;
            }
            if (MsfsData.Instance.ATC5)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_5, 0);
                MsfsData.Instance.ATC5 = false;
            }
            if (MsfsData.Instance.ATC6)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_6, 0);
                MsfsData.Instance.ATC6 = false;
            }
            if (MsfsData.Instance.ATC7)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_7, 0);
                MsfsData.Instance.ATC7 = false;
            }
            if (MsfsData.Instance.ATC8)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_8, 0);
                MsfsData.Instance.ATC8 = false;
            }
            if (MsfsData.Instance.ATC9)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ATC_MENU_9, 0);
                MsfsData.Instance.ATC9 = false;
            }
            if (MsfsData.Instance.DEBUG)
            {
                FSUIPCConnection.SendControlToFS(66752, 1);
                MsfsData.Instance.DEBUG = false;
            }
            if (MsfsData.Instance.Menu)
            {
                FSUIPCConnection.SendKeyToFS(Keys.Escape);
                MsfsData.Instance.Menu = false;
            }
            if (MsfsData.Instance.EngineAutoOn)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ENGINE_AUTO_START, 0);
                MsfsData.Instance.EngineAutoOn = false;
            }
            if (MsfsData.Instance.EngineAutoOff)
            {
                FSUIPCConnection.SendControlToFS(FsControl.ENGINE_AUTO_SHUTDOWN, 0);
                MsfsData.Instance.EngineAutoOff = false;
            }


        }

        private static void GetLightsFromMSFS(Int16 value)
        {
            MsfsData.Instance.CabinLightFromMSFS = IsInList(aircraftName.Value, invertedCabinLightAircraftsPatterns) ? value >= 512 : !(value >= 512);
            value %= 512;
            MsfsData.Instance.LogoLightFromMSFS = value >= 256;
            value %= 256;
            MsfsData.Instance.WingLightFromMSFS = value >= 128;
            value %= 128;
            MsfsData.Instance.RecognitionLightFromMSFS = value >= 64;
            value %= 64;
            MsfsData.Instance.InstrumentsLightFromMSFS = value >= 32;
            value %= 32;
            MsfsData.Instance.StrobesLightFromMSFS = value >= 16;
            value %= 16;
            MsfsData.Instance.TaxiLightFromMSFS = value >= 8;
            value %= 8;
            MsfsData.Instance.LandingLightFromMSFS = value >= 4;
            value %= 4;
            MsfsData.Instance.BeaconLightFromMSFS = value >= 2;
            value %= 2;
            MsfsData.Instance.NavigationLightFromMSFS = value >= 1;

        }
        private static Int16 GetLights()
        {
            Int16 result = 0;
            result += MsfsData.Instance.NavigationLight ? (Int16)1 : (Int16)0;
            result += MsfsData.Instance.BeaconLight ? (Int16)2 : (Int16)0;
            result += MsfsData.Instance.LandingLight ? (Int16)4 : (Int16)0;
            result += MsfsData.Instance.TaxiLight ? (Int16)8 : (Int16)0;
            result += MsfsData.Instance.StrobesLight ? (Int16)16 : (Int16)0;
            result += MsfsData.Instance.InstrumentsLight ? (Int16)32 : (Int16)0;
            result += MsfsData.Instance.RecognitionLight ? (Int16)64 : (Int16)0;
            result += MsfsData.Instance.WingLight ? (Int16)128 : (Int16)0;
            result += MsfsData.Instance.LogoLight ? (Int16)256 : (Int16)0;
            if (IsInList(aircraftName.Value, invertedCabinLightAircraftsPatterns))
            {
                result += MsfsData.Instance.CabinLight ? (Int16)512 : (Int16)0;
            }
            else
            {
                result += !MsfsData.Instance.CabinLight ? (Int16)512 : (Int16)0;
            }
            return result;
        }
        private static Int32 GetSpoilerFromMSFS(Int32 msfsValue, Int32 armValue) => armValue == 4800 ? -1 : msfsValue == 0 ? 0 : (Int32)Math.Round((Double)msfsValue / 16383 * 10);
        private static Int32 GetSpoiler(Int32 currentSpoiler) => currentSpoiler == 0 ? 0 : currentSpoiler == -1 ? 4800 : 5800 + currentSpoiler * (16383 - 5800) / 10;
        private static Boolean IsInList(String aircraftName, List<String> list) => list.Any(i => Regex.Match(aircraftName, i).Success);
    }
}
