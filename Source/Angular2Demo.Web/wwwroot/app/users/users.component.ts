import {Component, ChangeDetectionStrategy} from "angular2/core";
import {CORE_DIRECTIVES} from "angular2/common";
import {RouterLink} from "angular2/router";

import {DataService} from "../shared/services/data.service";
import {OrderByPipe} from "../shared/pipes/orderBy.pipe";

@Component({
	selector: "users",
	templateUrl: "app/users/users.component.html",
	directives: [CORE_DIRECTIVES, RouterLink],
	pipes: [OrderByPipe]
})

export class UsersComponent {
	title: string;
	users: server.User[];

	constructor(private dataService: DataService, private _dataService: DataService) {
	}

	ngOnInit() {
		this.title = "Users";

		this._dataService.Users.findAll().then((data) => {
			this.users = data;
		})
	}
}