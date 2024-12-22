using FitnessClub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFitnessClub
{
    public partial class FormProfile : Form
    {
        ConnectionClass cs = new ConnectionClass();
        Form1 f1 = new Form1();
        Style style = new Style();
        PrivateFontCollection pfc = new PrivateFontCollection();
        String login = null;
        String role = null;
        public FormProfile(string login, string role)
        {
            InitializeComponent();
            this.login = login;
            this.role = role;
            //стилизация
            pfc = style.getPFC();
            lblTitle.Font = new Font(pfc.Families[0], 36, FontStyle.Bold);
            lblDescription.Font = new Font(pfc.Families[0], 18);
            dataGridView.Font = new Font(pfc.Families[0], 21);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.AppWorkspace;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.BackColor = SystemColors.AppWorkspace;
            dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            SetRoundedShape(panel, 90);
            SetRoundedShape(buttonSubmit, 69);
            buttonSubmit.Font = new Font(pfc.Families[0], 21);
            buttonSubmit.BackColor = ColorTranslator.FromHtml("#E124E4");
            buttonSubmit.FlatAppearance.BorderSize = 0;
            buttonSubmit.FlatStyle = FlatStyle.Flat;
        }
        private void FormProfile_Load(object sender, EventArgs e)
        {
            LoadUserServices(login);
        }
        //метод получения информации после выполнения процедур в БД
        public void LoadUserServices(string login)
        {
            if (login != null)
            {
                using (cs.getConnection())
                {
                    try
                    {
                        cs.getConnection().Open();
                        using (SqlCommand command = new SqlCommand("GetUserServiceDetails", cs.getConnection()))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Добавляем параметр логина к команде
                            command.Parameters.AddWithValue("@Login", login);
                            // Создаем адаптер для заполнения DataTable
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            // Привязываем полученные данные к DataGridView
                            dataGridView.DataSource = dataTable;
                        }
                        using (SqlCommand comm = new SqlCommand("GetFIOByLogin", cs.getConnection()))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@Login", login);
                            SqlDataAdapter da = new SqlDataAdapter(comm);
                            DataTable dt = new DataTable();
                            var s = comm.ExecuteScalar();
                            lblTitle.Text = (string)s;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                        dataGridView.DataSource = null;
                    }
                    cs.getConnection().Close();
                }
            }
        }
        //кнопка возврата
        private void buttonOK_Click(object sender, EventArgs e)
        {
            f1.Show();
            this.Close();
        }
        //проверка заполнена ли таблица с услугами (тест)
        public bool IsDataTableOfDGVNull()
        {
            if(dataGridView.Rows.Count != 0)
                return false;
            else 
                return true;
        }
        //метод заливки фона градиентом
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Создание градиента из заданных цветов
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                   ColorTranslator.FromHtml("#E124E4"),
                   ColorTranslator.FromHtml("#21A7C5"),
                   LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        //метод закругления углов объекта формы
        static void SetRoundedShape(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }

    }
}
