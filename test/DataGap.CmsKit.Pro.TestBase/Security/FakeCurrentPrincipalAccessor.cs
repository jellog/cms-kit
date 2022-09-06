using System;
using System.Collections.Generic;
using System.Security.Claims;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Security.Claims;

namespace DataGap.CmsKit.Pro.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency
{
    public ClaimsPrincipal Principal => GetPrincipal();
    private ClaimsPrincipal _principal;

    private ClaimsPrincipal GetPrincipal()
    {
        if (_principal == null)
        {
            lock (this)
            {
                if (_principal == null)
                {
                    _principal = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new List<Claim>
                            {
                                    new Claim(JellogClaimTypes.UserId,"2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
                                    new Claim(JellogClaimTypes.UserName,"admin"),
                                    new Claim(JellogClaimTypes.Email,"admin@jellog.io")
                            }
                        )
                    );
                }
            }
        }

        return _principal;
    }

    public IDisposable Change(ClaimsPrincipal principal)
    {
        _principal = principal;
        return null;
    }
}
