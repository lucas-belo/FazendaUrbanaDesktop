using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using FazendaUrbanaDesktop.Database;

namespace FazendaUrbanaDesktop
{
    public partial class ProducaoGeral : Form
    {
        public ProducaoGeral()
        {
            InitializeComponent();
        }

        private void ProducaoGeral_Load(object sender, EventArgs e)
        {
            producaoListView.Columns.Add("Produto", 100);
            producaoListView.Columns.Add("Origem", 100);
            producaoListView.Columns.Add("Tipo", 100);
            producaoListView.Columns.Add("Quantidade", 100);

            LoadProducaoFromDatabase();
        }

        private void LoadProducaoFromDatabase()
        {
            string query = "SELECT Produto, Origem, Tipo, Quantidade FROM Producao";
            using (var reader = DatabaseConnection.ExecuteReader(query))
            {
                producaoListView.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Produto"].ToString());

                    item.SubItems.Add(reader["Origem"].ToString());
                    item.SubItems.Add(reader["Tipo"].ToString());
                    item.SubItems.Add(reader["Quantidade"].ToString());

                    producaoListView.Items.Add(item);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string produto = produtoBox.Text;
            string origem = origemBox.Text;
            string tipo = tipoBox.Text;
            string quantidade = quantidadeBox.Text;

            if (string.IsNullOrEmpty(produto) || string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(quantidade))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            string insertQuery = "INSERT INTO Producao (Produto, Origem, Tipo, Quantidade) " +
                                 "VALUES (@produto, @origem, @tipo, @quantidade)";

            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@produto", produto);
                    command.Parameters.AddWithValue("@origem", origem);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@quantidade", quantidade);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao adicionar a produção: {ex.Message}");
                    }
                }
            }

            produtoBox.Clear();
            origemBox.Clear();
            tipoBox.Clear();
            quantidadeBox.Clear();

            LoadProducaoFromDatabase();
        }

        private void remover_Click(object sender, EventArgs e)
        {
            if (producaoListView.SelectedItems.Count > 0)
            {
                var selectedItem = producaoListView.SelectedItems[0];
                string produto = selectedItem.SubItems[0].Text;
                string origem = selectedItem.SubItems[1].Text;

                string deleteQuery = "DELETE FROM Producao WHERE Produto = @produto AND Origem = @origem";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@produto", produto);
                        command.Parameters.AddWithValue("@origem", origem);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao remover a produção: {ex.Message}");
                        }
                    }
                }

                producaoListView.Items.Remove(selectedItem);
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
