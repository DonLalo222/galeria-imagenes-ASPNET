using GaleriaTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleriaTestMVC.Repository
{
    interface ICommentariesRepository : ICrudRepository<Commentaries>
    {
        IEnumerable<Commentaries> GetAllByIdPost(int id);
    }
}
