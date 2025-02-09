using Microsoft.FlightSimulator.SimConnect;
using Microsoft.UI.Xaml.Data;
using System;
using System.Runtime.InteropServices;
using Windows.Networking.Connectivity;

namespace SimConnectWrapper
{
    using static DataTransferTypes;
    using SimType = Microsoft.FlightSimulator.SimConnect.SIMCONNECT_DATATYPE;

    public class SimConnectWrapper : ISimConnectWrapper
    {
        public const Int32 WM_USER_SIMCONNECT = 0x0402;
        private SimConnect m_oSimConnect = null;
        private bool _simConnectConnected = false;
        private static readonly System.Timers.Timer timer = new System.Timers.Timer();
        private const double timerInterval = 200;
        private readonly object lockObject = new object();
        private Readers datas;
        private enum DATA_REQUESTS
        {
            REQUEST_1
        }
        public enum hSimconnect : int
        {
            group1
        }
        public void Connect()
        {
            try
            {
                m_oSimConnect = new SimConnect("MSFS Plugin", new IntPtr(0), WM_USER_SIMCONNECT, null, 0);
                m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);
                m_oSimConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(SimConnect_OnRecvException);
                m_oSimConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(SimConnect_OnRecvQuit);

                AddRequest();

                lock (timer)
                {
                    timer.Interval = timerInterval;
                    timer.Elapsed += Refresh;
                    timer.Enabled = true;
                }
                _simConnectConnected = true;
            }
            catch 
            {
                m_oSimConnect = null;
            }
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            SIMCONNECT_EXCEPTION eException = (SIMCONNECT_EXCEPTION)data.dwException;
        }

        // The case where the user closes game
        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Disconnect();
        }

        private void Refresh(Object source, EventArgs e)
        {
                try
                {
                    if (m_oSimConnect != null)
                    {

                        m_oSimConnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Readers, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                        m_oSimConnect.ReceiveMessage();
                    }
                    else
                    {
                        timer.Enabled = false;
                    }
                }
                catch (COMException)
                {
                    Disconnect();
                }

        }

        public void Disconnect(bool unloading = false)
        {
            if (m_oSimConnect != null)
            {
                m_oSimConnect.Dispose();
                m_oSimConnect = null;
                _simConnectConnected = false;
            }

            if (unloading)
                return;
        }
        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            timer.Interval = timerInterval;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            datas = (Readers)data.dwData[0];
        }

        public void send(Enum eventName, uint value)
        {
            m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
        }

        public bool IsConnected() => _simConnectConnected;

        public void register(Enum eventName, string key)
        {
            m_oSimConnect.MapClientEventToSimEvent(eventName, key);
        }

        public string getString(string key) => (string)datas.GetType().GetField(key).GetValue(datas);

        public long getLong(string key) => (long)datas.GetType().GetField(key).GetValue(datas);

        public double getDouble(string key) => (double)datas.GetType().GetField(key).GetValue(datas);

  
        private void AddRequest()
        {
            void AddReaderDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Readers, name, units, type);

            void AddWriterDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Writers, name, units, type);

            void AddToDataDefinition(DEFINITIONS dataDef, string name, string units, SimType type) =>
                m_oSimConnect.AddToDataDefinition(dataDef, name, units, type, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            AddReaderDef("TITLE", null, SimType.STRING256);                                               // Reader.title                - no bindings but set as MsfsData.Instance.AircraftName -
            AddReaderDef("Plane Latitude", "degrees", SimType.FLOAT64);                                   //        latitude             - no binding -
            AddReaderDef("Plane Longitude", "degrees", SimType.FLOAT64);                                  //        longitude            - no binding -
            AddReaderDef("Ground Altitude", "meters", SimType.FLOAT64);                                   //        groundaltitude       - no binding -
            AddReaderDef("GEAR RIGHT POSITION", "Boolean", SimType.FLOAT64);                              //        gearRightPos         GEAR_RIGHT
            AddReaderDef("GEAR LEFT POSITION", "Boolean", SimType.FLOAT64);                               //        gearLeftPos          GEAR_LEFT
            AddReaderDef("GEAR CENTER POSITION", "Boolean", SimType.FLOAT64);                             //        gearCenterPos        GEAR_FRONT
            AddReaderDef("IS GEAR RETRACTABLE", "Boolean", SimType.INT64);                                //        gearRetractable      GEAR_RETRACTABLE
            AddReaderDef("BRAKE PARKING POSITION", "Boolean", SimType.INT64);                             //        parkingBrake         PARKING_BRAKES
            AddReaderDef("ENGINE TYPE", "Enum", SimType.INT64);                                           //        engineType           ENGINE_TYPE
            AddReaderDef("TURB ENG N1:1", "Percent", SimType.FLOAT64);                                    //        E1N1                 E1N1
            AddReaderDef("TURB ENG N1:2", "Percent", SimType.FLOAT64);                                    //        E2N1                 E2N1
            AddReaderDef("TURB ENG N1:3", "Percent", SimType.FLOAT64);                                    //        E3N1                 E3N1
            AddReaderDef("TURB ENG N1:4", "Percent", SimType.FLOAT64);                                    //        E4N1                 E4N1
            AddReaderDef("TURB ENG N2:1", "Percent", SimType.FLOAT64);                                    //        E1N2                 E1N2
            AddReaderDef("TURB ENG N2:2", "Percent", SimType.FLOAT64);                                    //        E2N2                 E2N2
            AddReaderDef("TURB ENG N2:3", "Percent", SimType.FLOAT64);                                    //        E3N2                 E2N3
            AddReaderDef("TURB ENG N2:4", "Percent", SimType.FLOAT64);                                    //        E4N2                 E2N4
            AddReaderDef("FUEL TOTAL CAPACITY", "Gallon", SimType.INT64);                                 //        fuelCapacity         FUEL_PERCENT
            AddReaderDef("FUEL TOTAL QUANTITY", "Gallon", SimType.INT64);                                 //        fuelQuantity         FUEL_PERCENT, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW GPH:1", "Gallons per hour", SimType.FLOAT64);                     //        E1GPH                FUEL_FLOW_GPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW GPH:2", "Gallons per hour", SimType.FLOAT64);                     //        E2GPH                FUEL_FLOW_GPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW GPH:3", "Gallons per hour", SimType.FLOAT64);                     //        E3GPH                FUEL_FLOW_GPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW GPH:4", "Gallons per hour", SimType.FLOAT64);                     //        E4GPH                FUEL_FLOW_GPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW PPH:1", "Pounds per hour", SimType.FLOAT64);                      //        E1PPH                FUEL_FLOW_PPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW PPH:2", "Pounds per hour", SimType.FLOAT64);                      //        E2PPH                FUEL_FLOW_PPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW PPH:3", "Pounds per hour", SimType.FLOAT64);                      //        E3PPH                FUEL_FLOW_PPH, FUEL_TIME_LEFT
            AddReaderDef("ENG FUEL FLOW PPH:4", "Pounds per hour", SimType.FLOAT64);                      //        E4PPH                FUEL_FLOW_PPH, FUEL_TIME_LEFT
            AddReaderDef("PUSHBACK STATE:0", "Enum", SimType.INT64);                                      //        pushback             - no binding -
            AddReaderDef("PROP RPM:1", "RPM", SimType.INT64);                                             //        ENG1N1RPM            E1RPM
            AddReaderDef("PROP RPM:2", "RPM", SimType.INT64);                                             //        ENG2N1RPM            E2RPM
            AddReaderDef("PROP RPM:3", "RPM", SimType.INT64);                                             //        ENG3N1RPM            E3RPM
            AddReaderDef("PROP RPM:4", "RPM", SimType.INT64);                                             //        ENG4N1RPM            E4RPM
            AddReaderDef("NUMBER OF ENGINES", "Number", SimType.INT64);                                   //        engineNumber         ENGINE_NUMBER
            AddReaderDef("INDICATED ALTITUDE", "Feet", SimType.INT64);                                    //        planeAltitude        ALT
            AddReaderDef("AUTOPILOT ALTITUDE LOCK VAR", "Feet", SimType.INT64);                           //        apAltitude           AP_ALT
            AddReaderDef("GPS FLIGHT PLAN WP INDEX", "Number", SimType.INT64);                            //        wpID                 AP_NEXT_WP_ID
            AddReaderDef("GPS WP DISTANCE", "Meters", SimType.INT64);                                     //        wpDistance           AP_NEXT_WP_DIST
            AddReaderDef("GPS WP ETE", "Seconds", SimType.INT64);                                         //        wpETE                AP_NEXT_WP_ETE
            AddReaderDef("GPS WP BEARING", "degrees", SimType.INT64);                                     //        wpBearing            AP_NEXT_WP_HEADING
            AddReaderDef("GPS FLIGHT PLAN WP COUNT", "Number", SimType.INT64);                            //        wpCount              - no binding -
            AddReaderDef("AUTOPILOT HEADING LOCK DIR", "degrees", SimType.INT64);                         //        apHeading            AP_HEADING
            AddReaderDef("PLANE HEADING DEGREES MAGNETIC", "degrees", SimType.FLOAT64);                   //        planeHeading         HEADING
            AddReaderDef("AIRSPEED INDICATED", "Knots", SimType.INT64);                                   //        planeSpeed           SPEED
            AddReaderDef("VERTICAL SPEED", "feet/second", SimType.FLOAT64);                               //        planeVSpeed          VSPEED
            AddReaderDef("AUTOPILOT VERTICAL HOLD VAR", "Feet per minute", SimType.INT64);                //        apVSpeed             AP_VSPEED
            AddReaderDef("AUTOPILOT AIRSPEED HOLD VAR", "Knots", SimType.INT64);                          //        apSpeed              AP_SPEED
            AddReaderDef("ENG COMBUSTION:1", "Boolean", SimType.INT64);                                   //        E1On                 ENGINE_AUTO

            AddReaderDef("LIGHT NAV", "Boolean", SimType.INT64);                                          //        navLight             LIGHT_NAV
            AddReaderDef("LIGHT BEACON", "Boolean", SimType.INT64);                                       //        beaconLight          LIGHT_BEACON
            AddReaderDef("LIGHT LANDING", "Boolean", SimType.INT64);                                      //        landingLight         LIGHT_LANDING
            AddReaderDef("LIGHT TAXI", "Boolean", SimType.INT64);                                         //        taxiLight            LIGHT_TAXI
            AddReaderDef("LIGHT STROBE", "Boolean", SimType.INT64);                                       //        strobeLight          LIGHT_STROBE
            AddReaderDef("LIGHT PANEL", "Boolean", SimType.INT64);                                        //        panelLight           LIGHT_INSTRUMENT
            AddReaderDef("LIGHT RECOGNITION", "Boolean", SimType.INT64);                                  //        recognitionLight     LIGHT_RECOG
            AddReaderDef("LIGHT WING", "Boolean", SimType.INT64);                                         //        wingLight            LIGHT_WING
            AddReaderDef("LIGHT LOGO", "Boolean", SimType.INT64);                                         //        logoLight            LIGHT_LOGO
            AddReaderDef("LIGHT CABIN", "Boolean", SimType.INT64);                                        //        cabinLight           LIGHT_CABIN

            AddReaderDef("AUTOPILOT ALTITUDE LOCK", "Boolean", SimType.INT64);                            //        apAltHold            AP_ALT_SWITCH
            AddReaderDef("AUTOPILOT HEADING LOCK", "Boolean", SimType.INT64);                             //        apHeadingHold        AP_HEAD_SWITCH
            AddReaderDef("AUTOPILOT MACH HOLD", "Boolean", SimType.INT64);                                //        apSpeedHold          AP_SPEED_SWITCH
            AddReaderDef("AUTOPILOT MANAGED THROTTLE ACTIVE", "Boolean", SimType.INT64);                  //        apThrottleHold       AP_THROTTLE_SWITCH
            AddReaderDef("AUTOPILOT MASTER", "Boolean", SimType.INT64);                                   //        apMasterHold         AP_MASTER_SWITCH
            AddReaderDef("AUTOPILOT NAV1 LOCK", "Boolean", SimType.INT64);                                //        apNavHold            AP_NAV_SWITCH
            AddReaderDef("AUTOPILOT VERTICAL HOLD", "Boolean", SimType.INT64);                            //        apVerticalSpeedHold  AP_VSPEED_SWITCH
            AddReaderDef("KOHLSMAN SETTING HG:1", "inHg", SimType.FLOAT64);                               //        kohlsmanInHb         KOHLSMAN
            AddReaderDef("AILERON TRIM PCT", "Number", SimType.FLOAT64);                                  //        aileronTrim          AILERON_TRIM
            AddReaderDef("ELEVATOR TRIM PCT", "Percent Over 100", SimType.FLOAT64);                       //        elevatorTrim         ELEVATOR_TRIM
            AddReaderDef("FLAPS NUM HANDLE POSITIONS", "Number", SimType.INT64);                          //        flapMax              MAX_FLAP
            AddReaderDef("FLAPS HANDLE INDEX", "Number", SimType.INT64);                                  //        flapPosition         FLAP
            AddReaderDef("GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SimType.INT64);               //        mixtureE1            MIXTURE
            AddReaderDef("GENERAL ENG PROPELLER LEVER POSITION:1", "Percent", SimType.FLOAT64);           //        propellerE1          PROPELLER
            AddReaderDef("RUDDER TRIM PCT", "Percent Over 100", SimType.FLOAT64);                         //        rudderTrim           RUDDER_TRIM
            AddReaderDef("SPOILERS HANDLE POSITION", "Percent Over 100", SimType.FLOAT64);                //        spoiler              SPOILER
            AddReaderDef("GENERAL ENG THROTTLE LEVER POSITION:1", "Percent Over 100", SimType.FLOAT64);   //        throttle             THROTTLE
            AddReaderDef("PITOT HEAT SWITCH:1", "Boolean", SimType.INT64);                                //        pitot                PITOT
            AddReaderDef("CENTER WHEEL RPM", "RPM", SimType.INT64);                                       //        wheelRPM             PUSHBACK_ATTACHED
            AddReaderDef("AUTOPILOT FLIGHT DIRECTOR ACTIVE:1", "Boolean", SimType.INT64);                 //        apFD                 AP_FD_SWITCH
            AddReaderDef("AUTOPILOT FLIGHT LEVEL CHANGE", "Boolean", SimType.INT64);                      //        apFLC                AP_FLC_SWITCH
            AddReaderDef("AUTOPILOT APPROACH HOLD", "Boolean", SimType.INT64);                            //        apAPP                AP_APP_SWITCH
            AddReaderDef("AUTOPILOT APPROACH IS LOCALIZER", "Boolean", SimType.INT64);                    //        apLOC                AP_LOC_SWITCH
            AddReaderDef("SIM ON GROUND", "Boolean", SimType.INT64);                                      //        onGround             PUSHBACK_STATE
            AddReaderDef("GROUND VELOCITY", "Knots", SimType.INT64);                                      //        groundSpeed          AUTO_TAXI
            AddReaderDef("PUSHBACK ATTACHED", "Boolean", SimType.INT64);                                  //        pushbackAttached     PUSHBACK_ATTACHED
            AddReaderDef("COM ACTIVE FREQUENCY:1", "Hz", SimType.INT64);                                  //        COM1ActiveFreq       COM1_ACTIVE_FREQUENCY
            AddReaderDef("COM STANDBY FREQUENCY:1", "Hz", SimType.INT64);                                 //        COM1StbFreq          COM1_STBY
            AddReaderDef("COM AVAILABLE:1", "Boolean", SimType.INT64);                                    //        COM1Available        COM1_AVAILABLE
            AddReaderDef("COM STATUS:1", "Enum", SimType.INT64);                                          //        COM1Status           COM1_STATUS
            //AddReaderDef("COM ACTIVE FREQ TYPE:1", null, SimType.STRINGV);                              //        COM1Type             - no binding -
            AddReaderDef("COM ACTIVE FREQUENCY:2", "Hz", SimType.INT64);                                  //        COM2ActiveFreq       COM2_ACTIVE_FREQUENCY
            AddReaderDef("COM STANDBY FREQUENCY:2", "Hz", SimType.INT64);                                 //        COM2StbFreq          COM2_STBY
            AddReaderDef("COM AVAILABLE:2", "Boolean", SimType.INT64);                                    //        COM2Available        COM2_AVAILABLE
            AddReaderDef("COM STATUS:2", "Enum", SimType.INT64);                                          //        COM2Status           COM2_STATUS
            //AddReaderDef("COM ACTIVE FREQ TYPE:2", null, SimType.STRINGV);                              //        COM2Type             - no binding -
            AddReaderDef("LIGHT PEDESTRAL", "Boolean", SimType.INT64);                                    //        pedestralLight       LIGHT_PEDESTAL
            AddReaderDef("LIGHT GLARESHIELD", "Boolean", SimType.INT64);                                  //        glareshieldLight     LIGHT_GLARESHIELD
            AddReaderDef("AUTOPILOT YAW DAMPER", "Boolean", SimType.INT64);                               //        apYawDamper          AP_YAW_DAMPER_SWITCH
            AddReaderDef("AUTOPILOT BACKCOURSE HOLD", "Boolean", SimType.INT64);                          //        apBackCourse         AP_BC_SWITCH
            AddReaderDef("SIMULATION RATE", "Number", SimType.FLOAT64);                                   //        simRate              SIM_RATE
            AddReaderDef("SPOILERS ARMED", "Boolean", SimType.INT64);                                     //        spoilerArm           SPOILERS_ARM

            AddReaderDef("NAV ACTIVE FREQUENCY:1", "Hz", SimType.INT64);                                  //        NAV1ActiveFreq       NAV1_ACTIVE_FREQUENCY
            AddReaderDef("NAV ACTIVE FREQUENCY:2", "Hz", SimType.INT64);                                  //        NAV2ActiveFreq       NAV2_ACTIVE_FREQUENCY
            AddReaderDef("NAV AVAILABLE:1", "Boolean", SimType.INT64);                                    //        NAV1Available        NAV1_AVAILABLE
            AddReaderDef("NAV AVAILABLE:2", "Boolean", SimType.INT64);                                    //        NAV2Available        NAV2_AVAILABLE
            AddReaderDef("NAV STANDBY FREQUENCY:1", "Hz", SimType.INT64);                                 //        NAV1StbyFreq         NAV1_STBY_FREQUENCY
            AddReaderDef("NAV STANDBY FREQUENCY:2", "Hz", SimType.INT64);                                 //        NAV2StbyFreq         NAV2_STBY_FREQUENCY

            AddReaderDef("NAV OBS:1", "degrees", SimType.INT64);                                          //        NAV1Obs              VOR1_OBS
            AddReaderDef("NAV OBS:2", "degrees", SimType.INT64);                                          //        NAV2Obs              VOR2_OBS

            AddReaderDef("TOTAL AIR TEMPERATURE", "Celsius", SimType.FLOAT64);                            //        AirTemperature       AIR_TEMP
            AddReaderDef("AMBIENT WIND DIRECTION", "Degrees", SimType.INT64);                             //        WindDirection        WIND_DIRECTION
            AddReaderDef("AMBIENT WIND VELOCITY", "Knots", SimType.INT64);                                //        WindSpeed            WIND_SPEED
            AddReaderDef("AMBIENT VISIBILITY", "Meters", SimType.INT64);                                  //        Visibility           VISIBILITY
            AddReaderDef("SEA LEVEL PRESSURE", "Millibars", SimType.FLOAT64);                             //        SeaLevelPressure     SEA_LEVEL_PRESSURE

            AddReaderDef("ADF ACTIVE FREQUENCY:1", "KHz", SimType.FLOAT64);                               //        ADFActiveFreq        ADF_ACTIVE_FREQUENCY
            AddReaderDef("ADF STANDBY FREQUENCY:1", "KHz", SimType.FLOAT64);                              //        ADFStbyFreq          ADF_STBY_FREQUENCY
            AddReaderDef("ADF AVAILABLE:1", "Boolean", SimType.INT64);                                    //        ADF1Available        ADF1_AVAILABLE
            AddReaderDef("ADF STANDBY AVAILABLE:1", "Boolean", SimType.INT64);                            //        ADF2Available        ADF1_STBY_AVAILABLE

            //++ Make new data definitions here using a type that fits SimConnect variable if it needs to be read from the Sim

            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SimType.INT64);               // Writer.mixtureE1            MIXTURE
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:2", "Percent", SimType.INT64);               //        mixtureE2            MIXTURE
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:3", "Percent", SimType.INT64);               //        mixtureE3            MIXTURE
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:4", "Percent", SimType.INT64);               //        mixtureE4            MIXTURE


            m_oSimConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            m_oSimConnect.RegisterDataDefineStruct<Writers>(DEFINITIONS.Writers);
        }


    }
}
