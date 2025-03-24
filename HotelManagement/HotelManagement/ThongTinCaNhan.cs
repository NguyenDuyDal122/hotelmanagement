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

namespace HotelManagement
{
    public partial class ThongTinCaNhan : Form
    {
        private int userId; // ID của người dùng
        private string connectstring = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";
        public ThongTinCaNhan(int userId) // ✅ Constructor nhận userId
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserInfo(); // Gọi hàm để tải thông tin
        }

        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {

        }

        private void LoadUserInfo()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectstring))
                {
                    connect.Open();
                    string query = "SELECT username, full_name, phone, email, role FROM [User] WHERE id = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Nếu có dữ liệu
                            {
                                txt_username.Text = reader["username"].ToString();
                                txt_fullname.Text = reader["full_name"].ToString();
                                txt_phone.Text = reader["phone"].ToString();
                                txt_email.Text = reader["email"].ToString();
                                txt_role.Text = reader["role"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
