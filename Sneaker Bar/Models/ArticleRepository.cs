using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Models
{
    public class ArticleRepository
    {
        private readonly ArticleContext context;
        public ArticleRepository(ArticleContext _context)
        {
            context = _context;
        }
        public IQueryable<Article> getArticles() {
            return context.Articles.OrderBy(x => x.date);
        }

        public int SaveArticle(Article article)
        {
            if (article.Id == default)
            {
                context.Entry(article).State = EntityState.Added;
            }
            else
            {
                context.Entry(article).State = EntityState.Modified;
            }
            context.SaveChanges();
            return article.Id;
        }

        public void DeleteSneakers(Article article)
        {
            context.Articles.Remove(article);
            context.SaveChanges();
        }
    }
}
