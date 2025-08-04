using System.ComponentModel.DataAnnotations;

namespace leca_api.Models;

public class CalendarEvent
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public String Title { get; set; }
    public String Description { get; set; }
    public String Location { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public String Organizer { get; set; }
    public List<String> Attendees { get; set; }
}