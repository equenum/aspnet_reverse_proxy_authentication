using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MediaService.Api;

[ApiController]
[Route("api/public/[controller]")]
public class UserPreferencesController : ControllerBase
{
    [HttpGet("lang")]
    public async Task<IActionResult> GetLanguageAsync()
    {
        Console.WriteLine("Call received!");
        
        await Task.Delay(1000);
        return Ok("English");
    }
}
