
import {Component, Input, Optional} from "angular2/core";
import {RouterLink, RouteParams} from "angular2/router";

import {DataService} from "../shared/services/data.service";

import {CardsComponent} from "../cards/cards.component";


@Component({
	selector: "stack",
	templateUrl: "app/stacks/stack.component.html",
	directives: [CardsComponent],
	styles: [".panel-heading { color: #FFBB00; border-left: 5px solid orange }"] // We can add additional styles!
})
export class StackComponent {
	@Input() stack: server.Stack;

	constructor(private _dataService: DataService, private _routeParams: RouteParams) { }

	ngOnInit() {
		var stackId;
		if (this.stack) {
			stackId = this.stack.id;
		} else {
			stackId = parseInt(this._routeParams.get("id"), 10);

			this._dataService.Stacks.find(stackId).then(data => this.stack = data);
		}
	}
}