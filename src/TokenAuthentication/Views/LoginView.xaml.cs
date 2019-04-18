using System.Security;
using System.Windows.Controls;
using TokenAuthentication.Example.Core;

namespace TokenAuthentication.Example.Views
{
    public partial class LoginView : UserControl, IHavePassword
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public SecureString Password
        {
            get { return passwordBox.SecurePassword; }
        }

        public void ClearPassword()
        {
            passwordBox.Clear();
        }
    }
}
