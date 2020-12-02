using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyTunes_app.Core.Interfaces
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();

        public IEnumerable<T> GetAllAlbumWithSogs();
        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        public T FilterOne(Func<T, bool> predicate);

        public IQueryable<int> Selectuno(Expression<Func<T, int>> selector);

        public int Count();

        public double Sumartodos(Expression<Func<T, decimal>> selector);

        public T Addalabase(T entity);

        public T Actualizar(T entity);
    }
}
