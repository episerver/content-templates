using Stride.Mvc._1.Models.ViewModels;

namespace Stride.Mvc._1.Business;

/// <summary>
/// Defines a method which may be invoked by PageContextActionFilter allowing controllers
/// to modify common layout properties of the view model.
/// </summary>
internal interface IModifyLayout
{
    public void ModifyLayout(LayoutModel layoutModel);
}
