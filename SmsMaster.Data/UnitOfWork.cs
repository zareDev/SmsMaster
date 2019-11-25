using SmsMaster.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmsMaster.Data
{
    public class UnitOfWork : IUnitOfWork, System.IDisposable
    {
        private readonly SmsMasterContext _context;
        private ISmsRepository _smsRepository;
        private ICountriesRepository _countriesRepository;

        public UnitOfWork(SmsMasterContext context)
        {
            _context = context;
        }

        public ICountriesRepository Countries
        {
            get { return _countriesRepository ?? (_countriesRepository = new CountriesRepository(_context)); }
        }

        public ISmsRepository Sms
        {
            get { return _smsRepository ?? (_smsRepository = new SmsRepository(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
