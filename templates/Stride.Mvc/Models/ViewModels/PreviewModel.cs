namespace Stride.Mvc._1.Models.ViewModels;

public class PreviewModel : PageViewModel<PageData>
{
    public PreviewModel(PageData currentPage, IContent previewContent)
        : base(currentPage)
    {
        PreviewContent = previewContent;
        Areas = new List<PreviewArea>();
    }

    public IContent PreviewContent { get; set; }

    public List<PreviewArea> Areas { get; set; } = [];

    public class PreviewArea
    {
        public bool Supported { get; set; }

        public string AreaName { get; set; }

        public string AreaTag { get; set; }

        public ContentArea ContentArea { get; set; }
    }
}
