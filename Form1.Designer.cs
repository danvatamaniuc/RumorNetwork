namespace RumorNetwork
{
    partial class MainWindow
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
            this.lblMembersNumber = new System.Windows.Forms.Label();
            this.txtMembersNumber = new System.Windows.Forms.TextBox();
            this.cmdGenerateMembers = new System.Windows.Forms.Button();
            this.rbnEA = new System.Windows.Forms.RadioButton();
            this.rbnPSO = new System.Windows.Forms.RadioButton();
            this.cmdSolve = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.dgvMembersRelation = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembersRelation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMembersNumber
            // 
            this.lblMembersNumber.AutoSize = true;
            this.lblMembersNumber.Location = new System.Drawing.Point(12, 9);
            this.lblMembersNumber.Name = "lblMembersNumber";
            this.lblMembersNumber.Size = new System.Drawing.Size(50, 13);
            this.lblMembersNumber.TabIndex = 0;
            this.lblMembersNumber.Text = "Members";
            // 
            // txtMembersNumber
            // 
            this.txtMembersNumber.Location = new System.Drawing.Point(68, 6);
            this.txtMembersNumber.Name = "txtMembersNumber";
            this.txtMembersNumber.Size = new System.Drawing.Size(27, 20);
            this.txtMembersNumber.TabIndex = 1;
            // 
            // cmdGenerateMembers
            // 
            this.cmdGenerateMembers.Location = new System.Drawing.Point(101, 4);
            this.cmdGenerateMembers.Name = "cmdGenerateMembers";
            this.cmdGenerateMembers.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerateMembers.TabIndex = 2;
            this.cmdGenerateMembers.Text = "Generate";
            this.cmdGenerateMembers.UseVisualStyleBackColor = true;
            this.cmdGenerateMembers.Click += new System.EventHandler(this.cmdGenerateMembers_Click);
            // 
            // rbnEA
            // 
            this.rbnEA.AutoSize = true;
            this.rbnEA.Location = new System.Drawing.Point(263, 7);
            this.rbnEA.Name = "rbnEA";
            this.rbnEA.Size = new System.Drawing.Size(39, 17);
            this.rbnEA.TabIndex = 8;
            this.rbnEA.TabStop = true;
            this.rbnEA.Text = "EA";
            this.rbnEA.UseVisualStyleBackColor = true;
            // 
            // rbnPSO
            // 
            this.rbnPSO.AutoSize = true;
            this.rbnPSO.Location = new System.Drawing.Point(308, 7);
            this.rbnPSO.Name = "rbnPSO";
            this.rbnPSO.Size = new System.Drawing.Size(47, 17);
            this.rbnPSO.TabIndex = 9;
            this.rbnPSO.TabStop = true;
            this.rbnPSO.Text = "PSO";
            this.rbnPSO.UseVisualStyleBackColor = true;
            // 
            // cmdSolve
            // 
            this.cmdSolve.Location = new System.Drawing.Point(361, 4);
            this.cmdSolve.Name = "cmdSolve";
            this.cmdSolve.Size = new System.Drawing.Size(75, 23);
            this.cmdSolve.TabIndex = 10;
            this.cmdSolve.Text = "Solve";
            this.cmdSolve.UseVisualStyleBackColor = true;
            this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(182, 4);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 11;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // dgvMembersRelation
            // 
            this.dgvMembersRelation.AllowUserToAddRows = false;
            this.dgvMembersRelation.AllowUserToDeleteRows = false;
            this.dgvMembersRelation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembersRelation.Location = new System.Drawing.Point(361, 33);
            this.dgvMembersRelation.Name = "dgvMembersRelation";
            this.dgvMembersRelation.Size = new System.Drawing.Size(375, 339);
            this.dgvMembersRelation.TabIndex = 12;
            this.dgvMembersRelation.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMembersRelation_CellValueChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 384);
            this.Controls.Add(this.dgvMembersRelation);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdSolve);
            this.Controls.Add(this.rbnPSO);
            this.Controls.Add(this.rbnEA);
            this.Controls.Add(this.cmdGenerateMembers);
            this.Controls.Add(this.txtMembersNumber);
            this.Controls.Add(this.lblMembersNumber);
            this.Name = "MainWindow";
            this.Text = "Rumor Network";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembersRelation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMembersNumber;
        private System.Windows.Forms.TextBox txtMembersNumber;
        private System.Windows.Forms.Button cmdGenerateMembers;
        private System.Windows.Forms.RadioButton rbnEA;
        private System.Windows.Forms.RadioButton rbnPSO;
        private System.Windows.Forms.Button cmdSolve;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.DataGridView dgvMembersRelation;
    }
}

