using Microsoft.EntityFrameworkCore.Storage;
using OnionArchitectureDemo.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Domain.BaseEntities;

namespace YemekhaneApp.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : EntityBase, new();
        Task<int> SaveAsync();
        int Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
