using Halloween.Extensions;
using Halloween.Models;
using Halloween.Models.Displays;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using System.Linq;

namespace Halloween.Controllers
{
    public class DisplaysController : Controller
    {
        public DisplaysController(IOptions<AppSettings> options)
        {
            appSettings = options.Options;
        }

        private readonly AppSettings appSettings;

        public IActionResult Index()
        {
            var modelDictionary = appSettings.Displays.FirstOrDefault(x => x.Key == "TextGlitchDisplay").Value;

            var model = modelDictionary.ToObject(typeof(TextGlitchDisplay)) as IDisplay;

            return View("TextGlitchDisplay", model);
        }
    }
}