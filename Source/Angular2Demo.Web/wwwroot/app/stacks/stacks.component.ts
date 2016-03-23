import {Component, Input} from "angular2/core";
import {CORE_DIRECTIVES} from "angular2/common";
import {RouterLink, RouteParams} from "angular2/router";
import {StackComponent} from "./stack.component";

import {DataService} from "../shared/services/data.service";

@Component({
	selector: "stacks",
	templateUrl: "app/stacks/stacks.component.html",
	directives: [StackComponent]
})
export class StacksComponent {
	//@Input() stacks: server.Stack[] = null;
	public stacks: server.Stack[] = [];

	constructor(private _dataService: DataService) {

	}

	ngOnInit() {
		this._dataService.Stacks.findAll().then(data => this.stacks = data);
	}
}