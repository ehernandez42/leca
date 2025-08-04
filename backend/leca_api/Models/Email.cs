using System.ComponentModel.DataAnnotations;

namespace leca_api.Models;

public class Email
{
    [Key]
    public Guid Id { get; set; }
    public Guid EmailId { get; set; }
    public Guid MessageId { get; set; }
    
    public String Subject { get; set; }
    public String Body { get; set; }
    public String Sender { get; set; }
    public DateTime TimeStamp { get; set; }
    
    public int ImportanceScore { get; set; }
    public String Category { get; set; }
}