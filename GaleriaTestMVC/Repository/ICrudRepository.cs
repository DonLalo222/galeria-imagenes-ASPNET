using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaTestMVC.Repository
{
    interface ICrudRepository<T>
    {
        IEnumerable<T> GetAll();
        T FindById(int id);
        void Delete(T t);
        void Update(T t);
        void Create(T t);
    }
}
