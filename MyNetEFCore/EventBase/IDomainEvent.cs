using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MyNetEFCore
{
    /// <summary>
    /// 领域事件
    /// 这里使用MediatR插件进行事件的发送\发布
    /// MediatR是一种进程内消息传递机制
    /// 参考:https://www.jianshu.com/p/583bcba352ec
    /// </summary>
    public interface IDomainEvent : INotification
    {
    }
}
