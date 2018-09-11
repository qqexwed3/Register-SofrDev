using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace RegistOX
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Black;
            textBox2.Text = "";
            textBox2.PasswordChar = '*';
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.ForeColor = Color.Black;
            textBox3.Text = "";
        }
        class User
        {
            public ObjectId _id { get; set; }

            public string Username
            {
                get; set;
            }
            public string Password
            {
                get; set;
            }
            public string Name
            {
                get; set;
            }
            public string Avatar
            {
                get; set;
            }
            public int Win
            {
                get; set;
            }
            public int Draw
            {
                get; set;
            }
            public int Lose
            {
                get; set;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OK_Click_1(object sender, EventArgs e)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://admin:a123456@ds141902.mlab.com:41902/ox");
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("ox");
                MongoCollection mongoCollection = database.GetCollection<User>("User");
                User user = new User();
                BindingList<User> doclist = new BindingList<User>();
                var getCollection = database.GetCollection<User>("User");
                var insertUser = getCollection.AsQueryable().Where(pd => pd.Username == textBox1.Text);

                foreach (var p in insertUser)
                {
                    doclist.Add(p);
                    Application.DoEvents();
                }
                dataGridView1.DataSource = doclist;
                if (dataGridView1.Rows.Count == 0)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !textBox1.Text.Equals("Username"))
                    {
                        user.Username = textBox1.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอกUsername");
                    }
                    if (!string.IsNullOrEmpty(textBox2.Text.Trim()) && !textBox2.Text.Equals("Password"))
                    {
                        user.Password = textBox2.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอกPassword");
                    }
                    if (!string.IsNullOrEmpty(textBox3.Text.Trim()) && !textBox3.Text.Equals("Name"))
                    {
                        user.Name = textBox3.Text;
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอกName");
                    }
                    user.Avatar = null;
                    user.Win = 0;
                    user.Draw = 0;
                    user.Lose = 0;

                    if (!string.IsNullOrEmpty(textBox1.Text.Trim()) &&
                        !string.IsNullOrEmpty(textBox2.Text.Trim()) &&
                        !string.IsNullOrEmpty(textBox3.Text.Trim()) &&
                        (!textBox1.Text.Equals("Username") &&
                         !textBox2.Text.Equals("Password") &&
                         !textBox3.Text.Equals("Name")))
                    {
                        mongoCollection.Insert(user);
                        MessageBox.Show("สมัครเสร็จสิ้น");
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
        }
    }
}
