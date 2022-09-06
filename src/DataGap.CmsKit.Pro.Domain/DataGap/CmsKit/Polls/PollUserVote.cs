﻿using System;
using DataGap.Jellog.Auditing;
using DataGap.Jellog.Domain.Entities;
using DataGap.Jellog.MultiTenancy;

namespace DataGap.CmsKit.Polls;
public class PollUserVote : BasicAggregateRoot<Guid>, IMultiTenant, IHasCreationTime
{
    public virtual Guid? TenantId { get; protected set; }
    public virtual Guid PollId { get; protected set; }
    public virtual Guid UserId { get; protected set; }
    public virtual Guid PollOptionId { get; protected set; }
    public DateTime CreationTime { get; protected set; }

    protected PollUserVote()
    {

    }

    public PollUserVote(
        Guid id,
        Guid pollId,
        Guid userId,
        Guid pollOptionId,
        Guid? tenantId = null)
        : base(id)
    {
        PollId = pollId;
        UserId = userId;
        PollOptionId = pollOptionId;
        TenantId = tenantId;
    }
}
