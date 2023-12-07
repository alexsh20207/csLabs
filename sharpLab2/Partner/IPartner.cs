using CardPickStrategy;
namespace sharpLab2;
public interface IPartner
{
    public ICardPickStrategy Strategy { get; set; }
}