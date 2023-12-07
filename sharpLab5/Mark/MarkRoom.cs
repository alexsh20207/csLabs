using Microsoft.AspNetCore.Mvc;
using CardPickStrategy;
using sharpLab2;

namespace MarkRoom;

[ApiController]
[Route("[controller]")]
public class MarkRoomController : ControllerBase
{
    [HttpPost]
    public IActionResult Play([FromBody] Card[] cards)
    {
        IPartner mark = new Mark();
        ICardPickStrategy strategy = new FirstCardStrategy();

        mark.Strategy = strategy;
        int num = mark.Strategy.Pick(cards);
        return Ok(num);
    }
}