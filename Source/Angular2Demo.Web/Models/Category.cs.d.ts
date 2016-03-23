/// <reference path="Card.cs.d.ts" />

declare module server {
	interface Category {
		id: number;
		name: string;
		labelColor: string;
		cards: server.Card[];
	}
}
