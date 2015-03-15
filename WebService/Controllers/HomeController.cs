using System;
using System.Web.Mvc;
using MongoDB.Bson;
using TraficService;


namespace WebService.Controllers
{
    public class HomeController : Controller
    {
        static Cops Cops { get { return new Cops(); } }

        [HttpGet]
        [AllowCrossSiteJson]
        public JsonResult Index(double? lat, double? lng)
        {
            if (lat != null && lng != null)
            {
                try
                {
                    var cops = Cops.GetInsideRadiusFromLocation((double)lat, (double)lng, 5, Cops.Get());

                    Response.StatusCode = 200;
                    return Json(Cops, JsonRequestBehavior.AllowGet);
                }
                catch (Exception exception)
                {
                    Response.StatusCode = 500;
                    Response.Write("Server error, try again later...\n" + exception);
                    return null;
                }
            }
            else
            {
                Response.StatusCode = 400;
                Response.Write("Latitude and/or longitude missing!");
                return null;
            }
        }

        [HttpPost]
        public void Add(double? lat, double? lng)
        {
            if (lat != null && lng != null)
            {
                Cops.Add(new Location() {Latitude = (double)lat, Longitude = (double)lng, Date = DateTime.Now });
                Response.StatusCode = 200;
            }
            else
            {
                Response.StatusCode = 400;
                Response.Write("Latitude and/or longitude missing!");
            }
        }

        [HttpPost]
        public void Del(string id)
        {
            try
            {
                Cops.Remove(ObjectId.Parse(id));
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                Response.Write(e);
            }
        }
    }

    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }
}