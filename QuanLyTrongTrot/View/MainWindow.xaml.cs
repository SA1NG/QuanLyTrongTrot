using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTrongTrot
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private bool HanhChinhMoRong = false;
		private bool GiongCayTrongMoRong = false;
		private bool PhanBonMoRong = false;
		private bool ThuocbtvtMoRong = false;
		private bool HeThongMoRong = false;
		//xu ly su kien khi nhan nut hanh chinh
		private void HanhChinhButton_Click(object sender, RoutedEventArgs e)
		{
			//doi trang thai
			HanhChinhMoRong = !HanhChinhMoRong;
			HanhChinhConButton.Visibility = HanhChinhMoRong ? Visibility.Visible : Visibility.Collapsed;
		}

		private void GiongCayTrongButton_Click(object sender, RoutedEventArgs e)
		{
			GiongCayTrongMoRong = !GiongCayTrongMoRong;
			GiongCayTrongConButton.Visibility = GiongCayTrongMoRong ? Visibility.Visible : Visibility.Collapsed;
		}
		private void PhanBonButton_Click(object sender, RoutedEventArgs e)
		{
			PhanBonMoRong = !PhanBonMoRong;
			PhanBonConButton.Visibility = PhanBonMoRong ? Visibility.Visible : Visibility.Collapsed;
		}
		private void ThuocbvtvButton_Click(object sender, RoutedEventArgs e)
		{
			ThuocbtvtMoRong = !ThuocbtvtMoRong;
			ThuocbvtvButton.Visibility = ThuocbtvtMoRong ? Visibility.Visible : Visibility.Collapsed;
		}

		private void HeThongButton_Click(object sender, RoutedEventArgs e)
		{
			HeThongMoRong = !HeThongMoRong;
			HeThongButton.Visibility = HeThongMoRong ? Visibility.Visible : Visibility.Collapsed;		}
	}
}

