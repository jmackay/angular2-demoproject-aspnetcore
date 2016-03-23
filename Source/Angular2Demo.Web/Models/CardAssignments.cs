using System;

namespace Angular2Demo.Web.Models
{
	public class CardAssignment
	{
		public int Id { get; set; }
		public int CardId { get; set; }
		public int AssignedToId { get; set; }
		public int CreatedById { get; set; }
		public DateTimeOffset Created { get; set; }

		public virtual Card Card { get; set; }
		public virtual User AssignedTo { get; set; }
		public virtual User CreatedBy { get; set; }
	}
}