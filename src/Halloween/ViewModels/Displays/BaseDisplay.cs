﻿using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Halloween.ViewModels.Displays
{
    public abstract class BaseDisplay : IDisplay
    {
        public BaseDisplay()
        {
            IsEnabled = true;
        }

        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string AudioSource { get; set; }
        public bool IsEnabled { get; set; }

        public IHtmlContent ToJson()
        {
            return new HtmlString(JsonConvert.SerializeObject(this, jsonSerializerSettings));
        }
    }
}
