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
    public partial class FormClientsCredit : Form
    {
        private SqlConnection sqlConnection = null;

        public FormClientsCredit()
        {
            InitializeComponent();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FintexCS"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.View = View.Details;

            listView.Columns.Add("Id");
            listView.Columns.Add("Title");
            listView.Columns.Add("Duration");
            listView.Columns.Add("Sum");
            listView.Columns.Add("Percent_Value");
            listView.Columns.Add("Period_of_Payment");
            listView.Columns.Add("State");
            listView.Columns.Add("ClientId");

            await LoadCreditAsync();

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async Task LoadCreditAsync()
        {
            SqlDataReader sqlDataReader = null;

            SqlCommand getClientsCommand = new SqlCommand("SELECT * FROM [Credit]", sqlConnection);
            try
            {
                sqlDataReader = await getClientsCommand.ExecuteReaderAsync();

                while(await sqlDataReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlDataReader["Id"]),
                        Convert.ToString(sqlDataReader["Title"]),
                        Convert.ToString(sqlDataReader["Duration"]),
                        Convert.ToString(sqlDataReader["Sum"]),
                        Convert.ToString(sqlDataReader["Percent_Value"]),
                        Convert.ToString(sqlDataReader["Period_of_Payment"]),
                        Convert.ToString(sqlDataReader["State"]),
                        Convert.ToString(sqlDataReader["ClientId"])
                    });

                    listView.Items.Add(item);

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }
        }

        private async void toolStripButtonОбнов_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();

            await LoadCreditAsync();
        }

        private void списокКлиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClient formClient = new FormClient();
            formClient.Show();
        }

        private async void toolStripButtonDel_Click(object sender, EventArgs e)
        {

            if (listView.SelectedItems.Count > 0)
            {

                DialogResult res = MessageBox.Show("Вы действительно хотите удалить эту строку", "Удаление строки", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                switch (res)
                {
                    case DialogResult.OK:
                        SqlCommand delSql = new SqlCommand("DELETE FROM [Credit] WHERE [Id] = @Id", sqlConnection);

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

                        await LoadCreditAsync();

                        break;
                }

            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {

                FormUpdateClientsCredits form = new FormUpdateClientsCredits(sqlConnection, Convert.ToInt32(listView.SelectedItems[0].SubItems[0].Text));
                form.Show();
            }
            else
            {
                MessageBox.Show("Ни одна строка не была выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void предложенияКредитовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreditsOffer formCredits = new FormCreditsOffer();
            formCredits.Show();
        }

        private void toolStripButtonInsert_Click(object sender, EventArgs e)
        {
            FormInsertClientsCredit form = new FormInsertClientsCredit(sqlConnection);
            form.Show();
        }
    }
} 
    