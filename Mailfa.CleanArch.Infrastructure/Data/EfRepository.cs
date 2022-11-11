using Ardalis.Specification.EntityFrameworkCore;
using Mailfa.CleanArch.SharedKernel.Interfaces;

namespace Mailfa.CleanArch.Infrastructure.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
  }
}
