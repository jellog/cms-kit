using DataGap.CmsKit.Pro.UrlShorting;
using Xunit;

namespace DataGap.CmsKit.Pro.MongoDB.UrlShorting;

[Collection(MongoTestCollection.Name)]
public class ShortenedUrlRepository_Tests : ShortenedUrlRepository_Tests<CmsKitProMongoDbTestModule>
{
}
