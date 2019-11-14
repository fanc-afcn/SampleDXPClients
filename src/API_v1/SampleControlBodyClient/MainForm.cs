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
using SampleControlBodyClient.Properties;

namespace SampleControlBodyClient
{
    public partial class MainForm : Form
    {
        private string apiUri;
        private string userName;
        private string password;
        private bool useCompression;
        private bool setExplicitTls;

        public MainForm()
        {
            InitializeComponent();

            this.apiUri = Settings.Default.ApiUri;
            this.userName = Settings.Default.UserName;
            this.password = Settings.Default.Password;
            this.useCompression = Settings.Default.UseCompression;
            this.setExplicitTls = Settings.Default.SetExplicitTls;
        }

        /// <summary>
        /// Get Codelists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void butGetCodeLists_Click(object sender, EventArgs e)
        {
            var rdClient = new ReferenceDataClient(apiUri, userName, password, useCompression, setExplicitTls);
            var result = await rdClient.GetCodeListsAsync(this.txtAppCode.Text);
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
                var rdClient = new ReferenceDataClient(apiUri, userName, password, useCompression, setExplicitTls);
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
            var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);

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
                var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);
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
                var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);
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
            var amount = rnd.Next(500, 2500); //change here to generate a random number of items for testing purposes. Please do not use more than 10000 if submitting to the FANC servers.

            for (int i = 1; i <= amount; i++)
            {
                //generating a sample item here to submit (will most likely NOT pass validation)
                var item = new PhysicalItem()
                {
                    UniqueIdNumber = Guid.NewGuid().ToString("N"), //using a unique guid as example here. In reality, this should be the physical unique identifier as labelled on the item (e.g. unique serial number)
                    OperationalEntityNumber = "OE-123456",
                    LicenseItemTypeCode = "ionsimplanter",
                    UseCode = "1",
                    DistributerCode = "250",
                    ManufacturerCode = "250",
                    ModelCode = "720",
                    ConstructionYear = 1999,
                    PhysicalItemStatusCode = "INUSE"
                    //complete other fields as required
                };
                inventory.PhysicalItems.Add(item);
            }

            var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);
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
                var dataFileNumber = this.txtDataFileNumber.Text;

                this.rtxtLog.Text = "";

                var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);
                var result = await phiClient.GetDataFileStatusAsync(dataFileNumber);
                this.LogApiCallResult(result);
            }
            else
            {
                MessageBox.Show("Enter DataFileNumber !");
            }
        }

        private async void butGetDataFileProcessingResult_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtDataFileNumber.Text))
            {
                var dataFileNumber = this.txtDataFileNumber.Text;

                var phiClient = new PHIClient(apiUri, userName, password, useCompression, setExplicitTls);
                var result = await phiClient.GetDataFileProcessingResultAsync(dataFileNumber);
                this.LogApiCallResult(result, false);

                //as example here, serialize results to a json file. In reality, the validation results should probably be integrated back into your application in some kind of "FANC submission report"
                if (result.ReturnObject != null)
                {
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Downloads\", dataFileNumber + "_ProcessingResult.json");
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            using (var writer = new JsonTextWriter(sw))
                            {
                                var serializer = new JsonSerializer()
                                {
                                    Formatting = Formatting.Indented
                                };

                                serializer.Serialize(writer, result.ReturnObject);
                                MessageBox.Show(@"Processing result downloaded to \Downloads folder inside application folder");
                            }
                        }
                            
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
