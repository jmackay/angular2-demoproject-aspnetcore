using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;

namespace Angular2Demo.Web.Controllers
{
	public class ApiControllerBase : Controller
	{
		protected Angular2DemoContext _db;

		public ApiControllerBase(Angular2DemoContext db)
		{
			_db = db;
		}
	}
}