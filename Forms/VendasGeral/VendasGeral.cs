using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using FazendaUrbanaDesktop.Database;

namespace FazendaUrbanaDesktop
{
    public partial class VendasGeral : Form
    {
        public VendasGeral()
        {
            InitializeComponent();
        }

        private void VendasGeral_Load(object sender, EventArgs e)
        {
            listViewVendas.Columns.Add("Vendedor", 100);
            listViewVendas.Columns.Add("Produto", 100);
            listViewVendas.Columns.Add("Valor", 100);
            listViewVendas.Columns.Add("Quantidade", 100);

            LoadVendasFromDatabase();
        }

        private void LoadVendasFromDatabase()
        {
            string query = "SELECT Vendedor, Produto, ValorVenda, Quantidade FROM Vendas";
            using (var reader = DatabaseConnection.ExecuteReader(query))
            {
                listViewVendas.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Vendedor"].ToString());

                    item.SubItems.Add(reader["Produto"].ToString());
                    item.SubItems.Add(reader["ValorVenda"].ToString());
                    item.SubItems.Add(reader["Quantidade"].ToString());

                    listViewVendas.Items.Add(item);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string vendedor = vendedorBox.Text;
            string produto = produtoBox.Text;
            string valor = valorVendaBox.Text;
            string quantidade = quantidadeBox.Text;

            if (string.IsNullOrEmpty(vendedor) || string.IsNullOrEmpty(produto) || string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(quantidade))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            string insertQuery = "INSERT INTO Vendas (Vendedor, Produto, ValorVenda, Quantidade) " +
                                 "VALUES (@vendedor, @produto, @valor, @quantidade)";

            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@vendedor", vendedor);
                    command.Parameters.AddWithValue("@produto", produto);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@quantidade", quantidade);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao adicionar a venda: {ex.Message}");
                    }
                }
            }

            vendedorBox.Clear();
            produtoBox.Clear();
            valorVendaBox.Clear();
            quantidadeBox.Clear();

            LoadVendasFromDatabase();
        }

        private void remover_Click(object sender, EventArgs e)
        {
            if (listViewVendas.SelectedItems.Count > 0)
            {
                var selectedItem = listViewVendas.SelectedItems[0];
                string vendedor = selectedItem.SubItems[0].Text;
                string produto = selectedItem.SubItems[1].Text;

                string deleteQuery = "DELETE FROM Vendas WHERE Vendedor = @vendedor AND Produto = @produto";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@vendedor", vendedor);
                        command.Parameters.AddWithValue("@produto", produto);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao remover a venda: {ex.Message}");
                        }
                    }
                }

                listViewVendas.Items.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para remover.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();

            this.Hide();
        }
    }
}
