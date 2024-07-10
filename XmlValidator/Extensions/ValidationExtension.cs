using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using XmlValidator.Files;
using XmlValidator.Models;
using static System.Net.Mime.MediaTypeNames;

namespace XmlValidator.Extensions
{
    public class ValidationExtension
    {
        public XNamespace cac = @"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2";
        public XNamespace cbc = @"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
        public List<ErrorModel> ValidatePropertySize(string propertyValue, string propertyName, int size)
        {
            List<ErrorModel> _ErrorModel = new List<ErrorModel>();
            if (string.IsNullOrEmpty(propertyValue))
            {
                _ErrorModel.Add(new ErrorModel
                {

                    errMsg = propertyName + " cannot be empty.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                });
            }
            else if (propertyValue.Length > size)
            {
                _ErrorModel.Add(new ErrorModel
                {

                    errMsg = propertyName + " cannot exceed " + size + " characters.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                });
            }
            return _ErrorModel;
        }
        public List<ErrorModel> ValidatePropertyBySizeRange(string propertyValue, string propertyName, int minSize, int maxSize)
        {
            List<ErrorModel> _ErrorModel = new List<ErrorModel>();
            if (string.IsNullOrEmpty(propertyValue))
            {
                _ErrorModel.Add(new ErrorModel
                {

                    errMsg = propertyName + " cannot be empty.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                });
            }
            else if (propertyValue.Length < minSize && propertyValue.Length > maxSize)
            {
                _ErrorModel.Add(new ErrorModel
                {

                    errMsg = propertyName + " should be between " + minSize + "and" + minSize,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                });
            }
            return _ErrorModel;
        }
        public ErrorModel ValidatePropertyNormalization(string propertyValue, string propertyName)
        {
            var errorModel = new ErrorModel();

            if (propertyValue.Contains("\t") || propertyValue.Contains("\r") || propertyValue.Contains("\n"))
            {
                errorModel.errMsg = $"{propertyName} should be a normalized string.";
                errorModel.PropertyName = propertyName;
                errorModel.PropertyValue = propertyValue;
            }

            return errorModel;
        }
        public ErrorModel ValidateDateProperty(string propertyValue, string propertyName)
        {
            ErrorModel _ErrorModel = new ErrorModel();
            DateTime isDate;
            if (!DateTime.TryParse(propertyValue, out isDate))
            {
                _ErrorModel.errMsg = "Invalid " + propertyName;
                _ErrorModel.PropertyName = propertyName;
                _ErrorModel.PropertyValue = propertyValue;
            }
            return _ErrorModel;
        }
        public ErrorModel ValidateTimeProperty(string propertyValue, string propertyName)
        {
            ErrorModel _ErrorModel = new ErrorModel();
            if (propertyValue.EndsWith("Z"))
            {
                propertyValue = propertyValue.Replace("Z", "");
                TimeSpan isDate;
                if (!TimeSpan.TryParse(propertyValue, out isDate))
                {
                    _ErrorModel.errMsg = "Invalid " + propertyName;
                    _ErrorModel.PropertyName = propertyName;
                    _ErrorModel.PropertyValue = propertyValue;
                }
            }
            else
            {
                _ErrorModel.errMsg = "Invalid format for " + propertyName;
                _ErrorModel.PropertyName = propertyName;
                _ErrorModel.PropertyValue = propertyValue;
            }

            return _ErrorModel;
        }
        public ErrorModel ValidateDecimalProperty(string propertyValue, string propertyName)
        {
            ErrorModel _ErrorModel = new ErrorModel();
            decimal isDecimal;
            if (!decimal.TryParse(propertyValue, out isDecimal))
            {

                _ErrorModel.errMsg = "Invalid Value for " + propertyName;
                _ErrorModel.PropertyName = propertyName;
                _ErrorModel.PropertyValue = propertyValue;
            }
            return _ErrorModel;
        }
        public ErrorModel ValidateBooleanProperty(string propertyValue, string propertyName)
        {
            ErrorModel _ErrorModel = new ErrorModel();
            if (!(propertyValue == "false" || propertyValue == "true"))
            {

                _ErrorModel.errMsg = "Invalid Value for " + propertyName;
                _ErrorModel.PropertyName = propertyName;
                _ErrorModel.PropertyValue = propertyValue;
            }
            return _ErrorModel;
        }
        public ErrorModel ValidateSpecialStrings(string propertyValue, string propertyName, string allowedChars)
        {
            var errorModel = new ErrorModel();
            var allowedCharSet = new HashSet<char>(allowedChars) { '\t', '\r', '\n', ' ' };
            if (!string.IsNullOrWhiteSpace(propertyValue))
            {
                foreach (var character in propertyValue)
                {
                    if (!allowedCharSet.Contains(character) && !char.IsLetterOrDigit(character))
                    {
                        errorModel.errMsg = string.IsNullOrWhiteSpace(allowedChars)
                            ? $"Special characters are not allowed in {propertyName}."
                            : $"Special characters are not allowed except for '{allowedChars}' in {propertyName}.";
                        errorModel.PropertyName = propertyName;
                        errorModel.PropertyValue = propertyValue;
                        break;
                    }
                }
            }
            return errorModel;
        }
        public ErrorModel ValidateEmailFormat(string email, string propertyName)
        {
            var errorModel = new ErrorModel();
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            var regex = new Regex(emailPattern);
            if (!regex.IsMatch(email))
            {
                errorModel.errMsg = $"Invalid email format for {propertyName}.";
                errorModel.PropertyName = propertyName;
                errorModel.PropertyValue = email;
            }

            return errorModel;
        }
        public ErrorModel ValidateCountry(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var Countries = new GetSpecificCodes().GetCountryCodes();
            bool isValid = false;
            foreach (var item in Countries)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = Countries.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateClassification(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var Classifications = new GetSpecificCodes().GetClassificationCodes();
            bool isValid = false;
            foreach (var item in Classifications)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = Classifications.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateInvoiceType(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var InvoiceTypes = new GetSpecificCodes().GetInvoiceCodes();
            bool isValid = false;
            foreach (var item in InvoiceTypes)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = InvoiceTypes.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidatePaymentMethods(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var PaymentMethods = new GetSpecificCodes().GetPaymentMethodCodes();
            bool isValid = false;
            foreach (var item in PaymentMethods)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = PaymentMethods.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateStates(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var States = new GetSpecificCodes().GetStates();
            bool isValid = false;
            foreach (var item in States)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = States.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateTaxType(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var TaxTypes = new GetSpecificCodes().GetTaxTypeCodes();
            bool isValid = false;
            foreach (var item in TaxTypes)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = TaxTypes.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateUnitType(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var UnitTypes = new GetSpecificCodes().GetUnitCodes();
            bool isValid = false;
            foreach (var item in UnitTypes)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = UnitTypes.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateCurrencyType(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var CurrencyTypes = new GetSpecificCodes().GetCurrencyCodes();
            bool isValid = false;
            foreach (var item in CurrencyTypes)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = CurrencyTypes.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public ErrorModel ValidateMSICType(string propertyValue, string propertyName)
        {
            ErrorModel errorModel = new ErrorModel();
            var MSICTypes = new GetSpecificCodes().GetMSICcodes();
            bool isValid = false;
            foreach (var item in MSICTypes)
            {
                if (item.Code == propertyValue)
                {
                    isValid = true;
                }
            }
            if (!isValid)
            {
                var list = MSICTypes.Select(x => x.Code).ToList();
                errorModel = new ErrorModel
                {

                    errMsg = propertyName + $" should be among these {string.Join(",", list)}.",
                    PropertyName = propertyName,
                    PropertyValue = propertyValue
                };
            }
            return errorModel;
        }
        public string JsonHandler(string json)
        {
            return json.Replace("cbc:", "").
                Replace("cac:", "").
                Replace("@xmlns", "xmlns").
                Replace("@schemeAgencyName", "schemeAgencyName").
                Replace("@schemeAgencyID", "schemeAgencyID").
                Replace("@name", "name").
                Replace("@schemeID", "schemeID").
                Replace("@listID", "listID").
                Replace("@listAgencyID", "listAgencyID").
                Replace("@currencyID", "currencyID").
                Replace("@unitCode", "unitCode").
                Replace("@listVersionID", "listVersionID").
                Replace("@version", "version").
                Replace("@encoding", "encoding").
                Replace("?xml", "xml").
                Replace("#text", "text");
        }
        public List<T> GetMultiElements<T>(string PathToXml, string name, bool isCac, ref string error)
        {
            List<T> _MultiElements = new List<T>();
            XDocument xDoc = XDocument.Load(PathToXml);
            IEnumerable<XElement> MultiElements = isCac ? xDoc.Root.Elements(cac + name) : xDoc.Root.Elements(cbc + name);
            foreach (XElement MonoElement in MultiElements)
            {
                string jsontxt = JsonConvert.SerializeXNode(MonoElement);
                jsontxt = new ValidationExtension().JsonHandler(jsontxt);
                try
                {
                    T item = JsonConvert.DeserializeObject<T>(jsontxt);
                    _MultiElements.Add(item);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            return _MultiElements;
        }
        public List<T> GetMultiElements<T>(string PathToXml, string name, string ParentName, bool isCac, ref string error)
        {
            List<T> _MultiElements = new List<T>();
            XDocument xDoc = XDocument.Load(PathToXml);
            IEnumerable<XElement> MultiElements = isCac ? xDoc.Root.Elements(cac + ParentName).Elements(cac + name) : xDoc.Root.Elements(cbc + ParentName).Elements(cbc + name);
            foreach (XElement MonoElement in MultiElements)
            {
                string jsontxt = JsonConvert.SerializeXNode(MonoElement);
                jsontxt = new ValidationExtension().JsonHandler(jsontxt);
                try
                {
                    T item = JsonConvert.DeserializeObject<T>(jsontxt);
                    _MultiElements.Add(item);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            return _MultiElements;
        }
        public List<T> GetMultiElements<T>(string PathToXml, string name, string ParentName1, string ParentName2, bool isCac, ref string error)
        {
            List<T> _MultiElements = new List<T>();
            XDocument xDoc = XDocument.Load(PathToXml);
            IEnumerable<XElement> MultiElements = isCac ? xDoc.Root.Elements(cac + ParentName1).Elements(cac + ParentName2).Elements(cac + name) : xDoc.Root.Elements(cbc + ParentName1).Elements(cbc + ParentName2).Elements(cbc + name);
            foreach (XElement MonoElement in MultiElements)
            {
                string jsontxt = JsonConvert.SerializeXNode(MonoElement);
                jsontxt = new ValidationExtension().JsonHandler(jsontxt);
                try
                {
                    T item = JsonConvert.DeserializeObject<T>(jsontxt);
                    _MultiElements.Add(item);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            return _MultiElements;
        }
        public List<T> GetMultiElements<T>(string PathToXml, string name, string ParentName1, string ParentName2, string ParentName3, bool isCac, ref string error)
        {
            List<T> _MultiElements = new List<T>();
            XDocument xDoc = XDocument.Load(PathToXml);
            IEnumerable<XElement> MultiElements = isCac ? xDoc.Root.Elements(cac + ParentName1).Elements(cac + ParentName2).Elements(cac + ParentName3).Elements(cac + name) : xDoc.Root.Elements(cbc + ParentName1).Elements(cbc + ParentName2).Elements(cbc + ParentName3).Elements(cbc + name);
            foreach (XElement MonoElement in MultiElements)
            {
                string jsontxt = JsonConvert.SerializeXNode(MonoElement);
                jsontxt = new ValidationExtension().JsonHandler(jsontxt);
                try
                {
                    T item = JsonConvert.DeserializeObject<T>(jsontxt);
                    _MultiElements.Add(item);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            return _MultiElements;
        }
        public dynamic GetClassificationsWithId(string XmlJson, ref string error)
        {
            JObject jsonObject = JObject.Parse(XmlJson);
            var resultList = new List<dynamic>();
            JToken invoiceLineToken = jsonObject.SelectToken("Invoice.InvoiceLine");
            if (invoiceLineToken is JArray invoiceLines)
            {
                foreach (var line in invoiceLines)
                {
                    var result = new
                    {
                        ID = line["ID"].ToString(),
                        CommodityClassification = GetCommodityClassification(line["Item"]["CommodityClassification"], ref error)
                    };
                    resultList.Add(result);
                }
            }
            else if (invoiceLineToken is JObject invoiceLine)
            {
                var result = new
                {
                    ID = invoiceLine["ID"].ToString(),
                    CommodityClassification = GetCommodityClassification(invoiceLine["Item"]["CommodityClassification"], ref error)
                };
                resultList.Add(result);
            }
            return resultList;
        }
        private List<dynamic> GetCommodityClassification(JToken classificationToken, ref string error)
        {
            var classifications = new List<dynamic>();
            try
            {

                if (classificationToken is JArray classificationArray)
                {
                    foreach (var classification in classificationArray)
                    {
                        classifications.Add(new
                        {
                            listID = classification["ItemClassificationCode"]["listID"].ToString(),
                            text = classification["ItemClassificationCode"]["text"].ToString()
                        });
                    }
                }
                else if (classificationToken is JObject classificationObject)
                {
                    classifications.Add(new
                    {
                        listID = classificationObject["ItemClassificationCode"]["listID"].ToString(),
                        text = classificationObject["ItemClassificationCode"]["text"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {

                error = ex.Message;
            }

            return classifications;
        }
    }
}