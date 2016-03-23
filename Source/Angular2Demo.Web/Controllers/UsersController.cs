using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular2Demo.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiControllerBase
    {
	    public UsersController(Angular2DemoContext db) : base(db)
		{ 
	    }
		
		[HttpGet]
		[Route("", Name = "GetUsers")]
		public async Task<IActionResult> GetUsers()
		{
			return Ok(await _db.Users.ToListAsync());
		}

	    [HttpGet]
	    [Route("{id}", Name = "GetUser")]
	    public async Task<IActionResult> GetUser(int id)
	    {
		    return Ok(await _db.Users.FirstOrDefaultAsync(x => x.Id == id)); // .Include(x => x.OwnedCards).Include(x => x.CreatedCards)
		}

	    [HttpGet]
	    [Route("{id}/cards/created", Name = "CreatedCards")]
	    public async Task<IActionResult> GetCreatedCards(int id)
	    {
		    return Ok(await _db.Cards.Where(x => x.CreatedById == id).ToListAsync());
	    }

	    [HttpGet]
	    [Route("{id}/cards/assigned", Name = "AssignedCards")]
	    public async Task<IActionResult> GetAssignedCards(int id)
	    {
		    var assignments = await _db.CardAssignments.Where(x => x.AssignedToId == id).Select(x => x.CardId).Distinct().ToListAsync();

		    var results = await _db.Cards.Where(x => assignments.Contains(x.Id)).ToListAsync();

			return Ok(results);
	    }
	}
}
