// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Graph;
// using leca_api.Models;
// using leca_api;
// using leca_api.Services;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Graph.Models;
//
// namespace leca_api.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class EmailController : ControllerBase
// {
//     private readonly LecaDbContext _context;
//     private readonly IGraphService _graphService;
//     
//
//     public EmailController(LecaDbContext context, IGraphService graphService)
//     {
//         _context = context;
//         _graphService = graphService;
//     }
//
//     [HttpGet("fetch")]
//     public async Task<IActionResult> FetchEmails([FromQuery] string? userId = null, [FromQuery] int count = 50)
//     {
//         try
//         {
//             var user = userId ?? "me";
//             var graphClient = _graphService.GetGraphServiceClient();
//             var messages = await graphClient.Users[user].Messages
//                 .Select("id,subject,body,sender,receivedDateTime,importance")
//                 .GetAsync();
//
//             var emails = new List<Email>();
//
//             foreach (var message in messages)
//             {
//                 var email = new Email
//                 {
//                     Id = Guid.NewGuid(),
//                     EmailId = message.Id,
//                     MessageId =message.Id,
//                     Subject = message.Subject ?? "",
//                     Body = message.Body?.Content ?? "",
//                     Sender = message.Sender?.EmailAddress?.Address ?? "",
//                     TimeStamp = message.ReceivedDateTime?.DateTime ?? DateTime.UtcNow,
//                     ImportanceScore = GetImportanceScore(message.Importance),
//                     Category = "Inbox"
//                 };
//
//                 emails.Add(email);
//             }
//
//             await _context.Emails.AddRangeAsync(emails);
//             await _context.SaveChangesAsync();
//
//             return Ok(new { message = $"Successfully fetched and stored {emails.Count} emails", emails = emails.Count });
//         }
//         catch (Exception ex)
//         {
//             return BadRequest(new { error = ex.Message });
//         }
//     }
//
//     [HttpGet("list")]
//     public async Task<IActionResult> GetStoredEmails([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
//     {
//         try
//         {
//             var skip = (page - 1) * pageSize;
//             var emails = await _context.Emails
//                 .OrderByDescending(e => e.TimeStamp)
//                 .Skip(skip)
//                 .Take(pageSize)
//                 .ToListAsync();
//
//             var totalCount = await _context.Emails.CountAsync();
//
//             return Ok(new 
//             { 
//                 emails, 
//                 pagination = new 
//                 { 
//                     page, 
//                     pageSize, 
//                     totalCount, 
//                     totalPages = (int)Math.Ceiling((double)totalCount / pageSize) 
//                 } 
//             });
//         }
//         catch (Exception ex)
//         {
//             return BadRequest(new { error = ex.Message });
//         }
//     }
//
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetEmail(Guid id)
//     {
//         try
//         {
//             var email = await _context.Emails.FindAsync(id);
//             if (email == null)
//             {
//                 return NotFound(new { message = "Email not found" });
//             }
//
//             return Ok(email);
//         }
//         catch (Exception ex)
//         {
//             return BadRequest(new { error = ex.Message });
//         }
//     }
//
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteEmail(Guid id)
//     {
//         try
//         {
//             var email = await _context.Emails.FindAsync(id);
//             if (email == null)
//             {
//                 return NotFound(new { message = "Email not found" });
//             }
//
//             _context.Emails.Remove(email);
//             await _context.SaveChangesAsync();
//
//             return Ok(new { message = "Email deleted successfully" });
//         }
//         catch (Exception ex)
//         {
//             return BadRequest(new { error = ex.Message });
//         }
//     }
//
//     private static int GetImportanceScore(Importance? importance)
//     {
//         return importance switch
//         {
//             Importance.Low => 1,
//             Importance.Normal => 2,
//             Importance.High => 3,
//             _ => 2
//         };
//     }
// }