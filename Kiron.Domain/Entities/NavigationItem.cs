namespace Kiron.Domain.Entities;
public class NavigationItem
{
    public int ID { get; set; }
    public string Text { get; set; }
    public int ParentID { get; set; }
    public List<NavigationItem> Children { get; set; }
}
