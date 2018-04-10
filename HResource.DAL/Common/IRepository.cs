﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace HResource.DAL.Common
{
    public interface IRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
