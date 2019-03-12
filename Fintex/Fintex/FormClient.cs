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
    public partial class FormClient : Form
    {
      
        private SqlConnection sqlConnection = null;

        public FormClient()
        {
            InitializeComponent();
        }
                  
        private async Task LoadClientsAsync()
        {
            SqlDataReader sqlDataReader = null;

            SqlCommand getClientsCommand = new SqlCommand("SELECT * FROM [Client]", sqlConnection);
            try
            {
                sqlDataReader = await getClientsCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlDataReader["Id"]),
                        Convert.ToString(sqlDataReader["FIO"]),
                        Convert.ToString(sqlDataReader["Email"]),
                        Convert.ToString(sqlDataReader["BirthDay"]),
                        Convert.ToString(sqlDataReader["Phone"]),
                        Convert.ToString(sqlDataReader["Passport"]),
                        Convert.ToString(sqlDataReader["Account_number"]),
                        Convert.ToString(sqlDataReader["Is_Blocked"])
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

       
        private async void FormClient_Load_1(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FintexCS"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.View = View.Details;

            listView.Columns.Add("Id");
            listView.Columns.Add("FIO");
            listView.Columns.Add("Email");
            listView.Columns.Add("BirthDay");
            listView.Columns.Add("Phone");
            listView.Columns.Add("Passport");
            listView.Columns.Add("Account_number");
            listView.Columns.Add("Is_Blocked");

            await LoadClientsAsync();
        }

        private void FormClient_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }
    }
}
