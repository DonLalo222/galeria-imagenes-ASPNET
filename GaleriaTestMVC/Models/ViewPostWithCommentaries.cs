using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriaTestMVC.Models
{
    public class ViewPostWithCommentaries
    {
        public Posts Post { get; set; }
        public IEnumerable<Commentaries> Commentaries { get; set; }
    }
}