namespace ProjectFitnessClub
{
    partial class FormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonStatisticsCouches = new System.Windows.Forms.Button();
            this.buttonStatisticsClients = new System.Windows.Forms.Button();
            this.lineBottom = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dateTimePicker);
            this.panel1.Controls.Add(this.lineBottom);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.buttonStatisticsCouches);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Controls.Add(this.buttonStatisticsClients);
            this.panel1.Location = new System.Drawing.Point(188, 131);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 719);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(273, 121);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(472, 54);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Администрирование";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(246, 551);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(526, 94);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Выйти";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonStatisticsCouches
            // 
            this.buttonStatisticsCouches.Location = new System.Drawing.Point(246, 368);
            this.buttonStatisticsCouches.Name = "buttonStatisticsCouches";
            this.buttonStatisticsCouches.Size = new System.Drawing.Size(526, 103);
            this.buttonStatisticsCouches.TabIndex = 1;
            this.buttonStatisticsCouches.Text = "Статистика по тренерам";
            this.buttonStatisticsCouches.UseVisualStyleBackColor = true;
            this.buttonStatisticsCouches.Click += new System.EventHandler(this.buttonStatisticsCouches_Click);
            // 
            // buttonStatisticsClients
            // 
            this.buttonStatisticsClients.Location = new System.Drawing.Point(246, 231);
            this.buttonStatisticsClients.Name = "buttonStatisticsClients";
            this.buttonStatisticsClients.Size = new System.Drawing.Size(526, 103);
            this.buttonStatisticsClients.TabIndex = 0;
            this.buttonStatisticsClients.Text = "Статистика по клиентам";
            this.buttonStatisticsClients.UseVisualStyleBackColor = true;
            this.buttonStatisticsClients.Click += new System.EventHandler(this.buttonStatisticsClients_Click);
            // 
            // lineBottom
            // 
            this.lineBottom.Enabled = false;
            this.lineBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lineBottom.Location = new System.Drawing.Point(92, 513);
            this.lineBottom.Name = "lineBottom";
            this.lineBottom.Size = new System.Drawing.Size(848, 5);
            this.lineBottom.TabIndex = 12;
            this.lineBottom.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(790, 231);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(150, 22);
            this.dateTimePicker.TabIndex = 13;
            // 
            // FormAdmin
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 951);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonStatisticsCouches;
        private System.Windows.Forms.Button buttonStatisticsClients;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button lineBottom;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}