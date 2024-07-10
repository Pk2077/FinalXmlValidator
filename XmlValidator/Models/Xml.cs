using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XmlValidator.Extensions;

namespace XmlValidator.Models
{
    public class Rootobject
    {
        public Xml xml { get; set; }
        public Invoice Invoice { get; set; }
    }
    public class Xml
    {
        public string version { get; set; }
        public string encoding { get; set; }
    }
    public class Invoice
    {
        public string xmlns { get; set; }
        public string xmlnscac { get; set; }
        public string xmlnscbc { get; set; }
        public string ID { get; set; }
        public string IssueDate { get; set; }
        public string IssueTime { get; set; }
        public Invoicetypecode InvoiceTypeCode { get; set; }
        public string DocumentCurrencyCode { get; set; }
        public string TaxCurrencyCode { get; set; }
        public Invoiceperiod InvoicePeriod { get; set; }
        public Accountingsupplierparty AccountingSupplierParty { get; set; }
        public Accountingcustomerparty AccountingCustomerParty { get; set; }
        public Paymentmeans PaymentMeans { get; set; }
        public Taxtotal TaxTotal { get; set; }
        public Legalmonetarytotal LegalMonetaryTotal { get; set; }
        public Paymentterms PaymentTerms { get; set; }
        public Prepaidpayment PrepaidPayment { get; set; }
        public Delivery Delivery { get; set; }
    }
    public class Legalmonetarytotal
    {
        public Amount LineExtensionAmount { get; set; }
        public Amount TaxExclusiveAmount { get; set; }
        public Amount TaxInclusiveAmount { get; set; }
        public Amount AllowanceTotalAmount { get; set; }
        public Amount ChargeTotalAmount { get; set; }
        public Amount PayableRoundingAmount { get; set; }
        public Amount PayableAmount { get; set; }
    }
    public class Invoicetypecode
    {
        public string listVersionID { get; set; }
        public string text { get; set; }
    }
    public class Invoiceperiod
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }
    public class Billingreference
    {
        public InvoiceDocumentReference InvoiceDocumentReference { get; set; }
        public BillingAdditionaldocumentreference AdditionalDocumentReference { get; set; }
    }
    public class Additionaldocumentreference
    {
        public string ID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDescription { get; set; }
    }
    public class BillingAdditionaldocumentreference
    {
        public string ID { get; set; }
    }
    public class InvoiceDocumentReference
    {
        public string ID { get; set; }
        public string UUID { get; set; }
    }
    public class DocumentID
    {
        public string ID { get; set; }
        public string DocumentType { get; set; }
        public string DocumentDescription { get; set; }
    }
    public class Accountingsupplierparty
    {
        public Party Party { get; set; }
        public Additionalaccountid AdditionalAccountID { get; set; }
    }
    public class Party
    {
        public Industryclassificationcode IndustryClassificationCode { get; set; }
        public Postaladdres PostalAddress { get; set; }
        public Partylegalentity PartyLegalEntity { get; set; }
        public Contact Contact { get; set; }
    }
    public class Industryclassificationcode
    {
        public string name { get; set; }
        public string text { get; set; }
    }
    public class Postaladdres
    {
        public string PostalZone { get; set; }
        public string CityName { get; set; }
        public string CountrySubentityCode { get; set; }
        public Country Country { get; set; }
    }
    public class Country
    {
        public IdentificationCode IdentificationCode { get; set; }
    }
    public class IdentificationCode
    {
        public string listAgencyID { get; set; }
        public string listID { get; set; }
        public string text { get; set; }
    }
    public class Addressline
    {
        public string Line { get; set; }
    }
    public class Partylegalentity
    {
        public string RegistrationName { get; set; }
    }
    public class Contact
    {
        public string Telephone { get; set; }
        public string ElectronicMail { get; set; }
    }
    public class PartyIdentification
    {
        public ID ID { get; set; }
    }
    public class ID
    {
        public string schemeID { get; set; }
        public string schemeAgencyID { get; set; }
        public string text { get; set; }
    }
    public class Additionalaccountid
    {
        public string schemeAgencyName { get; set; }
        public string text { get; set; }
    }
    public class Accountingcustomerparty
    {
        public Party Party { get; set; }
    }
    public class Paymentmeans
    {
        public string PaymentMeansCode { get; set; }
        public Payeefinancialaccount PayeeFinancialAccount { get; set; }
    }
    public class Payeefinancialaccount
    {
        public string ID { get; set; }
    }
    public class Taxtotal
    {
        public Amount TaxAmount { get; set; }
    }
    public class Taxsubtotal
    {
        public Amount TaxableAmount { get; set; }
        public Amount perUnitAmount { get; set; }
        public string percent { get; set; }
        public Amount TaxAmount { get; set; }
        public Taxcategory TaxCategory { get; set; }
        public Baseunitmeasure Baseunitmeasure { get; set; }
    }
    public class Taxcategory
    {
        public string ID { get; set; }
        public string Percent { get; set; }
        public Taxscheme TaxScheme { get; set; }
        public string TaxExemptionReason { get; set; }
    }
    public class Taxscheme
    {
        public ID ID { get; set; }
    }
    public class Invoiceline
    {
        public string ID { get; set; }
        public Invoicedquantity InvoicedQuantity { get; set; }
        public Amount LineExtensionAmount { get; set; }
        public Taxtotal TaxTotal { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }
        public Itempriceextension ItemPriceExtension { get; set; }
    }
    public class Invoicedquantity
    {
        public string unitCode { get; set; }
        public string text { get; set; }
    }
    public class Baseunitmeasure
    {
        public string unitCode { get; set; }
        public string text { get; set; }
    }
    public class Perunitamount
    {
        public string currencyID { get; set; }
        public string text { get; set; }
    }
    public class Item
    {
        public string Description { get; set; }
        public Origincountry OriginCountry { get; set; }
    }
    public class Origincountry
    {
        public string IdentificationCode { get; set; }
    }
    public class Commodityclassification
    {
        public Itemclassificationcode ItemClassificationCode { get; set; }
    }
    public class Itemclassificationcode
    {
        public string listID { get; set; }
        public string text { get; set; }
    }
    public class Price
    {
        public Amount PriceAmount { get; set; }
    }
    public class Itempriceextension
    {
        public Amount Amount { get; set; }
    }
    public class Amount
    {
        public string currencyID { get; set; }
        public string text { get; set; }
    }
    public class Allowancecharge
    {
        public string ChargeIndicator { get; set; }
        public Amount Amount { get; set; }
        public string MultiplierFactorNumeric { get; set; }
        public string AllowanceChargeReason { get; set; }
    }
    public class Paymentterms
    {
        public string Note { get; set; }
    }
    public class Prepaidpayment
    {
        public string ID { get; set; }
        public Amount PaidAmount { get; set; }
        public string PaidDate { get; set; }
        public string PaidTime { get; set; }
    }
    public class Delivery
    {
        public Deliveryparty DeliveryParty { get; set; }
        public Shipment Shipment { get; set; }
    }
    public class Deliveryparty
    {
        public Partylegalentity PartyLegalEntity { get; set; }
        public Postaladdres PostalAddres { get; set; }
    }
    public class Shipment
    {
        public string ID { get; set; }
        public Freightallowancecharge FreightAllowanceCharge { get; set; }
    }
    public class Freightallowancecharge
    {
        public string ChargeIndicator { get; set; }
        public Amount Amount { get; set; }
        public string AllowanceChargeReason { get; set; }
    }
    public class ErrorModel
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string errMsg { get; set; }
    }
}