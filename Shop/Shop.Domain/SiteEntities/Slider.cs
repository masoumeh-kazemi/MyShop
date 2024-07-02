using Common.Domain;

namespace Shop.Domain.SiteEntities;

public class Slider : BaseEntity
{
    public Slider(string title, string link, string imageName)
    {
        Title = title;
        Link = link;
        ImageName = imageName;
    }
    public string Title { get; private set; }
    public string Link { get; private set; }
    public string ImageName { get; private set; }
}