namespace SequenceIntToBigint
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDbConn = new System.Windows.Forms.TextBox();
            this.btnConnDatabase = new System.Windows.Forms.Button();
            this.lblDbConn = new System.Windows.Forms.Label();
            this.gvSequence = new System.Windows.Forms.DataGridView();
            this.lblSeq = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.Convert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvSequence)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDbConn
            // 
            this.txtDbConn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDbConn.Font = new System.Drawing.Font("細明體", 9F);
            this.txtDbConn.Location = new System.Drawing.Point(12, 24);
            this.txtDbConn.Name = "txtDbConn";
            this.txtDbConn.Size = new System.Drawing.Size(766, 22);
            this.txtDbConn.TabIndex = 11;
            // 
            // btnConnDatabase
            // 
            this.btnConnDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnDatabase.Font = new System.Drawing.Font("細明體", 9F);
            this.btnConnDatabase.Location = new System.Drawing.Point(784, 24);
            this.btnConnDatabase.Name = "btnConnDatabase";
            this.btnConnDatabase.Size = new System.Drawing.Size(83, 23);
            this.btnConnDatabase.TabIndex = 10;
            this.btnConnDatabase.Text = "Connect";
            this.btnConnDatabase.UseVisualStyleBackColor = true;
            this.btnConnDatabase.Click += new System.EventHandler(this.btnConnDatabase_Click);
            // 
            // lblDbConn
            // 
            this.lblDbConn.AutoSize = true;
            this.lblDbConn.Font = new System.Drawing.Font("細明體", 9F);
            this.lblDbConn.Location = new System.Drawing.Point(12, 9);
            this.lblDbConn.Name = "lblDbConn";
            this.lblDbConn.Size = new System.Drawing.Size(53, 12);
            this.lblDbConn.TabIndex = 9;
            this.lblDbConn.Text = "Database";
            // 
            // gvSequence
            // 
            this.gvSequence.AllowUserToAddRows = false;
            this.gvSequence.AllowUserToDeleteRows = false;
            this.gvSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSequence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSequence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Convert,
            this.Sequence,
            this.TableName,
            this.ColumnName});
            this.gvSequence.Location = new System.Drawing.Point(12, 64);
            this.gvSequence.Name = "gvSequence";
            this.gvSequence.RowHeadersVisible = false;
            this.gvSequence.RowTemplate.Height = 24;
            this.gvSequence.Size = new System.Drawing.Size(855, 396);
            this.gvSequence.TabIndex = 12;
            this.gvSequence.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSequence_CellValueChanged);
            // 
            // lblSeq
            // 
            this.lblSeq.AutoSize = true;
            this.lblSeq.Font = new System.Drawing.Font("細明體", 9F);
            this.lblSeq.Location = new System.Drawing.Point(12, 49);
            this.lblSeq.Name = "lblSeq";
            this.lblSeq.Size = new System.Drawing.Size(53, 12);
            this.lblSeq.TabIndex = 13;
            this.lblSeq.Text = "Sequence";
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Location = new System.Drawing.Point(792, 466);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 14;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // Convert
            // 
            this.Convert.HeaderText = "Convert";
            this.Convert.Name = "Convert";
            this.Convert.Width = 50;
            // 
            // Sequence
            // 
            this.Sequence.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sequence.DataPropertyName = "Sequence";
            this.Sequence.HeaderText = "Sequence";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "TableName";
            this.TableName.HeaderText = "TableName";
            this.TableName.Name = "TableName";
            this.TableName.Width = 200;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "ColumnName";
            this.ColumnName.HeaderText = "ColumnName";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Width = 200;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 501);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblSeq);
            this.Controls.Add(this.gvSequence);
            this.Controls.Add(this.txtDbConn);
            this.Controls.Add(this.btnConnDatabase);
            this.Controls.Add(this.lblDbConn);
            this.Name = "frmMain";
            this.Text = "SequenceIntToBigint";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvSequence)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDbConn;
        private System.Windows.Forms.Button btnConnDatabase;
        private System.Windows.Forms.Label lblDbConn;
        private System.Windows.Forms.DataGridView gvSequence;
        private System.Windows.Forms.Label lblSeq;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Convert;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewComboBoxColumn TableName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnName;
    }
}

