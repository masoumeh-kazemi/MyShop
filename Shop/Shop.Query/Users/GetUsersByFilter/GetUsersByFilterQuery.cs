using Common.Query;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetUsersByFilter;

public class GetUsersByFilterQuery : QueryFilter<UserFilterResult, UserFilterParam>
{
    public GetUsersByFilterQuery(UserFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetUsersByFilterQueryHandler : IQueryHandler<GetUsersByFilterQuery, UserFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetUsersByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }
    public async Task<UserFilterResult>  Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var result =  _shopContext.Users.OrderByDescending(f => f.Id).AsQueryable();

        if (@params.Email != null)
        {
            result = result.Where(f => f.Email.Contains(@params.Email));
        }


        if (@params.Id != null)
        {
            result = result.Where(f => f.Id == @params.Id);

        }

        if (@params.PhoneNumber != null)
        {
            result = result.Where(f=>f.PhoneNumber == @params.PhoneNumber);
        }
        var skip = (@params.PageId -1 ) * @params.Take;

        var model = new UserFilterResult()
        {
            Data = result.Skip(skip).Take(@params.Take)
                .Select(user => new UserFilterData()
                {
                    AvatarName = user.AvatarName,
                    CreationDate = user.CreationDate,
                    Id = user.Id,
                    Email = user.Email,
                    Family = user.Family,
                    Name = user.Name,
                    Gender = user.Gender,
                    PhoneNumber = user.PhoneNumber,
                }).ToList(),
            FilterParams = @params
        };

            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
    }
}
