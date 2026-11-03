using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoiceFieldworkToXero
{
    public class Invoice
    {
        public bool IsSelected { get; set; }
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("approved")]
        public bool Approved { get; set; }

        [JsonPropertyName("invoice_number")]
        public int InvoiceNumber { get; set; }

        [JsonPropertyName("tax_amount")]
        public string TaxAmount { get; set; } = "0";

        [JsonPropertyName("total")]
        public string Amount { get; set; } = "0";

        [JsonPropertyName("discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("discount_amount")]
        public string DiscountAmount { get; set; } = "0";

        [JsonPropertyName("callback")]
        public bool Callback { get; set; }

        [JsonPropertyName("due_amount")]
        public string DueAmount { get; set; } = "0";

        [JsonPropertyName("branch_id")]
        public int BranchId { get; set; }

        [JsonPropertyName("due_date")]
        public DateTimeOffset? DueDate { get; set; }

        [JsonPropertyName("emailed_on")]
        public DateTimeOffset? EmailedOn { get; set; }

        [JsonPropertyName("last_printed_at")]
        public DateTimeOffset? LastPrintedAt { get; set; }

        [JsonPropertyName("paid_amount")]
        public string PaidAmount { get; set; } = "0";

        [JsonPropertyName("paid_date")]
        public DateTimeOffset? PaidDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "";

        [JsonPropertyName("customer_id")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customer_name")]
        public string Customer { get; set; } = "";

        [JsonPropertyName("service_location_id")]
        public int ServiceLocationId { get; set; }

        [JsonPropertyName("service_location_name")]
        public string ServiceLocationName { get; set; } = "";

        [JsonPropertyName("tax_rate_id")]
        public int TaxRateId { get; set; }

        [JsonPropertyName("payments")]
        public List<InvoicePayment> Payments { get; set; } = new();

        [JsonPropertyName("line_items")]
        public List<InvoiceLineItem> LineItems { get; set; } = new();
    }

    public class InvoicePayment
    {
        // The supplied example contains no payment objects, so their fields are not yet known.
    }

    public class InvoiceLineItem
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("payable_id")]
        public int PayableId { get; set; }

        [JsonPropertyName("payable_type")]
        public string PayableType { get; set; } = "";

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; } = "0";

        [JsonPropertyName("price")]
        public string Price { get; set; } = "0";

        [JsonPropertyName("total")]
        public string Total { get; set; } = "0";

        [JsonPropertyName("taxable")]
        public bool Taxable { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; } = "";

        [JsonPropertyName("line_item_service_location_units")]
        public List<JsonElement> LineItemServiceLocationUnits { get; set; } = new();
    }
}


