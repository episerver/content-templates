namespace Stride.Mvc._1.Models.ViewModels;

public class PreviewModel(
    PageData currentPage,
    IContent previewContent) : PageViewModel<PageData>(currentPage)
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
