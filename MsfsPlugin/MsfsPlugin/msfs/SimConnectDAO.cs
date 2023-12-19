namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Runtime.InteropServices;
    using Loupedeck.MsfsPlugin.tools;

    using Microsoft.FlightSimulator.SimConnect;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    public class SimConnectDAO
    {
        private SimConnectDAO() { }
        private static readonly Lazy<SimConnectDAO> lazy = new Lazy<SimConnectDAO>(() => new SimConnectDAO());
        
        public static SimConnectDAO Instance => lazy.Value;

        public const Int32 WM_USER_SIMCONNECT = 0x0402;

        private SimConnect m_oSimConnect = null;

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();

        private const double timerInterval = 200;

        private enum DATA_REQUESTS
        {
            REQUEST_1
        }

        public enum hSimconnect : int
        {
            group1
        }

        public static void Refresh(Object source, EventArgs e) => Instance.OnTick();

        public void setPlugin(Plugin plugin) => DataTransferOut.setPlugin(plugin);

        public void Connect()
        {
            if (MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 0)
            {
                DebugTracing.Trace("Trying cnx");
                MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(2);
                foreach (Binding binding in MsfsData.Instance.bindings.Values)
                {
                    binding.MSFSChanged = true;
                }
                MsfsData.Instance.Changed();
                try
                {
                    m_oSimConnect = new SimConnect("MSFS Plugin", new IntPtr(0), WM_USER_SIMCONNECT, null, 0);
                    m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(SimConnect_OnRecvOpen);
                    m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(SimConnect_OnRecvSimobjectDataBytype);

                    DataTransferIn.AddRequest(m_oSimConnect);

                    lock (timer)
                    {
                        timer.Interval = timerInterval;
                        timer.Elapsed += Refresh;
                        timer.Enabled = true;
                    }
                }
                catch (COMException ex)
                {
                    DebugTracing.Trace(ex);
                    MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(0);
                    foreach (Binding binding in MsfsData.Instance.bindings.Values)
                    {
                        binding.MSFSChanged = true;
                    }
                    MsfsData.Instance.Changed();
                }
            }
        }

        public void Disconnect()
        {
            if (m_oSimConnect != null)
            {
                m_oSimConnect.Dispose();
                m_oSimConnect = null;
            }

            //>> If called from Unload, then I think that the rest here is superfluous to do. We could add a parameter
            // indicating whether we are about to unload and if so return here.

            MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(0);
            foreach (Binding binding in MsfsData.Instance.bindings.Values)
            {
                binding.MSFSChanged = true;
            }
            MsfsData.Instance.Changed();
        }

        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            DebugTracing.Trace("Cnx opened");
            MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(1);
            foreach (Binding binding in MsfsData.Instance.bindings.Values)
            {
                binding.MSFSChanged = true;
            }
            MsfsData.Instance.Changed();
            timer.Interval = timerInterval;
        }

        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            var reader = (Readers)data.dwData[0];
            DataTransferIn.ReadMsfsValues(reader);

            DataTransferOut.SendEvents(m_oSimConnect);
            AutoTaxiInput(reader);
        }

        private readonly object lockObject = new object();

        private void OnTick()
        {
            lock (lockObject)
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
                catch (COMException exception)
                {
                    DebugTracing.Trace(exception);
                    Disconnect();
                }
            }
        }

        private void AutoTaxiInput(Readers reader)
        {
            if (reader.onGround == 1)
            {
                if (MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].ControllerValue >= 2)
                {
                    if (reader.groundSpeed > 19)
                    {
                        MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(3);
                        m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.BRAKES, 1, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    }
                    else
                    {
                        MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(2);
                    }
                }
                else
                {
                    MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(1);
                }
            }
            else
            {
                MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(0);
            }
        }
    }
}



