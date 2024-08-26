using System.Security.AccessControl;
using Common.Domain;
using Shop.Domain.SiteEntities.Enum;

namespace Shop.Domain.SiteEntities;

public class Banner : BaseEntity
{
    public string Link { get; private set; }
    public string ImageName { get; private set; }
    public BannerPosition Position { get; private set; }

    public Banner(string link, string imageName, BannerPosition position)
    {
        //Guard(link, imageName);
        Link = link;
        ImageName = imageName;
        Position = position;
    }

    public void Edit(string link,BannerPosition position)
    {
        Link = link;
        Position = position;
    }

    public void SetImageName(string imageName)
    {
        ImageName = imageName;
    }
}