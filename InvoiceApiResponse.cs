using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace InvoiceFieldworkToXero
{
    public class InvoiceApiResponse
    {
        [JsonPropertyName("invoices")]
        public List<InvoiceResult> Invoices { get; set; } = new();
    }

    // Represents an individual invoice.
    public class InvoiceResult
    {
        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; } = "";

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }
}
