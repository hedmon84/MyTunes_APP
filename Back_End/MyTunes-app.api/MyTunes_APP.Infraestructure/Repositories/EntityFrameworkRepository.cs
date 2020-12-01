using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyTunes_APP.Infraestructure;
using System.Linq;
using MyTunes_app.Core.Interfaces;

namespace MyTunes_APP.Infraestructure.Repositories
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class 
    {

        private readonly MyTunes_appDbContext _myTunes_appDbContext;
        public EntityFrameworkRepository(MyTunes_appDbContext MyItunesAppDbContext)
        {
            _myTunes_appDbContext = MyItunesAppDbContext;

        }

        public T Actualizar(T entity)
        {

            T update = _myTunes_appDbContext.Update(entity).Entity;
            _myTunes_appDbContext.SaveChanges();
            return update;
        }

        public T Addalabase(T entity)
        {
            T add = _myTunes_appDbContext.Add(entity).Entity;
            _myTunes_appDbContext.SaveChanges();

            return add;
        }


        public int Count()
        {
            return _myTunes_appDbContext.Set<T>().Count();
        }
        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _myTunes_appDbContext.Set<T>().Where(predicate);
        }

        public T FilterOne(Func<T, bool> predicate)
        {
            return _myTunes_appDbContext.Set<T>().FirstOrDefault(predicate);
        }
        public IEnumerable<T> GetAllAlbumWithSogs()
        {

            return (IEnumerable<T>)_myTunes_appDbContext.Albums.Include(x => x.Msongs);
        }

        public IQueryable<int> Selectuno(Expression<Func<T, int>> selector)
        {
            return _myTunes_appDbContext.Set<T>().Select(selector);
        }


        public double Sumartodos(Expression<Func<T, decimal>> selector)
        {
            return (double)_myTunes_appDbContext.Set<T>().Sum(selector);
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {


            return _myTunes_appDbContext.Set<T>().ToList();
        }
    }





}
