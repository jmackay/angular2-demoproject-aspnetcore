using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System.Linq;

namespace Angular2Demo.Web.Controllers
{
	[Route("api/[controller]")]
	public class StacksController : ApiControllerBase
	{
		public StacksController(Angular2DemoContext db) : base(db) { }

		[HttpGet]
		[Route("", Name = "GetStacks")]
		public async Task<IActionResult> GetStacks()
		{
			return Ok(await _db.Stacks.ToListAsync());
		}

		[HttpGet]
		[Route("{id}", Name = "GetStack")]
		public async Task<IActionResult> GetStack(int id)
		{
			return Ok(await _db.Stacks.FirstOrDefaultAsync(x => x.Id == id));
		}

		[HttpPost]
		[Route("", Name = "AddStack")]
		public async Task<IActionResult> AddStack(Stack stack)
		{
			_db.Stacks.Add(stack);

			await _db.SaveChangesAsync();

			return CreatedAtRoute("GetStack", new { id = stack.Id });
		}

		[HttpPost, HttpPut]
		[Route("{id}", Name = "UpdateStack")]
		public async Task<IActionResult> UpdateStack(int id, Stack stack)
		{
			if (stack == null || stack.Id != id)
			{
				return HttpBadRequest();
			}

			var existingStack = await _db.Stacks.FirstOrDefaultAsync(x => x.Id == id);

			if (existingStack == null)
			{
				return HttpNotFound();
			}

			existingStack.Name = stack.Name;
			//existingStack.NextStackId = stack.NextStackId;
			existingStack.PreviousStackId = stack.PreviousStackId;

			await _db.SaveChangesAsync();

			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStack(int id)
		{
			var stack = await _db.Stacks.FirstOrDefaultAsync(x => x.Id == id);

			if (stack == null)
			{
				return new NoContentResult();
			}

			_db.Stacks.Remove(stack);

			await _db.SaveChangesAsync();

			return new NoContentResult();
		}

		[HttpGet("{id}/cards", Name="GetStackCards")]
		public async Task<IActionResult> GetStackCards(int id)
		{
			var cards = await _db.Cards.Where(x => x.StackId == id).ToListAsync();

			return Ok(cards);
		}
	}
}