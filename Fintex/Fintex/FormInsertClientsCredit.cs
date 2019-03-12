using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fintex
{
    public partial class FormInsertClientsCredit : Form
    {

       private SqlConnection sqlConnection;

        public FormInsertClientsCredit(SqlConnection connection)
        {
            InitializeComponent();
            connection = sqlConnection;
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {

            SqlCommand update = new SqlCommand("INSERT INTO [Credit] (Title, Duration, Sum, Percent_Value, Period_of_Payment) VALUES(@Title, @Duration, @Sum, @Percent_Value, @Period_of_Payment)", sqlConnection);


            update.Parameters.AddWithValue("Title", textBox1.Text);
            update.Parameters.AddWithValue("Duration", textBox2.Text);
            update.Parameters.AddWithValue("Sum", textBox3.Text);
            update.Parameters.AddWithValue("Percent_Value", textBox4.Text);
            update.Parameters.AddWithValue("Period_of_Payment", textBox5.Text);
            //update.Parameters.AddWithValue("State", textBox6.Text);

            try
            {
                await update.ExecuteNonQueryAsync();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonCansel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
