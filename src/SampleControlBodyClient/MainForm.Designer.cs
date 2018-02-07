namespace SampleControlBodyClient
{
    partial class MainForm
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
            this.butGetCodeLists = new System.Windows.Forms.Button();
            this.butGetCodeListValues = new System.Windows.Forms.Button();
            this.butGetLicenses = new System.Windows.Forms.Button();
            this.butGetLicenseDetails = new System.Windows.Forms.Button();
            this.butSendPhysicalInventory = new System.Windows.Forms.Button();
            this.dgResults = new System.Windows.Forms.DataGridView();
            this.txtCodeList = new System.Windows.Forms.TextBox();
            this.txtLicenseNumber = new System.Windows.Forms.TextBox();
            this.lblCodelist = new System.Windows.Forms.Label();
            this.lblLicenseNumber = new System.Windows.Forms.Label();
            this.lblSinceDate = new System.Windows.Forms.Label();
            this.dtpSinceDate = new System.Windows.Forms.DateTimePicker();
            this.lblApplication = new System.Windows.Forms.Label();
            this.txtAppCode = new System.Windows.Forms.TextBox();
            this.butGetLicenseDocuments = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
            this.SuspendLayout();
            // 
            // butGetCodeLists
            // 
            this.butGetCodeLists.Location = new System.Drawing.Point(209, 9);
            this.butGetCodeLists.Name = "butGetCodeLists";
            this.butGetCodeLists.Size = new System.Drawing.Size(118, 23);
            this.butGetCodeLists.TabIndex = 0;
            this.butGetCodeLists.Text = "GetCodeLists";
            this.butGetCodeLists.UseVisualStyleBackColor = true;
            this.butGetCodeLists.Click += new System.EventHandler(this.butGetCodeLists_Click);
            // 
            // butGetCodeListValues
            // 
            this.butGetCodeListValues.Location = new System.Drawing.Point(209, 38);
            this.butGetCodeListValues.Name = "butGetCodeListValues";
            this.butGetCodeListValues.Size = new System.Drawing.Size(118, 23);
            this.butGetCodeListValues.TabIndex = 1;
            this.butGetCodeListValues.Text = "GetCodeListValues";
            this.butGetCodeListValues.UseVisualStyleBackColor = true;
            this.butGetCodeListValues.Click += new System.EventHandler(this.butGetCodeListValues_Click);
            // 
            // butGetLicenses
            // 
            this.butGetLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butGetLicenses.Location = new System.Drawing.Point(692, 12);
            this.butGetLicenses.Name = "butGetLicenses";
            this.butGetLicenses.Size = new System.Drawing.Size(118, 23);
            this.butGetLicenses.TabIndex = 2;
            this.butGetLicenses.Text = "GetLicenses";
            this.butGetLicenses.UseVisualStyleBackColor = true;
            this.butGetLicenses.Click += new System.EventHandler(this.butGetLicenses_Click);
            // 
            // butGetLicenseDetails
            // 
            this.butGetLicenseDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butGetLicenseDetails.Location = new System.Drawing.Point(692, 41);
            this.butGetLicenseDetails.Name = "butGetLicenseDetails";
            this.butGetLicenseDetails.Size = new System.Drawing.Size(118, 23);
            this.butGetLicenseDetails.TabIndex = 3;
            this.butGetLicenseDetails.Text = "GetLicenseDetails";
            this.butGetLicenseDetails.UseVisualStyleBackColor = true;
            this.butGetLicenseDetails.Click += new System.EventHandler(this.butGetLicenseDetails_Click);
            // 
            // butSendPhysicalInventory
            // 
            this.butSendPhysicalInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butSendPhysicalInventory.Location = new System.Drawing.Point(649, 514);
            this.butSendPhysicalInventory.Name = "butSendPhysicalInventory";
            this.butSendPhysicalInventory.Size = new System.Drawing.Size(161, 23);
            this.butSendPhysicalInventory.TabIndex = 4;
            this.butSendPhysicalInventory.Text = "Send Physical Inventory";
            this.butSendPhysicalInventory.UseVisualStyleBackColor = true;
            this.butSendPhysicalInventory.Click += new System.EventHandler(this.butSendPhysicalInventory_Click);
            // 
            // dgResults
            // 
            this.dgResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResults.Location = new System.Drawing.Point(12, 99);
            this.dgResults.Name = "dgResults";
            this.dgResults.Size = new System.Drawing.Size(798, 409);
            this.dgResults.TabIndex = 5;
            // 
            // txtCodeList
            // 
            this.txtCodeList.Location = new System.Drawing.Point(72, 41);
            this.txtCodeList.Name = "txtCodeList";
            this.txtCodeList.Size = new System.Drawing.Size(131, 20);
            this.txtCodeList.TabIndex = 6;
            // 
            // txtLicenseNumber
            // 
            this.txtLicenseNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenseNumber.Location = new System.Drawing.Point(495, 41);
            this.txtLicenseNumber.Name = "txtLicenseNumber";
            this.txtLicenseNumber.Size = new System.Drawing.Size(191, 20);
            this.txtLicenseNumber.TabIndex = 7;
            // 
            // lblCodelist
            // 
            this.lblCodelist.AutoSize = true;
            this.lblCodelist.Location = new System.Drawing.Point(12, 44);
            this.lblCodelist.Name = "lblCodelist";
            this.lblCodelist.Size = new System.Drawing.Size(54, 13);
            this.lblCodelist.TabIndex = 8;
            this.lblCodelist.Text = "Code List:";
            // 
            // lblLicenseNumber
            // 
            this.lblLicenseNumber.AutoSize = true;
            this.lblLicenseNumber.Location = new System.Drawing.Point(402, 44);
            this.lblLicenseNumber.Name = "lblLicenseNumber";
            this.lblLicenseNumber.Size = new System.Drawing.Size(87, 13);
            this.lblLicenseNumber.TabIndex = 9;
            this.lblLicenseNumber.Text = "License Number:";
            // 
            // lblSinceDate
            // 
            this.lblSinceDate.AutoSize = true;
            this.lblSinceDate.Location = new System.Drawing.Point(402, 21);
            this.lblSinceDate.Name = "lblSinceDate";
            this.lblSinceDate.Size = new System.Drawing.Size(63, 13);
            this.lblSinceDate.TabIndex = 11;
            this.lblSinceDate.Text = "Since Date:";
            // 
            // dtpSinceDate
            // 
            this.dtpSinceDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpSinceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSinceDate.Location = new System.Drawing.Point(495, 15);
            this.dtpSinceDate.Name = "dtpSinceDate";
            this.dtpSinceDate.ShowCheckBox = true;
            this.dtpSinceDate.Size = new System.Drawing.Size(191, 20);
            this.dtpSinceDate.TabIndex = 12;
            this.dtpSinceDate.Value = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            // 
            // lblApplication
            // 
            this.lblApplication.AutoSize = true;
            this.lblApplication.Location = new System.Drawing.Point(12, 14);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(60, 13);
            this.lblApplication.TabIndex = 14;
            this.lblApplication.Text = "App. Code:";
            // 
            // txtAppCode
            // 
            this.txtAppCode.Location = new System.Drawing.Point(72, 11);
            this.txtAppCode.Name = "txtAppCode";
            this.txtAppCode.Size = new System.Drawing.Size(131, 20);
            this.txtAppCode.TabIndex = 13;
            this.txtAppCode.Text = "PHI";
            // 
            // butGetLicenseDocuments
            // 
            this.butGetLicenseDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butGetLicenseDocuments.Location = new System.Drawing.Point(692, 70);
            this.butGetLicenseDocuments.Name = "butGetLicenseDocuments";
            this.butGetLicenseDocuments.Size = new System.Drawing.Size(118, 23);
            this.butGetLicenseDocuments.TabIndex = 15;
            this.butGetLicenseDocuments.Text = "GetLicenseDocs";
            this.butGetLicenseDocuments.UseVisualStyleBackColor = true;
            this.butGetLicenseDocuments.Click += new System.EventHandler(this.butGetLicenseDocuments_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 549);
            this.Controls.Add(this.butGetLicenseDocuments);
            this.Controls.Add(this.lblApplication);
            this.Controls.Add(this.txtAppCode);
            this.Controls.Add(this.dtpSinceDate);
            this.Controls.Add(this.lblSinceDate);
            this.Controls.Add(this.lblLicenseNumber);
            this.Controls.Add(this.lblCodelist);
            this.Controls.Add(this.txtLicenseNumber);
            this.Controls.Add(this.txtCodeList);
            this.Controls.Add(this.dgResults);
            this.Controls.Add(this.butSendPhysicalInventory);
            this.Controls.Add(this.butGetLicenseDetails);
            this.Controls.Add(this.butGetLicenses);
            this.Controls.Add(this.butGetCodeListValues);
            this.Controls.Add(this.butGetCodeLists);
            this.Name = "MainForm";
            this.Text = "FANC ControlBody Test Client";
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butGetCodeLists;
        private System.Windows.Forms.Button butGetCodeListValues;
        private System.Windows.Forms.Button butGetLicenses;
        private System.Windows.Forms.Button butGetLicenseDetails;
        private System.Windows.Forms.Button butSendPhysicalInventory;
        private System.Windows.Forms.DataGridView dgResults;
        private System.Windows.Forms.TextBox txtCodeList;
        private System.Windows.Forms.TextBox txtLicenseNumber;
        private System.Windows.Forms.Label lblCodelist;
        private System.Windows.Forms.Label lblLicenseNumber;
        private System.Windows.Forms.Label lblSinceDate;
        private System.Windows.Forms.DateTimePicker dtpSinceDate;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.TextBox txtAppCode;
        private System.Windows.Forms.Button butGetLicenseDocuments;
    }
}

