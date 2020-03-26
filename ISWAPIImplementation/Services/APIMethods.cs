using ISWAPIImplementation.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ISWAPIImplementation.Services
{
    public class AllMethods
    {
        private readonly IConfiguration _configuration;
        public AllMethods(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        ISWAPI _api = new ISWAPI();

        private string clientId; 
        private string secretKey;
       
        enum MyHttpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        //Get all Billers
        public HttpClient GetAllBillers()
        {
            clientId = _configuration["SandboxClientId"];
            secretKey = _configuration["SandboxSecretkey"];

            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = MyHttpVerb.GET;
            
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb.ToString(), client.BaseAddress + "billers", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);            
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //Get all Categories
        public HttpClient GetAllCategories()
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "GET";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "categorys", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //Get all Bank Codes
        public HttpClient GetBankCodes()
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "GET";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "configuration/fundstransferbanks", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //Get Payment Items
        public HttpClient GetBillerPaymentItems(int id)
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "GET";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "billers/" + id + "/paymentitems", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //Get Billers by Cateory
        public HttpClient GetBillerByCategory(int categoryId)
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "GET";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "categorys/" + categoryId + "/billers", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //Transaction Query
        public HttpClient TransactionQuery(string requestRef)
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "GET";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "transactions?requestreference=" + requestRef, timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }

        //POST
        //Send Bill Payment Advice
        public HttpClient SendBillPaymentAdvice()
        {
            var timeStamp = _api.GetTimeStamp();
            var nounce = _api.GetNonce();
            var httpVerb = "POST";
            var auth = "InterswitchAuth " + _api.ConvertStringToBase64(clientId);

            HttpClient client = _api.Initials();

            var signatureCipher = _api.signatureCipher(httpVerb, client.BaseAddress + "payments/advices", timeStamp, nounce, clientId, secretKey);
            var signature = _api.SHA1(signatureCipher);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("terminalId", "3ERT0001");
            client.DefaultRequestHeaders.Add("Timestamp", timeStamp);
            client.DefaultRequestHeaders.Add("Nonce", nounce);
            client.DefaultRequestHeaders.Add("Signature", signature);
            client.DefaultRequestHeaders.Add("SignatureMethod", "SHA1");
            client.DefaultRequestHeaders.Add("Authorization", auth);

            return client;
        }
    }
}
