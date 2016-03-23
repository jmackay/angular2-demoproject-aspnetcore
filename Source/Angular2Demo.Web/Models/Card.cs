using System;
using System.Collections.Generic;

namespace Angular2Demo.Web.Models
{
	public class Card
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int? PreviousCardId { get; set; }
		//public int? NextCardId { get; set; }
		public string Description { get; set; }
		public int StackId { get; set; }
		public int CategoryId { get; set; }
		public int CreatedById { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset Due { get; set; }
		public DateTimeOffset LastModified { get; set; }

		public virtual Card PreviousCard { get; set; }
		//public virtual Card NextCard { get; set; }
		public virtual Stack Stack { get; set; }
		public virtual Category Category { get; set; }
		public virtual User CreatedBy { get; set; }
		public virtual ICollection<CardAssignment> CardAssignments { get; set; }
	}
}