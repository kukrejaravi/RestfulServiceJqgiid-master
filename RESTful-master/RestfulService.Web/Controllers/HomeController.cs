using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTfulTutorial.Data;
using System.Xml;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
namespace RestfulService.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        
        {
            return View();
        }

        public JsonResult GetGridData(int page, int rows)
        {            
            
                //page is page no, rows is no of rows in a page, is set in rownum MyGrid.js page
            int totalRows = 0, totalPages = 0;
                 ArrayOfBlogPost gridData = new ArrayOfBlogPost(); 
                 try
                 {
                      gridData = ConnectToService.GetBlogPosts();

                     if (gridData.Items != null && gridData.Items.Count() > 0)
                     {
                         int skip, take;
                         totalRows = gridData.Items.Count();
                         totalPages = (int)Math.Ceiling((float)totalRows / (float)rows);

                          skip = (page - 1) * rows > totalRows ? 0 : (page - 1) * rows;
                          take = totalRows - skip > 0 ? page == totalPages ? totalRows - skip : rows : 0;

                         gridData.Items = gridData.Items.Skip(skip).Take(take).ToArray();
                     }
                     
                 }
                 catch (Exception ex)
                 {                   
                     //Log error to DB
                 }
                 var jsonData = new
                 {
                     rows = gridData.Items,//Data
                     page,//Current page number
                     total = totalPages, //total number of pages          
                     records = totalRows, //Total records             
                 };

                 return Json(jsonData, JsonRequestBehavior.AllowGet);    
        }

        public string Edit(BlogPost objBlogPost)
        {            
            string result = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    result = ConnectToService.EditBlogPost(objBlogPost);
                    if (result == "OK")
                    {
                        return "Saved Successfully";                       
                    }
                }                                                 
 
            }
            catch (Exception ex)
            {                
               //Log error to DB
                 
            }
            return "Edit unsuccessful. Please try again.";
        }
        public string Delete(string Id)
        {
            try
            {
                string result = string.Empty;
                result = ConnectToService.DeleteBlogPost(Id);
                if (result == null ? false : (result.ToUpper() == "OK"))
                {
                    return "Deleted successfully";
                }               
            }
            catch (Exception ex)
            {
              //Log error to DB
                
            }
            return "Delete unsuccessful. Please try again.";
        }

        public string Create([Bind(Exclude = "Id")] BlogPost objBlogPost)
        {
            string result = string.Empty;;
            try
            {
                if (ModelState.IsValid)
                {
                  result=  ConnectToService.CreateBlogPosts(objBlogPost);
                     if (result == null ? false : (result.ToUpper() == "OK"))
                    {
                        return "Created successfully";
                    }       
                    //msg = "Saved Successfully";
                }               
            }
            catch (Exception ex)
            {
                 //Log error to DB
               // msg = "Error occured:" + ex.Message;
            }
            return "Create unsuccessful. Please try again.";
        }
    }
}
