using System.Collections.Generic;

namespace Angular2Demo.Web.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Username { get; set; }

		public virtual ICollection<Card> CreatedCards { get; set; }
		public virtual ICollection<CardAssignment> AssignedTo { get; set; }
		public virtual ICollection<CardAssignment> CreatedAssignments { get; set; }
	}
}