namespace Loupedeck.MsfsPlugin.input.misc
{
    using System;

    using Loupedeck.MsfsPlugin.msfs;
    using Loupedeck.MsfsPlugin.tools;

    internal class MiscInput : ActionEditorCommand
    {
        private const string vendorListBox = "vendorListBox";
        private const string aircraftListBox = "aircraftListBox";
        private const string componentListBox = "componentListBox";
        private const string eventListBox = "eventListBox";
        private readonly ActionEditorListbox VendorGroup;
        private readonly ActionEditorListbox AircraftGroup;
        private readonly ActionEditorListbox ComponentGroup;
        private readonly ActionEditorListbox EventList;
        private bool vendorLoaded = false;

        public MiscInput()
        {
            DisplayName = "Miscellaneous input";
            Description = "Allow to choose which event to send to MSFS";
            GroupName = "Misc";

            VendorGroup = new ActionEditorListbox(name: vendorListBox, labelText: "Vendor:");
            ActionEditor.AddControlEx(VendorGroup.SetRequired());

            AircraftGroup = new ActionEditorListbox(name: aircraftListBox, labelText: "Aircraft:");
            ActionEditor.AddControlEx(AircraftGroup.SetRequired());

            ComponentGroup = new ActionEditorListbox(name: componentListBox, labelText: "Components:");
            ActionEditor.AddControlEx(ComponentGroup.SetRequired());

            EventList = new ActionEditorListbox(name: eventListBox, labelText: "Event to send:");
            ActionEditor.AddControlEx(EventList.SetRequired());

            ActionEditor.ListboxItemsRequested += OnActionEditorListboxItemsRequested;
            ActionEditor.ControlValueChanged += OnActionEditorControlValueChanged;
        }

        private void OnActionEditorControlValueChanged(object sender, ActionEditorControlValueChangedEventArgs e)
        {
            var nameItem = e.ControlName;
            var currentVendor = e.ActionEditorState.GetControlValue(vendorListBox);
            var currentAircraft = e.ActionEditorState.GetControlValue(aircraftListBox);
            var currentComponent = e.ActionEditorState.GetControlValue(componentListBox);
            DebugTracing.Trace("Value changed on " + nameItem + $" for {currentVendor} {currentAircraft} {currentComponent}");
            if (nameItem.Equals(vendorListBox))
            {
                ActionEditor.ListboxItemsChanged(aircraftListBox);
            }
            else if (nameItem.Equals(aircraftListBox))
            {
                ActionEditor.ListboxItemsChanged(componentListBox);
            }
            else if (nameItem.Equals(componentListBox))
            {
                ActionEditor.ListboxItemsChanged(eventListBox);
            }
        }

        private void OnActionEditorListboxItemsRequested(object sender, ActionEditorListboxItemsRequestedEventArgs e)
        {
            var nameItem = e.ControlName;
            var currentVendor = e.ActionEditorState.GetControlValue(vendorListBox);
            var currentAircraft = e.ActionEditorState.GetControlValue(aircraftListBox);
            var currentComponent = e.ActionEditorState.GetControlValue(componentListBox);
            var currentEvent = e.ActionEditorState.GetControlValue(eventListBox);
            DebugTracing.Trace("Request list on " + nameItem.ToString() + $" for {currentVendor} {currentAircraft} {currentComponent} {currentEvent}");
            if (nameItem.Equals(vendorListBox) || !vendorLoaded)
            {
                foreach (var GroupKey in DataTransferOut.MobiEvents.Keys)
                {
                    e.AddItem(GroupKey, GroupKey, GroupKey);
                }
                vendorLoaded = true;
            }
            else if (nameItem.Equals(aircraftListBox))
            {
                if (!string.IsNullOrEmpty(currentVendor))
                {
                    var exist = false;
                    foreach (var aircraftKey in DataTransferOut.MobiEvents[currentVendor].Keys)
                    {
                        e.AddItem(aircraftKey, aircraftKey, aircraftKey);
                        if (currentAircraft != null && DataTransferOut.MobiEvents[currentVendor].ContainsKey(currentAircraft))
                        {
                            exist = true;
                        }
                    }
                    if (!exist)
                    {
                        e.SetSelectedItemName("");
                        e.ActionEditorState.SetValue(componentListBox, "");
                        e.ActionEditorState.SetValue(eventListBox, "");
                    }

                }
            }
            else if (nameItem.Equals(componentListBox))
            {
                var exist = false;
                if (currentVendor != null && currentVendor != "" && currentAircraft != null && currentAircraft != "")
                {
                    foreach (var componentKey in DataTransferOut.MobiEvents[currentVendor][currentAircraft].Keys)
                    {
                        e.AddItem(currentAircraft + "/" + componentKey, componentKey, componentKey);
                        if (currentAircraft.Equals(currentComponent.Split("/")[0]) && currentComponent != null && DataTransferOut.MobiEvents[currentVendor][currentAircraft].ContainsKey(currentComponent.Split("/")[1]))
                        {
                            exist = true;
                        }

                    }
                }
                if (!exist)
                {
                    e.SetSelectedItemName("");
                    e.ActionEditorState.SetValue(eventListBox, "");
                }
            }
            else
            {
                if (currentVendor != null && currentVendor != "" && currentAircraft != null && currentAircraft != "" && currentComponent != null && currentComponent != "")
                {
                    var exist = false;
                    foreach (Tuple<string, uint> eventKey in DataTransferOut.MobiEvents[currentVendor][currentAircraft][currentComponent.Split("/")[1]])
                    {
                        e.AddItem(eventKey.Item1, eventKey.Item1, eventKey.Item1);
                        if (eventKey.Item1.Equals(currentEvent))
                        {
                            exist = true;
                        }

                    }
                    if (!exist)
                    {
                        e.SetSelectedItemName("");
                    }
                }
            }
        }

        protected override bool RunCommand(ActionEditorActionParameters actionParameters)
        {
            actionParameters.TryGetString(vendorListBox, out var vendor);
            actionParameters.TryGetString(aircraftListBox, out var aircraft);
            actionParameters.TryGetString(componentListBox, out var components);
            actionParameters.TryGetString(eventListBox, out var eventName);
            DebugTracing.Trace("Exec " + vendor + " " + aircraft + " " + components + " " + eventName);
            Tuple<string, uint> eventItem = DataTransferOut.MobiEvents[vendor][aircraft][components.Split("/")[1]].Find(x => x.Item1 == eventName);
            SimConnectWrapper.Instance.send((DataTransferTypes.DUMMY_EVENTS)eventItem.Item2, 1);
            return true;
        }

    }
}
