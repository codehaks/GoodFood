using GoodFood.Domain.Entities;

namespace GoodFood.Domain;

public class Menu
{
    public IList<MenuLine> Lines { get;}

    public Menu(IList<MenuLine> lines)
    {
        Lines = lines;
    }

    public void Add(MenuLine line)
    {
        Lines.Add(line);
    }

    public void Remove(MenuLine line)
    {
        Lines.Remove(line);
    }
}
