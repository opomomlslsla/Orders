using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class CustomerRepository(Context context) : BaseRepository<Customer>(context);