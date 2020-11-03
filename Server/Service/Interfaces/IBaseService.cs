using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IBaseService<T>
    {
        List<T> Get();
        T GetById(int id);
        bool Insert(T model);
        bool Update(T model);
        bool Delete(int id);
    }
}
