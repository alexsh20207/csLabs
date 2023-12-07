using Microsoft.AspNetCore.Mvc;
using sharpLab2;
using CardPickStrategy;

namespace ElonRoom;

[ApiController]
[Route("[controller]")]
public class ElonRoomController : ControllerBase
{
    [HttpPost]
    public IActionResult Play([FromBody] Card[] cards)
    {
        IPartner elon = new Elon();
        ICardPickStrategy strategy = new FirstCardStrategy();

        elon.Strategy = strategy;
        int num = elon.Strategy.Pick(cards);
        return Ok(num);
    }
}