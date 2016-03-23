using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2Demo.Web.Controllers
{
	[Route("api/[controller]")]
	public class CardAssignmentsController : ApiControllerBase
	{

		public CardAssignmentsController(Angular2DemoContext db) : base(db)
		{
		}
		
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _db.CardAssignments.ToListAsync());
		}
	}
}
