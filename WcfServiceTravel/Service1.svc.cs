using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceTravel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        TravelEntities1 db = new TravelEntities1();
        
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

       

        public bool Post(Post post, Image image)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();

                Post currentPost = db.Posts.Where(x => x.title == post.title).SingleOrDefault();
                var sImage = new Image()
                {
                   
                    createdAt = image.createdAt,
                   
                    name = image.name,
                    url = image.url,
                    PostId = currentPost.id,
                    updatedAt = image.updatedAt
                };
                for (int i = 0; i < 2; i++)
                {
                    db.Images.Add(sImage);
                    db.SaveChanges();
                }
                
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Registor(Traveler traveler)
        {
            try
            {
                db.Travelers.Add(traveler);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<Traller> GetTravelers()
        {
            List<Traller> ls = new List<Traller>();
            List<Traveler> list = db.Travelers.ToList();
            foreach (var item in list)
            {
                Traller name = new Traller()
                {
                    firstName = item.firstName
                };
                ls.Add(name);
            }
            return ls;
        }

        public Traller Login(string email, string password)
        {
            try
            {
             var dataItem =    db.Travelers.Where(x => x.email == email && x.password == password).SingleOrDefault();
                if (dataItem!=null)
                {
                    var traveller = new Traller()
                    {
                        id = dataItem.id,
                        firstName = dataItem.firstName,
                        LastName = dataItem.lastName
                    };
                    return traveller;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Post1(string title, string content)
        {
            var post = new Post()
            {
                title = title,
                content = content
            };
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public List<Posted> GetPosts()
        {
            List<Posted> ls = new List<Posted>();
            var list = db.Posts.ToList();
            foreach (var item in list)
            {
                var posted = new Posted()
                {
                    Content = item.content,
                    id = item.id,
                    Title = item.title
                };
                ls.Add(posted);
            }
            return ls;
        }

        public List<Posted> Search(string name)
        {
            List<Posted> ls = new List<Posted>();
            var list = db.Posts.Where(x => x.title.Contains(name) || name == null).ToList();
            foreach (var item in list)
            {
                var posted = new Posted()
                {
                    Content = item.content,
                    id = item.id,
                    Title = item.title
                };
                ls.Add(posted);
            }
            return ls;
        }

        public List<ImagePost> GetImages()
        {
            List<ImagePost> ls = new List<ImagePost>();
            var list = db.Images.ToList();
            foreach (var item in list)
            {
                var posted = new ImagePost()
                {
                   Id = item.id,
                   Name = item.name,
                   Path = item.url
                  
                };
                ls.Add(posted);
            }
            return ls;
        }

        public bool ImagePost(ImagePost imagePost, int PostId)
        {
            try
            {
                var image = new Image()
                {
                    url = imagePost.Path,
                    name = imagePost.Name,
                    PostId = PostId

                };
                db.Images.Add(image);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public Posted CurrentPost(string name)
        {
            var currentPost = db.Posts.Where(x => x.title == name).SingleOrDefault();
            var currentPosted = new Posted()
            {
                Content = currentPost.content,
                id = currentPost.id,
                Title = currentPost.title
            };
            return currentPosted;
        }

      

        public Posted PostDetail(int id)
        {
            var post = db.Posts.Find(id);
            var posted = new Posted()
            {
                Content = post.content,
                id = post.id,
                Title = post.title,
                CreatedAt = post.createdAt
                

            };
            return posted;
        }

        public List<PostComment> GetComments()
        {
            List<PostComment> ls = new List<PostComment>();
            var list = db.Comments.ToList();
            foreach (var item in list)
            {
                var posted = new PostComment()
                {
                    Id = item.id,
                    comment1 = item.comment1,
                  
                   
                    postId = item.postId,
                    UserId = item.UserId

                };
                ls.Add(posted);
            }
            return ls;
        }

        public bool PostComment(PostComment postComment)
        {
            try
            {
                var comment = new Comment()
                {
                   comment1 = postComment.comment1,
                  
                   postId = postComment.postId,

                   UserId = postComment.UserId
                   
                   

                };
                db.Comments.Add(comment);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool createImage()
        {
            throw new NotImplementedException();
        }
    }
}
