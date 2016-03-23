using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angular2Demo.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Angular2Demo.Web.Controllers
{
	[Route("api/reset")]
	public class ResetController : ApiControllerBase
	{
		public ResetController(Angular2DemoContext db): base(db)
		{
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> ResetDatabase()
		{
			using (var transaction = await _db.Database.BeginTransactionAsync())
			{
				_db.Cards.RemoveRange(await _db.Cards.ToListAsync());
				_db.Categories.RemoveRange(await _db.Categories.ToListAsync());
				_db.Users.RemoveRange(await _db.Users.ToListAsync());
				_db.Stacks.RemoveRange(await _db.Stacks.ToListAsync());
				_db.CardAssignments.RemoveRange(await _db.CardAssignments.ToListAsync());

				await _db.SaveChangesAsync();

				var user1 = new User
				{
					Name = "John Mackay",
					Username = "jmackay"
				};
				var user2 = new User
				{
					Name = "Matthew Overall",
					Username = "moverall"
				};
				var user3 = new User
				{
					Name = "Peter Parker",
					Username = "pparker"
				};
				var user4 = new User
				{
					Name = "Sally Sue",
					Username = "ssue"
				};
				var user5 = new User()
				{
					Name = "Mary Jane",
					Username = "mjane"
				};

				var users = new List<User>
				{
					user1,
					user2,
					user3,
					user4,
					user5
				};

				var category1 = new Category
				{
					Name = "Regular",
					LabelColor = ""
				};

				var category2 = new Category
				{
					Name = "Important",
					LabelColor = "FF6666"
				};
				var category3 = new Category
				{
					Name = "Bug",
					LabelColor = "FFFF66"
				};


				var categories = new List<Category>
				{
					category1,
					category2,
					category3
				};

				var stack1 = new Stack
				{
					Name = "Backlog"
				};

				var stack2 = new Stack
				{
					Name = "In Progress",
					PreviousStackId = 1
				};

				var stack3 = new Stack
				{
					Name = "Completed",
					PreviousStackId = 2
				};

				var stacks = new List<Stack>
				{
					stack1,
					stack2,
					stack3
				};


				var card1 = new Card
				{
					Stack = stack1,
					Category = category1,
					CreatedBy = user1,
					Due = DateTimeOffset.Now.AddDays(4),
					Title = "Sample Card",
					Created = DateTimeOffset.Now.AddDays(-1),
					LastModified = DateTimeOffset.Now.AddDays(-1).AddMinutes(35),
					Description = "This is the description of the item"
				};

				var card2 = new Card
				{
					Stack = stack1,
					Category = category3,
					CreatedBy = user2,
					Due = DateTimeOffset.Now.AddDays(7),
					Title = "Sample 2",
					Created = DateTimeOffset.Now.AddHours(-5),
					LastModified = DateTimeOffset.Now.AddHours(-4),
					Description = "Some description here"
				};


				var card3 = new Card
				{
					Stack = stack2,
					Category = category2,
					CreatedBy = user1,
					Due = DateTimeOffset.Now.AddDays(2),
					Title = "Sample Important",
					Created = DateTimeOffset.Now.AddHours(-2),
					LastModified = DateTimeOffset.Now.AddHours(-1),
					Description = "Some description here"
				};

				var card4 = new Card()
				{
					Stack = stack1,
					Category = category1,
					CreatedBy = user4,
					Due = DateTimeOffset.Now.AddDays(3),
					Created = DateTimeOffset.Now.AddHours(-1),
					LastModified = DateTimeOffset.Now.AddMinutes(-45),
					Title = "Another Card",
					Description = "Another description",
				};

				var card5 = new Card
				{
					Stack = stack2,
					Category = category2,
					CreatedBy = user2,
					Due = DateTimeOffset.Now.AddHours(60),
					Created = DateTimeOffset.Now.AddHours(-4),
					LastModified = DateTimeOffset.Now.AddHours(-3),
					Title = "Yet another card",
					Description = "Do something very important today",
					PreviousCard = card3
				};

				var card6 = new Card
				{
					Stack = stack3,
					Category = category1,
					CreatedBy = user5,
					Due = DateTimeOffset.Now.AddDays(-4),
					Created = DateTimeOffset.Now.AddDays(-6),
					LastModified = DateTimeOffset.Now.AddDays(-4),
					Title = "Something we've finished",
					Description = "Probably should be named different...",
				};

				var cards = new List<Card>
				{
					card1,
					card2,
					card3,
					card4,
					card5,
					card6
				};

				var cardAssignment1 = new CardAssignment
				{
					AssignedTo = user2,
					CreatedBy = user1,
					Card = card1,
					Created = card1.LastModified
				};

				var cardAssignment2 = new CardAssignment
				{
					AssignedTo = user3,
					CreatedBy = user4,
					Card = card2,
					Created = card2.LastModified
				};

				var cardAssignment3 = new CardAssignment
				{
					AssignedTo = user4,
					CreatedBy = user4,
					Card = card3,
					Created = card3.LastModified
				};

				var cardAssignment4 = new CardAssignment
				{
					AssignedTo = user5,
					CreatedBy = user4,
					Card = card3,
					Created = card3.LastModified
				};

				var cardAssignment5 = new CardAssignment
				{
					AssignedTo = user1,
					CreatedBy = user1,
					Card = card4,
					Created = card4.LastModified
				};

				var cardAssignment6 = new CardAssignment
				{
					AssignedTo = user2,
					CreatedBy = user1,
					Card = card5,
					Created = card5.LastModified
				};

				var cardAssignment7 = new CardAssignment
				{
					AssignedTo = user3,
					CreatedBy = user1,
					Card = card5,
					Created = card5.LastModified
				};

				var cardAssignment8 = new CardAssignment
				{
					AssignedTo = user4,
					CreatedBy = user1,
					Card = card5,
					Created = card5.LastModified
				};

				var cardAssignment9 = new CardAssignment
				{
					AssignedTo = user5,
					CreatedBy = user1,
					Card = card5,
					Created = card5.LastModified
				};

				var cardAssignment10 = new CardAssignment
				{
					AssignedTo = user5,
					CreatedBy = user1,
					Card = card6,
					Created = card5.LastModified
				};

				var cardAssignments = new List<CardAssignment>
				{
					cardAssignment1,
					cardAssignment2,
					cardAssignment3,
					cardAssignment4,
					cardAssignment5,
					cardAssignment6,
					cardAssignment7,
					cardAssignment8,
					cardAssignment9,
					cardAssignment10
				};

				_db.Users.AddRange(users);
				_db.Categories.AddRange(categories);
				_db.Stacks.AddRange(stacks);
				_db.Cards.AddRange(cards);
				_db.CardAssignments.AddRange(cardAssignments);

				await _db.SaveChangesAsync();


				// Handle ordering relationships

				//stack1.NextStackId = stack2.Id;
				stack2.PreviousStackId = stack1.Id;

				//card1.NextCardId = card2.Id;
				card2.PreviousCardId = card1.Id;

				card4.PreviousCardId = card2.Id;

				await _db.SaveChangesAsync();

				transaction.Commit();
			}

			return Ok("Reset Database");
		}
	}
}