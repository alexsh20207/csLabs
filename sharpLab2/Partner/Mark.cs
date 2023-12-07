using CardPickStrategy;
using sharpLab2;

public class Mark : IPartner
{
    public ICardPickStrategy Strategy { get; set; }
}