using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FANC.DXP.API.Client;
using FANC.DXP.API.Models;

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
        private async void butGetCodeLists_Click(object sender, EventArgs e)
        {
            var rds = new ReferenceDataClient();
            var result = await rds.GetCodeListsAsync(this.txtAppCode.Text);

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
        private async void butGetCodeListValues_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtCodeList.Text))
            {
                var rds = new ReferenceDataClient();
                var result = await rds.GetCodeListValuesAsync(this.txtCodeList.Text, this.txtAppCode.Text);

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
        private async void butGetLicenses_Click(object sender, EventArgs e)
        {
            var cbsc = new ControlBodyClient();

            var sinceDate = this.dtpSinceDate.Checked ? dtpSinceDate.Value : new DateTime?();

            var result = await cbsc.GetLicensesAsync(sinceDate);

            this.dgResults.DataSource = result.ReturnObject.ToList();
            this.dgResults.Refresh();
        }

        /// <summary>
        /// Retrieves details of a specified license
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butGetLicenseDetails_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtLicenseNumber.Text))
            {
                var cbsc = new FANC.DXP.API.Client.ControlBodyClient();
                var result = await cbsc.GetLicenseDetailsAsync(this.txtLicenseNumber.Text);

                this.dgResults.DataSource = new List<FANC.DXP.API.Models.License>() {result.ReturnObject};
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
                var cbsc = new ControlBodyClient();
                var result = await cbsc.GetLicenseDocumentsAsync(this.txtLicenseNumber.Text);

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
                
            }
            else
                MessageBox.Show("Enter LicenseNumber !");
        }

        /// <summary>
        /// Creates inspection data and sends it to the API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butSendInspectionData_Click(object sender, EventArgs e)
        {

            MessageBox.Show(@"Configure butSendInspectionData_Click method !");

            var inspectionData = new InspectionData();
            for (int i = 1; i <= 100; i++)
            {
                inspectionData.PhysicalItems.Add(new PhysicalItem()
                {
                    UniqueIdNumber = "UID" + i.ToString(),
                    DataProviderReference = "DPR" + i.ToString(),
                    OperationalEntityNumber = "OE" + i.ToString(),
                    PhysicalItemCategoryCode = "EQUIP",
                    LicenseItemTypeCode = "47",
                    ManufacturerCode = "162",
                    BrandTypeCustomValue = "BrandType",
                    BrandCustomValue = "Brand",
                    NumberOfDetectors = 1,
                    NumberOfTubes = 1
                });
            }


            var cbsc = new ControlBodyClient();
            var result = await cbsc.PostInspectionDataAsync(inspectionData);
        }

        
    }
}
