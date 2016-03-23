/// <reference path="Card.cs.d.ts" />
/// <reference path="CardAssignments.cs.d.ts" />

declare module server {
	interface User {
		id: number;
		name: string;
		username: string;
		createdCards: server.Card[];
		assignedTo: server.CardAssignment[];
		createdAssignments: server.CardAssignment[];
	}
}
