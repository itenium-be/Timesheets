namespace Itenium.Timesheet.WinForms
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CustomerReference = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Customer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.IsFreelancer = new System.Windows.Forms.CheckBox();
            this.ConsultantName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(405, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Timesheet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProjectName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CustomerReference);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Customer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.IsFreelancer);
            this.groupBox1.Controls.Add(this.ConsultantName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 140);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // ProjectName
            // 
            this.ProjectName.Location = new System.Drawing.Point(155, 106);
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.Size = new System.Drawing.Size(296, 22);
            this.ProjectName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Project name";
            // 
            // CustomerReference
            // 
            this.CustomerReference.Location = new System.Drawing.Point(155, 78);
            this.CustomerReference.Name = "CustomerReference";
            this.CustomerReference.Size = new System.Drawing.Size(296, 22);
            this.CustomerReference.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Customer reference";
            // 
            // Customer
            // 
            this.Customer.Location = new System.Drawing.Point(155, 50);
            this.Customer.Name = "Customer";
            this.Customer.Size = new System.Drawing.Size(296, 22);
            this.Customer.TabIndex = 4;
            this.Customer.Text = "Solvay";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer";
            // 
            // IsFreelancer
            // 
            this.IsFreelancer.AutoSize = true;
            this.IsFreelancer.Location = new System.Drawing.Point(465, 17);
            this.IsFreelancer.Name = "IsFreelancer";
            this.IsFreelancer.Size = new System.Drawing.Size(98, 21);
            this.IsFreelancer.TabIndex = 2;
            this.IsFreelancer.Text = "Freelancer";
            this.IsFreelancer.UseVisualStyleBackColor = true;
            // 
            // ConsultantName
            // 
            this.ConsultantName.Location = new System.Drawing.Point(155, 22);
            this.ConsultantName.Name = "ConsultantName";
            this.ConsultantName.Size = new System.Drawing.Size(296, 22);
            this.ConsultantName.TabIndex = 1;
            this.ConsultantName.Text = "Jonas Vandermosten";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Consultant";
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 213);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Itenium Timesheet Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox IsFreelancer;
        private System.Windows.Forms.TextBox ConsultantName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CustomerReference;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Customer;
        private System.Windows.Forms.Label label2;
    }
}

