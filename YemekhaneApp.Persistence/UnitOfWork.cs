using Microsoft.EntityFrameworkCore.Storage;
using OnionArchitectureDemo.Application.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekhaneApp.Application.Interfaces;
using YemekhaneApp.Persistence.Context;

namespace YemekhaneApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async ValueTask DisposeAsync()
        {
            await appDbContext.DisposeAsync();
        }

        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(appDbContext);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await appDbContext.Database.BeginTransactionAsync();
        }
    }
}
