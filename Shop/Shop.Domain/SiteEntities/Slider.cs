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

    public void Edit(string title, string link)
    {
        Title = title;
        Link = link;
    }

    public void SetImageName(string imageName)
    {
        ImageName = imageName;
    }
}