namespace WebAPI.Models;
public class Worker
{
    public int code { get; set; }
    public string? id { get; set; }
    public string? fullname { get; set; }
    public int role { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public string pass { get; set; }
    public int active { get; set; }
}
