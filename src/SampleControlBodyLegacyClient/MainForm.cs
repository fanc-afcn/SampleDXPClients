using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FANC.DXP.API.Client.Legacy.ControlBody;
using FANC.DXP.API.Client.Legacy.ReferenceData;
using FANC.DXP.API.Models;

namespace SampleControlBodyLegacyClient
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
            ReferenceDataServiceClient rds = new ReferenceDataServiceClient();
            var result = rds.GetCodeLists(this.txtAppCode.Text);

            if (result != null)
            {
                if (result.CodeLists != null)
                {
                    this.dgResults.DataSource = result.CodeLists.ToList();
                    this.dgResults.Refresh();
                }
                else
                {
                    MessageBox.Show("CodeLists null. ServiceOperationResult: " + result.ServiceOperationResult?.ToString());
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
                ReferenceDataServiceClient rds = new ReferenceDataServiceClient();
                var result = rds.GetCodeListValues(new GetCodeListValuesRequest()
                {
                    ApplicationCode = this.txtAppCode.Text,
                    CodeListType = this.txtCodeList.Text
                });

                if (result != null)
                {
                    if (result.CodeListValues != null)
                    {
                        this.dgResults.DataSource = result.CodeListValues.ToList();
                        this.dgResults.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("CodeListValues null. ServiceOperationResult: " + result.ServiceOperationResult?.ToString());
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
            ControlBodyServiceClient cbsc = new ControlBodyServiceClient();

            var request = new GetLicensesRequest();
            request.SinceDate = this.dtpSinceDate.Checked ? dtpSinceDate.Value : new DateTime?();

            var result = cbsc.GetLicenses(request);

            this.dgResults.DataSource = result.Licenses.ToList();
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
                ControlBodyServiceClient cbsc = new ControlBodyServiceClient();
                var result = cbsc.GetLicenseDetails(new GetLicenseDetailsRequest()
                {
                    LicenseNumber = this.txtLicenseNumber.Text
                });

                this.dgResults.DataSource = new List<FANC.DXP.API.Models.License>() {result.License};
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
                ControlBodyServiceClient cbsc = new ControlBodyServiceClient();
                var result = cbsc.GetLicenseDocuments(new GetLicenseDocumentsRequest()
                {
                    LicenseNumber = this.txtLicenseNumber.Text
                });

                if (result.Stream != null)
                {
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Downloads\", result.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await result.Stream.CopyToAsync(fs);
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

            //var inspectionData = new InspectionData();
            //for (int i = 1; i <= 5000; i++)
            //{
            //    inspectionData.PhysicalItems.Add(new PhysicalItem()
            //    {
            //        UniqueIdNumber = "UID" + i.ToString(),
            //        DataProviderReference = "DPR" + i.ToString(),
            //        OperationalEntityNumber = "OE" + i.ToString(),
            //        PhysicalItemCategoryCode = "EQUIP",
            //        LicenseItemTypeCode = "47",
            //        ManufacturerCode = "162",
            //        BrandTypeCustomValue = "BrandType",
            //        BrandCustomValue = "Brand",
            //        NumberOfDetectors = 1,
            //        NumberOfTubes = 1
            //    });
            //}


            //ControlBodyServiceClient cbsc = new ControlBodyServiceClient();
            //var request = new SendInspectionDataRequest();
            //request.InspectionData = inspectionData;
            //var result = cbsc.SendInspectionData(request);
        }

        
    }
}
