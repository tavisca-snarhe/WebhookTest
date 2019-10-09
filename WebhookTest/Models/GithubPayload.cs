namespace WebhookTest.Models
{
    public class GithubPayload
    {
        public PullRequest pull_request;
    }

    public class PullRequest
    {
        public string review_comments_url;
    }
}
