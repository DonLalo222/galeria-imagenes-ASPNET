using GaleriaTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriaTestMVC.Repository
{
    public class PostsRepositoryImpl : IPostsRepository
    {
        private readonly GaleriaTestDBEntities context;

        public PostsRepositoryImpl()
        {
            context = new GaleriaTestDBEntities();
        }

        public void Create(Posts t)
        {
            context.Posts.Add(t);
            context.SaveChanges();
        }

        public void Delete(Posts t)
        {
            context.Posts.Remove(t);
        }

        public Posts FindById(int id)
        {
            return context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Posts> GetAll()
        {
            return context.Posts.ToList();
        }

        public void Update(Posts t)
        {
            // no implementado.
        }
    }
}