namespace Kiron.Application.Models;

public class HolidayDto 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
    public bool Bunting { get; set; }
}
