using System;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Pro;

public class CmsKitProTestData : ISingletonDependency
{
    public Guid NewsletterEmailId { get; } = Guid.NewGuid();

    public string Email { get; } = "info@jellog.io";

    public string Preference { get; } = "Community";

    public string Source { get; } = "Community.ArticleRead";

    public Guid ShortenedUrlId1 { get; } = Guid.NewGuid();

    public string ShortenedUrlSource1 { get; } = "/SomeUrl";

    public string ShortenedUrlTarget1 { get; } = "Blog/51235-12354-21323-a2412";

    public Guid ShortenedUrlId2 { get; } = Guid.NewGuid();

    public string ShortenedUrlSource2 { get; } = "SomeUrl2";

    public string ShortenedUrlTarget2 { get; } = "Docs/51235-12354-5234-a2412";

    public Guid PollId { get; } = Guid.NewGuid();
    public Guid PollOptionId { get; } = Guid.NewGuid();
    public string Question { get; } = "What is your question?";
    public string Widget { get; } = "poll-left";

}
