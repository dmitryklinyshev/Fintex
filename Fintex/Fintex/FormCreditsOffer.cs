using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fintex
{
    public partial class FormCreditsOffer : Form
    {

        private SqlConnection sqlConnection = null;



        public FormCreditsOffer()
        {
            InitializeComponent();


        }

        private async void FormCreditsOffer_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FintexCS"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.View = View.Details;

            listView.Columns.Add("Id");
            listView.Columns.Add("Title");
            listView.Columns.Add("Term");
            listView.Columns.Add("MaxSum");
            listView.Columns.Add("Procent");
            listView.Columns.Add("Conditions");

            await LoadCreditsOfferAsync();

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async Task LoadCreditsOfferAsync()
        {
            SqlDataReader sqlDataReader = null;

            SqlCommand getClientsCommand = new SqlCommand("SELECT * FROM [CreditOffer]", sqlConnection);
            try
            {
                sqlDataReader = await getClientsCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlDataReader["Id"]),
                        Convert.ToString(sqlDataReader["Title"]),
                        Convert.ToString(sqlDataReader["Term"]),
                        Convert.ToString(sqlDataReader["MaxSum"]),
                        Convert.ToString(sqlDataReader["Procent"]),
                        Convert.ToString(sqlDataReader["Conditions"]),

                    });

                    listView.Items.Add(item);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
        }

        private void toolStripButtonIns_Click(object sender, EventArgs e)
        {
            FormInsertCreditsOffer formInsertCredits = new FormInsertCreditsOffer(sqlConnection);
            formInsertCredits.Show();
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count > 0)
            {

                FormUpdateCreditOffer creditsOffer = new FormUpdateCreditOffer(sqlConnection, Convert.ToInt32(listView.SelectedItems[0].SubItems[0].Text));
                creditsOffer.Show();
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void toolStripButtonDel_Click(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count > 0)
            {

                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand delSql = new SqlCommand("DELETE FROM [CreditOffer] WHERE [Id] = @Id", sqlConnection);

                        delSql.Parameters.AddWithValue("Id", Convert.ToInt32(listView.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await delSql.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        listView.Items.Clear();

                        await LoadCreditsOfferAsync();

                        break;
                }

            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void toolStripButtonRef_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();

            await LoadCreditsOfferAsync();
        }
    }
}

