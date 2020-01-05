using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GaleriaTestMVC.Models;

namespace GaleriaTestMVC.Repository
{
    public class CommentariesRepositoryImpl : ICommentariesRepository
    {
        private readonly GaleriaTestDBEntities context;
        public CommentariesRepositoryImpl()
        {
            context = new GaleriaTestDBEntities();
        }
        public void Create(Commentaries t)
        {
            context.Commentaries.Add(t);
            context.SaveChanges();
        }

        public void Delete(Commentaries t)
        {
            throw new NotImplementedException();
        }

        public Commentaries FindById(int id)
        {
            return context.Commentaries.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Commentaries> GetAll()
        {
            return context.Commentaries.ToList();
        }

        public IEnumerable<Commentaries> GetAllByIdPost(int id)
        {
            return context.Commentaries.Where(c => c.PostId == id).ToList();
        }

        public void Update(Commentaries t)
        {
            throw new NotImplementedException();
        }
    }
}