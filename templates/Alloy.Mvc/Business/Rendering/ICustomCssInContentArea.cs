namespace Alloy.Mvc._1.Business.Rendering
{
    /// <summary>
    /// Defines a property for CSS class(es) which will be added to the class
    /// attribute of containing elements when rendered in a content area with a size tag.
    /// </summary>
    internal interface ICustomCssInContentArea
    {
        string ContentAreaCssClass { get; }
    }
}
