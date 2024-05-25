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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Lab4_END
{
    public partial class FormPhongBan : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=LAPTOP-D3V3BSKB;Initial Catalog=Lab4;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * From PhongBan";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            Data_PhongBan.DataSource = table;
        }
        public FormPhongBan()
        {
            InitializeComponent();
        }

        private void BTN_Home_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form hiện tại (PhongBan)
        }

        private void FormPhongBan_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void Data_PhongBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Data_PhongBan.CurrentRow.Index;
            txt_MaPhongBan.Text = Data_PhongBan.Rows[i].Cells[0].Value.ToString();
            txt_SoDienThoai.Text = Data_PhongBan.Rows[i].Cells[1].Value.ToString();
            txt_DiaChi.Text = Data_PhongBan.Rows[i].Cells[2].Value.ToString();
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            txt_MaPhongBan.Text = "";
            txt_SoDienThoai.Text = "";
            txt_DiaChi.Text = "";
            loaddata();
        }

        private void BTN_Add_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các trường thông tin đã được nhập đầy đủ chưa
            if (string.IsNullOrEmpty(txt_MaPhongBan.Text) || string.IsNullOrEmpty(txt_SoDienThoai.Text) || string.IsNullOrEmpty(txt_DiaChi.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin. Vui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại và không tiếp tục thêm dữ liệu
            }

            // Kiểm tra xem Mã phòng ban đã tồn tại trong cơ sở dữ liệu chưa
            command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM PhongBan WHERE MaPhongBan = @MaPhongBan";
            command.Parameters.AddWithValue("@MaPhongBan", txt_MaPhongBan.Text);
            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Mã số phòng đã tồn tại. Vui lòng nhập mã số phòng khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại và không tiếp tục thêm dữ liệu
            }

            // Thêm dữ liệu vào cơ sở dữ liệu nếu không có lỗi
            command.CommandText = "Insert into PhongBan Values (@MaPhongBan, @SoDienThoai, @DiaChi)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@MaPhongBan", txt_MaPhongBan.Text);
            command.Parameters.AddWithValue("@SoDienThoai", txt_SoDienThoai.Text);
            command.Parameters.AddWithValue("@DiaChi", txt_DiaChi.Text);

            command.ExecuteNonQuery();
            loaddata();

            // Hiển thị thông báo khi thêm thành công
            MessageBox.Show("Thêm phòng ban thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTN_Edit_Click(object sender, EventArgs e)
        {
         

            // Thực hiện câu lệnh UPDATE trong cơ sở dữ liệu
            command = connection.CreateCommand();
            command.CommandText = "UPDATE PhongBan SET soDT = @SoDienThoai, diaDiem = @DiaChi WHERE maPhongBan = @MaPhongBan";
            command.Parameters.AddWithValue("@SoDienThoai", txt_SoDienThoai.Text);
            command.Parameters.AddWithValue("@DiaChi", txt_DiaChi.Text);
            command.Parameters.AddWithValue("@MaPhongBan", txt_MaPhongBan.Text);

            // Thực thi câu lệnh UPDATE và load lại dữ liệu
            command.ExecuteNonQuery();
            loaddata();

            // Hiển thị thông báo khi sửa thành công
            MessageBox.Show("Sửa thông tin phòng ban thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void BTN_Delete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "Delete From NhanVien Where maPhongBan = '" + txt_MaPhongBan.Text + "'";
            command.ExecuteNonQuery();
            command.CommandText = "Delete From PhongBan Where maPhongBan = '" + txt_MaPhongBan.Text + "'";
            command.ExecuteNonQuery();
            loaddata();
            loaddata();
            MessageBox.Show("Xóa Phong Ban thành công, Các Nhân Viên trong phòng ban cũng bị xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
