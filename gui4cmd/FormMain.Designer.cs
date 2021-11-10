
namespace Ambiesoft.gui4cmd
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabRoot = new System.Windows.Forms.TabControl();
            this.tbOutput = new System.Windows.Forms.TabPage();
            this.tbError = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.timerToControl = new System.Windows.Forms.Timer(this.components);
            this.tabRoot.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(631, 335);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // tabRoot
            // 
            this.tabRoot.Controls.Add(this.tbOutput);
            this.tabRoot.Controls.Add(this.tbError);
            this.tabRoot.Location = new System.Drawing.Point(85, 15);
            this.tabRoot.Name = "tabRoot";
            this.tabRoot.SelectedIndex = 0;
            this.tabRoot.Size = new System.Drawing.Size(198, 76);
            this.tabRoot.TabIndex = 1;
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(4, 22);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tbOutput.Size = new System.Drawing.Size(190, 50);
            this.tbOutput.TabIndex = 0;
            this.tbOutput.Text = "Output";
            this.tbOutput.UseVisualStyleBackColor = true;
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(4, 22);
            this.tbError.Name = "tbError";
            this.tbError.Padding = new System.Windows.Forms.Padding(3);
            this.tbError.Size = new System.Drawing.Size(190, 50);
            this.tbError.TabIndex = 1;
            this.tbError.Text = "Error";
            this.tbError.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(200, 108);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(108, 47);
            this.txtOutput.TabIndex = 0;
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(23, 96);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtError.Size = new System.Drawing.Size(129, 69);
            this.txtError.TabIndex = 2;
            // 
            // panelRoot
            // 
            this.panelRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRoot.Controls.Add(this.tabRoot);
            this.panelRoot.Controls.Add(this.txtOutput);
            this.panelRoot.Controls.Add(this.txtError);
            this.panelRoot.Location = new System.Drawing.Point(12, 11);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(694, 318);
            this.panelRoot.TabIndex = 0;
            // 
            // timerToControl
            // 
            this.timerToControl.Enabled = true;
            this.timerToControl.Tick += new System.EventHandler(this.timerToControl_Tick);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 367);
            this.Controls.Add(this.panelRoot);
            this.Controls.Add(this.btnOK);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabRoot.ResumeLayout(false);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabRoot;
        private System.Windows.Forms.TabPage tbOutput;
        private System.Windows.Forms.TabPage tbError;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Timer timerToControl;
    }
}

