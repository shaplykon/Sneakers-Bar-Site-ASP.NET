using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Models
{
    public class CommentRepository
    {
        private readonly CommentContext context;
        public CommentRepository(CommentContext _context) {
            context = _context;
        }

        public IQueryable<Comment> getCommentsByArticleId(int Id) {
            return context.Comments.Where(x => x.articleId == Id);
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

        public void DeleteSneakers(Comment comment)
        {
            context.Comments.Remove(comment);
            context.SaveChanges();
        }
    }
}
