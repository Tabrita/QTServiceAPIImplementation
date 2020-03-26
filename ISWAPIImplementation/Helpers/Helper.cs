using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISWAPIImplementation.Helpers
{
    public class ISWAPI
    {
        private const string baseUrl = "https://sandbox.interswitchng.com/api/v2/quickteller/";

        public HttpClient Initials()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            return client;
        }
      
        public string SHA1(string plainText)
        {            
            SHA1 HashTool = new SHA1Managed();
            Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(plainText));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }

        public string ConvertStringToBase64(string plainText)
        {
            Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(plainText));
            return Convert.ToBase64String(PhraseAsByte);
        }

        public string GetNonce()
        {
            Guid uuid = Guid.NewGuid();
            String nonce = uuid.ToString();
            nonce = nonce.Replace("-", "");
            
            return nonce;
        }                

        public string GetTimeStamp()
        {
            return ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }

        public string Timestamp()
        {
            return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public string signatureCipher(string http_verb, string URL, string timestamp, string nonce, string clientId, string secretKey)
        {
            //System.Web.HttpUtility.UrlEncode returned lowercase. This is a big difference between the two
            String baseStringToBeSigned = http_verb + "&" + System.Net.WebUtility.UrlEncode(URL) + "&" 
                                            + timestamp + "&" + nonce + "&" + clientId + "&" + secretKey;
            
            return baseStringToBeSigned;
        }
        
    }
}
