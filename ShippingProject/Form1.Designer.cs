namespace ShippingProject
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
            this.fillInvBtn = new System.Windows.Forms.Button();
            this.procTransBtn = new System.Windows.Forms.Button();
            this.outputBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fillInvBtn
            // 
            this.fillInvBtn.Location = new System.Drawing.Point(73, 46);
            this.fillInvBtn.Name = "fillInvBtn";
            this.fillInvBtn.Size = new System.Drawing.Size(185, 33);
            this.fillInvBtn.TabIndex = 1;
            this.fillInvBtn.Text = "Fill Inventory List";
            this.fillInvBtn.UseVisualStyleBackColor = true;
            this.fillInvBtn.Click += new System.EventHandler(this.FillInvBtn_Click);
            // 
            // procTransBtn
            // 
            this.procTransBtn.Enabled = false;
            this.procTransBtn.Location = new System.Drawing.Point(73, 94);
            this.procTransBtn.Name = "procTransBtn";
            this.procTransBtn.Size = new System.Drawing.Size(184, 32);
            this.procTransBtn.TabIndex = 2;
            this.procTransBtn.Text = "Process Transactions";
            this.procTransBtn.UseVisualStyleBackColor = true;
            this.procTransBtn.Click += new System.EventHandler(this.ProcTransBtn_Click);
            // 
            // outputBtn
            // 
            this.outputBtn.Enabled = false;
            this.outputBtn.Location = new System.Drawing.Point(72, 142);
            this.outputBtn.Name = "outputBtn";
            this.outputBtn.Size = new System.Drawing.Size(185, 32);
            this.outputBtn.TabIndex = 3;
            this.outputBtn.Text = "Create Output Files";
            this.outputBtn.UseVisualStyleBackColor = true;
            this.outputBtn.Click += new System.EventHandler(this.OutputBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(73, 222);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(183, 32);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 280);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.outputBtn);
            this.Controls.Add(this.procTransBtn);
            this.Controls.Add(this.fillInvBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button fillInvBtn;
        private System.Windows.Forms.Button procTransBtn;
        private System.Windows.Forms.Button outputBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}

