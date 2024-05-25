using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_END
{
    public partial class FromNhanVien : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=LAPTOP-D3V3BSKB;Initial Catalog=Lab4;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * From NhanVien";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            Data_NhanVien.DataSource = table;
        }
        public FromNhanVien()
        {
            InitializeComponent();
        }

        private void BTN_Home_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form hiện tại (PhongBan)
        }

        private void FromNhanVien_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void Data_NhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Data_NhanVien.CurrentRow.Index;
            txtMaNhanVien.Text = Data_NhanVien.Rows[i].Cells[0].Value.ToString();
            txtHoVaTen.Text = Data_NhanVien.Rows[i].Cells[1].Value.ToString();
            txtDiaChi.Text = Data_NhanVien.Rows[i].Cells[2].Value.ToString();
            txtSoDienThoai.Text = Data_NhanVien.Rows[i].Cells[3].Value.ToString();
            txtMaPhongBan.Text = Data_NhanVien.Rows[i].Cells[4].Value.ToString();
        }

        private void BTN_Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNhanVien.Text) || string.IsNullOrEmpty(txtHoVaTen.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text) || string.IsNullOrEmpty(txtMaPhongBan.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin. Vui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM NhanVien WHERE maNhanVien = @MaNhanVien";
                command.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã nhân viên khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    command.CommandText = "INSERT INTO NhanVien VALUES (@MaNhanVien, @HoVaTen, @DiaChi, @SoDienThoai, @MaPhongBan)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
                    command.Parameters.AddWithValue("@HoVaTen", txtHoVaTen.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                    command.Parameters.AddWithValue("@MaPhongBan", txtMaPhongBan.Text);

                    command.ExecuteNonQuery();
                    loaddata();

                    MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            txtMaNhanVien.Text = "";
            txtHoVaTen.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
            txtMaPhongBan.Text = "";
            loaddata();
        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {
            {
                command = connection.CreateCommand();
                command.CommandText = "Update NhanVien set hoTen = @HoVaTen, diaChi = @DiaChi, soDT = @SoDienThoai, maPhongBan = @MaPhongBan where maNhanVien = @MaNhanVien";
                command.Parameters.AddWithValue("@HoVaTen", txtHoVaTen.Text);
                command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                command.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                command.Parameters.AddWithValue("@MaPhongBan", txtMaPhongBan.Text);
                command.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);

                command.ExecuteNonQuery();
                loaddata();

                MessageBox.Show("Sửa thông tin nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "Delete From NhanVien Where maNhanVien = @MaNhanVien";
            command.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
            command.ExecuteNonQuery();
            loaddata();

            MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
