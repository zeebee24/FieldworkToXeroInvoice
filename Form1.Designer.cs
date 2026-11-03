


namespace InvoiceFieldworkToXero
{
    partial class InvoiceXero
    {

        private DateTimePicker dtpInvoiceDate;
        private Label label1;
        private Button btnSubmit;
        private ProgressBar progressBarLoading;
        private Label lblStatus;
        private ListBox lstSuccessfulInvoices;
        private Label lblSuccessHeading;
        private Label label2;
        private ListBox lstFailedInvoices;
        private Button btnGetInvoices;
        private DataGridView dgvInvoices;
        private Label lblRetrivedInv;
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtpInvoiceDate = new DateTimePicker();
            label1 = new Label();
            btnSubmit = new Button();
            progressBarLoading = new ProgressBar();
            lblStatus = new Label();
            lstSuccessfulInvoices = new ListBox();
            lblSuccessHeading = new Label();
            label2 = new Label();
            lstFailedInvoices = new ListBox();
            btnGetInvoices = new Button();
            dgvInvoices = new DataGridView();
            lblRetrivedInv = new Label();
            chkSelectAll = new CheckBox();
            colSelect = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvInvoices).BeginInit();
            SuspendLayout();
            // 
            // dtpInvoiceDate
            // 
            dtpInvoiceDate.Location = new Point(246, 44);
            dtpInvoiceDate.Margin = new Padding(3, 4, 3, 4);
            dtpInvoiceDate.Name = "dtpInvoiceDate";
            dtpInvoiceDate.Size = new Size(258, 27);
            dtpInvoiceDate.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 49);
            label1.Name = "label1";
            label1.Size = new Size(199, 20);
            label1.TabIndex = 1;
            label1.Text = "Select invoice modified date";
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(292, 127);
            btnSubmit.Margin = new Padding(3, 4, 3, 4);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(182, 44);
            btnSubmit.TabIndex = 2;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            // 
            // progressBarLoading
            // 
            progressBarLoading.Location = new Point(56, 191);
            progressBarLoading.Margin = new Padding(3, 4, 3, 4);
            progressBarLoading.Name = "progressBarLoading";
            progressBarLoading.Size = new Size(394, 52);
            progressBarLoading.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(213, 207);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status";
            // 
            // lstSuccessfulInvoices
            // 
            lstSuccessfulInvoices.FormattingEnabled = true;
            lstSuccessfulInvoices.Location = new Point(52, 775);
            lstSuccessfulInvoices.Margin = new Padding(3, 4, 3, 4);
            lstSuccessfulInvoices.Name = "lstSuccessfulInvoices";
            lstSuccessfulInvoices.Size = new Size(185, 384);
            lstSuccessfulInvoices.TabIndex = 5;
            // 
            // lblSuccessHeading
            // 
            lblSuccessHeading.AutoSize = true;
            lblSuccessHeading.Location = new Point(52, 742);
            lblSuccessHeading.Name = "lblSuccessHeading";
            lblSuccessHeading.Size = new Size(81, 20);
            lblSuccessHeading.TabIndex = 6;
            lblSuccessHeading.Text = "Succeeded";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(303, 742);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 7;
            label2.Text = "Failed";
            // 
            // lstFailedInvoices
            // 
            lstFailedInvoices.FormattingEnabled = true;
            lstFailedInvoices.Location = new Point(303, 775);
            lstFailedInvoices.Margin = new Padding(3, 4, 3, 4);
            lstFailedInvoices.Name = "lstFailedInvoices";
            lstFailedInvoices.Size = new Size(183, 384);
            lstFailedInvoices.TabIndex = 8;
            // 
            // btnGetInvoices
            // 
            btnGetInvoices.Location = new Point(30, 127);
            btnGetInvoices.Name = "btnGetInvoices";
            btnGetInvoices.Size = new Size(151, 44);
            btnGetInvoices.TabIndex = 9;
            btnGetInvoices.Text = "Get Invoices";
            btnGetInvoices.UseVisualStyleBackColor = true;
            // 
            // dgvInvoices
            // 
            dgvInvoices.AllowUserToAddRows = false;
            dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInvoices.Columns.AddRange(new DataGridViewColumn[] { colSelect });
            dgvInvoices.Location = new Point(56, 325);
            dgvInvoices.Name = "dgvInvoices";
            dgvInvoices.RowHeadersWidth = 51;
            dgvInvoices.Size = new Size(1037, 379);
            dgvInvoices.TabIndex = 10;
            // 
            // lblRetrivedInv
            // 
            lblRetrivedInv.AutoSize = true;
            lblRetrivedInv.Location = new Point(52, 262);
            lblRetrivedInv.Name = "lblRetrivedInv";
            lblRetrivedInv.Size = new Size(129, 20);
            lblRetrivedInv.TabIndex = 11;
            lblRetrivedInv.Text = "Retrieved Invoices";
            // 
            // chkSelectAll
            // 
            chkSelectAll.AutoSize = true;
            chkSelectAll.Location = new Point(56, 285);
            chkSelectAll.Name = "chkSelectAll";
            chkSelectAll.Size = new Size(93, 24);
            chkSelectAll.TabIndex = 12;
            chkSelectAll.Text = "Select &All";
            chkSelectAll.UseVisualStyleBackColor = true;
            // 
            // colSelect
            // 
            colSelect.HeaderText = "colSelect";
            colSelect.MinimumWidth = 6;
            colSelect.Name = "colSelect";
            colSelect.Visible = false;
            colSelect.Width = 125;
            // 
            // InvoiceXero
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 1182);
            Controls.Add(chkSelectAll);
            Controls.Add(lblRetrivedInv);
            Controls.Add(dgvInvoices);
            Controls.Add(btnGetInvoices);
            Controls.Add(lstFailedInvoices);
            Controls.Add(label2);
            Controls.Add(lblSuccessHeading);
            Controls.Add(lstSuccessfulInvoices);
            Controls.Add(lblStatus);
            Controls.Add(progressBarLoading);
            Controls.Add(btnSubmit);
            Controls.Add(label1);
            Controls.Add(dtpInvoiceDate);
            Margin = new Padding(3, 4, 3, 4);
            Name = "InvoiceXero";
            Text = "InvoiceXero";
            Load += InvoiceXero_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInvoices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkSelectAll;
        private DataGridViewCheckBoxColumn colSelect;
    }
}
