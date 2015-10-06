using System;

namespace Halloween.Models
{
    public interface IPinProvider
    {
        Action<int, bool> InputPinStateChange { get; set; }
        Action<int, bool> OutputPinStateChange { get; set; }

        void SetOutputPinState(int pinIndex, bool state);
    }
}
