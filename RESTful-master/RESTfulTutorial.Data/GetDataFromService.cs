using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
namespace RESTfulTutorial.Data
{
    public static class ConnectToService
    {
        public static ArrayOfBlogPost GetBlogPosts()
        {
            ArrayOfBlogPost arrayOfBlogPost = new ArrayOfBlogPost();

            try
            {
                //Create request
                string sURL = "http://localhost/RestfulTutorialService/Service.svc/Posts";
                var request = (HttpWebRequest)WebRequest.Create(sURL);

                //Get response
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string serviceResponse = reader.ReadToEnd();

                //Deserialize response
                 arrayOfBlogPost = FromXml<ArrayOfBlogPost>(serviceResponse);

                
            }
            catch (Exception ex)
            {
                //Log Exception
         
            }
            return arrayOfBlogPost;
        }       

        public static string EditBlogPost(BlogPost objBlogPost)
        {
            string responseStatus = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://localhost/RestfulTutorialService/Service.svc/UpdatePost");
                request.Method = "PUT";
                request.ContentType = "application/xml";
                if (objBlogPost != null)
                {
                    Stream dataStream = request.GetRequestStream();
                    var ser = new DataContractSerializer(objBlogPost.GetType());
                    ser.WriteObject(dataStream, objBlogPost);
                    dataStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseStatus = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                //Log error to DB
            }

            return responseStatus;
        }
        public static string DeleteBlogPost(string id)
        {
            string responseStatus = string.Empty;

            try
            {
                string url = "http://localhost/RestfulTutorialService/Service.svc/DeletePost/" + id;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "DELETE";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseStatus = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                //Log error to DB
            }

             return responseStatus;
        }

        public static string CreateBlogPosts(BlogPost objBlogPost)
        {
            string responseStatus = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://localhost/RestfulTutorialService/Service.svc/CreatePost");
                request.Method = "POST";
                request.ContentType = "application/xml";
                if (objBlogPost != null)
                {
                    Stream dataStream = request.GetRequestStream();
                    var ser = new DataContractSerializer(objBlogPost.GetType());
                    ser.WriteObject(dataStream, objBlogPost);
                    dataStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseStatus = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                //Log error to DB               
            }

            return responseStatus;
        }

        public static T FromXml<T>(string xml)
        {
            T returnedXmlClass = default(T);

            try
            {
                using (TextReader reader = new StringReader(xml))
                {
                    try
                    {
                        var x = new XmlSerializer(typeof(ArrayOfBlogPost));
                        returnedXmlClass =
                            (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                    catch (InvalidOperationException)
                    {
                        //Log exception
                        // String passed is not XML, simply return defaultXmlClass
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception
            }

            return returnedXmlClass;
        }
    }
}
