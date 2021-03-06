using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyNetEFCore
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
