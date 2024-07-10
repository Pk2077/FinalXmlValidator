using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlValidator.Models
{
    public class InvoicelineMulti
    {
        public Invoiceline Invoiceline { get; set; }
    }
    public class AllowancechargeMulti
    {
        public Allowancecharge Allowancecharge { get; set; }
    }
    public class AdditionalDocumentReferenceMulti
    {
        public Additionaldocumentreference AdditionalDocumentReference { get; set; }
    }
    public class CommodityclassificationMulti
    {
        public Commodityclassification Commodityclassification { get; set; }
    }
    public class PartyIdentificationMulti
    {
        public PartyIdentification PartyIdentification { get; set; }
    }
    public class AddresslinesMulti
    {
        public Addressline Addressline { get; set; }
    }
    public class BillingReferenceMulti
    {
        public Billingreference Billingreference { get; set; }
    }
    public class TaxsubtotalMulti
    {
        public Taxsubtotal Taxsubtotal { get; set; }
    }
}