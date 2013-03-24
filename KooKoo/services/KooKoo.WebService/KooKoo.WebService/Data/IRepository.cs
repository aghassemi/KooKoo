﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooKoo.WebService.Data {

    public interface IRepository<T> : IReadonlyRepository<T> where T : class  {

        void Save(T entity);
        void Delete(T entity);
        void DeleteAll();

    }
}
