using System.Linq.Expressions;

namespace DALQueryChain.Interfaces.QueryBuilder
{
    public partial interface IUpdatableSetterQueryChain<TEntity> where TEntity : class, IDbModelBase
	{
		public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, TV value);
		public IUpdatableSetterQueryChain<TEntity> Set<TV>(Expression<Func<TEntity, TV>> extract, Expression<Func<TV>> value);
	}
}
