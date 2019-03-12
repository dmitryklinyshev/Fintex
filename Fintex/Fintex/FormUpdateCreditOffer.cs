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
    public partial class FormUpdateCreditOffer : Form
    {

        private SqlConnection sqlConnection = null;
        private int id;


        public FormUpdateCreditOffer(SqlConnection connection, int id)
        {
            InitializeComponent();

            sqlConnection = connection;
            this.id = id;
        }

      
        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("UPDATE [CreditOffer] SET [Title]=@Title, [Term]=@Term, [MaxSum]=@MaxSum, [Procent]=@Procent, [Conditions]=@Conditions", sqlConnection);

            update.Parameters.AddWithValue("Id", id);
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

        private async void FormUpdateCreditOffer_Load_1(object sender, EventArgs e)
        {
            SqlCommand getCreditsInfoCommand = new SqlCommand("SELECT [Title], [Term], [MaxSum], [Procent], [Conditions] FROM [CreditOffer] WHERE [Id]=@Id", sqlConnection);
            getCreditsInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlDataReader = null;

            try
            {
                sqlDataReader = await getCreditsInfoCommand.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlDataReader["Title"]);
                    textBox2.Text = Convert.ToString(sqlDataReader["Term"]);
                    textBox3.Text = Convert.ToString(sqlDataReader["MaxSum"]);
                    textBox4.Text = Convert.ToString(sqlDataReader["Procent"]);
                    textBox5.Text = Convert.ToString(sqlDataReader["Conditions"]);

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

        private void buttonCansel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
