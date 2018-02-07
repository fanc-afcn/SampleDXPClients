using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FANC.DXP.API.Client;
using FANC.DXP.DTO;
using FANC.DXP.DTO.PHI;

namespace SampleControlBodyClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get Codelists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGetCodeLists_Click(object sender, EventArgs e)
        {
            var rds = new ReferenceDataClient();
            var result = rds.GetCodeLists(this.txtAppCode.Text);

            if (result != null)
            {
                if (result.ReturnObject != null)
                {
                    this.dgResults.DataSource = result.ReturnObject.ToList();
                    this.dgResults.Refresh();
                }
                else
                {
                    MessageBox.Show(result.ToString());
                }
            }

        }

        /// <summary>
        /// Get Codelist values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGetCodeListValues_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtCodeList.Text))
            {
                var rdClient = new ReferenceDataClient();
                var result = rdClient.GetCodeListValues(this.txtCodeList.Text, this.txtAppCode.Text);

                if (result != null)
                {
                    if (result.ReturnObject != null)
                    {
                        this.dgResults.DataSource = result.ReturnObject.ToList();
                        this.dgResults.Refresh();
                    }
                    else
                    {
                        MessageBox.Show(result.ToString());
                    }
                }

            }
            else
                MessageBox.Show("Enter CodeList !");

        }

        /// <summary>
        /// Get all basic license information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGetLicenses_Click(object sender, EventArgs e)
        {
            var phiClient = new PHIClient();

            var sinceDate = this.dtpSinceDate.Checked ? dtpSinceDate.Value : new DateTime?();

            var result = phiClient.GetLicenses(sinceDate);

            this.dgResults.DataSource = result.ReturnObject.ToList();
            this.dgResults.Refresh();
        }

        /// <summary>
        /// Retrieves details of a specified license
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGetLicenseDetails_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtLicenseNumber.Text))
            {
                var phiClient = new PHIClient();
                var result = phiClient.GetLicenseDetails(this.txtLicenseNumber.Text);

                this.dgResults.DataSource = new List<License>() {result.ReturnObject};
                this.dgResults.Refresh();
            }
            else
                MessageBox.Show("Enter LicenseNumber !");
            
        }

        /// <summary>
        /// Get license documents and saves it to a "\Downloads" folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butGetLicenseDocuments_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtLicenseNumber.Text))
            {
                var phiClient = new PHIClient();
                var result = phiClient.GetLicenseDocuments(this.txtLicenseNumber.Text);

                if (result.ReturnObject?.Stream != null)
                {
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Downloads\", result.ReturnObject.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await result.ReturnObject.Stream.CopyToAsync(fs);
                        MessageBox.Show(@"Document downloaded to \Downloads folder inside application folder");
                    }
                }
                else
                {
                    MessageBox.Show("Documents not available for " + this.txtLicenseNumber.Text);
                }
                
            }
            else
                MessageBox.Show("Enter LicenseNumber !");
        }

        private void butSendPhysicalInventory_Click(object sender, EventArgs e)
        {
            var inventory = new PhysicalInventory();
            var item1 = new PhysicalItem()
            {
                UniqueIdNumber = "test123",
                OperationalEntityNumber = "OE-123456"
                //complete other fields as required
            };
            inventory.PhysicalItems.Add(item1);

            var phiClient = new PHIClient();
            phiClient.PostPhysicalInventory(inventory);
        }
    }
}
