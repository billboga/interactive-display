using Microsoft.AspNet.Html.Abstractions;

namespace Halloween.ViewModels.Displays
{
    public interface IDisplay
    {
        IHtmlContent ToJson();
    }
}
