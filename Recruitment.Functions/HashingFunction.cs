using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Recruitment.Functions
{
    public static class HashingFunction
    {
        [FunctionName("HashingFunction")]
        public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            IEnumerable<string> values = GetValues(req);
            string hash = GetHash(values);
            return new JsonResult(new { hash_value = hash });
        }

        private static IEnumerable<string> GetValues(HttpRequest req)
        {
            return req.Form.Keys.Select(key =>
            {
                StringValues value;
                var success = req.Form.TryGetValue(key, out value);
                if (success)
                    return value.ToString();
                else
                    return string.Empty;
            })
            .Where(value => !string.IsNullOrWhiteSpace(value));
        }

        private static string GetHash(IEnumerable<string> values)
        {
            string hash;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(string.Join("", values));
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                hash = sb.ToString();
            }

            return hash;
        }
    }
}
