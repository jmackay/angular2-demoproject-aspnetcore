using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Angular2Demo.Web.Controllers
{
	[Route("api/[controller]")]
	public class CategoriesController : ApiControllerBase
	{
		public CategoriesController(Angular2DemoContext db) : base(db)
		{
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> GetCategory()
		{
			return Ok(await _db.Categories.ToListAsync());
		}

		[HttpGet]
		[Route("{id}", Name = "GetCategory")]
		public async Task<IActionResult> GetCategory(int id)
		{
			return Ok(await _db.Categories.FirstOrDefaultAsync(x => x.Id == id));
		}

		[HttpPost]
		[Route("", Name = "AddCategory")]
		public async Task<IActionResult> AddCategory(Category category)
		{
			_db.Categories.Add(category);

			await _db.SaveChangesAsync();
			return CreatedAtRoute("GetCategory", new {id = category.Id});
		}
	}
}