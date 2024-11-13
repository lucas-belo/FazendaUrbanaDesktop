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
    public partial class ProducaoGeral : Form
    {
        public ProducaoGeral()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            producaoListBox.Items.Add($"Produto: {produtoBox.Text} | Origem: R${origemBox.Text} | Tipo: {tipoBox.Text} | Quantidade: {quantidadeBox.Text}");

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();

            this.Hide();
        }
    }
}

