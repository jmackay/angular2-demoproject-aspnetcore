using System.Collections.Generic;

namespace Angular2Demo.Web.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LabelColor { get; set; }

		public virtual ICollection<Card> Cards { get; set; }
	}
}