import {Component, Input} from "angular2/core";
import {CORE_DIRECTIVES} from "angular2/common";
import {RouterLink, RouteParams} from "angular2/router";

import {CardComponent} from "./card.component";
import {DataService} from "../shared/services/data.service";

@Component({
	selector: "cards",
	templateUrl: "app/cards/cards.component.html",
	directives: [CardComponent]
})
export class CardsComponent {
	@Input() cards: server.Card[];
	@Input() minimized: boolean = true;
	@Input() title: string = "";
	@Input() stackId: number;
	newCard: server.Card;
	categories: server.Category[];
	newForm: any;
	adding = false;

	constructor(private _dataService: DataService){}

	ngOnInit() {
		this._dataService.Categories.findAll().then(data => this.categories = data);
	}

	startNew() {
		var myId = JSON.parse(localStorage.getItem("myId"));
		this.newCard = this._dataService.Cards.createInstance({
			createdById: myId,
			stackID: this.stackId
		});
	}

	addNew() {
		this.adding = false;
		this._dataService.Cards.create(this.newCard, { upsert: true })
		.then(() => this.newCard = null)
		.finally(() => this.adding = false);
	}
}