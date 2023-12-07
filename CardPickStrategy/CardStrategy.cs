using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPickStrategy;

public class CardStrategy : ICardPickStrategy
{
    private int _pickCardName;
    public CardStrategy(int PickCardNum)
    {
        _pickCardName = PickCardNum;
    }
    public int Pick(Card[] cards)
    {
        return _pickCardName;
    }
}