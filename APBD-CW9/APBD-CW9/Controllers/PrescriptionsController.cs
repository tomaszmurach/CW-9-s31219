using APBD_CW9.DTOs;
using APBD_CW9.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW9.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDTO dto)
    {
        try
        {
            await service.AddPrescriptionAsync(dto);
            return CreatedAtAction(nameof(AddPrescription), null);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}