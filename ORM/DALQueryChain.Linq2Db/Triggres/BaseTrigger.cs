using DALQueryChain.Interfaces;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.SqlQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DALQueryChain.Linq2Db.Triggres
{
    public partial class BaseTrigger<TContext, TEntity>
        where TContext : DataConnection
        where TEntity : class, IDbModelBase
    {
        private IEnumerable<TEntity>? _entities = null;
        private IQueryable<TEntity>? _query = null;
        private readonly DataConnection _context;

        public BaseTrigger(DataConnection context)
        {
            _context = context;
        }

        private void ClearData()
        {
            _entities = null;
            _query = null;
        }

        #region Init
        internal void InitTriggers(TEntity entity)
        {
            ClearData();
            _entities = new List<TEntity>() { entity };
        }

        internal void InitTriggers(IEnumerable<TEntity> entities)
        {
            ClearData();
            _entities = entities;
        }

        internal void InitTriggers(IQueryable<TEntity> query)
        {
            ClearData();
            _query = query;
        }

        internal void InitTriggers(Expression<Func<TEntity, bool>> predicate)
        {
            ClearData();
            _query = _context.GetTable<TEntity>().Where(predicate);
        }

        #endregion

        #region Get Data

        protected async Task<IEnumerable<TEntity>?> GetTriggerData(CancellationToken ctn = default)
        {
            _entities ??= (_query is null ? null : await _query.ToListAsync(ctn));
            return _entities;
        }

        #endregion

        #region Events

        protected internal virtual Task OnBeforeInsert(CancellationToken ctn = default) => Task.CompletedTask;

        protected internal virtual Task OnAfterInsert(CancellationToken ctn = default) => Task.CompletedTask;

        protected internal virtual Task OnBeforeUpdate(CancellationToken ctn = default) => Task.CompletedTask;

        protected internal virtual Task OnAfterUpdate(CancellationToken ctn = default) => Task.CompletedTask;

        protected internal virtual Task OnBeforeDelete(CancellationToken ctn = default) => Task.CompletedTask;

        protected internal virtual Task OnAfterDelete(CancellationToken ctn = default) => Task.CompletedTask;

        #endregion
    }
}
