import { Pipe } from "angular2/core";


@Pipe({
	name: 'dateConvert'
})

export class DateConvert {
	transform(date, args) {
		if (date instanceof Date) {
			return date;
		}
		return new Date(date);
	}
}