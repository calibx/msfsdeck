namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Runtime.InteropServices;

    using Loupedeck.MsfsPlugin.tools;
    using static Loupedeck.MsfsPlugin.msfs.DataTransferTypes;

    public class SimConnectDAO
    {
        private bool registered = false;

        private static System.Timers.Timer timer;

        private static readonly Lazy<SimConnectDAO> lazy = new Lazy<SimConnectDAO>(() => new SimConnectDAO());
        public static SimConnectDAO Instance => lazy.Value;


        private const double timerInterval = 200;
       
        private readonly Binding connection;
        private readonly Binding autoTaxi;

        private SimConnectDAO()
        {
            connection = MsfsData.Instance.Register(BindingKeys.CONNECTION);
            autoTaxi = MsfsData.Instance.Register(BindingKeys.AUTO_TAXI);
        }

        private void Refresh(object source, EventArgs e)
        {
            lock (lockObject)
            {
                try
                {
                    if (SimConnectWrapper.Instance.IsConnected())
                    {
                        DebugTracing.Trace("Connected");
                        connection.SetMsfsValue(1);
                        if (!registered)
                        {
                            DataTransferOut.setPlugin(MsfsData.Instance.plugin);
                            DataTransferOut.initEvents();
                            registered = true;
                        }
                        DataTransferOut.SendEvents(SimConnectWrapper.Instance);
                        DataTransferIn.ReadMsfsValues(SimConnectWrapper.Instance);
                        MsfsData.Instance.Changed(false);
                    }
                    else
                    {
                        DebugTracing.Trace("Disconnect");
                        timer.Enabled = false;
                        timer = null;
                        connection.SetMsfsValue(0);
                        registered = false;
                        MsfsData.Instance.Changed(true);
                    }
                    
                }
                catch (COMException exception)
                {
                    DebugTracing.Trace(exception);
                    Disconnect();
                }
            }
        }
        public void Connect()
        {
            if (connection.MsfsValue == 0)
            {
                DebugTracing.Trace("Trying cnx");
                connection.SetMsfsValue(2);
                foreach (Binding binding in MsfsData.Instance.bindings.Values)
                {
                    binding.MSFSChanged = true;
                }
                MsfsData.Instance.Changed(true);
                try
                {
                    SimConnectWrapper.Instance.Connect();
                    timer = new System.Timers.Timer();
                    lock (timer)
                    {
                        timer = new System.Timers.Timer();
                        timer.Interval = timerInterval;
                        timer.Elapsed += Refresh;
                        timer.Enabled = true;
                    }
                }
                catch (COMException ex)
                {
                    DebugTracing.Trace("Error during cnx" + ex.ToString());
                    connection.SetMsfsValue(0);
                    foreach (Binding binding in MsfsData.Instance.bindings.Values)
                    {
                        binding.MSFSChanged = true;
                    }
                    MsfsData.Instance.Changed(true);
                }
            }
        }
      
        public void Disconnect(bool unloading = false)
        {
            DebugTracing.Trace($"Disconnecting - unloading={unloading}");
            SimConnectWrapper.Instance.Disconnect();

            if (unloading)
                return;

            connection.SetMsfsValue(0);
            foreach (Binding binding in MsfsData.Instance.bindings.Values)
            {
                binding.MSFSChanged = true;
            }
            MsfsData.Instance.Changed(true);
        }

        private readonly object lockObject = new object();

/*        private void AutoTaxiInput(Readers reader)
        {
            if (reader.onGround == 1)
            {
                if (autoTaxi.ControllerValue >= 2)
                {
                    if (reader.groundSpeed > 19)
                    {
                        autoTaxi.SetMsfsValue(3);
                        DataTransferOut.Transmit(m_oSimConnect, EVENTS.BRAKES, 1);
                    }
                    else
                    {
                        autoTaxi.SetMsfsValue(2);
                    }
                }
                else
                {
                    autoTaxi.SetMsfsValue(1);
                }
            }
            else
            {
                autoTaxi.SetMsfsValue(0);
            }
        }*/


    }
}
