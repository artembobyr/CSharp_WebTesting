using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RestSharp;

namespace FloodIO
{
    [TestFixture]
    public class FloodIo
    {
        private const string Baseurl = "https://challengers.flood.io";
        private RestClient _client;
        private IRestRequest _request;
        private IRestResponse _response;

        private string _token;
        private string _stepId;
        private string _stepNumber;

        private string FindRegex(string pattern)
        {
            var tokenPattern = new Regex(pattern);
            return tokenPattern.Match(_response.Content).Groups[1].Value;
        }

        [Test]
        [Order(1)]
        public void Step1()
        {
            _client = new RestClient(Baseurl);
            _request = new RestRequest(Method.GET)
                .AddHeader("Accept",
                    "ext/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");

            _response = _client.Execute(_request);
            Assert.AreEqual(200, (int) _response.StatusCode);

            _token = FindRegex(@"authenticity_token.*? value=""(.*?)""");
            _stepId = FindRegex(@"step_id.+?value=""(.+?)""");
            _stepNumber = FindRegex(@"step_number.*?value=""(.*?)""");
        }

        [Test]
        [Order(2)]
        public void Step2()
        {
            _request = new RestRequest("https://challengers.flood.io/start", Method.POST)
                //{ RequestFormat = DataFormat.Json }
                .AddParameter(
                    $"authenticity_token={_token}&challenger[step_id]={_stepId}&challenger[step_number]={_stepNumber}&commit=Start",
                    ParameterType.RequestBody)
                .AddHeader("Accept",
                    "ext/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3")
                .AddHeader("Accept-Encoding", "gzip, deflate");

            _response = _client.Execute(_request);
            var body = _request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Console.WriteLine(body?.Value);
            //Console.WriteLine("\n");
            //Console.WriteLine(response.Content);
            //Assert.That((int)response.StatusCode, Is.AnyOf(302,201), "Incorrect status");
        }

        //[Test, Order(3)]
        //public void Step3()
        //{

        //}

        //[Test, Order(4)]
        //public void Step4()
        //{

        //}

        //[Test, Order(5)]
        //public void Step5()
        //{

        //}
    }
}