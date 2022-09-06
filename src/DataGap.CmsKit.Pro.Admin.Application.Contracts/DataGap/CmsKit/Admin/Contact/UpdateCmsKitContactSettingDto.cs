using System.ComponentModel.DataAnnotations;

namespace DataGap.CmsKit.Admin.Contact;

public class UpdateCmsKitContactSettingDto
{
    [Required]
    [EmailAddress]
    public string ReceiverEmailAddress { get; set; }
}
