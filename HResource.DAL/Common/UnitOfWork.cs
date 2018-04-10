using System;
using System.Collections.Generic;
using System.Text;
using HResource.DAL.Companies;

namespace HResource.DAL.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICompanyRepository Companies { get; }
        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
