﻿using System.ServiceModel;

namespace RESTfulTutorial.Service
{
    using System.ServiceModel.Web;

    using RESTfulTutorial.Data;

    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Posts")]
        BlogPost[] GetBlogPosts();

        [OperationContract]
        [WebGet(UriTemplate = "/Post/{id}")]
        BlogPost GetBlogPost(string id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreatePost")]
        void CreateBlogPost(BlogPost post);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/UpdatePost", RequestFormat = WebMessageFormat.Xml)]
        void UpdateBlogPost(BlogPost post);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/DeletePost/{id}")]
        void DeleteBlogPost(string id);
    }
}
