using System;
using System.Transactions;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.DataAccess.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextProvider _dbContextProvider;

        private readonly  TransactionScope _transactionScope;

        private bool _commited;

        public UnitOfWork(IDbContextProvider dbContextProvider, bool autoDetectChanges)
        {
            _dbContextProvider = dbContextProvider;

            _transactionScope = null;
            _dbContextProvider.GetDbContext().Configuration.AutoDetectChangesEnabled = autoDetectChanges;
        }

        public UnitOfWork(IDbContextProvider dbContextProvider, IsolationLevel isolationLevel, bool autoDetectChanges)
        {
            _dbContextProvider = dbContextProvider;

            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel });
            _dbContextProvider.GetDbContext().Configuration.AutoDetectChangesEnabled = autoDetectChanges;
        }

        public void SaveChanges()
        {
            if (_commited)
            {
                throw new Exception("Transaction was commited already.");
            }

            _dbContextProvider.GetDbContext().Configuration.AutoDetectChangesEnabled = true;
            _dbContextProvider.GetDbContext().SaveChanges();
        }

        public void Commit()
        {
            SaveChanges();

            if (_transactionScope != null)
            {
                _transactionScope.Complete();
            }

            _commited = true;
        }

        public void Dispose()
        {
            _dbContextProvider.GetDbContext().Configuration.AutoDetectChangesEnabled = true;

            if (_transactionScope != null)
            {
                _transactionScope.Dispose();
            }
        }
    }
}