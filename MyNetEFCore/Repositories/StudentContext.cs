using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MyNetEFCore
{
    public class StudentContext : EFContext
    {
        public StudentContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
