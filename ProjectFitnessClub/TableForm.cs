using FitnessClub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFitnessClub
{
    public partial class TableForm : Form
    {
        FormAdmin fA;
        ConnectionClass cs = new ConnectionClass();
        PrivateFontCollection pfc = new PrivateFontCollection();
        Style style = new Style();
        bool isClients;
        DateTime date;
        public TableForm(FormAdmin fA, bool isClients, DateTime date)
        {
            InitializeComponent();
            this.fA = fA;
            this.isClients = isClients;
            this.date = date;
            //шрифты
            pfc = style.getPFC();
            lblSum.Font = new Font(pfc.Families[0], 24);
            dataGridView.Font = new Font(pfc.Families[0], 24);
            //стиль
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.AppWorkspace;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.BackColor = SystemColors.AppWorkspace;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            
        }
        private void TableForm_Load(object sender, EventArgs e)
        {
            if (isClients)
            {
                this.Text = "Статистика по полученными клиентами услугам";
                GetClintsData();
            }
            else
            {
                this.Text = "Статистика по оказанным тренерами услугам";
                GetCouchesData();
            }
        }

        public void GetClintsData()
        {
            using (cs.getConnection())
            {
                using (SqlCommand command = new SqlCommand("GetUserServiceStatistics", cs.getConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Month", date.Month);
                    command.Parameters.AddWithValue("@Year", date.Year);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        cs.getConnection().Open();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable; // Обновите ваш DataGridView с данными
                        int sum = CounterServices();
                        lblSum.Text = "Всего услуг: " + sum;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
            dataGridView.Rows[0].Selected = false;
        }
        public int CounterServices()
        {
            int sum = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView.Rows[i].Cells[1].Value.ToString());
            }
            return sum;
        }
        public void GetCouchesData()
        {
            using (cs.getConnection())
            {
                using (SqlCommand command = new SqlCommand("GetCouchesServiceStatistics", cs.getConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Month", date.Month);
                    command.Parameters.AddWithValue("@Year", date.Year);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        cs.getConnection().Open();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable; // Обновите ваш DataGridView с данными
                        int sum = CounterServices();
                        lblSum.Text = "Всего услуг: " + sum;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
            dataGridView.Rows[0].Selected = false;
        }
        private void TableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fA.Show();
        }
    }
}
