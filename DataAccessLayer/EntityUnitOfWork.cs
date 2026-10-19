using System;
using Microsoft.EntityFrameworkCore.Storage;
using PizzaModel;

namespace DataAccessLayer
{
    public class EntityUnitOfWork : BaseUnitOfWork 
    {
        private readonly PizzaDbContext _context;
        private IDbContextTransaction _transaction;

        public EntityUnitOfWork()
        {
            _context = new PizzaDbContext();
            _context.Database.EnsureCreated();

            Pizzas = new EntityRepository<Pizza>(_context);
            Discounts = new EntityRepository<SizeDiscount>(_context);
        }

        public override void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public override void Commit()
        {
            _context.SaveChanges();
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public override void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public override void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}