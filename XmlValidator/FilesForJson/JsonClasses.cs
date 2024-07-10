using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;

namespace XmlValidator.Files
{
    public class ClassificationCodes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class CountryCodes
    {
        public string Code { get; set; }
        public string Country { get; set; }
    }
    public class InvoiceCodes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class TaxTypeCodes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class UnitCodes
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    public class PaymentMethodCodes
    {
        public string Code { get; set; }
        public string PaymentMethod { get; set; }
    }
    public class State
    {
        public string Code { get; set; }
        public string PaymentMethod { get; set; }
    }
    public class CurrencyCodes
    {
        public string Code { get; set; }
        public string Currency { get; set; }
    }
    public class MSIC
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string MSICCategoryReference { get; set; }
    }
    public class GetSpecificCodes
    {
        HttpClient client = new HttpClient();
        private string GetFilePath(string fileName)
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/FilesForJson"), $"{fileName}.json");
        }
        public List<ClassificationCodes> GetClassificationCodes()
        {
            string filePath = GetFilePath("ClassificationCodes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<ClassificationCodes>>(jsonString);
            return objects;
        }
        public List<CountryCodes> GetCountryCodes()
        {
            string filePath = GetFilePath("CountryCodes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<CountryCodes>>(jsonString);
            return objects;
        }
        public List<InvoiceCodes> GetInvoiceCodes()
        {
            string url = "https://sdk.myinvois.hasil.gov.my/files/EInvoiceTypes.json";
            string jsonString = client.GetStringAsync(url).GetAwaiter().GetResult();
            var objects = JsonConvert.DeserializeObject<List<InvoiceCodes>>(jsonString);
            return objects;
        }
        public List<TaxTypeCodes> GetTaxTypeCodes()
        {
            string filePath = GetFilePath("TaxTypes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<TaxTypeCodes>>(jsonString);
            return objects;
        }
        public List<UnitCodes> GetUnitCodes()
        {
            string filePath = GetFilePath("UnitTypes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<UnitCodes>>(jsonString);
            return objects;
        }
        public List<PaymentMethodCodes> GetPaymentMethodCodes()
        {
            string filePath = GetFilePath("PaymentMethods");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<PaymentMethodCodes>>(jsonString);
            return objects;
        }
        public List<State> GetStates()
        {
            string filePath = GetFilePath("StateCodes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<State>>(jsonString);
            return objects;
        }
        public List<CurrencyCodes> GetCurrencyCodes()
        {
            string filePath = GetFilePath("CurrencyCodes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<CurrencyCodes>>(jsonString);
            return objects;
        }
        public List<MSIC> GetMSICcodes()
        {
            string filePath = GetFilePath("MSICSubCategoryCodes");
            var jsonString = File.ReadAllText(filePath);
            var objects = JsonConvert.DeserializeObject<List<MSIC>>(jsonString);
            return objects;
        }
    }
}
