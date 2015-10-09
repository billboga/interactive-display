using Halloween.Extensions;
using Halloween.Hubs;
using Halloween.Models;
using Halloween.ViewModels.Displays;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Halloween.Controllers
{
    public class DisplaysController : Controller
    {
        public DisplaysController(
            IOptions<AppSettings> options,
            IHubContext<PinHub> pinHub)
        {
            appSettings = options.Options;
            displays = Assembly.GetAssembly(typeof(DisplaysController))
                .GetTypes()
                .Where(x => typeof(IDisplay).IsAssignableFrom(x)
                && !x.IsInterface
                && !x.IsAbstract);
            this.pinHub = pinHub;
        }

        private readonly AppSettings appSettings;
        private readonly IEnumerable<Type> displays;
        private readonly IHubContext<PinHub> pinHub;

        public IActionResult Index()
        {
            var display = displays.ElementAt(new Random().Next(0, displays.Count()));
            var displayName = display.Name;

            var modelDictionary = appSettings.Displays.FirstOrDefault(x => x.Key == displayName).Value;

            var model = modelDictionary.ToObject(display) as IDisplay;

            return View(displayName, model);
        }

        public IActionResult TestInput(int pinIndex, bool pinState)
        {
            pinHub.Clients.All.inputPinStateChange(pinIndex, pinState);

            return Ok();
        }
    }
}