using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using MyNetEFCore.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNetEFCore
{
    public class StudentContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<StudentContext, TRequest, TResponse>
    {
        public StudentContextTransactionBehavior(StudentContext dbContext, ILogger<StudentContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, logger)
        {
        }
    }
}
