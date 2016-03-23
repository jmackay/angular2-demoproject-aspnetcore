import {Component} from "angular2/core";
import {CORE_DIRECTIVES} from "angular2/common";
import {RouterLink, RouteParams} from "angular2/router";

import {DataService} from "../shared/services/data.service";

import {OrderByPipe} from "../shared/pipes/orderBy.pipe";

import {CardsComponent} from "../cards/cards.component";

@Component({
	selector: "userDetails",
	templateUrl: "app/users/userDetails.component.html",
	directives: [CORE_DIRECTIVES, RouterLink, CardsComponent],
	pipes: [OrderByPipe]
})

export class UserDetailsComponent {
	title: string;
	user: server.User;
	assignedCards: server.Card[];
	createdCards: server.Card[];

	constructor(private _dataService: DataService, private _routeParams: RouteParams) {}
	
	ngOnInit() {
		this.title = "User Details";
		let userId = parseInt(this._routeParams.get("id"), 10);

		this._dataService.Users.find(userId).then(data => {
			this.user = data;
		});
	}
}