using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Globalization;
using QuanLyTrongTrot.Controller;
using QuanLyTrongTrot.Model;

namespace QuanLyTrongTrot.View
{
    /// <summary>
    /// Interaction logic for HanhChinhPage.xaml
    /// </summary>
    public partial class HanhChinhPage : UserControl
    {
        private HanhChinhController _controller;
        private CapDoHanhChinh _selectedItem;
        private DonViHanhChinh _selectedItem1;

        public HanhChinhPage()
        {
            InitializeComponent();
            _controller = new HanhChinhController(new Provider()); // Thay bằng lớp provider cụ thể
            LoadData();
        }

        /// <summary>
        /// Tải dữ liệu ban đầu
        /// </summary>
        private void LoadData()
        {
            var data = _controller.GetCapDoHanhChinh();
            CapDoHanhChinh.ItemsSource = data;
        }
        private void LoadData1() {
            var data = _controller.GetDonViHanhChinh();
            DonViHanhChinh.ItemsSource = data;
        }

        /// <summary>
        /// Xử lý khi nhấn nút tìm kiếm
        /// </summary>
        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var keyword = txtSearch.Text.Trim();
        //    var searchResult = _controller.SearchCapDoHanhChinh(keyword);
        //    HanhChinhDataGrid.ItemsSource = searchResult;
        //}

        /// <summary>
        /// Xử lý khi chọn một dòng trong bảng
        /// </summary>
        private void DataGridHanhChinh_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedItem = HanhChinhDataGrid.SelectedItem as CapDoHanhChinh;
        }

        /// <summary>
        /// Xử lý khi nhấn nút thêm mới
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new DonViHanhChinh
            {
                MaDonVi = "MDV_NEW",
                TenDonVi = "Đơn vị mới",
                CapDoID = 0,
                CapTrenID = null
            };

            if (_controller.AddDonViHanhChinh(newItem))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Xử lý khi nhấn nút xóa (nếu có thêm nút xóa sau này)
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_controller.DeleteDonViHanhChinh(_selectedItem.ID))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
