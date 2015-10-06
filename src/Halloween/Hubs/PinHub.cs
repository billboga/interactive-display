using Halloween.Models;
using Microsoft.AspNet.SignalR;

namespace Halloween.Hubs
{
    public class PinHub : Hub
    {
        public PinHub(IPinProvider pinProvider)
        {
            this.pinProvider = pinProvider;

            pinProvider.InputPinStateChange += (index, state) =>
            {
                InputPinStateChange(index, state);
            };
        }

        private readonly IPinProvider pinProvider;

        public void InputPinStateChange(int pinIndex, bool state)
        {
            Clients.All.inputPinStateChange(pinIndex, state);
        }

        public void SetOutputPinState(int pinIndex, bool state)
        {
            pinProvider.SetOutputPinState(pinIndex, state);
        }
    }
}
