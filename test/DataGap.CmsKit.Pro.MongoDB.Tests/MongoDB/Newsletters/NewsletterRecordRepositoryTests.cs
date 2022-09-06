using DataGap.CmsKit.Pro.Newsletters;
using Xunit;

namespace DataGap.CmsKit.Pro.MongoDB.Newsletters;

[Collection(MongoTestCollection.Name)]
public class NewsletterRecordRepositoryTests : NewsletterRecordRepository_Tests<CmsKitProMongoDbTestModule>
{
}
