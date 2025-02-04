using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class OrderRepository(Context context) : BaseRepository<Order>(context);