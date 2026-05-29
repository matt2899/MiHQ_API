using CodePulse.API.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public AiController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("summarize")]
        public async Task<IActionResult> Summarize([FromBody] SummaryRequestDto request)
        {
            try
            {
                var summary = await _geminiService.GenerateSummary(request.Content);

                return Ok(new
                {
                    summary
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message

                });
            }
        }

        [HttpPost("generate-title")]
        public async Task<IActionResult> GenerateTitle([FromBody] SummaryRequestDto request)
        {
            var title = await _geminiService.GenerateTitle(request.Content);

            return Ok(new
            {
                title
            });
        }
    }

    
    public class SummaryRequestDto
    {
        public string Content { get; set; } = string.Empty;
    }


}
