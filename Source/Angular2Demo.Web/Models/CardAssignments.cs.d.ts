/// <reference path="Card.cs.d.ts" />

declare module server {
	interface CardAssignment {
		id: number;
		cardId: number;
		assignedToId: number;
		createdById: number;
		created: Date;
		card: server.Card;
		assignedTo: {
			id: number;
			name: string;
			username: string;
			createdCards: server.Card[];
			assignedTo: server.CardAssignment[];
			createdAssignments: server.CardAssignment[];
		};
		createdBy: {
			id: number;
			name: string;
			username: string;
			createdCards: server.Card[];
			assignedTo: server.CardAssignment[];
			createdAssignments: server.CardAssignment[];
		};
	}
}
