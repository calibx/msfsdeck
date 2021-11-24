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
        private static Offset<int> verticalSpeed = new Offset<int>(0x02C8);          // 4-byte offset - Signed integer 
        private static Offset<int> verticalSpeedAP = new Offset<int>(0x07F2);          // 4-byte offset - Signed integer 
        private static Offset<double> compass = new Offset<double>(0x02CC);          // 8 byte offset - double (FLOAT64)
        private static Offset<double> compassAP = new Offset<double>(0x07CC);          //
        private static Offset<int> fps = new Offset<int>(0x0274);          // Frame rate is given by 32768/this value
        private static Offset<int> altitude = new Offset<int>(0x0574);          // 
        private static Offset<int> altitudeAP = new Offset<int>(0x07D4);          // 


        public static void Initialise()
        {
            try
            {
                FSUIPCConnection.Open();
                var timer = new System.Timers.Timer();
                timer.Interval = 500;
                timer.Enabled = true;
                timer.Elapsed += refresh;
            }
            catch (FSUIPCException ex)
            {
                MsfsData.Instance.state = "Cnx Fail";
            }
            if (FSUIPCConnection.IsOpen)
            {
                MsfsData.Instance.state = "Cnx OK";
            }
            MsfsData.Instance.changed();


        }

        public static void refresh(object source, EventArgs e)
        {
            // Call Process() to get the data from FSUIPC
            FSUIPCConnection.Process();

            // --------------------
            // VERTICAL SPEED
            // --------------------
            // FSUIPC Documentation says this offset is 4 bytes, signed (int) and holds the speed as metres/second * 256
            // We need to convert back to metres/second by / 256
            // Offset is 'int' so cast to double for conversion.
            double verticalSpeedMPS = (double)verticalSpeed.Value / 256d;
            // If you want to display as feet/minute a further conversion is required:
            double verticalSpeedFPM = verticalSpeedMPS * 60d * 3.28084d;
            // Display one of these on the form (this time rounded to 0dp)
            // this.txtVerticalSpeed.Text = verticalSpeedMPS.ToString("F0");
            MsfsData.Instance.currentVerticalSpeed = (int)verticalSpeedFPM;
            MsfsData.Instance.currentAPVerticalSpeed = (int)((verticalSpeedAP.Value / 256d) * 60d * 3.28084d);

            // --------------------
            // COMPASS HEADING
            // --------------------
            // FSUIPC Documentation says this offset is 8 bytes and holds a FLOAT64 (double). The value is in degrees.
            // No conversion needed for this offset - Display directly on the form - rounded to 1dp.
            MsfsData.Instance.currentHeading = (int)compass.Value;
            MsfsData.Instance.currentAPHeading = (int)compassAP.Value;
;

            MsfsData.Instance.currentAltitude = (int)(altitude.Value*3.28);
            MsfsData.Instance.currentAPAltitude = (int)((altitudeAP.Value/65536)*3.28);

            MsfsData.Instance.changed();

            


        }

    }
}
