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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace ProjectFitnessClub
{
    public partial class Form1 : Form
    {
        Style style = new Style();
        PrivateFontCollection pfc = new PrivateFontCollection();
        Panel roundedPanel = new Panel();
        string login = null;
        string password;

        ConnectionClass cs = new ConnectionClass();
        
        public Form1()
        {
            InitializeComponent();
            
            //шрифты
            pfc = style.getPFC();
            lblTitle.Font = new Font(pfc.Families[0], 54, FontStyle.Bold);
            lblDescription.Font = new Font(pfc.Families[0], 18);
            lblLogin.Font = new Font(pfc.Families[0], 21);
            lblPass.Font = new Font(pfc.Families[0], 21);
            txtLogin.Font = new Font(pfc.Families[0], 21);
            txtPass.Font = new Font(pfc.Families[0], 21);
            buttonSubmit.Font = new Font(pfc.Families[0], 21, FontStyle.Bold);
            //изменение формы элементов
            SetRoundedShape(panel, 90);
            SetRoundedShape(txtLogin, 45);
            SetRoundedShape(txtPass, 45);
            SetRoundedShape(buttonSubmit, 90);
            SetRoundedShape(lineBottom, 5);
            //прочее стиль элементов
            txtLogin.MaxLength = 20;
            txtPass.MaxLength = 20;
            txtLogin.BorderStyle = BorderStyle.None;
            txtPass.BorderStyle = BorderStyle.None;
            txtLogin.BackColor = SystemColors.AppWorkspace;
            txtPass.BackColor = SystemColors.AppWorkspace;
            txtLogin.SelectionAlignment = HorizontalAlignment.Center;
            txtPass.SelectionAlignment=HorizontalAlignment.Center;
            buttonSubmit.BackColor = ColorTranslator.FromHtml("#E124E4");
            buttonSubmit.FlatAppearance.BorderSize = 0;
            buttonSubmit.FlatStyle = FlatStyle.Flat;
            lineBottom.BackColor = ColorTranslator.FromHtml("#E124E4");
            lineBottom.FlatAppearance.BorderSize = 0;
            lineBottom.FlatStyle = FlatStyle.Flat;
            lblError.Visible = false;
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

        //метод получения роли пользователя по процедуре в БД
        public string GetRole(string login)
        {
            try
            {
                using (cs.getConnection())
                {
                    using (SqlCommand command = new SqlCommand("GetRoleNameByLogin", cs.getConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Добавляем входной параметр
                        command.Parameters.AddWithValue("@Login", login);

                        // Добавляем выходной параметр
                        SqlParameter outputRoleName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputRoleName);

                        cs.getConnection().Open();
                        command.ExecuteNonQuery();
                        login = outputRoleName.Value.ToString();
                        cs.getConnection().Close();
                        if (string.IsNullOrEmpty(login))
                        {
                            login = "юзер";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении роли: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return login;
        }

        public void buttonSubmit_Click(object sender, EventArgs e)
        {
            //проверка  на пустые поля
            if (txtLogin.TextLength != 0 && txtPass.TextLength != 0)
            {
                //поля с ошибками скрыть
                lblError.Visible = false;
                //обращение к методу по проверке логина и пароля
                bool result = UserLogin(txtLogin.Text, txtPass.Text);
                if (result)
                {
                    string role = GetRole(login);
                    if (role == "администратор")
                    {
                        FormAdmin fa = new FormAdmin(login, this);
                        fa.Show();
                        this.Hide();
                    }
                    else
                    {
                        FormProfile fpf = new FormProfile(login, role);
                        fpf.Show();
                        this.Hide();
                    }
                }
                else
                {
                    lblError.Text = "Ошибка логина или пароля";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Не все поля заполнены";
                lblError.Visible = true;
            }
        }
        public bool UserLogin(string login, string password)
        {
            bool result = false;
            this.login = login;
            //хэширование
            password = Hasher.HashPassword(password);
            //запрос в БД
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cs.getConnection().Open();
                using (SqlCommand comm = new SqlCommand("dbo.ValidateUserLogin", cs.getConnection()))
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@Login", login);
                    comm.Parameters.AddWithValue("@Password", password);
                    SqlParameter isValidParam = new SqlParameter("@IsValid", SqlDbType.Bit);
                    isValidParam.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(isValidParam);
                    comm.ExecuteNonQuery();
                    result = (bool)isValidParam.Value;
                }

            }
            catch (System.Data.SqlClient.SqlException) { lblError.Text = "Вход не удался"; }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            cs.getConnection().Close();
            return result;
        }
        //обработчик нажатия клавиш для txtbox
        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                string clipboardText = Clipboard.GetText();
                InsertCenteredText(clipboardText, (RichTextBox)sender);
            }
        }
        private void InsertCenteredText(string text, RichTextBox rtb)
        {
            rtb.Text = text; ;
            rtb.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
