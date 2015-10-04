using Microsoft.AspNet.Html.Abstractions;

namespace Halloween.ViewModels.Displays
{
    public interface IDisplay
    {
        string AudioSource { get; }

        IHtmlContent ToJson();
    }
}
