namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    
    using System.Diagnostics;

    using Microsoft.FlightSimulator.SimConnect;


    
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String _sPropertyName = null)
        {
            PropertyChangedEventHandler hEventHandler = this.PropertyChanged;
            if (hEventHandler != null && !String.IsNullOrEmpty(_sPropertyName))
            {
                hEventHandler(this, new PropertyChangedEventArgs(_sPropertyName));
            }
        }

        protected bool SetProperty<T>(ref T _tField, T _tValue, [CallerMemberName] string _sPropertyName = null)
        {
            return this.SetProperty(ref _tField, _tValue, out T tPreviousValue, _sPropertyName);
        }

        protected bool SetProperty<T>(ref T _tField, T _tValue, out T _tPreviousValue, [CallerMemberName] string _sPropertyName = null)
        {
            if (!object.Equals(_tField, _tValue))
            {
                _tPreviousValue = _tField;
                _tField = _tValue;
                this.OnPropertyChanged(_sPropertyName);
                return true;
            }

            _tPreviousValue = default(T);
            return false;
        }
    }
    public class SimConnectDAO : ObservableObject
    {
        // Singleton
        private static readonly Lazy<SimConnectDAO> lazy = new Lazy<SimConnectDAO>(() => new SimConnectDAO());
        public static SimConnectDAO Instance => lazy.Value;

        /// User-defined win32 event
        public const Int32 WM_USER_SIMCONNECT = 0x0402;
        private const UInt32 TUG_ANGLE = 4294967295;

        /// SimConnect object
        private SimConnect m_oSimConnect = null;

        private Plugin pluginForKey;

        public ObservableCollection<string> lErrorMessages { get; private set; }

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();
        private enum DATA_REQUESTS
        {
            REQUEST_1
        }
        enum EVENTS
        {
            GEAR_SET,
            PARKING_BRAKE,
            ENGINE_AUTO_START,
            ENGINE_AUTO_SHUTDOWN,
            PAUSE_ON,
            PAUSE_OFF,
            PITOT_HEAT_SET,
            TOGGLE_PUSHBACK,
            KEY_TUG_HEADING,
            TUG_DISABLE, 
            NAV_LIGHTS_SET,
            LANDING_LIGHTS_SET,
            BEACON_LIGHTS_SET,
            TAXI_LIGHTS_SET,
            STROBES_SET,
            PANEL_LIGHTS_SET,
            RECOGNITION_LIGHTS_SET,
            WING_LIGHTS_SET,
            LOGO_LIGHTS_SET,
            CABIN_LIGHTS_SET,
            ATC_MENU_OPEN,
            ATC_MENU_CLOSE,
            ATC_MENU_0,
            ATC_MENU_1,
            ATC_MENU_2,
            ATC_MENU_3,
            ATC_MENU_4,
            ATC_MENU_5,
            ATC_MENU_6,
            ATC_MENU_7,
            ATC_MENU_8,
            ATC_MENU_9
        };
        enum GROUPID
        {
            SIMCONNECT_GROUP_PRIORITY_DEFAULT = 2000000000,
        };

        private enum DEFINITIONS
        {
            Struct1
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Struct1
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public String title;
            public Double latitude;
            public Double longitude;
            public Double trueheading;
            public Double groundaltitude;
            public Double gearRightPos;
            public Double gearLeftPos;
            public Double gearCenterPos;
            public Int64 gearRetractable;
            public Int64 parkingBrake;
            public Int64 engineType;
            public Int64 E1N1;
            public Int64 E2N1;
            public Int64 E3N1;
            public Int64 E4N1;
            public Int64 fuelCapacity;
            public Int64 fuelQuantity;
            public Int64 E1GPH;
            public Int64 E2GPH;
            public Int64 E3GPH;
            public Int64 E4GPH;
            public Int64 pushback;
            public Int64 ENG1N1RPM;
            public Int64 ENG2N1RPM;
            public Int64 engineNumber;
            public Int64 planeAltitude;
            public Int64 apAltitude;
            public Int64 wpID;
            public Int64 wpDistance;
            public Int64 wpETE;
            public Int64 wpBearing;
            public Int64 wpCount;
            public Int64 apHeading;
            public Int64 planeHeading;
            public Int64 planeSpeed;
            public Double planeVSpeed;
            public Int64 apVSpeed;
            public Int64 apSpeed;
            public Int64 E1On;

            public Int64 navLight;
            public Int64 beaconLight;
            public Int64 landingLight;
            public Int64 taxiLight;
            public Int64 strobeLight;
            public Int64 panelLight;
            public Int64 recognitionLight;
            public Int64 wingLight;
            public Int64 logoLight;
            public Int64 cabinLight;
        }

        public enum hSimconnect : int
        {
            group1
        }
        private SimConnectDAO()
        {
            lock (timer)
            {
                if (!MsfsData.Instance.SimConnected && !MsfsData.Instance.SimTryConnect)
                {
                    this.lErrorMessages = new ObservableCollection<string>();

                    timer.Interval = 2000;
                    timer.Elapsed += Refresh;
                    timer.Enabled = true;
                }
            }
        }
        public static void Refresh(Object source, EventArgs e) => Instance.OnTick();


        public void Connect(Plugin plugin)
        {
            Debug.WriteLine("Trying cnx");
            this.pluginForKey = plugin;
            MsfsData.Instance.SimTryConnect = true;
            MsfsData.Instance.SimConnected = false;
            try
            {
                this.m_oSimConnect = new SimConnect("Simconnect - Simvar test", new IntPtr(0), WM_USER_SIMCONNECT, null, 0);
                this.m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(this.SimConnect_OnRecvOpen);
                this.m_oSimConnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(this.SimConnect_OnRecvQuit);
                this.m_oSimConnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(this.SimConnect_OnRecvException);
                this.m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(this.SimConnect_OnRecvSimobjectDataBytype);
            }
            catch (COMException ex)
            {
                Debug.WriteLine(ex);
                MsfsData.Instance.SimTryConnect = false;
                MsfsData.Instance.SimConnected = false;
            }
            MsfsData.Instance.Changed();
            this.AddRequest();
        }
        public void Disconnect()
        {
            timer.Enabled = false;
            if (m_oSimConnect != null)
            {
                /// Dispose serves the same purpose as SimConnect_Close()
                m_oSimConnect.Dispose();
                m_oSimConnect = null;
            }

            MsfsData.Instance.SimConnected = false;
            MsfsData.Instance.SimTryConnect = false;
            MsfsData.Instance.Changed();

        }
        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Debug.WriteLine("Cnx opened");
            MsfsData.Instance.SimConnected = true;
            MsfsData.Instance.SimTryConnect = false;
            timer.Interval = 200;
        }

        /// The case where the user closes game
        private void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            Console.WriteLine("SimConnect_OnRecvQuit");
            Console.WriteLine("KH has exited");

            this.Disconnect();
        }

        private void SimConnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            var eException = (SIMCONNECT_EXCEPTION)data.dwException;
            Console.WriteLine("SimConnect_OnRecvException: " + eException.ToString());

            lErrorMessages.Add("SimConnect : " + eException.ToString());
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            Debug.WriteLine("Received Data");
            var struct1 = (Struct1)data.dwData[0];
            MsfsData.Instance.AircraftName = struct1.title;
            MsfsData.Instance.GearFront = struct1.gearCenterPos;
            MsfsData.Instance.GearLeft = struct1.gearLeftPos;
            MsfsData.Instance.GearRight = struct1.gearRightPos;
            MsfsData.Instance.GearRetractable = (Byte)struct1.gearRetractable;
            MsfsData.Instance.EngineType = (Int32)struct1.engineType;
            MsfsData.Instance.E1On = struct1.E1On == 1;
            MsfsData.Instance.E1N1 = (Int32)struct1.E1N1;
            MsfsData.Instance.E2N1 = (Int32)struct1.E2N1;
            MsfsData.Instance.E3N1 = (Int32)struct1.E3N1;
            MsfsData.Instance.E4N1 = (Int32)struct1.E4N1;
            MsfsData.Instance.NumberOfEngines = (Int32)struct1.engineNumber;
            MsfsData.Instance.E1Rpm = (Int32)struct1.ENG1N1RPM;
            MsfsData.Instance.E2Rpm = (Int32)struct1.ENG2N1RPM;

            MsfsData.Instance.FuelPercent = (Int32)(struct1.fuelQuantity * 100 / struct1.fuelCapacity);
            MsfsData.Instance.FuelFlow = (Int32)(struct1.E1GPH + struct1.E2GPH + struct1.E3GPH + struct1.E4GPH);
            MsfsData.Instance.FuelTimeLeft = (Int32)(struct1.fuelQuantity / (Double)(struct1.E1GPH + struct1.E2GPH + struct1.E3GPH + struct1.E4GPH) * 3600);

            MsfsData.Instance.PushbackFromMSFS = (Int16)struct1.pushback;
            MsfsData.Instance.CurrentAltitude = (Int32)struct1.planeAltitude;
            MsfsData.Instance.CurrentHeading = (Int32)struct1.planeHeading;
            MsfsData.Instance.CurrentSpeed = (Int32)struct1.planeSpeed;
            MsfsData.Instance.CurrentVerticalSpeed = (Int32)(struct1.planeVSpeed * 60f);

            MsfsData.Instance.ApNextWPDist = struct1.wpDistance * 0.00053996f;
            MsfsData.Instance.ApNextWPETE = (Int32)struct1.wpETE;
            MsfsData.Instance.ApNextWPHeading = struct1.wpBearing;
            MsfsData.Instance.ApNextWPID = struct1.wpID;

            MsfsData.Instance.NavigationLightState = struct1.navLight == 1;
            MsfsData.Instance.LandingLightState = struct1.landingLight == 1;
            MsfsData.Instance.BeaconLightState = struct1.beaconLight == 1;
            MsfsData.Instance.TaxiLightState = struct1.taxiLight == 1;
            MsfsData.Instance.StrobesLightState = struct1.strobeLight == 1;
            MsfsData.Instance.InstrumentsLightState = struct1.panelLight == 1;
            MsfsData.Instance.RecognitionLightState = struct1.recognitionLight == 1;
            MsfsData.Instance.WingLightState = struct1.wingLight == 1;
            MsfsData.Instance.LogoLightState = struct1.logoLight == 1;
            MsfsData.Instance.CabinLightState = struct1.cabinLight == 1;

            Debug.WriteLine(struct1.E1On);
            

            var pushChanged = false;
            UInt32 tug_angle = 0;
            if (MsfsData.Instance.PushbackLeft == 1)
            {
                tug_angle = (UInt32)(TUG_ANGLE * 0.8);
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, (UInt32)tug_angle, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                MsfsData.Instance.PushbackLeft = 0;
            }
            if (MsfsData.Instance.PushbackRight == 1)
            {
                tug_angle = TUG_ANGLE / 8;
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, (UInt32)tug_angle, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                MsfsData.Instance.PushbackRight = 0;
            }
            if (MsfsData.Instance.PushbackClick == 1)
            {
                if (MsfsData.Instance.Pushback == 0)
                {
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.TUG_DISABLE, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                else
                {
                    tug_angle = 0;
                    pushChanged = MsfsData.Instance.Pushback == 3;
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    if (pushChanged)
                        this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.TOGGLE_PUSHBACK, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                MsfsData.Instance.PushbackClick = 0;
            }

            if (MsfsData.Instance.SetToMSFS)
            {
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.GEAR_SET, (UInt32)MsfsData.Instance.CurrentGearHandle, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.PARKING_BRAKE, (UInt32)(MsfsData.Instance.CurrentBrakes ? 1 : 0), hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);

                if (MsfsData.Instance.Pause)
                {
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.PAUSE_ON, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                } else
                {
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.PAUSE_OFF, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }

                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.PITOT_HEAT_SET, (UInt32)(MsfsData.Instance.CurrentPitot ? 1 : 0), hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);

                MsfsData.Instance.SetToMSFS = false;
            } else
            {
                MsfsData.Instance.CurrentBrakesFromMSFS = struct1.parkingBrake == 1;
                MsfsData.Instance.CurrentAPAltitudeFromMSFS = (Int32)struct1.apAltitude;
                MsfsData.Instance.CurrentAPHeadingFromMSFS = (Int32)struct1.apHeading;
                MsfsData.Instance.CurrentAPSpeedFromMSFS = (Int32)struct1.apSpeed;
                MsfsData.Instance.CurrentAPVerticalSpeedFromMSFS = (Int32)struct1.apVSpeed;
            }
            
            this.SendEvent(MsfsData.Instance.EngineAutoOff, EVENTS.ENGINE_AUTO_SHUTDOWN, 0);
            this.SendEvent(MsfsData.Instance.EngineAutoOn, EVENTS.ENGINE_AUTO_START, 0);

            this.SendEvent(MsfsData.Instance.NavigationLight, EVENTS.NAV_LIGHTS_SET, MsfsData.Instance.NavigationLightState ? 0:1 );
            this.SendEvent(MsfsData.Instance.LandingLight, EVENTS.LANDING_LIGHTS_SET, MsfsData.Instance.LandingLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.BeaconLight, EVENTS.BEACON_LIGHTS_SET, MsfsData.Instance.BeaconLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.TaxiLight, EVENTS.TAXI_LIGHTS_SET, MsfsData.Instance.TaxiLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.StrobesLight, EVENTS.STROBES_SET, MsfsData.Instance.StrobesLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.InstrumentsLight, EVENTS.PANEL_LIGHTS_SET, MsfsData.Instance.InstrumentsLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.RecognitionLight, EVENTS.RECOGNITION_LIGHTS_SET, MsfsData.Instance.RecognitionLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.WingLight, EVENTS.WING_LIGHTS_SET, MsfsData.Instance.WingLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.LogoLight, EVENTS.LOGO_LIGHTS_SET, MsfsData.Instance.LogoLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.CabinLight, EVENTS.CABIN_LIGHTS_SET, MsfsData.Instance.CabinLightState ? 0 : 1);
            //this.SendEvent(MsfsData.Instance.ATC, EVENTS.ATC_MENU_OPEN, 0); // => with key waiting for simconnect inclusion
            //this.SendEvent(MsfsData.Instance.ATCClose, EVENTS.ATC_MENU_CLOSE, 0);
            this.SendEvent(MsfsData.Instance.ATC0, EVENTS.ATC_MENU_0, 0);
            this.SendEvent(MsfsData.Instance.ATC1, EVENTS.ATC_MENU_1, 0);
            this.SendEvent(MsfsData.Instance.ATC2, EVENTS.ATC_MENU_2, 0);
            this.SendEvent(MsfsData.Instance.ATC3, EVENTS.ATC_MENU_3, 0);
            this.SendEvent(MsfsData.Instance.ATC4, EVENTS.ATC_MENU_4, 0);
            this.SendEvent(MsfsData.Instance.ATC5, EVENTS.ATC_MENU_5, 0);
            this.SendEvent(MsfsData.Instance.ATC6, EVENTS.ATC_MENU_6, 0);
            this.SendEvent(MsfsData.Instance.ATC7, EVENTS.ATC_MENU_7, 0);
            this.SendEvent(MsfsData.Instance.ATC8, EVENTS.ATC_MENU_8, 0);
            this.SendEvent(MsfsData.Instance.ATC9, EVENTS.ATC_MENU_9, 0);


            if (MsfsData.Instance.ATC)
            {
                this.pluginForKey.ClientApplication.SendKeyboardShortcut((VirtualKeyCode)0x91);
            }

            this.ResetEvents();
        }
        private void SendEvent(Boolean inputKey, EVENTS eventName, Int64 value)
        {
            if (inputKey)
            {
                Debug.WriteLine("Send " + eventName);
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, (UInt32)value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
            }
        }

        private void ResetEvents()
        {
            MsfsData.Instance.EngineAutoOn = false;
            MsfsData.Instance.EngineAutoOff = false;
            MsfsData.Instance.NavigationLight = false;
            MsfsData.Instance.LandingLight = false;
            MsfsData.Instance.BeaconLight = false;
            MsfsData.Instance.TaxiLight = false;
            MsfsData.Instance.StrobesLight = false;
            MsfsData.Instance.InstrumentsLight = false;
            MsfsData.Instance.RecognitionLight = false;
            MsfsData.Instance.WingLight = false;
            MsfsData.Instance.LogoLight = false;
            MsfsData.Instance.CabinLight = false;
            MsfsData.Instance.ATC = false;
            MsfsData.Instance.ATCClose = false;
            MsfsData.Instance.ATC0 = false;
            MsfsData.Instance.ATC1 = false;
            MsfsData.Instance.ATC2 = false;
            MsfsData.Instance.ATC3 = false;
            MsfsData.Instance.ATC4 = false;
            MsfsData.Instance.ATC5 = false;
            MsfsData.Instance.ATC6 = false;
            MsfsData.Instance.ATC7 = false;
            MsfsData.Instance.ATC8 = false;
            MsfsData.Instance.ATC9 = false;
        }

        private void OnTick()
        {
            Debug.WriteLine("OnTick");
            m_oSimConnect?.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Struct1, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            m_oSimConnect?.ReceiveMessage();
            MsfsData.Instance.Changed();
        }

        private void AddRequest()
        {
            Console.WriteLine("AddRequest");
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Plane Heading Degrees True", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "Ground Altitude", "meters", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR RIGHT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR LEFT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GEAR CENTER POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "IS GEAR RETRACTABLE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "BRAKE PARKING POSITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENGINE TYPE", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "TURB ENG N1:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "TURB ENG N1:2", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "TURB ENG N1:3", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "TURB ENG N1:4", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "FUEL TOTAL CAPACITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "FUEL TOTAL QUANTITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENG FUEL FLOW GPH:1", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENG FUEL FLOW GPH:2", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENG FUEL FLOW GPH:3", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENG FUEL FLOW GPH:4", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "PUSHBACK STATE:0", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "PROP RPM:1", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "PROP RPM:2", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "NUMBER OF ENGINES", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "INDICATED ALTITUDE", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT ALTITUDE LOCK VAR", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS FLIGHT PLAN WP INDEX", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS WP DISTANCE", "Meters", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS WP ETE", "Seconds", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS WP BEARING", "Radians", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "GPS FLIGHT PLAN WP COUNT", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT HEADING LOCK DIR", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "PLANE HEADING DEGREES True", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "AIRSPEED INDICATED", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "VERTICAL SPEED", "feet/second", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT VERTICAL HOLD VAR", "Feet per minute", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "AUTOPILOT AIRSPEED HOLD VAR", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "ENG COMBUSTION:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT NAV", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT BEACON", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT LANDING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT TAXI", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT STROBE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT PANEL", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT RECOGNITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT WING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT LOGO", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Struct1, "LIGHT CABIN", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            
    
                
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.GEAR_SET, "GEAR_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PARKING_BRAKE, "PARKING_BRAKE_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ENGINE_AUTO_SHUTDOWN, "ENGINE_AUTO_SHUTDOWN");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ENGINE_AUTO_START, "ENGINE_AUTO_START");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PAUSE_ON, "PAUSE_ON");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PAUSE_OFF, "PAUSE_OFF");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PITOT_HEAT_SET, "PITOT_HEAT_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_PUSHBACK, "TOGGLE_PUSHBACK");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.KEY_TUG_HEADING, "KEY_TUG_HEADING");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TUG_DISABLE, "TUG_DISABLE");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.NAV_LIGHTS_SET, "NAV_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.LANDING_LIGHTS_SET, "LANDING_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.BEACON_LIGHTS_SET, "BEACON_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TAXI_LIGHTS_SET, "TAXI_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.STROBES_SET, "STROBES_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PANEL_LIGHTS_SET, "PANEL_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.RECOGNITION_LIGHTS_SET, "RECOGNITION_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.WING_LIGHTS_SET, "WING_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.LOGO_LIGHTS_SET, "LOGO_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.CABIN_LIGHTS_SET, "CABIN_LIGHTS_SET");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_OPEN, "ATC_MENU_OPEN");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_CLOSE, "SIMUI_WINDOW_HIDESHOW");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_0, "ATC_MENU_0");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_1, "ATC_MENU_1");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_2, "ATC_MENU_2");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_3, "ATC_MENU_3");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_4, "ATC_MENU_4");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_5, "ATC_MENU_5");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_6, "ATC_MENU_6");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_7, "ATC_MENU_7");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_8, "ATC_MENU_8");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_9, "ATC_MENU_9");

            this.m_oSimConnect.RegisterDataDefineStruct<Struct1>(DEFINITIONS.Struct1);
        }
    }
}


