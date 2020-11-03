using Infra;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Repository
{
    public interface IBaseRepository<T>
    {
        List<T> Select();
        T Select(int id);
        bool Update(T model);
        bool Insert(T model);
        bool Delete(int id);
    }
}
