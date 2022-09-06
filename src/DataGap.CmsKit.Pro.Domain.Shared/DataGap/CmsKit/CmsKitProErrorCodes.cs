namespace DataGap.CmsKit;

public static class CmsKitProErrorCodes
{
    public static class UrlShorting
    {
        public const string ShortenedUrlAlreadyExistsException = "CmsKitPro:UrlForwarding:0001";
    }
    public static class Poll
    {
        public const string PollAllowSingleVoteException = "CmsKitPro:Poll:0001"; 
        public const string PollEndDateCannotSetBeforeStartDateException = "CmsKitPro:Poll:0002"; 
        public const string PollResultShowingEndDateCannotSetBeforeStartDate = "CmsKitPro:Poll:0003";
        public const string PollUserVoteVotedBySameUser = "CmsKitPro:Poll:0004";
        public const string PollOptionWidgetNameCannotBeSame = "CmsKitPro:Poll:0005";
    }
    
}
