using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataGap.Jellog.DependencyInjection;

namespace DataGap.CmsKit.Newsletters.Helpers;

public class SecurityCodeProvider : ITransientDependency
{
    private const string Salt = "this-is-a-jellog-newsletter-key";

    public virtual string GetSecurityCode(string emailAddress)
    {
        using (var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(Salt)))
        {
            var securityCode = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(emailAddress));

            return string.Concat(securityCode.Select(x => x.ToString("x2")));
        }
    }
}
