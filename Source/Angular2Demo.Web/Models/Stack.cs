using System.Collections.Generic;

namespace Angular2Demo.Web.Models
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Stack
    {
	    public int Id { get; set; }
	    public string Name { get; set; }
		public int? PreviousStackId { get; set; }
		//public int? NextStackId { get; set; }


		//[ForeignKey("PreviousStackId")]
		public virtual Stack PreviousStack { get; set; }
		//public virtual Stack NextStack { get; set; }

		public virtual ICollection<Card> Cards { get; set; }
    }
}