namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using Microsoft.FlightSimulator.SimConnect;

    class SimulatorDAO
    {
        public static SimConnect simConnect;

        const int WM_USER_SIMCONNECT = 0x0402;

        enum DEFINITIONS
        {
            DataStructure,
        }

        enum DATA_REQUESTS
        {
            DataRequest,
        };

        // this is how you declare a data structure so that
        // simconnect knows how to fill it/read it.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        struct DataStructure
        {
            // this is how you declare a fixed size string
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public double pitch;
            public double roll;
            public double xAccel;
            public double yAccel;
            public double zAccel;
            public double gforce;
            public double rpm1;
            public double rpm2;
            public double surface;
            public double crashed;
            public double tas;
            public double heading;
            public double altitude;
        };

        public static SimConnect Initialise()
        {
            try
            {
                MsfsData.Instance.state = "Tentative";
                MsfsData.Instance.changed();
                simConnect = new SimConnect("Motion Simulator", IntPtr.Zero, WM_USER_SIMCONNECT, null, 0);
                MsfsData.Instance.state = "Connecté";
                MsfsData.Instance.changed();
                SetupEvents();
                return simConnect;
            }
            catch (COMException ex)
            {
                MsfsData.Instance.state = "Connexion impossible";
                MsfsData.Instance.changed();
                return simConnect;
            }
        }

        public static void CloseConnection()
        {
            if (simConnect != null)
            {
                // Dispose serves the same purpose as SimConnect_Close()
                simConnect.Dispose();
                simConnect = null;
                MsfsData.Instance.state = "Déconnecté";
                //displayText("Connection closed");
            }
        }

        private static void SetupEvents()
        {
            try
            {
                simConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(simConnect_OnRecvOpen);
                simConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(simConnect_OnRecvQuit);

                simConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(simConnect_OnRecvException);

                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Plane Pitch Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Plane Bank Degrees", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Acceleration Body X", "feet per second squared", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Acceleration Body Y", "feet per second squared", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Acceleration Body Z", "feet per second squared", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "G Force", "GForce", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "General Eng Rpm:1", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "General Eng Rpm:2", "rpm", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                //simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Surface Type", "", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                //simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Crash Flag", "feet per second squared", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Airspeed True", "knots", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Plane Heading Degrees Magnetic", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simConnect.AddToDataDefinition(DEFINITIONS.DataStructure, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                // IMPORTANT: register it with the simconnect managed wrapper marshaller
                // if you skip this step, you will only receive a uint in the .dwData field.
                simConnect.RegisterDataDefineStruct<DataStructure>(DEFINITIONS.DataStructure);

                // catch a simobject data request
                simConnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simConnect_OnRecvSimobjectData);

            }
            catch (COMException ex)
            {

            }
        }

        static void simConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            //return "Connected to FSX";
        }

        // The case where the user closes FSX
        static void simConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            CloseConnection();
            //return "FSX has exited";
        }

        static void simConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            // return "Exception received: " + data.dwException;
        }

        static void simConnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
            MsfsData.Instance.state = "Reception Data";
            Dictionary<string, string> simData = new Dictionary<string, string>();
            switch ((DATA_REQUESTS)data.dwRequestID)
            {
                case DATA_REQUESTS.DataRequest:
                    DataStructure s1 = (DataStructure)data.dwData[0];

                    simData.Add("Pitch", s1.pitch.ToString());
                    simData.Add("Roll", s1.roll.ToString());
                    simData.Add("XAccel", s1.xAccel.ToString());
                    simData.Add("YAccel", s1.yAccel.ToString()); 
                    simData.Add("ZAccel", s1.zAccel.ToString());
                    simData.Add("GForce", s1.gforce.ToString());
                    simData.Add("RPM1", s1.rpm2.ToString());
                    simData.Add("RPM2", s1.rpm1.ToString());
                    simData.Add("Surface", s1.surface.ToString());
                    simData.Add("Crashed", s1.crashed.ToString());
                    simData.Add("TAS", s1.tas.ToString());
                    simData.Add("Heading", s1.heading.ToString());
                    simData.Add("Altitude", s1.altitude.ToString());
                    MsfsData.Instance.currentHeading = (Int32)s1.heading;
                    
                    break;

                default:
                    //displayText("Unknown request ID: " + data.dwRequestID);
                    break;
            }
            
            //return simData;
        }

        public static void GetData()
        {
            simConnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(simConnect_OnRecvSimobjectData);
            simConnect.RequestDataOnSimObject(DATA_REQUESTS.DataRequest, DEFINITIONS.DataStructure, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_PERIOD.SECOND, SIMCONNECT_DATA_REQUEST_FLAG.DEFAULT, 0, 0, 0);

        }
    }
}
