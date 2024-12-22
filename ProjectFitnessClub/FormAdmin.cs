using FitnessClub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFitnessClub
{
    public partial class FormAdmin : Form
    {
        Form1 f1 = new Form1();
        string login = null;
        Style style = new Style();
        PrivateFontCollection pfc = new PrivateFontCollection();
        DateTime date = new DateTime();


        public FormAdmin(string login, Form1 f1)
        {
            InitializeComponent();
            //шрифты
            pfc = style.getPFC();
            lblTitle.Font = new Font(pfc.Families[0], 28, FontStyle.Bold);
            buttonStatisticsClients.Font = new Font(pfc.Families[0], 21);
            buttonStatisticsCouches.Font = new Font(pfc.Families[0], 21);
            buttonOK.Font = new Font(pfc.Families[0], 21);
            //стили изменение формы элементов
            SetRoundedShape(panel1, 90);
            SetRoundedShape(buttonStatisticsClients, 90);
            SetRoundedShape(buttonStatisticsCouches, 90);
            SetRoundedShape(buttonOK, 90);
            SetRoundedShape(lineBottom, 5);
            //стили прочее
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            buttonStatisticsClients.BackColor = ColorTranslator.FromHtml("#E124E4");
            buttonStatisticsCouches.BackColor = ColorTranslator.FromHtml("#E124E4");
            buttonOK.BackColor = ColorTranslator.FromHtml("#E124E4");
            buttonStatisticsClients.FlatAppearance.BorderSize = 0;
            buttonStatisticsClients.FlatStyle = FlatStyle.Flat;
            buttonStatisticsCouches.FlatAppearance.BorderSize = 0;
            buttonStatisticsCouches.FlatStyle = FlatStyle.Flat;
            buttonOK.FlatAppearance.BorderSize = 0;
            buttonOK.FlatStyle = FlatStyle.Flat;
            lineBottom.BackColor = ColorTranslator.FromHtml("#E124E4");
            lineBottom.FlatAppearance.BorderSize = 0;
            lineBottom.FlatStyle = FlatStyle.Flat;
            this.login = login;
            //формат выбора даты
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "MM/yyyy";
        }
        private void buttonStatisticsClients_Click(object sender, EventArgs e)
        {
            date = dateTimePicker.Value;
            TableForm tableForm = new TableForm(this, true, date);
            tableForm.ShowDialog();
        }

        private void buttonStatisticsCouches_Click(object sender, EventArgs e)
        {
            date = dateTimePicker.Value;
            TableForm tableForm = new TableForm(this, false, date);
            tableForm.ShowDialog();
        }
        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            f1.Show();
            this.Close();
        }
        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }
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
