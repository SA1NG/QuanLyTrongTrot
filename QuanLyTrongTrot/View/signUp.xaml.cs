	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyTrongTrot.View
{
	/// <summary>
	/// Interaction logic for signUp.xaml
	/// </summary>
	public partial class signUp : Window
	{
		public signUp()
		{
			InitializeComponent();
		}
		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			string username = Username.Text.Trim();
			string email = Email.Text.Trim();
			string password = FloatingPasswordBox.Password.Trim();
			string confirmpassword = FloatingPasswordBox1.Password.Trim();

			//kiem tra cac du lieu nhap
			if (string.IsNullOrWhiteSpace(username))
			{
				ShowError(Username, "Tài khoản không được để trống!");
				return;
			}
			if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
			{
				ShowError(Email, "Email không hợp lệ!");
				return;
			}
			if (string.IsNullOrWhiteSpace(password)) {
				ShowError(FloatingPasswordBox, "Mật khẩu không được để trống!");
				return;
			}
			if (password != confirmpassword) {
				ShowError(FloatingPasswordBox1, "Mật khẩu không khớp!");
				return;	
			}
			MessageBox.Show("Đăng ký thành công","Hello World!", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		//hien thi loi
		private void ShowError(Control control, string errormassage)
		{
			ToolTip tooltip = new ToolTip()
			{
				Content = errormassage
            };
			control.ToolTip = tooltip;
			control.BorderBrush=System.Windows.Media.Brushes.Red;
		}

		//kiem tra email hop le
		private bool IsValidEmail(string email) {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern);
        }
    }
}
