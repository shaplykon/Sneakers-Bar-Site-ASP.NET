using Microsoft.EntityFrameworkCore;
using Sneaker_Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Model
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext context;
        public CommentRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Comment> getCommentsByArticleId(int Id)
        {
            return context.Comments.Where(x => x.ArticleId == Id);
        }

        public int SaveComment(Comment comment)
        {
            if (comment.Id == default)
            {
                context.Entry(comment).State = EntityState.Added;
            }
            else
            {
                context.Entry(comment).State = EntityState.Modified;
            }
            context.SaveChanges();
            return comment.Id;
        }


        public void DeleteCommentById(Comment comment)
        {
            context.Comments.Remove(comment);
            context.SaveChanges();

        }
    }
}
