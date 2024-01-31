namespace Loupedeck.MsfsPlugin.msfs
{
    using System;

    using Microsoft.FlightSimulator.SimConnect;
    using SimType = Microsoft.FlightSimulator.SimConnect.SIMCONNECT_DATATYPE;

    using static DataTransferTypes;
    using Loupedeck.MsfsPlugin.tools;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferIn
    {
        internal static void ReadMsfsValues(Readers reader)
        {
            MsfsData.Instance.AircraftName = reader.title;

            SetMsfsValue(BindingKeys.ENGINE_AUTO, reader.E1On);
            SetMsfsValue(BindingKeys.AILERON_TRIM, (Int64)Math.Round(reader.aileronTrim * 100));
            SetMsfsValue(BindingKeys.AP_ALT, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT, reader.planeAltitude);
            SetMsfsValue(BindingKeys.KOHLSMAN, (Int64)Math.Round(reader.kohlsmanInHb * 100));
            SetMsfsValue(BindingKeys.ELEVATOR_TRIM, (Int64)Math.Round(reader.elevatorTrim * 100));
            SetMsfsValue(BindingKeys.MAX_FLAP, reader.flapMax);
            SetMsfsValue(BindingKeys.FLAP, reader.flapPosition);
            SetMsfsValue(BindingKeys.AP_HEADING, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.MIXTURE, reader.mixtureE1);
            SetMsfsValue(BindingKeys.PROPELLER, (Int64)Math.Round(reader.propellerE1));
            SetMsfsValue(BindingKeys.RUDDER_TRIM, (Int64)Math.Round(reader.rudderTrim * 100));
            SetMsfsValue(BindingKeys.AP_SPEED, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED, reader.planeSpeed);
            SetMsfsValue(BindingKeys.SPOILER, (Int64)Math.Round(reader.spoiler * 100));
            SetMsfsValue(BindingKeys.THROTTLE, (Int64)Math.Round(reader.throttle * 100));
            SetMsfsValue(BindingKeys.AP_VSPEED, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.PARKING_BRAKES, reader.parkingBrake);
            SetMsfsValue(BindingKeys.PITOT, reader.pitot);
            SetMsfsValue(BindingKeys.GEAR_RETRACTABLE, reader.gearRetractable);
            SetMsfsValue(BindingKeys.GEAR_FRONT, (Int64)Math.Round(reader.gearCenterPos * 10));
            SetMsfsValue(BindingKeys.GEAR_LEFT, (Int64)Math.Round(reader.gearLeftPos * 10));
            SetMsfsValue(BindingKeys.GEAR_RIGHT, (Int64)Math.Round(reader.gearRightPos * 10));
            SetMsfsValue(BindingKeys.FUEL_FLOW_GPH, (Int64)Math.Round(reader.FuelGph));
            SetMsfsValue(BindingKeys.FUEL_FLOW_PPH, (Int64)Math.Round(reader.FuelPph * 100.0));
            SetMsfsValue(BindingKeys.FUEL_PERCENT, (Int64)Math.Round(reader.fuelQuantity * 100.0 / reader.fuelCapacity));
            SetMsfsValue(BindingKeys.FUEL_TIME_LEFT, (Int64)(reader.fuelQuantity / reader.FuelGph * 3600));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ID, reader.wpID);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_DIST, (Int64)Math.Round(reader.wpDistance * 0.00053996f, 1));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ETE, reader.wpETE);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_HEADING, reader.wpBearing);
            SetMsfsValue(BindingKeys.ENGINE_TYPE, reader.engineType);
            SetMsfsValue(BindingKeys.ENGINE_NUMBER, reader.engineNumber);
            SetMsfsValue(BindingKeys.E1RPM, reader.ENG1N1RPM);
            SetMsfsValue(BindingKeys.E2RPM, reader.ENG2N1RPM);
            SetMsfsValue(BindingKeys.E3RPM, reader.ENG3N1RPM);
            SetMsfsValue(BindingKeys.E4RPM, reader.ENG4N1RPM);
            SetMsfsValue(BindingKeys.E1N1, PercentValue(reader.E1N1));
            SetMsfsValue(BindingKeys.E2N1, PercentValue(reader.E2N1));
            SetMsfsValue(BindingKeys.E3N1, PercentValue(reader.E3N1));
            SetMsfsValue(BindingKeys.E4N1, PercentValue(reader.E4N1));
            SetMsfsValue(BindingKeys.E1N2, PercentValue(reader.E1N2));
            SetMsfsValue(BindingKeys.E2N2, PercentValue(reader.E2N2));
            SetMsfsValue(BindingKeys.E3N2, PercentValue(reader.E3N2));
            SetMsfsValue(BindingKeys.E4N2, PercentValue(reader.E4N2));
            SetMsfsValue(BindingKeys.PUSHBACK_ATTACHED, (reader.pushbackAttached == 1 && reader.wheelRPM != 0) ? 1 : 0);
            SetMsfsValue(BindingKeys.PUSHBACK_STATE, reader.onGround);
            //SetMsfsValue(BindingKeys.PUSHBACK_ANGLE, reader.pushback); // Can read but set so stay on the controller state

            SetMsfsValue(BindingKeys.LIGHT_NAV, reader.navLight);
            SetMsfsValue(BindingKeys.LIGHT_BEACON, reader.beaconLight);
            SetMsfsValue(BindingKeys.LIGHT_LANDING, reader.landingLight);
            SetMsfsValue(BindingKeys.LIGHT_TAXI, reader.taxiLight);
            SetMsfsValue(BindingKeys.LIGHT_STROBE, reader.strobeLight);
            SetMsfsValue(BindingKeys.LIGHT_INSTRUMENT, reader.panelLight);
            SetMsfsValue(BindingKeys.LIGHT_RECOG, reader.recognitionLight);
            SetMsfsValue(BindingKeys.LIGHT_WING, reader.wingLight);
            SetMsfsValue(BindingKeys.LIGHT_LOGO, reader.logoLight);
            SetMsfsValue(BindingKeys.LIGHT_CABIN, reader.cabinLight);
            SetMsfsValue(BindingKeys.LIGHT_PEDESTAL, reader.pedestralLight);
            SetMsfsValue(BindingKeys.LIGHT_GLARESHIELD, reader.glareshieldLight);

            SetMsfsValue(BindingKeys.AP_ALT_SWITCH, reader.apAltHold);
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH, reader.apHeadingHold);
            SetMsfsValue(BindingKeys.AP_NAV_SWITCH, reader.apNavHold);
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH, reader.apSpeedHold);
            SetMsfsValue(BindingKeys.AP_MASTER_SWITCH, reader.apMasterHold);
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH, reader.apThrottleHold);
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH, reader.apVerticalSpeedHold);
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_SWITCH, reader.apYawDamper);
            SetMsfsValue(BindingKeys.AP_BC_SWITCH, reader.apBackCourse);

            SetMsfsValue(BindingKeys.AP_FD_SWITCH_AL_FOLDER, reader.apFD);
            SetMsfsValue(BindingKeys.AP_FLC_SWITCH_AL_FOLDER, reader.apFLC);
            SetMsfsValue(BindingKeys.AP_APP_SWITCH_AL_FOLDER, reader.apAPP);
            SetMsfsValue(BindingKeys.AP_LOC_SWITCH_AL_FOLDER, reader.apLOC);

            SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY, reader.COM1ActiveFreq);
            SetMsfsValue(BindingKeys.COM1_STBY, reader.COM1StbFreq);
            SetMsfsValue(BindingKeys.COM1_AVAILABLE, reader.COM1Available);
            SetMsfsValue(BindingKeys.COM1_STATUS, reader.COM1Status);
            //SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE, COMtypeToInt(reader.COM1Type));

            SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY, reader.COM2ActiveFreq);
            SetMsfsValue(BindingKeys.COM2_STBY, reader.COM2StbFreq);
            SetMsfsValue(BindingKeys.COM2_AVAILABLE, reader.COM2Available);
            SetMsfsValue(BindingKeys.COM2_STATUS, reader.COM2Status);
            //SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE, this.COMtypeToInt(reader.COM2Type));

            SetMsfsValue(BindingKeys.SIM_RATE, (Int64)(reader.simRate * 100));
            SetMsfsValue(BindingKeys.SPOILERS_ARM, reader.spoilerArm);

            SetMsfsValue(BindingKeys.NAV1_ACTIVE_FREQUENCY, reader.NAV1ActiveFreq);
            SetMsfsValue(BindingKeys.NAV1_AVAILABLE, reader.NAV1Available);
            SetMsfsValue(BindingKeys.NAV1_STBY_FREQUENCY, reader.NAV1StbyFreq);
            SetMsfsValue(BindingKeys.NAV2_ACTIVE_FREQUENCY, reader.NAV2ActiveFreq);
            SetMsfsValue(BindingKeys.NAV2_AVAILABLE, reader.NAV2Available);
            SetMsfsValue(BindingKeys.NAV2_STBY_FREQUENCY, reader.NAV2StbyFreq);

            SetMsfsValue(BindingKeys.VOR1_OBS, reader.NAV1Obs);
            SetMsfsValue(BindingKeys.VOR2_OBS, reader.NAV2Obs);

            SetMsfsValue(BindingKeys.AIR_TEMP, (long)Math.Round(reader.AirTemperature * 10));
            SetMsfsValue(BindingKeys.WIND_DIRECTION, reader.WindDirection);
            SetMsfsValue(BindingKeys.WIND_SPEED, reader.WindSpeed);
            SetMsfsValue(BindingKeys.VISIBILITY, reader.Visibility);
            SetMsfsValue(BindingKeys.SEA_LEVEL_PRESSURE, (long)Math.Round(reader.SeaLevelPressure * 10));

            SetMsfsValue(BindingKeys.ADF_ACTIVE_FREQUENCY, (long)Math.Round(reader.ADFActiveFreq * 10));
            SetMsfsValue(BindingKeys.ADF_STBY_FREQUENCY, (long)Math.Round(reader.ADFStbyFreq * 10));
            SetMsfsValue(BindingKeys.ADF1_AVAILABLE, reader.ADF1Available);
            SetMsfsValue(BindingKeys.ADF1_STBY_AVAILABLE, reader.ADF2Available);

            //++ Insert appropriate SetMsfsValue calls here using the new binding keys and the new fields in reader.

            MsfsData.Instance.Changed();
        }

        // Percentages are rounded to nearest integer value:
        static Int64 PercentValue(Double value) => (Int64)Math.Round(value);

        static void SetMsfsValue(BindingKeys key, long value) => MsfsData.Instance.bindings[key].SetMsfsValue(value);

        internal static void AddRequest(SimConnect simConnect)
        {
            void AddReaderDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Readers, name, units, type);

            void AddWriterDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Writers, name, units, type);

            void AddToDataDefinition(DEFINITIONS dataDef, string name, string units, SimType type) =>
                simConnect.AddToDataDefinition(dataDef, name, units, type, 0.0f, SimConnect.SIMCONNECT_UNUSED);

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
            AddReaderDef("GPS WP BEARING", "Radians", SimType.INT64);                                     //        wpBearing            AP_NEXT_WP_HEADING
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
            AddReaderDef("AUTOPILOT FLIGHT DIRECTOR ACTIVE:1", "Boolean", SimType.INT64);                 //        apFD                 AP_FD_SWITCH_AL_FOLDER
            AddReaderDef("AUTOPILOT FLIGHT LEVEL CHANGE", "Boolean", SimType.INT64);                      //        apFLC                AP_FLC_SWITCH_AL_FOLDER
            AddReaderDef("AUTOPILOT APPROACH HOLD", "Boolean", SimType.INT64);                            //        apAPP                AP_APP_SWITCH_AL_FOLDER
            AddReaderDef("AUTOPILOT APPROACH IS LOCALIZER", "Boolean", SimType.INT64);                    //        apLOC                AP_LOC_SWITCH_AL_FOLDER
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

            foreach (EVENTS evt in Enum.GetValues(typeof(EVENTS)))
            {
                simConnect.MapClientEventToSimEvent(evt, evt.ToString());
            }

            simConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            simConnect.RegisterDataDefineStruct<Writers>(DEFINITIONS.Writers);
        }
    }
}
