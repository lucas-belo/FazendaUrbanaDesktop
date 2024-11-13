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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VendasGeral vendasGeral = new VendasGeral();
            vendasGeral.Show();

            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProducaoGeral producaoGeral = new ProducaoGeral();
            producaoGeral.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FornecedoresGeral fornecedoresGeral = new FornecedoresGeral();
            fornecedoresGeral.Show();

            this.Hide();
        }
    }
    }

