using Alloy.Mvc._1.Models.Blocks;
using AlloyMvc1;

namespace Alloy.Mvc._1.Models.ViewModels;

public class PageListModel
{
    public PageListModel(PageListBlock block)
    {
        Heading = block.Heading;
        ShowIntroduction = block.IncludeIntroduction;
        ShowPublishDate = block.IncludePublishDate;
    }
    public string Heading { get; set; }

    public IPageListBlockQuery_SitePageData ListResult { get; set; }

    public bool ShowIntroduction { get; set; }

    public bool ShowPublishDate { get; set; }
}
