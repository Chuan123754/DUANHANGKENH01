using appAPI.IRepository;
using appAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactReponsetory _repon;
        private readonly HttpClient _httpClient;
        private const string TelegramBotToken = "7222454306:AAFQqAQRTnkCnb62B_xPIppcHClOmkRvkak"; // Thay bằng API token của bạn
        private const string TelegramChatId = "5683379490"; // Thay bằng chat ID bạn muốn gửi thông báo

        public ContactController(IContactReponsetory contactReponsetory, HttpClient httpClient)
        {
            _repon = contactReponsetory;
            _httpClient = httpClient;
        }

        // GET: api/<SizeController>
        [HttpGet("GetAllContact")]
        public async Task<List<Contact>> GetAll()
        {
            return await _repon.GetAll();
        }
        [HttpGet("GetByIdContact")]

        public async Task<Contact> GetById(long id)
        {
            return await _repon.GetById(id);
        }

        [HttpPost("CreateContact")]
        public async Task<IActionResult> Create(Contact contact)
        {
            // Lưu vào database
            await _repon.Create(contact);

            // Xử lý các trường null thành "N/A"
            var fullName = contact.FullName ?? "N/A";
            var email = contact.Email ?? "N/A";
            var phone = contact.Phone ?? "N/A";
            var message = contact.Message ?? "N/A";
            var createdAt = contact.CreatedAt.HasValue
                ? contact.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss")
                : "N/A";

            // Tạo thông điệp gửi tới Telegram
            var telegramMessage = $"Bạn có một liên hệ mới:\n" +
                                  $"Name: {fullName}\n" +
                                  $"Email: {email}\n" +
                                  $"Phone: {phone}\n" +
                                  $"Message: {message}\n" +
                                  $"Thời gian gửi: {createdAt}";

            // Gửi thông báo tới Telegram
            var result = await SendTelegramMessage(telegramMessage);

            if (!result)
            {
                return StatusCode(500, "Failed to send Telegram notification.");
            }

            return Ok("Contact created and notification sent.");
        }

        private async Task<bool> SendTelegramMessage(string message)
        {
            try
            {
                var url = $"https://api.telegram.org/bot{TelegramBotToken}/sendMessage";
                var payload = new
                {
                    chat_id = TelegramChatId, // Chat ID của người nhận tin nhắn, có thể là chat ID nhóm hoặc cá nhân
                    text = message
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete("DeleteContact")]
        public async Task Delete(long id)
        {
            await _repon.Delete(id);
        }
        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByType([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var list = await _repon.GetByTypeAsync(pageNumber, pageSize, searchTerm);


            return Ok(list);
        }
        [HttpGet("Get-Total-Count")]
        public async Task<IActionResult> GetTotalCount([FromQuery] string? searchTerm = null)
        {
            var totalCount = await _repon.GetTotalCountAsync(searchTerm);
            return Ok(totalCount);
        }
    }
}
