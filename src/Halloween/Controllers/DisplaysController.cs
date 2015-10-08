using Halloween.Extensions;
using Halloween.Hubs;
using Halloween.Models;
using Halloween.ViewModels.Displays;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Framework.OptionsModel;
using System.Linq;

namespace Halloween.Controllers
{
    public class DisplaysController : Controller
    {
        public DisplaysController(
            IOptions<AppSettings> options,
            IHubContext<PinHub> pinHub)
        {
            appSettings = options.Options;
            this.pinHub = pinHub;
        }

        private readonly AppSettings appSettings;
        private readonly IHubContext<PinHub> pinHub;

        public IActionResult Index()
        {
            var modelDictionary = appSettings.Displays.FirstOrDefault(x => x.Key == "TextWarpDisplay").Value;

            var model = modelDictionary.ToObject(typeof(TextWarpDisplay)) as IDisplay;

            return View("TextWarpDisplay", model);
        }

        public IActionResult TestInput()
        {
            pinHub.Clients.All.inputPinStateChange(0, true);

            return Ok();
        }
    }
}