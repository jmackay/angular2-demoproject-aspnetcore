import {Pipe, PipeTransform} from "angular2/core";

// http://www.fueltravel.com/blog/migrating-from-angular-1-to-2-part-1-pipes/
// Uses:
//*ngFor="#x of array | orderBy"
//	* ngFor="#x of array | orderBy : '+'"
//		* ngFor="#x of array | orderBy : ['+']"

//Descending:

//*ngFor="#x of array | orderBy : '-'"
//	* ngFor="#x of array | orderBy : ['-']"

//Multi - Dimension Arrays by Single Property
//Ascending:

//*ngFor="#x of array | orderBy : 'propertyName'
//	* ngFor="#x of array | orderBy : ['propertyName']"

//Descending:

//*ngFor="#x of array | orderBy : '-propertyName'
//	* ngFor="#x of array | orderBy : ['-propertyName']"

//Multi - Dimension Arrays by Multiple Properties
//Just add '-' in front of property name if wanting descending sorting:

//*ngFor="#x of array | orderBy : ['-propertyName1', 'propertyName2', '-propertyName3']"

@Pipe({ name: "orderBy", pure: false })
export class OrderByPipe implements PipeTransform {
	transform(input: any, [config = '+']): any {
		if (!Array.isArray(input)) return input; // single item

		if (!Array.isArray(config) || Array.isArray(config) && config.length == 1) {
			var propertyToCheck: string = !Array.isArray(config) ? config : config[0];
			var descending = propertyToCheck.substr(0, 1) == "-";

			if (!propertyToCheck || propertyToCheck == "-" || propertyToCheck == "+") {
				return !descending ? input.sort() : input.sort().reverse();
			}

			var property: string = propertyToCheck.substr(0, 1) == "+" || propertyToCheck.substr(0, 1) == "-" ? propertyToCheck.substr(1) : propertyToCheck;

			return input.sort(function (a: any, b: any) {
				return !descending ? OrderByPipe._orderByComparator(a[property], b[property]) : -OrderByPipe._orderByComparator(a[property], b[property]);
			});
		} else {
			// loop over property of the array in order and sort each
			return input.sort(function (a: any, b: any) {
				for (var i = 0; i < config.length; i++) {
					var descending = config[i].substr(0, 1) == "-";

					var property = config[i].substr(0, 1) == "+" || config[i].substr(0, 1) == "-" ? config[i].substr(1) : config[i];

					var comparison = !descending ? OrderByPipe._orderByComparator(a[property], b[property]) : -OrderByPipe._orderByComparator(a[property], b[property]);

					// Keep 0 (equal) to compare next part
					if (comparison != 0) return comparison;
				}

				return 0;
			});
		}
	}

	static _orderByComparator(a: any, b: any): number {
		if ((isNaN(parseFloat(a)) || !isFinite(a)) || (isNaN(parseFloat(b)) || !isFinite(b))) {
			// Not a number
			if (a.toLowerCase() < b.toLowerCase()) return -1;
			if (a.toLowerCase() > b.toLowerCase()) return 1;
		} else {
			if (parseFloat(a) < parseFloat(b)) return -1;
			if (parseFloat(a) > parseFloat(b)) return 1;
		}
		return 0;
	}
}