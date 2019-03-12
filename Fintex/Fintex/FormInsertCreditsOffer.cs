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
    public partial class FormInsertCreditsOffer : Form
    {
        private SqlConnection sqlConnection;
       


        public FormInsertCreditsOffer(SqlConnection connection)
        {
            InitializeComponent();

            sqlConnection = connection;
        }

     
        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("INSERT INTO [CreditOffer] (Title, Term, MaxSum, Procent, Conditions) VALUES(@Title, @Term, @MaxSum, @Procent, @Conditions)", sqlConnection);


            update.Parameters.AddWithValue("Title", textBox1.Text);
            update.Parameters.AddWithValue("Term", textBox2.Text);
            update.Parameters.AddWithValue("MaxSum", textBox3.Text);
            update.Parameters.AddWithValue("Procent", textBox4.Text);
            update.Parameters.AddWithValue("Conditions", textBox5.Text);

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