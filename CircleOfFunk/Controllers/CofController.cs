using System;
using System.Web.Mvc;

namespace CircleOfFunk.Controllers
{
    public class CofController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            SetViewBagMenu();
            base.OnActionExecuted(filterContext);
        }

        public virtual void SetViewBagMenu()
        {
            throw new NotImplementedException("SetViewBagMenu has not been overridden");
        }
    }
}