using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_END
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void BTN_NhanVien_Click(object sender, EventArgs e)
        {
            // Ẩn form Menu
            this.Hide();

            // Hiển thị form NhanVien
            FromNhanVien formNhanVien = new FromNhanVien();
            formNhanVien.FormClosed += (s, args) => this.Show(); // Hiển thị lại form Menu khi form NhanVien đóng
            formNhanVien.Show();
        }

        private void BTN_PhongBan_Click(object sender, EventArgs e)
        {
            // Ẩn form Menu
            this.Hide();

            // Hiển thị form PhongBan
            FormPhongBan formPhongBan = new FormPhongBan();
            formPhongBan.FormClosed += (s, args) => this.Show(); // Hiển thị lại form Menu khi form PhongBan đóng
            formPhongBan.Show();
        }

        private void BTN_Exit_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng chọn "Đồng ý"
            if (result == DialogResult.Yes)
            {
                // Đóng ứng dụng
                Application.Exit();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
