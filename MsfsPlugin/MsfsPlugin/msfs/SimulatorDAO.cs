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
        private static readonly Offset<Int32> verticalSpeedAP = new Offset<Int32>(0x07F2);
        private static readonly Offset<Double> compass = new Offset<Double>(0x02CC);
        private static readonly Offset<Int16> compassAP = new Offset<Int16>(0x07CC);
        private static readonly Offset<Int32> fps = new Offset<Int32>(0x0274);
        private static readonly Offset<Int32> altitude = new Offset<Int32>(0x0574);

        private static readonly Offset<Int32> altitudeAP = new Offset<Int32>(0x07D4);
        private static readonly Offset<Int32> apSwitch = new Offset<Int32>(0x07BC);
        private static readonly Offset<Int32> parkingBrakes = new Offset<Int32>(0x0BC8);

        private static readonly Offset<Byte> gearOverSpeed = new Offset<Byte>(0x0B4F);
        private static readonly Offset<Int32> gearHandle = new Offset<Int32>(0x0BE8);
        private static readonly Offset<Int32> gearFront = new Offset<Int32>(0x0BEC);
        private static readonly Offset<Int32> gearLeft = new Offset<Int32>(0x0BF0);
        private static readonly Offset<Int32> gearRight = new Offset<Int32>(0x0BF4);

        private static Timer timer = new System.Timers.Timer();

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
                    FSUIPCConnection.Process();
                    if (MsfsData.Instance.DirtyAP)
                    {
                        verticalSpeedAP.Value = (Int32)(MsfsData.Instance.CurrentAPVerticalSpeed * 256d / (60d * 3.28084d) / 1.3);
                        compassAP.Value = (Int16)(MsfsData.Instance.CurrentAPHeading * 182);
                        altitudeAP.Value = (Int32)(MsfsData.Instance.CurrentAPAltitude * 65536 / 3.28);
                        apSwitch.Value = (Int32)MsfsData.Instance.ApSwitch;
                        parkingBrakes.Value = (Int32)MsfsData.Instance.CurrentBrakes;
                        gearHandle.Value = (Int32)MsfsData.Instance.CurrentGearHandle;
                        MsfsData.Instance.DirtyAP = false;
                    }
                    else
                    {
                        MsfsData.Instance.CurrentAPVerticalSpeedFromMSFS = (Int32)(verticalSpeedAP.Value / 256d * 60d * 3.28084d * 1.3);
                        MsfsData.Instance.CurrentAPHeadingFromMSFS = compassAP.Value / 182;
                        if (MsfsData.Instance.CurrentAPHeading <= 0)
                        {
                            MsfsData.Instance.CurrentAPHeadingFromMSFS += 360;
                        }
                        MsfsData.Instance.CurrentAPAltitudeFromMSFS = (Int32)Math.Round(altitudeAP.Value / 65536 * 3.28 / 10.0) * 10;
                        MsfsData.Instance.ApSwitchFromMSFS = apSwitch.Value;
                        MsfsData.Instance.CurrentBrakes = parkingBrakes.Value;
                        MsfsData.Instance.CurrentGearHandle = gearHandle.Value;
                    }

                    MsfsData.Instance.CurrentHeading = (Int32)compass.Value;
                    MsfsData.Instance.CurrentVerticalSpeed = (Int32)(verticalSpeed.Value / 256d * 60d * 3.28084d * 1.3);
                    MsfsData.Instance.CurrentAltitude = (Int32)(altitude.Value * 3.28);
                    MsfsData.Instance.Fps = 32768 / (fps.Value + 1);
                    MsfsData.Instance.GearOverSpeed = gearOverSpeed.Value;
                    MsfsData.Instance.GearLeft = gearLeft.Value;
                    MsfsData.Instance.GearFront = gearFront.Value;
                    MsfsData.Instance.GearRight = gearRight.Value;

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

    }
}
