using DataGap.CmsKit.Pro.Polls;
using Xunit;

namespace DataGap.CmsKit.Pro.MongoDB.Polls;
[Collection(MongoTestCollection.Name)]
public class PollRepository_Tests : PollRepository_Tests<CmsKitProMongoDbTestModule>
{
}
