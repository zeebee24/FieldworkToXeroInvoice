
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvoiceFieldworkToXero
{
    public partial class InvoiceXero : Form
    {
        // API endpoints
        private const string GetInvoicesUrl =
            "http://localhost:8080/Fieldwork/webservice/get_invoice2";

        private const string SubmitInvoicesUrl =
            "http://localhost:8080/Fieldwork/webservice/post_selectedinvoice2";

        private const string DateHeaderName = "InvoiceDate";

        private static readonly HttpClient httpClient = new HttpClient();

        // Existing invoice objects
        private readonly BindingList<Invoice> invoices =
            new BindingList<Invoice>();

        private readonly BindingSource invoiceBindingSource =
            new BindingSource();

        // Store selected invoice IDs separately from the Invoice class
        private readonly HashSet<int> selectedInvoiceIds =
            new HashSet<int>();

        private bool updatingSelection;

        private static readonly JsonSerializerOptions JsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        public InvoiceXero()
        {
            InitializeComponent();

            ConfigureInvoiceGrid();
            ConfigureControls();
            RegisterEvents();
        }

        // =========================================================
        // FORM CONFIGURATION
        // =========================================================

        private void ConfigureControls()
        {
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            progressBarLoading.MarqueeAnimationSpeed = 30;
            progressBarLoading.Visible = false;

            lblStatus.Text = "Select a date and click Get Invoices.";

            btnSubmit.Visible = false;

            lstSuccessfulInvoices.Items.Clear();
            lstFailedInvoices.Items.Clear();
        }

        private void RegisterEvents()
        {
            btnGetInvoices.Click -= btnGetInvoices_Click;
            btnGetInvoices.Click += btnGetInvoices_Click;

            btnSubmit.Click -= btnSubmit_Click;
            btnSubmit.Click += btnSubmit_Click;

            chkSelectAll.CheckedChanged -= ChkSelectAll_CheckedChanged;
            chkSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;

            dgvInvoices.CurrentCellDirtyStateChanged -=
                DgvInvoices_CurrentCellDirtyStateChanged;

            dgvInvoices.CurrentCellDirtyStateChanged +=
                DgvInvoices_CurrentCellDirtyStateChanged;

            dgvInvoices.CellValueChanged -=
                DgvInvoices_CellValueChanged;

            dgvInvoices.CellValueChanged +=
                DgvInvoices_CellValueChanged;
        }

        // Keep this method if your Designer still references it.
        private void InvoiceXero_Load(object sender, EventArgs e)
        {
        }

        // =========================================================
        // DATAGRIDVIEW CONFIGURATION
        // =========================================================

        private void ConfigureInvoiceGrid()
        {
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.AllowUserToAddRows = false;
            dgvInvoices.AllowUserToDeleteRows = false;
            dgvInvoices.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvInvoices.MultiSelect = true;
            dgvInvoices.ReadOnly = false;

            dgvInvoices.Columns.Clear();

            // Selection checkbox - deliberately NOT bound to Invoice
            dgvInvoices.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colSelect",
                HeaderText = "Select",
                Width = 55,
                ReadOnly = false,
                ThreeState = false
            });

            // Invoice ID
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = nameof(Invoice.id),
                ReadOnly = true,
                Width = 90
            });

            // Invoice number
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colInvoiceNumber",
                HeaderText = "Invoice Number",
                DataPropertyName = nameof(Invoice.InvoiceNumber),
                ReadOnly = true,
                Width = 130
            });

            // Customer
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCustomer",
                HeaderText = "Customer",
                DataPropertyName = nameof(Invoice.Customer),
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Amount
            dgvInvoices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "Amount",
                DataPropertyName = nameof(Invoice.Amount),
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    Alignment =
                        DataGridViewContentAlignment.MiddleRight
                }
            });

            // Only the selection column can be edited
            foreach (DataGridViewColumn column in dgvInvoices.Columns)
            {
                column.ReadOnly = column.Name != "colSelect";
            }

            // Bind the grid once
            invoiceBindingSource.DataSource = invoices;
            dgvInvoices.DataSource = invoiceBindingSource;
        }

        // =========================================================
        // GET INVOICES
        // =========================================================

        private async void btnGetInvoices_Click(
            object sender,
            EventArgs e)
        {
            await GetInvoicesAsync();
        }

        private async Task GetInvoicesAsync()
        {
            SetLoading(true);

            try
            {
                string invoiceDate =
                    dtpInvoiceDate.Value.ToString("yyyy-MM-dd");

                using var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    GetInvoicesUrl);

                request.Headers.Add(
                    DateHeaderName,
                    invoiceDate);

                using HttpResponseMessage response =
                    await httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                string json =
                    await response.Content.ReadAsStringAsync();

                List<Invoice> retrievedInvoices =
                    JsonSerializer.Deserialize<List<Invoice>>(
                        json,
                        JsonOptions
                    ) ?? new List<Invoice>();

                // Reset selection separately from invoice data
                updatingSelection = true;

                try
                {
                    selectedInvoiceIds.Clear();

                    // Update the existing bound collection.
                    // Do not detach or reassign the DataSource.
                    invoices.Clear();

                    foreach (Invoice invoice in retrievedInvoices)
                    {
                        invoices.Add(invoice);
                    }

                    chkSelectAll.Checked = false;
                }
                finally
                {
                    updatingSelection = false;
                }

                btnSubmit.Visible = invoices.Count > 0;

                lblStatus.Text =
                    $"{invoices.Count} invoice(s) retrieved.";

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error retrieving invoices:\n\n{ex.Message}",
                    "Invoice Retrieval Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        // =========================================================
        // INDIVIDUAL CHECKBOX SELECTION
        // =========================================================

        private void DgvInvoices_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (dgvInvoices.CurrentCell == null)
                return;

            if (dgvInvoices.CurrentCell.OwningColumn.Name ==
                    "colSelect" &&
                dgvInvoices.IsCurrentCellDirty)
            {
                dgvInvoices.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvInvoices_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (updatingSelection)
                return;

            // Ignore header and invalid row/column indexes
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (e.RowIndex >= dgvInvoices.Rows.Count ||
                e.ColumnIndex >= dgvInvoices.Columns.Count)
                return;

            if (dgvInvoices.Columns[e.ColumnIndex].Name != "colSelect")
                return;

            DataGridViewRow row = dgvInvoices.Rows[e.RowIndex];

            Invoice invoice = row.DataBoundItem as Invoice;

            if (invoice == null)
                return;

            bool isSelected =
                row.Cells["colSelect"].Value is bool value && value;

            if (isSelected)
            {
                selectedInvoiceIds.Add(invoice.id);
            }
            else
            {
                selectedInvoiceIds.Remove(invoice.id);
            }

            UpdateSelectAllCheckbox();
        }

        // =========================================================
        // SELECT ALL
        // =========================================================

        private void ChkSelectAll_CheckedChanged(
            object sender,
            EventArgs e)
        {
            if (updatingSelection)
                return;

            bool selectAll = chkSelectAll.Checked;

            updatingSelection = true;

            try
            {
                selectedInvoiceIds.Clear();

                foreach (DataGridViewRow row in dgvInvoices.Rows)
                {
                    Invoice invoice = row.DataBoundItem as Invoice;

                    if (invoice == null)
                        continue;

                    row.Cells["colSelect"].Value = selectAll;

                    if (selectAll)
                    {
                        selectedInvoiceIds.Add(invoice.id);
                    }
                }
            }
            finally
            {
                updatingSelection = false;
            }

            lblStatus.Text =
                $"{selectedInvoiceIds.Count} invoice(s) selected.";
        }

        // =========================================================
        // UPDATE SELECT ALL CHECKBOX
        // =========================================================

        private void UpdateSelectAllCheckbox()
        {
            if (updatingSelection)
                return;

            bool allSelected =
                invoices.Count > 0 &&
                selectedInvoiceIds.Count == invoices.Count;

            updatingSelection = true;

            try
            {
                chkSelectAll.Checked = allSelected;
            }
            finally
            {
                updatingSelection = false;
            }

            lblStatus.Text =
                $"{selectedInvoiceIds.Count} invoice(s) selected.";
        }

        // =========================================================
        // SUBMIT SELECTED INVOICES
        // =========================================================

        private async void btnSubmit_Click(
            object sender,
            EventArgs e)
        {
            await SubmitSelectedInvoicesAsync();
        }

        private async Task SubmitSelectedInvoicesAsync()
        {
            // Get the original Invoice objects for selected IDs
            List<Invoice> selectedInvoices = invoices
                .Where(invoice =>
                    selectedInvoiceIds.Contains(invoice.id))
                .ToList();

            if (selectedInvoices.Count == 0)
            {
                MessageBox.Show(
                    "Please select at least one invoice.",
                    "No Invoices Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DialogResult confirmation = MessageBox.Show(
                $"Are you sure you want to submit " +
                $"{selectedInvoices.Count} invoice(s)?",
                "Confirm Submission",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            SetLoading(true);

            try
            {
                string invoiceDate =
                    dtpInvoiceDate.Value.ToString("yyyy-MM-dd");

                // Serialize the original Invoice objects.
                // The selection checkbox is NOT part of the JSON.
                string json = JsonSerializer.Serialize(
                    selectedInvoices,
                    JsonOptions);

                using var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    SubmitInvoicesUrl);

                request.Headers.Add(
                    DateHeaderName,
                    invoiceDate);

                request.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                using HttpResponseMessage response =
                    await httpClient.SendAsync(request);

                string responseBody =
                    await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                lblStatus.Text =
                    $"{selectedInvoices.Count} invoice(s) submitted successfully.";

                MessageBox.Show(
                    $"Invoice submission completed.\n\n" +
                    $"Invoices submitted: {selectedInvoices.Count}\n\n" +
                    $"API response:\n{responseBody}",
                    "Submission Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error submitting invoices:\n\n{ex.Message}",
                    "Submission Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        // =========================================================
        // LOADING INDICATOR
        // =========================================================

        private void SetLoading(bool loading)
        {
            progressBarLoading.Visible = loading;

            btnGetInvoices.Enabled = !loading;
            btnSubmit.Enabled = !loading;
            chkSelectAll.Enabled = !loading;
            dgvInvoices.Enabled = !loading;
        }
    }
}
