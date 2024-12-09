namespace Heris.Extensions.Serializers.NewtonSoft.Sample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewtonSoftController(IJsonSerializer jsonSerializer) : ControllerBase
{
    [HttpGet("Serilize")]
    public IActionResult Serilize([FromQuery] Person person) => Ok(jsonSerializer.Serialize(person));

    [HttpGet("Deserialize")]
    public IActionResult Deserialize([FromQuery] string input) => Ok(jsonSerializer.Deserialize<Person>(input));
}
