using System;
using System.Collections.Generic;
using System.Text;
using HResource.DAL.Companies;

namespace HResource.DAL.Common
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        int Complete();
    }
}
