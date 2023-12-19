namespace Loupedeck.MsfsPlugin.msfs
{
    using System;

    using Microsoft.FlightSimulator.SimConnect;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferIn
    {
        internal static void ReadMsfsValues(Readers reader)
        {
            MsfsData.Instance.AircraftName = reader.title;

            MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO].SetMsfsValue(reader.E1On);
            MsfsData.Instance.bindings[BindingKeys.AILERON_TRIM].SetMsfsValue((Int64)Math.Round(reader.aileronTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_ALT].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_INPUT].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_INPUT].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.KOHLSMAN].SetMsfsValue((Int64)Math.Round(reader.kohlsmanInHb * 100));
            MsfsData.Instance.bindings[BindingKeys.ELEVATOR_TRIM].SetMsfsValue((Int64)Math.Round(reader.elevatorTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].SetMsfsValue(reader.flapMax);
            MsfsData.Instance.bindings[BindingKeys.FLAP].SetMsfsValue(reader.flapPosition);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_INPUT].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_INPUT].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.MIXTURE].SetMsfsValue(reader.mixtureE1);
            MsfsData.Instance.bindings[BindingKeys.PROPELLER].SetMsfsValue((Int64)Math.Round(reader.propellerE1));
            MsfsData.Instance.bindings[BindingKeys.RUDDER_TRIM].SetMsfsValue((Int64)Math.Round(reader.rudderTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_INPUT].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_INPUT].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPOILER].SetMsfsValue((Int64)Math.Round(reader.spoiler * 100));
            MsfsData.Instance.bindings[BindingKeys.THROTTLE].SetMsfsValue((Int64)Math.Round(reader.throttle * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_INPUT].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_INPUT].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.PARKING_BRAKES].SetMsfsValue(reader.parkingBrake);
            MsfsData.Instance.bindings[BindingKeys.PITOT].SetMsfsValue(reader.pitot);
            MsfsData.Instance.bindings[BindingKeys.GEAR_RETRACTABLE].SetMsfsValue(reader.gearRetractable);
            MsfsData.Instance.bindings[BindingKeys.GEAR_FRONT].SetMsfsValue((Int64)Math.Round(reader.gearCenterPos * 10));
            MsfsData.Instance.bindings[BindingKeys.GEAR_LEFT].SetMsfsValue((Int64)Math.Round(reader.gearLeftPos * 10));
            MsfsData.Instance.bindings[BindingKeys.GEAR_RIGHT].SetMsfsValue((Int64)Math.Round(reader.gearRightPos * 10));
            MsfsData.Instance.bindings[BindingKeys.FUEL_FLOW].SetMsfsValue((Int64)(reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH));
            MsfsData.Instance.bindings[BindingKeys.FUEL_PERCENT].SetMsfsValue((Int64)(reader.fuelQuantity * 100 / reader.fuelCapacity));
            MsfsData.Instance.bindings[BindingKeys.FUEL_TIME_LEFT].SetMsfsValue((Int64)(reader.fuelQuantity / (Double)(reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH) * 3600));
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_ID].SetMsfsValue(reader.wpID);
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_DIST].SetMsfsValue((Int64)Math.Round(reader.wpDistance * 0.00053996f, 1));
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_ETE].SetMsfsValue(reader.wpETE);
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_HEADING].SetMsfsValue(reader.wpBearing);
            MsfsData.Instance.bindings[BindingKeys.ENGINE_TYPE].SetMsfsValue(reader.engineType);
            MsfsData.Instance.bindings[BindingKeys.ENGINE_NUMBER].SetMsfsValue(reader.engineNumber);
            MsfsData.Instance.bindings[BindingKeys.E1RPM].SetMsfsValue(reader.ENG1N1RPM);
            MsfsData.Instance.bindings[BindingKeys.E2RPM].SetMsfsValue(reader.ENG2N1RPM);
            MsfsData.Instance.bindings[BindingKeys.E1N1].SetMsfsValue(reader.E1N1);
            MsfsData.Instance.bindings[BindingKeys.E2N1].SetMsfsValue(reader.E2N1);
            MsfsData.Instance.bindings[BindingKeys.E3N1].SetMsfsValue(reader.E3N1);
            MsfsData.Instance.bindings[BindingKeys.E4N1].SetMsfsValue(reader.E4N1);
            MsfsData.Instance.bindings[BindingKeys.PUSHBACK_ATTACHED].SetMsfsValue((reader.pushbackAttached == 1 && reader.wheelRPM != 0) ? 1 : 0);
            MsfsData.Instance.bindings[BindingKeys.PUSHBACK_STATE].SetMsfsValue(reader.onGround);
            //MsfsData.Instance.bindings[BindingKeys.PUSHBACK_ANGLE].SetMsfsValue(reader.pushback); // Can read but set so stay on the controller state
            MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_MULTI].SetMsfsValue(reader.navLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_MULTI].SetMsfsValue(reader.beaconLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_MULTI].SetMsfsValue(reader.landingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_MULTI].SetMsfsValue(reader.taxiLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_MULTI].SetMsfsValue(reader.strobeLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_MULTI].SetMsfsValue(reader.panelLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_MULTI].SetMsfsValue(reader.recognitionLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_MULTI].SetMsfsValue(reader.wingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_MULTI].SetMsfsValue(reader.logoLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_MULTI].SetMsfsValue(reader.cabinLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_PEDESTRAL_MULTI].SetMsfsValue(reader.pedestralLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_GLARESHIELD_MULTI].SetMsfsValue(reader.glareshieldLight);

            MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_FOLDER].SetMsfsValue(reader.navLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_FOLDER].SetMsfsValue(reader.beaconLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_FOLDER].SetMsfsValue(reader.landingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_FOLDER].SetMsfsValue(reader.taxiLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_FOLDER].SetMsfsValue(reader.strobeLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_FOLDER].SetMsfsValue(reader.panelLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_FOLDER].SetMsfsValue(reader.recognitionLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_FOLDER].SetMsfsValue(reader.wingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_FOLDER].SetMsfsValue(reader.logoLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_FOLDER].SetMsfsValue(reader.cabinLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_PEDESTRAL_FOLDER].SetMsfsValue(reader.pedestralLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_GLARESHIELD_FOLDER].SetMsfsValue(reader.glareshieldLight);

            MsfsData.Instance.bindings[BindingKeys.AP_ALT_AP_FOLDER].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_AP_FOLDER].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AP_FOLDER].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_AP_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AP_FOLDER].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_AP_FOLDER].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AP_FOLDER].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_AP_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AP_FOLDER].SetMsfsValue(reader.apAltHold);
            MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AP_FOLDER].SetMsfsValue(reader.apHeadingHold);
            MsfsData.Instance.bindings[BindingKeys.AP_NAV_SWITCH_AP_FOLDER].SetMsfsValue(reader.apNavHold);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AP_FOLDER].SetMsfsValue(reader.apSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_MASTER_SWITCH_AP_FOLDER].SetMsfsValue(reader.apMasterHold);
            MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AP_FOLDER].SetMsfsValue(reader.apThrottleHold);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AP_FOLDER].SetMsfsValue(reader.apVerticalSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_YAW_DAMPER_AP_FOLDER].SetMsfsValue(reader.apYawDamper);
            MsfsData.Instance.bindings[BindingKeys.AP_BC_AP_FOLDER].SetMsfsValue(reader.apBackCourse);

            MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH].SetMsfsValue(reader.apAltHold);
            MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH].SetMsfsValue(reader.apHeadingHold);
            MsfsData.Instance.bindings[BindingKeys.AP_NAV_SWITCH].SetMsfsValue(reader.apNavHold);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH].SetMsfsValue(reader.apSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_MASTER_SWITCH].SetMsfsValue(reader.apMasterHold);
            MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH].SetMsfsValue(reader.apThrottleHold);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH].SetMsfsValue(reader.apVerticalSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_YAW_DAMPER_SWITCH].SetMsfsValue(reader.apYawDamper);
            MsfsData.Instance.bindings[BindingKeys.AP_BC_SWITCH].SetMsfsValue(reader.apBackCourse);

            MsfsData.Instance.bindings[BindingKeys.AP_ALT_AL_FOLDER].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_AL_FOLDER].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AL_FOLDER].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_AL_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AL_FOLDER].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_AL_FOLDER].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AL_FOLDER].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_AL_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_FD_SWITCH_AL_FOLDER].SetMsfsValue(reader.apFD);
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AL_FOLDER].SetMsfsValue(reader.apAltHold);
            MsfsData.Instance.bindings[BindingKeys.AP_SWITCH_AL_FOLDER].SetMsfsValue(reader.apMasterHold);
            MsfsData.Instance.bindings[BindingKeys.AP_GPS_SWITCH_AL_FOLDER].SetMsfsValue(reader.apNavHold);
            MsfsData.Instance.bindings[BindingKeys.AP_FLC_SWITCH_AL_FOLDER].SetMsfsValue(reader.apFLC);
            MsfsData.Instance.bindings[BindingKeys.AP_APP_SWITCH_AL_FOLDER].SetMsfsValue(reader.apAPP);
            MsfsData.Instance.bindings[BindingKeys.AP_LOC_SWITCH_AL_FOLDER].SetMsfsValue(reader.apLOC);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AL_FOLDER].SetMsfsValue(reader.apSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AL_FOLDER].SetMsfsValue(reader.apHeadingHold);
            MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AL_FOLDER].SetMsfsValue(reader.apThrottleHold);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AL_FOLDER].SetMsfsValue(reader.apVerticalSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_YAW_DAMPER_AL_FOLDER].SetMsfsValue(reader.apYawDamper);
            MsfsData.Instance.bindings[BindingKeys.AP_BC_AL_FOLDER].SetMsfsValue(reader.apBackCourse);

            MsfsData.Instance.bindings[BindingKeys.COM1_ACTIVE_FREQUENCY].SetMsfsValue(reader.COM1ActiveFreq);
            MsfsData.Instance.bindings[BindingKeys.COM1_STBY].SetMsfsValue(reader.COM1StbFreq);
            MsfsData.Instance.bindings[BindingKeys.COM1_AVAILABLE].SetMsfsValue(reader.COM1Available);
            MsfsData.Instance.bindings[BindingKeys.COM1_STATUS].SetMsfsValue(reader.COM1Status);
            //MsfsData.Instance.bindings[BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE].SetMsfsValue(COMtypeToInt(reader.COM1Type));

            MsfsData.Instance.bindings[BindingKeys.COM2_ACTIVE_FREQUENCY].SetMsfsValue(reader.COM2ActiveFreq);
            MsfsData.Instance.bindings[BindingKeys.COM2_STBY].SetMsfsValue(reader.COM2StbFreq);
            MsfsData.Instance.bindings[BindingKeys.COM2_AVAILABLE].SetMsfsValue(reader.COM2Available);
            MsfsData.Instance.bindings[BindingKeys.COM2_STATUS].SetMsfsValue(reader.COM2Status);
            //MsfsData.Instance.bindings[BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE].SetMsfsValue(this.COMtypeToInt(reader.COM2Type));

            MsfsData.Instance.bindings[BindingKeys.SIM_RATE].SetMsfsValue((Int64)(reader.simRate * 100));
            MsfsData.Instance.bindings[BindingKeys.SPOILERS_ARM].SetMsfsValue(reader.spoilerArm);

            MsfsData.Instance.bindings[BindingKeys.NAV1_ACTIVE_FREQUENCY].SetMsfsValue(reader.NAV1ActiveFreq);
            MsfsData.Instance.bindings[BindingKeys.NAV1_AVAILABLE].SetMsfsValue(reader.NAV1Available);
            MsfsData.Instance.bindings[BindingKeys.NAV1_STBY_FREQUENCY].SetMsfsValue(reader.NAV1StbyFreq);
            MsfsData.Instance.bindings[BindingKeys.NAV2_ACTIVE_FREQUENCY].SetMsfsValue(reader.NAV2ActiveFreq);
            MsfsData.Instance.bindings[BindingKeys.NAV2_AVAILABLE].SetMsfsValue(reader.NAV2Available);
            MsfsData.Instance.bindings[BindingKeys.NAV2_STBY_FREQUENCY].SetMsfsValue(reader.NAV2StbyFreq);

            MsfsData.Instance.bindings[BindingKeys.VOR1_OBS].SetMsfsValue(reader.NAV1Obs);
            MsfsData.Instance.bindings[BindingKeys.VOR2_OBS].SetMsfsValue(reader.NAV2Obs);

            MsfsData.Instance.bindings[BindingKeys.AIR_TEMP].SetMsfsValue((long)Math.Round(reader.AirTemperature * 10));
            MsfsData.Instance.bindings[BindingKeys.WIND_DIRECTION].SetMsfsValue(reader.WindDirection);
            MsfsData.Instance.bindings[BindingKeys.WIND_SPEED].SetMsfsValue(reader.WindSpeed);
            MsfsData.Instance.bindings[BindingKeys.VISIBILITY].SetMsfsValue(reader.Visibility);
            MsfsData.Instance.bindings[BindingKeys.SEA_LEVEL_PRESSURE].SetMsfsValue((long)Math.Round(reader.SeaLevelPressure * 10));

            MsfsData.Instance.bindings[BindingKeys.ADF_ACTIVE_FREQUENCY].SetMsfsValue((long)Math.Round(reader.ADFActiveFreq * 10));
            MsfsData.Instance.bindings[BindingKeys.ADF_STBY_FREQUENCY].SetMsfsValue((long)Math.Round(reader.ADFStbyFreq * 10));
            MsfsData.Instance.bindings[BindingKeys.ADF1_AVAILABLE].SetMsfsValue(reader.ADF1Available);
            MsfsData.Instance.bindings[BindingKeys.ADF1_STBY_AVAILABLE].SetMsfsValue(reader.ADF2Available);

            //++ Insert appropriate SetMsfsValue calls here using the new binding keys and the new fields in reader.

            MsfsData.Instance.Changed();
        }

        internal static void AddRequest(SimConnect simConnect)
        {
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "Ground Altitude", "meters", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR RIGHT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR LEFT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR CENTER POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "IS GEAR RETRACTABLE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "BRAKE PARKING POSITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENGINE TYPE", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:2", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:3", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:4", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "FUEL TOTAL CAPACITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "FUEL TOTAL QUANTITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:1", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:2", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:3", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:4", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PUSHBACK STATE:0", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PROP RPM:1", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PROP RPM:2", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NUMBER OF ENGINES", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "INDICATED ALTITUDE", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT ALTITUDE LOCK VAR", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS FLIGHT PLAN WP INDEX", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP DISTANCE", "Meters", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP ETE", "Seconds", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP BEARING", "Radians", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS FLIGHT PLAN WP COUNT", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT HEADING LOCK DIR", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PLANE HEADING DEGREES MAGNETIC", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AIRSPEED INDICATED", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "VERTICAL SPEED", "feet/second", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT VERTICAL HOLD VAR", "Feet per minute", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT AIRSPEED HOLD VAR", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG COMBUSTION:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT NAV", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT BEACON", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT LANDING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT TAXI", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT STROBE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT PANEL", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT RECOGNITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT WING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT LOGO", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT CABIN", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT ALTITUDE LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT HEADING LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MACH HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MANAGED THROTTLE ACTIVE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MASTER", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT NAV1 LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT VERTICAL HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "KOHLSMAN SETTING HG:1", "inHg", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AILERON TRIM PCT", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ELEVATOR TRIM PCT", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "FLAPS NUM HANDLE POSITIONS", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "FLAPS HANDLE INDEX", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG PROPELLER LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "RUDDER TRIM PCT", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "SPOILERS HANDLE POSITION", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PITOT HEAT SWITCH:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "CENTER WHEEL RPM", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT FLIGHT DIRECTOR ACTIVE:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT FLIGHT LEVEL CHANGE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT APPROACH HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT APPROACH IS LOCALIZER", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "SIM ON GROUND", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "GROUND VELOCITY", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "PUSHBACK ATTACHED", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQUENCY:1", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM STANDBY FREQUENCY:1", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM AVAILABLE:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM STATUS:1", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            //this.simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQ TYPE:1", null, SIMCONNECT_DATATYPE.STRINGV, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQUENCY:2", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM STANDBY FREQUENCY:2", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM AVAILABLE:2", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM STATUS:2", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            //this.simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQ TYPE:2", null, SIMCONNECT_DATATYPE.STRINGV, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT PEDESTRAL", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT GLARESHIELD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT YAW DAMPER", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT BACKCOURSE HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "SIMULATION RATE", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "SPOILERS ARMED", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV ACTIVE FREQUENCY:1", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV ACTIVE FREQUENCY:2", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV AVAILABLE:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV AVAILABLE:2", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV STANDBY FREQUENCY:1", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV STANDBY FREQUENCY:2", "Hz", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV OBS:1", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "NAV OBS:2", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "TOTAL AIR TEMPERATURE", "Celsius", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AMBIENT WIND DIRECTION", "Degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AMBIENT WIND VELOCITY", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "AMBIENT VISIBILITY", "Meters", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "SEA LEVEL PRESSURE", "Millibars", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ADF ACTIVE FREQUENCY:1", "KHz", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ADF STANDBY FREQUENCY:1", "KHz", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ADF AVAILABLE:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Readers, "ADF STANDBY AVAILABLE:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            //++ Make new data definitions here using a type that fits SimConnect variable if it needs to be read from the Sim

            simConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:3", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            simConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:4", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            foreach (EVENTS evt in Enum.GetValues(typeof(EVENTS)))
            {
                simConnect.MapClientEventToSimEvent(evt, evt.ToString());
            }

            simConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            simConnect.RegisterDataDefineStruct<Writers>(DEFINITIONS.Writers);
        }
    }
}
