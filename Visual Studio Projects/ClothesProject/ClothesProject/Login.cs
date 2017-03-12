using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if ("".Equals(textBox_username))
            {
                label_prompt.Text = "用户名不能为空！";
            } else
            {
                if ("".Equals(textBox_password))
                {
                    label_prompt.Text = "密码不能为空！";
                } else
                {
                    connection_DB();
                }
            }
        }
        private void connection_DB()
        {
            string connStr = "server=localhost;user=root;database=fashion;port=3306;password=mysql_rootadmin;IgnorePrepare=false;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                label_prompt.Text = "Connecting MySQL Database ...";
                conn.Open();
                string sql = "SELECT user_id,user_pass FROM user WHERE user_id=@username AND user_pass=@password";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", textBox_username.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@password", textBox_password.Text.ToString().Trim());
                cmd.Prepare();
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read() == true)  //  有数据
                {
                    label_prompt.Text = "欢迎登录！";
                } else
                {
                    label_prompt.Text = "密码错误,请核对用户名和密码！";
                }
                rdr.Close();
            } catch (MySqlException e)
            {
                label_prompt.Text = "Error " + e.Number + " has occurred: " + e.Message;
            }
            conn.Close();
        }
    }
}
