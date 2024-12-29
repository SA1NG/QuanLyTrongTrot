using QuanLyTrongTrot.View;
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
		//khai bao cac trang
		private UserControl trang_chu_page;
		private UserControl ban_do_page;
		private UserControl giong_cay_page;
		private UserControl hanh_chinh_page;
		private UserControl he_thong_page;
		private UserControl phan_bon_page;
		private UserControl sinh_vat_gay_hai_page;
		private UserControl thuoc_bvtv_page;
		public MainWindow()
		{
			InitializeComponent();
			//khoi tao cac trang
			trang_chu_page = new TrangChuPage();
			ban_do_page = new BanDoPage();
			giong_cay_page = new GiongCayPage();
			hanh_chinh_page = new HanhChinhPage();
			he_thong_page = new HeThongPage();
			phan_bon_page = new PhanBonPage();
			sinh_vat_gay_hai_page = new SinhVatGayHaiPage();
			thuoc_bvtv_page = new ThuocbvtvPage();

			//mac dinh
			MainPage.Content = trang_chu_page;
		}

		

		//khai bao cac nut mo rong
		private bool HanhChinhMoRong = false;
		private bool GiongCayTrongMoRong = false;
		private bool PhanBonMoRong = false;
		private bool ThuocbtvtMoRong = false;
		private bool HeThongMoRong = false;

		//xu ly su kien khi nhan nut hanh chinh
		private void TrangChuButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.Content = trang_chu_page;
		}
		private void HanhChinhButton_Click(object sender, RoutedEventArgs e)
		{
			//doi trang thai
			HanhChinhMoRong = !HanhChinhMoRong;
			HanhChinhConButton.Visibility = HanhChinhMoRong ? Visibility.Visible : Visibility.Collapsed;
			MainPage.Content = hanh_chinh_page;
		}

		private void GiongCayTrongButton_Click(object sender, RoutedEventArgs e)
		{
			GiongCayTrongMoRong = !GiongCayTrongMoRong;
			GiongCayTrongConButton.Visibility = GiongCayTrongMoRong ? Visibility.Visible : Visibility.Collapsed;
			MainPage.Content = giong_cay_page;
		}
		private void PhanBonButton_Click(object sender, RoutedEventArgs e)
		{
			PhanBonMoRong = !PhanBonMoRong;
			PhanBonConButton.Visibility = PhanBonMoRong ? Visibility.Visible : Visibility.Collapsed;
			MainPage.Content = phan_bon_page;
		}
		private void ThuocbvtvButton_Click(object sender, RoutedEventArgs e)
		{
			ThuocbtvtMoRong = !ThuocbtvtMoRong;
			ThuocbvtvButton.Visibility = ThuocbtvtMoRong ? Visibility.Visible : Visibility.Collapsed;
			MainPage.Content = thuoc_bvtv_page;
		}

		private void HeThongButton_Click(object sender, RoutedEventArgs e)
		{
			HeThongMoRong = !HeThongMoRong;
			HeThongButton.Visibility = HeThongMoRong ? Visibility.Visible : Visibility.Collapsed;
			MainPage.Content=he_thong_page;
		}
		private void SinhVatGayHaiButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.Content = sinh_vat_gay_hai_page;
		}

		private void BanDoButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.Content = ban_do_page;
		}

		// nut tat va thu nho
		private void TatButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }

		private void ThuNhoButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

	}
}

