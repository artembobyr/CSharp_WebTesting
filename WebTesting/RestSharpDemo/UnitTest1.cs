using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace RestSharpDemo
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void GetMethodAndSelialization()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{id}", Method.GET);
            request.AddUrlSegment("id", 2);

            var response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //FIRSTWAY
            //var decerial = new JsonDeserializer();
            //var JSONObj = decerial.Deserialize<Dictionary<String, String>>(response);
            //Console.WriteLine(JSONObj["title"]);
            //Assert.That(JSONObj["author"], Is.EqualTo("Bob"), "Author's name isn't correct");

            //SecondWay
            var obs = JObject.Parse(response.Content);
            Assert.That(obs["author"].ToString(), Is.EqualTo("Bob"), "Author's name isn't correct");
        }

        [Test]
        public void PostMethod()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST)
                    {RequestFormat = DataFormat.Json}
                .AddJsonBody(new Posts {Id = 115, Author = "VASYA", Title = "Posoni"});


            var response = client.Execute<Posts>(request);
            var statusCode = response.StatusCode;
            Console.WriteLine((int) statusCode);
            Assert.That((int) statusCode, Is.EqualTo(201), "Incorrect status");

            //ThirdWay
            //var decerial = new JsonDeserializer();
            //var JSONObj = decerial.Deserialize<Dictionary<String, String>>(response);

            Assert.Multiple(() =>
            {
                Assert.That((int) statusCode, Is.EqualTo(201), "Incorrect status");
                Assert.That(response.Data.Author, Is.EqualTo("VASYA"), "Author's name isn't correct");
            });
        }
    }
}