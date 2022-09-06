using System;
using JetBrains.Annotations;
using DataGap.Jellog;
using DataGap.Jellog.Auditing;
using DataGap.Jellog.Domain.Entities;
using DataGap.Jellog.Domain.Entities.Auditing;
using DataGap.Jellog.MultiTenancy;

namespace DataGap.CmsKit.UrlShorting
{
    public class ShortenedUrl : BasicAggregateRoot<Guid>, IMultiTenant, ICreationAuditedObject
    {
        [NotNull]
        public virtual string Source { get; protected set; }

        [NotNull]
        public virtual string Target { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }

        public DateTime CreationTime { get; protected set; }

        public Guid? CreatorId { get; protected set; }

        protected ShortenedUrl()
        {

        }

        public ShortenedUrl(
            Guid id,
            [NotNull] string source,
            [NotNull] string target,
            [CanBeNull] Guid? tenantId = null
        ) : base(id)
        {
            Source = Check.NotNullOrWhiteSpace(source, nameof(source), ShortenedUrlConst.MaxSourceLength);
            SetTarget(target);
            TenantId = tenantId;
        }

        public virtual void SetTarget(string target)
        {
            Target = Check.NotNullOrWhiteSpace(target, nameof(target), ShortenedUrlConst.MaxTargetLength);
        }
    }
}
