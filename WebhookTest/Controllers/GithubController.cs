using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using WebhookTest.Models;

namespace WebhookTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubController : ControllerBase
    {
        private readonly string _accessToken = "d2f41c22dab0c1b094b5f91397d1b15ab25eaab7";

        // POST: api/Github
        [HttpPost]
        public void Post(GithubPayload githubPayload)
        {
            string review_comments_url = githubPayload.pull_request.review_comments_url;

            IRestResponse response = CallGetAPI(review_comments_url);

            JObject commentsJson = JObject.Parse("{ 'comments':" + response.Content + "}");
            int commentsCount = commentsJson.SelectToken("comments").Count();

            Console.WriteLine(commentsCount);
        }

        private IRestResponse CallGetAPI(string review_comments_url)
        {
            RestClient restClient = new RestClient(review_comments_url);
            RestRequest restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Authorization", $"Bearer {_accessToken}");

            IRestResponse response = restClient.Execute(restRequest);

            return response;
        }
    }
}
