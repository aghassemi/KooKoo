using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooKoo.WebService.Data {

    public interface IRepository<T> where T : class {

        IEnumerable<T> GetAll();
        void Save(T entity);
        void Delete(T entity);
        void DeleteAll();

    }
}
