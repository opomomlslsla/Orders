using Application.DTO;
using Domain.Entities;
using Mapster;

namespace Application.MapsterConfiguration;

public class MapsterConfiguration
{
    public MapsterConfiguration()
    {
        TypeAdapterConfig<Order, OrderDTO>.NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString());
        TypeAdapterConfig<User, UserDTO>.NewConfig()
            .Map(dest => dest.Role, src => src.Role.ToString());
    }
}