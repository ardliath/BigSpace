using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace
{
    public class OuterSpaceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OuterSpace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OuterSpace_default",
                "OuterSpace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}