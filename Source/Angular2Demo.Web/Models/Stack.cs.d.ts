/// <reference path="Card.cs.d.ts" />

declare module server {
	interface Stack {
		id: number;
		name: string;
		previousStackId: number;
		previousStack: server.Stack;
		cards: server.Card[];
	}
}
