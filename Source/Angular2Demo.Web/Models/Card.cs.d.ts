declare module server {
	interface Card {
		id: number;
		title: string;
		previousCardId: number;
		description: string;
		stackId: number;
		categoryId: number;
		createdById: number;
		created: Date;
		due: Date;
		lastModified: Date;
		previousCard: server.Card;
		stack: {
			id: number;
			name: string;
			previousStackId: number;
			previousStack: any;
			cards: server.Card[];
		};
		category: {
			id: number;
			name: string;
			labelColor: string;
			cards: server.Card[];
		};
		createdBy: {
			id: number;
			name: string;
			username: string;
			createdCards: server.Card[];
			assignedTo: any[];
			createdAssignments: any[];
		};
		cardAssignments: any[];
	}
}
