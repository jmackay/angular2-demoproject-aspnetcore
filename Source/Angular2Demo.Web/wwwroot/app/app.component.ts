import {Component} from "angular2/core";
import {ROUTER_DIRECTIVES, RouterLink, RouteConfig } from "angular2/router";

import {StacksComponent} from "./stacks/stacks.component";
import {UsersComponent} from "./users/users.component";
import {UserDetailsComponent} from "./users/userDetails.component";

import {DataService} from "./shared/services/data.service";

@Component({
	selector: "app-container",
	providers: [DataService],
	template: `
	<div class="navbar navbar-inverse navbar-fixed-top">
		<div class="container">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</button>
				<a [routerLink]="['Stacks']" class="navbar-brand">Angular 2 Demo</a>
			</div>
			<div class="navbar-collapse collapse">
				<ul class="nav navbar-nav">
					<li><a [routerLink]="['Stacks']">Stacks</a></li>
					<li><a [routerLink]="['Users']">Users</a></li>
				</ul>
			</div>
		</div>
	</div>
	<div class="container body-content">
		<br />
		<router-outlet></router-outlet>
		<hr />
		<footer>
			<p>Angular 2 Starter Demo by John Mackay and Matthew Overall</p>
		</footer>
	</div>`,
	directives: [ROUTER_DIRECTIVES]
})

@RouteConfig([
	{ path: "/Stacks", name: "Stacks", component: StacksComponent, useAsDefault: true },
	{ path: "/Users", name: "Users", component: UsersComponent },
	{ path: "/Users/:id/Details", name: "UserDetails", component: UserDetailsComponent }
])

export class AppComponent {
	constructor(private _dataService: DataService) {
	}

	ngOnInit() {
		this._dataService.reloadAll();
	}
}