namespace CodePulse.API.Services.interfaces
{
    public interface IGeminiService
    {
        Task<string> GenerateSummary(string content);
        Task<string> GenerateTitle(string content);
    }
}
