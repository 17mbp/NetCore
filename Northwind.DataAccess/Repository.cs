using Dapper.Contrib.Extensions;
using Northwind.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Northwind.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected string _cnnstring;
        public Repository(string cnnstring)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{ type.Name }"; };
            _cnnstring = cnnstring;
        }

        public bool Delete(T entity)
        {
            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.Delete(entity);
            }
        }

        public T GetById(int id)
        {
            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.Get<T>(id);
            }
        }

        public IEnumerable<T> GetList()
        {
            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.GetAll<T>();
            }
        }

        public int Insert(T entity)
        {
            using (var conn = new SqlConnection(_cnnstring))
            {
                return (int)conn.Insert(entity);
            }
        }

        public bool Update(T entity)
        {
            using (var conn = new SqlConnection(_cnnstring))
            {
                return conn.Update<T>(entity);
            }
        }
    }
}