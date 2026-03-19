using Alloy.Mvc._1.Models.Pages;

namespace Alloy.Mvc._1.Models.ViewModels;

public class PreviewModel(
    SitePageData currentPage,
    IContent previewContent) : PageViewModel<SitePageData>(currentPage)
{
    public IContent PreviewContent { get; set; } = previewContent;

    public List<PreviewArea> Areas { get; set; } = [];

    public class PreviewArea
    {
        public bool Supported { get; set; }

        public string AreaName { get; set; }

        public string AreaTag { get; set; }

        public ContentArea ContentArea { get; set; }
    }
}
