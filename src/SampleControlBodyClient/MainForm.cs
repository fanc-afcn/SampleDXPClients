using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FANC.DXP.API.Client;
using FANC.DXP.DTO;
using FANC.DXP.DTO.PHI;
using Newtonsoft.Json;

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
            this.LogApiCallResult(result, false);

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
                var rdClient = new ReferenceDataClient();
                var result = await rdClient.GetCodeListValuesAsync(this.txtCodeList.Text, this.txtAppCode.Text);
                this.LogApiCallResult(result, false);

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
            var phiClient = new PHIClient();

            var sinceDate = this.dtpSinceDate.Checked ? dtpSinceDate.Value : new DateTime?();

            var result = await phiClient.GetLicensesAsync(sinceDate);
            this.LogApiCallResult(result, false);

            this.dgResults.DataSource = result.ReturnObject?.ToList();
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
                var phiClient = new PHIClient();
                var result = await phiClient.GetLicenseDetailsAsync(this.txtLicenseNumber.Text);
                this.LogApiCallResult(result, false);

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
                var result = await phiClient.GetLicenseDocumentsAsync(this.txtLicenseNumber.Text);
                this.LogApiCallResult(result, false);

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

        /// <summary>
        /// Creates a random number of physical items and posts them to the API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butSendPhysicalInventory_Click(object sender, EventArgs e)
        {
            this.txtDataFileNumber.Text = "";
            this.rtxtLog.Text = "";

            var inventory = new PhysicalInventory();

            var rnd = new Random();
            var amount = rnd.Next(1, 10);

            for (int i = 1; i <= amount; i++)
            {
                var item = new PhysicalItem()
                {
                    UniqueIdNumber = new Guid().ToString("N"),
                    OperationalEntityNumber = "OE-123456"
                    //complete other fields as required
                };
                inventory.PhysicalItems.Add(item);
            }

            var phiClient = new PHIClient();
            var result = await phiClient.PostPhysicalInventoryAsync(inventory);
            this.LogApiCallResult(result);

            if (result.Success)
                this.txtDataFileNumber.Text = result.ReturnObject;
        }

        /// <summary>
        /// Retrieves the status of a data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butGetDataFileStatus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtDataFileNumber.Text))
            {
                this.rtxtLog.Text = "";

                var phiClient = new PHIClient();
                var result = await phiClient.GetDataFileStatusAsync(this.txtDataFileNumber.Text);
                this.LogApiCallResult(result);
            }
            else
            {
                MessageBox.Show("Enter DataFileNumber !");
            }
        }

        /// <summary>
        /// Downloads the processing result of a datafile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butDownloadProcessingResult_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtDataFileNumber.Text))
            {
                var phiClient = new PHIClient();
                var result = await phiClient.GetDataFileProcessingResultAsync(this.txtDataFileNumber.Text);
                this.LogApiCallResult(result, false);

                if (result.ReturnObject?.Stream != null)
                {
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Downloads\", result.ReturnObject.FileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        await result.ReturnObject.Stream.CopyToAsync(fs);
                        MessageBox.Show(@"Processing result downloaded to \Downloads folder inside application folder");
                    }
                }
                else
                {
                    MessageBox.Show("Processing result not available for " + this.txtDataFileNumber.Text);
                }
            }
            else
            {
                MessageBox.Show("Enter DataFileNumber !");
            }
        }

        private void LogApiCallResult(ApiCallResult apiCallResult)
        {
            this.rtxtLog.Text = "";

            var sb = new StringBuilder();

            sb.AppendLine("Call Result Success: " + apiCallResult.Success.ToString());
            sb.AppendLine("HTTP Status Code: " + apiCallResult.HttpStatusCode.ToString());

            if (apiCallResult.BusinessErrors.Any())
                sb.AppendLine("Business Errors: " + apiCallResult.BusinessErrors.Aggregate((i, j) => i + "; " + j));

            if(sb.Length > 0)
                this.rtxtLog.Text += sb.ToString();
        }


        private void LogApiCallResult<T>(ApiCallResult<T> apiCallResult, bool showReturnObject = true)
        {
            var sb = new StringBuilder();

            this.LogApiCallResult((ApiCallResult)apiCallResult);

            if (showReturnObject && apiCallResult.ReturnObject != null)
            {
                sb.AppendLine("Return Object:");
                sb.AppendLine(JsonConvert.SerializeObject(apiCallResult.ReturnObject, Formatting.Indented));
            }

            if(sb.Length > 0)
                this.rtxtLog.Text += sb.ToString();
        }

        
    }
}
