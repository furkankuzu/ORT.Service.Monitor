namespace Service.ORT.Api.Models;

public class AddWebsiteRequest
{
    public required string Url { get; set; }
    public required string Email { get; set; }
    public required int PeriodInMinutes { get; set; }
}
