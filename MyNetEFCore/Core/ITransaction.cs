using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyNetEFCore
{
    /// <summary>
    /// 事务接口
    /// </summary>
    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();

        bool HasActiveTransaction { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);

        void RollbackTransaction();
    }
}
