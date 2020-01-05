using GaleriaTestMVC.Models;
using GaleriaTestMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriaTestMVC.Service
{
    public class CommentariesService
    {
        private readonly CommentariesRepositoryImpl repo;
        public CommentariesService()
        {
            repo = new CommentariesRepositoryImpl();
        }
        public void Create(Commentaries c)
        {
            repo.Create(c);
        }
        public IEnumerable<Commentaries> GetAllByIdPost(int id)
        {
            return repo.GetAllByIdPost(id);
        }

    }
}