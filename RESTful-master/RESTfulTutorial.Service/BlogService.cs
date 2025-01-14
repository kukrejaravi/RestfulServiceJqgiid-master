﻿namespace RESTfulTutorial.Service
{
    using System.Data.Entity;
    using System.Linq;

    using RESTfulTutorial.Data;

    public class BlogService : IBlogService
    {
        static BlogService()
        {
           // Database.SetInitializer(new BlogInitializer());
            Database.SetInitializer<BlogContext>(null);
        }

        public BlogPost[] GetBlogPosts()
        {
            using (BlogContext context = new BlogContext())
            {
                return context.BlogPosts.ToArray();
            }
        }

        public BlogPost GetBlogPost(string id)
        {
            int identifier;
            if (int.TryParse(id, out identifier))
            {
                using (BlogContext context = new BlogContext())
                {
                    return context.BlogPosts.FirstOrDefault(post => post.Id == identifier);
                }
            }

            return null;
        }

        public void CreateBlogPost(BlogPost post)
        {
            using (BlogContext context = new BlogContext())
            {
                context.BlogPosts.Add(post);
                context.SaveChanges();
            }
           
        }

        public void UpdateBlogPost(BlogPost post)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Entry(post).State = EntityState.Modified;
                context.SaveChanges();
            }
           
        }

        public void DeleteBlogPost(string id)
        {
            int identifier;
            if (int.TryParse(id, out identifier))
            {
                using (BlogContext context = new BlogContext())
                {
                    var entity = context.BlogPosts.FirstOrDefault(blogPost => blogPost.Id == identifier);
                    if (entity != null)
                    {
                        context.BlogPosts.Remove(entity);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
