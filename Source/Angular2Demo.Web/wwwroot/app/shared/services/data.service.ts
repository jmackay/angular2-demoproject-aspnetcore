import {Injectable} from 'angular2/core';

@Injectable()
export class DataService {
    private _store: JSData.DS;
    Users: JSData.DSResourceDefinition<server.User>;
    CardAssignments: JSData.DSResourceDefinition<server.CardAssignment>;
    Cards: JSData.DSResourceDefinition<server.Card>;
    Stacks: JSData.DSResourceDefinition<server.Stack>;
    Categories: JSData.DSResourceDefinition<server.Category>;

    constructor() {
        this._store = new JSData.DS();
        var adapter: any = new DSHttpAdapter();

		var useStatic = false; // If you don't want to use a backend API you can turn this on to load from static json files, saving and updates will error on their post/put requests!

		if (useStatic) {
			adapter.defaults.basePath = "/api/static";
			this._store.registerAdapter("http", adapter, { default: true });
			this.defineJsDataRelationshipsStatic();
		} else {
			adapter.defaults.basePath = "/api";
			this._store.registerAdapter("http", adapter, { default: true });
			this.defineJsDataRelationships();
		}
    }
	
	reloadAll() {
		this._store.clear();

		this.Users.findAll().then(data => {
			localStorage.setItem("myId", JSON.stringify(data[0].id));
		});
		this.CardAssignments.findAll();
		this.Cards.findAll();
		this.Categories.findAll();
		this.Stacks.findAll();
	}

    defineJsDataRelationships() {
        this.Users = this._store.defineResource<server.User>({
            name: "user",
            endpoint: "users",
			computed: {
				assignedCards: {
					get: function (): server.Card[]{
						return (<server.User>this).assignedTo.map(x => x.card)
					}
				}
			},
            relations: {
                hasMany: {
                    card: {
                        localField: "createdCards",
                        foreignKey: "createdById"
                    },
                    cardAssignment: [{
                        localField: "assignedTo",
                        foreignKey: "assignedToId"
                    }, {
                            localField: "createdAssignments",
                            foreignKey: "createdById"
                        }]
                }
            }
        });

        this.CardAssignments = this._store.defineResource<server.CardAssignment>({
			name: "cardAssignment",
			endpoint: "cardAssignments",
			relations: {
				belongsTo: {
					card: {
						localField: "card",
						localKey: "cardId"
					},
					user: [{
						localField: "assignedTo",
						localKey: "assignedToId"
					}, {
							localField: "createdBy",
							localKey: "createdById"
						}]
				}
			}
		});

		this.Cards = this._store.defineResource<server.Card>({
			name: "card",
			endpoint: "cards",
			relations: {
				belongsTo: {
					user: {
						localField: "createdBy",
						localKey: "createdById"
					},
					stack: {
						localField: "stack",
						localKey: "stackId"
					},
					category: {
						localField: "category",
						localKey: "categoryId"
					}
				},
				hasMany: {
					cardAssignment: {
						localField: "cardAssignments",
						foreignKey: "cardId"
					}
				}
			}
		});

		this.Stacks = this._store.defineResource<server.Stack>({
			name: "stack",
			endpoint: "stacks",
			relations: {
				hasMany: {
					card: {
						localField: "cards",
						foreignKey: "stackId"
					}
				}
			}
		});

		this.Categories = this._store.defineResource<server.Category>({
			name: "category",
			endpoint: "categories",
			relations: {
				hasMany: {
					card: {
						localField: "cards",
						foreignKey: "categoryId"
					}
				}
			}
		});
    }

	defineJsDataRelationshipsStatic() {
        this.Users = this._store.defineResource<server.User>({
            name: "user",
            endpoint: "users.json",
			computed: {
				assignedCards: {
					get: function (): server.Card[] {
						return (<server.User>this).assignedTo.map(x => x.card)
					}
				}
			},
            relations: {
                hasMany: {
                    card: {
                        localField: "createdCards",
                        foreignKey: "createdById"
                    },
                    cardAssignment: [{
                        localField: "assignedTo",
                        foreignKey: "assignedToId"
                    }, {
                            localField: "createdAssignments",
                            foreignKey: "createdById"
                        }]
                }
            }
        });

        this.CardAssignments = this._store.defineResource<server.CardAssignment>({
			name: "cardAssignment",
			endpoint: "cardAssignments.json",
			relations: {
				belongsTo: {
					card: {
						localField: "card",
						localKey: "cardId"
					},
					user: [{
						localField: "assignedTo",
						localKey: "assignedToId"
					}, {
							localField: "createdBy",
							localKey: "createdById"
						}]
				}
			}
		});

		this.Cards = this._store.defineResource<server.Card>({
			name: "card",
			endpoint: "cards.json",
			relations: {
				belongsTo: {
					user: {
						localField: "createdBy",
						localKey: "createdById"
					},
					stack: {
						localField: "stack",
						localKey: "stackId"
					},
					category: {
						localField: "category",
						localKey: "categoryId"
					}
				},
				hasMany: {
					cardAssignment: {
						localField: "cardAssignments",
						foreignKey: "cardId"
					}
				}
			}
		});

		this.Stacks = this._store.defineResource<server.Stack>({
			name: "stack",
			endpoint: "stacks.json",
			relations: {
				hasMany: {
					card: {
						localField: "cards",
						foreignKey: "stackId"
					}
				}
			}
		});

		this.Categories = this._store.defineResource<server.Category>({
			name: "category",
			endpoint: "categories.json",
			relations: {
				hasMany: {
					card: {
						localField: "cards",
						foreignKey: "categoryId"
					}
				}
			}
		});
    }
}