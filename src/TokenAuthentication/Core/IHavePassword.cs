using System.Security;

namespace TokenAuthentication.Example.Core
{
    public interface IHavePassword
    {
        SecureString Password { get; }
        void ClearPassword();
    }
}