using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bench
{
    public abstract class Bus<T, K> where T : IMessageBus
    {
        public Bus() { }

        protected IList<IInstructionBus> _listeDesInstructions;

        protected void chargerLaListeDesInstruction(Assembly assembly)
        {
            _listeDesInstructions = assembly
                .GetTypes()
                .Where(x => x
                    .GetInterfaces()
                    .Any(y => y == typeof(IInstructionBus)))
                .Select(x => (IInstructionBus)Activator.CreateInstance(x))
                .ToList();
        }

        public K exécuter(T message)
        {
            try
            {
                Type typeDuMessage = trouverLeTypeDuMessage(message);
                object instructionAssociéeAuMessage = _listeDesInstructions.Single(instruction => instruction.TypeDuMessage == typeDuMessage);
                string nomDeLaMéthodeDExécution = typeof(IInstructionBus<T, K>).GetMethods().FirstOrDefault().Name;
                MethodInfo methodInfo = instructionAssociéeAuMessage.GetType().GetMethod(nomDeLaMéthodeDExécution);
                object[] paramètres = new object[] { message };
                return (K)methodInfo.Invoke(instructionAssociéeAuMessage, paramètres);
            }
            catch (Exception e)
            {
                throw new BusException(e);
            }
        }

        protected Type trouverLeTypeDuMessage(IMessageBus message)
        {
            Type[] interfaces = message.GetType().GetInterfaces();
            return interfaces
                .FirstOrDefault(x => 
                    _listeDesInstructions.Any(instruction => 
                        instruction.TypeDuMessage == x ));
        }
    }
}
