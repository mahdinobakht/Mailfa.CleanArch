using Ardalis.Specification;

namespace Mailfa.CleanArch.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
