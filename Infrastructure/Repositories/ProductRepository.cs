using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class ProductRepository(Context context) : BaseRepository<Product>(context);