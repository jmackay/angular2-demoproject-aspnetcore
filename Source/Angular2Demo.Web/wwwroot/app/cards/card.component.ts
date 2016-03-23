import {Component, Input} from "angular2/core";
import {CORE_DIRECTIVES} from "angular2/common";
import {RouterLink, RouteParams} from "angular2/router";

import {DateConvert} from "../shared/pipes/dateConvert.pipe";

import {DataService} from "../shared/services/data.service";

@Component({
	selector: "card",
	templateUrl: "app/cards/card.component.html",
	pipes: [DateConvert]
})
export class CardComponent {
	@Input() card: server.Card;
	@Input() minimized: boolean = false;
	categories: server.Category[];
	stacks: server.Stack[];
	
	constructor(private _dataService: DataService) { }

	ngOnInit() {
		this.categories = this._dataService.Categories.getAll();
		this.stacks = this._dataService.Stacks.getAll();
	}

	save(event) {
		// TODO: Fix this in a better way, the model isn't updated before the on change event fires...
		setTimeout(() => {
			this._dataService.Cards.update(this.card.id, this.card);
		}, 100);
	}
}