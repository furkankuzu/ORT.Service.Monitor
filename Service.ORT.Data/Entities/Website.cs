namespace Service.ORT.Data.Entities;

public class Website
{
    public int Id { get; set; }
    public required string WebsiteUrl { get; set; }
    public required string UserEmail { get; set; }
    public bool IsUp { get; set; }
    public int Period { get; set; }
}
