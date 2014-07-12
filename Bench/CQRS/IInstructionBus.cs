using System;

namespace Bench
{
    public interface IInstructionBus<T, K> where T : IMessageBus
    {
        K exécuter(T message);
    }

    public interface IInstructionBus
    {
        Type TypeDuMessage { get; }
    }
}
