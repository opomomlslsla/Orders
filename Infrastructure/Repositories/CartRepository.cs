using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class CartRepository(Context context) : BaseRepository<Cart>(context);