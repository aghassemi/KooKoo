using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooKoo.WebService.Data {

    public interface IReadonlyRepository<T> where T : class {

        IEnumerable<T> GetAll();
        T Get( Guid id );

    }
}
