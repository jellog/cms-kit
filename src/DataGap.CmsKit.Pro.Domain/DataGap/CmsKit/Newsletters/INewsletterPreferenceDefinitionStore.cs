using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGap.CmsKit.Newsletters;

public interface INewsletterPreferenceDefinitionStore
{
    Task<List<NewsletterPreferenceDefinition>> GetNewslettersAsync();
}
