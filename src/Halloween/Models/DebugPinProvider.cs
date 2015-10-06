using System;
using System.Diagnostics;

namespace Halloween.Models
{
    public class DebugPinProvider : IPinProvider
    {
        public DebugPinProvider()
        {
            OutputPinStateChange = (pinIndex, state) =>
            {
                Debug.WriteLine(
                    $"Output pin [{pinIndex}] has changed state to {state.ToString().ToLower()}.");
            };
        }

        public Action<int, bool> InputPinStateChange { get; set; }
        public Action<int, bool> OutputPinStateChange { get; set; }

        public void SetOutputPinState(int pinIndex, bool state)
        {
            OutputPinStateChange(pinIndex, state);
        }
    }
}
