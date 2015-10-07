using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;

namespace Halloween.Models
{
    public class RaspberryPiPinProvider : IPinProvider
    {
        public RaspberryPiPinProvider()
        {
            inputPins = new List<PinConfiguration>()
            {
                ConnectorPin.P1Pin11.Input().PullDown()
            };

            outputPins = new List<PinConfiguration>()
            {
                ConnectorPin.P1Pin12.Output(),
                ConnectorPin.P1Pin13.Output(),
                ConnectorPin.P1Pin15.Output(),
                ConnectorPin.P1Pin16.Output()
            };

            connection = new GpioConnection();

            inputPins.ForEach(x =>
            {
                x.OnStatusChanged(state =>
                {
                    if (InputPinStateChange != null)
                    {
                        InputPinStateChange(inputPins.IndexOf(x), state);
                    }
                });

                connection.Add(x);
            });

            outputPins.ForEach(x => connection.Add(x));
        }

        private readonly GpioConnection connection;
        private readonly List<PinConfiguration> inputPins;
        private readonly List<PinConfiguration> outputPins;

        public Action<int, bool> InputPinStateChange { get; set; }
        public Action<int, bool> OutputPinStateChange { get; set; }

        public void SetOutputPinState(int pinIndex, bool state)
        {
            var outputPin = outputPins[pinIndex];

            connection[outputPin] = state;

            OutputPinStateChange(pinIndex, state);
        }
    }
}
