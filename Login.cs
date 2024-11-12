using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazendaUrbanaDesktop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usuarioTextBox.Text == "admin" && senhaTextBox.Text == "admin123")
            {
                Menu menu = new Menu();
                menu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario ou senha invalidos");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
