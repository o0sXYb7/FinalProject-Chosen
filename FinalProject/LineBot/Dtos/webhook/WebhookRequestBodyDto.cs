using static FinalProject.Dtos.webhook.WebhookEventsDto;

namespace FinalProject.Dtos.webhook
{
    public class WebhookRequestBodyDto
    {
        public string? Destination { get; set; }
        public List<WebhookEventDto> Events { get; set; }
    }
}
