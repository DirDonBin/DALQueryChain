using DALQueryChain.Enums;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public interface IChainSettings<T>
    {
        public T WithoutTriggers(TriggerType trigger = TriggerType.All);
    }
}
