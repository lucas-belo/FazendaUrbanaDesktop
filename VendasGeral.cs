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
    public partial class VendasGeral : Form
    {
        public VendasGeral()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            vendasListBox.Items.Add($"Vendedor: {vendedorBox.Text} | Produto: {produtoBox.Text} | Valor: R${valorVendaBox.Text} | Quantidade: {quantidadeBox.Text}");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();

            this.Hide();
        }
    }
}
