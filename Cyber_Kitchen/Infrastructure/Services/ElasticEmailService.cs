using Cyber_Kitchen.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cyber_Kitchen.Infrastructure.Services
{
  public class ElasticEmailService : IEmailService    {        public async Task<string> Send(string to, string subject, string content)        {            var dict = new Dictionary<string, string>            {                { "apikey", "5b8a6fd9-8e07-4748-affa-0dcc39bf6754" },                { "from", "dotnotreply@naijawella.com" },                { "fromName", /*"Naijawella"*/"CyberKitchen" },                { "to", to },                { "subject", subject },                { "bodyHtml", content },                { "isTransactional", "true" }            };

            var values = dict.Select(v => new KeyValuePair<string, string>(v.Key, v.Value));            string address = "https://api.elasticemail.com/v2/email/send";            using (HttpClient client = new HttpClient())            {                var formContent = new FormUrlEncodedContent(values);                var apiResponse = await client.PostAsync(address, formContent);                apiResponse.EnsureSuccessStatusCode();                var response = await apiResponse.Content.ReadAsStringAsync();                return response;            }        }    }
}