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

    namespace QuanLyTrongTrot.View
    {
        public partial class ThuócBVTV  : UserControl
        {
           

            // Sự kiện khi nhấn nút Tìm kiếm
            private void SearchButton_Click(object sender, RoutedEventArgs e)
            {
                // Xử lý tìm kiếm theo ID và Tên Thuốc
                MessageBox.Show("Tìm kiếm theo ID hoặc Tên Thuốc");
            }

            // Sự kiện khi nhấn nút Thêm
            private void AddButton_Click(object sender, RoutedEventArgs e)
            {
                // Xử lý thêm thuốc mới
                MessageBox.Show("Thêm Thuốc Bảo Vệ Thực Vật");
            }

            // Sự kiện khi nhấn nút Sửa
            private void EditButton_Click(object sender, RoutedEventArgs e)
            {
                // Xử lý sửa thuốc
                MessageBox.Show("Sửa Thuốc Bảo Vệ Thực Vật");
            }

            // Sự kiện khi nhấn nút Xóa
            private void DeleteButton_Click(object sender, RoutedEventArgs e)
            {
                // Xử lý xóa thuốc
                MessageBox.Show("Xóa Thuốc Bảo Vệ Thực Vật");
            }
        }
    }