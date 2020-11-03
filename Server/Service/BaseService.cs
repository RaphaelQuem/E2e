using Repository;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        public T GetById(int id)
        {
            return _repository.Select(id);
        }
        public List<T> Get()
        {
            return _repository.Select();
        }
        public bool Insert(T model)
        {
            return _repository.Insert(model);
        }
        public bool Update(T model)
        {
            return _repository.Update(model);
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

    }
}
