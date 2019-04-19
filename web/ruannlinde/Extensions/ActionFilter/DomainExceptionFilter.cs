using System.Web.Mvc;
namespace Ruann.Linde.Extensions.ActionFilter {
	public class DomainExceptionFilter : ActionFilterAttribute {
		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			if (filterContext.Exception != null) {
				if (filterContext.Exception.GetType() == typeof(DomainException)) {
					filterContext.ExceptionHandled = true;

					var domainException = (filterContext.Exception as DomainException);

					if (domainException.AdditionalData != null) {
						filterContext.Result = new JsonResult {
							Data = new { filterContext.Exception.Message, domainException.AdditionalData }
						};
					}
					else {
						filterContext.Result = new JsonResult { Data = new { filterContext.Exception.Message } };
					}
				}
			}
			base.OnActionExecuted(filterContext);
		}
	}
}