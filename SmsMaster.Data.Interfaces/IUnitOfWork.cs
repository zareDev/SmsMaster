using SmsMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmsMaster.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICountriesRepository Countries { get; }
        ISmsRepository Sms { get; }
        void Save();
    }
}
