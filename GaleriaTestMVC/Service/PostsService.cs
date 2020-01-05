using GaleriaTestMVC.Models;
using GaleriaTestMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriaTestMVC.Service
{
    public class PostsService
    {
        private readonly PostsRepositoryImpl repo;
        public PostsService()
        {
            repo = new PostsRepositoryImpl();
        }
        public void Create(Posts p)
        {
            repo.Create(p);
        }
        public void Delete(Posts p)
        {
            repo.Delete(p);
        }
        public Posts FindById(int id)
        {
            return repo.FindById(id);
        }
        public IEnumerable<Posts> GetAll()
        {
            return repo.GetAll();
        }


    }
}