using CodePulse.API.Services.interfaces;
using Google.GenAI;

namespace CodePulse.API.Services.implementations
{
    public class GeminiService : IGeminiService
    {

        private readonly string _apiKey;

        public GeminiService(IConfiguration configuration)
        {
            _apiKey = configuration["Gemini:ApiKey"]!;
        }

        public async Task<string> GenerateSummary(string content)
        {
            var client = new Client(apiKey: _apiKey);

            var response = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: $"Summarize this:\n{content}"
            );

            return response.Text ?? "";
        }

        public async Task<string> GenerateTitle(string content)
        {
            var client = new Client(apiKey: _apiKey);

            var response = await client.Models.GenerateContentAsync(
                model: "gemini-2.5-flash",
                contents: $"""
            Generate ONE engaging blog title based on the following article.

            The title must be:
            - concise
            - SEO friendly
            - relevant to the content

            ARTICLE:
            {content}
        """
            );

            return response.Text ?? "";
        }
    }
}
