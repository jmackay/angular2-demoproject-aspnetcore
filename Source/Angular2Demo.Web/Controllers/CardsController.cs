using System;
using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Angular2Demo.Web.Controllers
{
	[Route("api/[controller]")]
	public class CardsController : ApiControllerBase
	{
		public CardsController(Angular2DemoContext db) : base(db)
		{
		}

		[HttpGet(Name = "GetCards")]
		public async Task<IActionResult> GetCards()
		{
			return Ok(await _db.Cards.ToListAsync());
		}

		[HttpGet("{id}", Name = "GetCard")]
		public async Task<IActionResult> GetCard(int id)
		{
			return Ok(await _db.Cards.FirstOrDefaultAsync(x => x.Id == id));
		}

		[HttpPost(Name = "AddCard")]
		public async Task<IActionResult> AddCard([FromBody]Card card)
		{
			_db.Cards.Add(card);
			card.LastModified = DateTimeOffset.Now;
			card.Created = DateTimeOffset.Now;
			
			await _db.SaveChangesAsync();
			
			return CreatedAtRoute("GetCard", new { id = card.Id }, card);
		}

		[HttpPost, HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> UpdateCard(int id, [FromBody]Card card)
		{
			if (card == null || card.Id != id)
			{
				return HttpBadRequest();
			}

			var existingCard = await _db.Cards.FirstOrDefaultAsync(x => x.Id == id);



			if (existingCard == null)
			{
				return HttpNotFound();
			}

			if (existingCard.PreviousCardId != card.PreviousCardId)
			{
				// Update existing Ids

				/*
				a -> b -> c -> d
				
				c -> a -> b -> d // Moving c to front

				we send in c.prev = null
				we get existing c
				we query where prev = existing.id, get "d"
				we update d.prev = existing.prev "b"
				we query where prev = c.prev, get "a"
				we update a.prev = c
				we update existing.prev = c.prev "null"

				a -> c -> b - > d // Moving c before b

				we send in c.prev = a (from b.prev in client)
				we get existing c
				we query where prev = existing.id, get "d"
				we update d.prev = existing.prev "b"
				we query where prev = c.prev, get "b"
				we update b.prev = c
				we update existing.prev = c.prev "a"
				
				a -> b -> d -> c // Moving c to end
				
				we send in c.prev = d (from client)
				we get existing c
				we query where prev = existing.id, get "d"
				we update b.prev = existing.prev "b"
				we query where prev = c.prev get "null"
				we skip that
				we update existing.prev = c.prev "d"


				a -> c -> b // Moving c from end to middle
				
				we send in c.prev = "a" from b.prev in client
				we get existing c
				we query where prev = existing.id, get "null"
				we skip that
				we query where prev = c.prev get "b"
				we update b.prev = c
				we update existing.prev = c.prev "a"
				
				*/

				//
				// Limit calls to the new correct Stack Id, should help performance searching indexes
				//
	
				// Update any card that has this card as its previous to what this card's previous was previously set to <-- Say that 5 times fast!
				var nextCard = await _db.Cards.FirstOrDefaultAsync(x => x.StackId == existingCard.StackId && x.PreviousCardId == existingCard.Id);
				if (nextCard != null)
				{
					nextCard.PreviousCardId = existingCard.PreviousCardId;
				}

				// Find the existing card that references what we are now referencing, set it to our card
				var leapedCard = await _db.Cards.FirstOrDefaultAsync(x => x.StackId == card.StackId && x.PreviousCardId == card.PreviousCardId);
				if (leapedCard != null)
				{
					leapedCard.PreviousCardId = card.Id;
				}

				existingCard.PreviousCardId = card.PreviousCardId;
			}

			existingCard.Title = card.Title;
			existingCard.Description = card.Description;
			existingCard.CategoryId = card.CategoryId;
			//existingCard.NextCardId = card.NextCardId;
			existingCard.PreviousCardId = card.PreviousCardId;
			existingCard.StackId = card.StackId;
			existingCard.Due = card.Due;
			
			existingCard.LastModified = DateTimeOffset.Now;

			await _db.SaveChangesAsync();

			return Ok(existingCard);
		}

		[HttpGet("{cardId}/assign/{assignId}", Name = "GetCardAssignment")]
		
		public async Task<IActionResult> GetAssignment(int cardId, int assignId)
		{
			var assignment = await _db.CardAssignments.FirstOrDefaultAsync(x => x.Id == assignId && x.CardId == cardId);

			if (assignment == null)
			{
				return HttpNotFound();
			}

			return Ok(assignment);
		}

		[HttpPost("{id}/assign")]
		public async Task<IActionResult> AddAssignment(int id, CardAssignment cardAssignment)
		{
			if (cardAssignment == null || cardAssignment.CardId != id)
			{
				return HttpBadRequest();
			}

			_db.CardAssignments.Add(cardAssignment);

			await _db.SaveChangesAsync();

			return CreatedAtRoute("GetCardAssignment", new {cardId = cardAssignment.CardId, assignId = cardAssignment.Id});
		}
	}
}