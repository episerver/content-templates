using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace MyAppNamespace
{
    [ContentType(
        DisplayName = "MyContentData",
        GUID = "CA56CED4-2CA9-4C5E-9EDE-2A5A6F1F06EF",
        Description = "")]
    public class MyContentData : PageData
    {
        [CultureSpecific]
        [Display(
            Name = "MyProperty",
            Description = "My property description",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual string MyProperty { get; set; }
    }
}
