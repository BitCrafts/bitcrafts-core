CREATE TABLE Groupes (
                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                         Nom TEXT NOT NULL
);
CREATE TABLE Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nom TEXT NOT NULL
);
CREATE TABLE Commandes (
                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                           ClientId INTEGER,
                           DateCommande TEXT NOT NULL,
                           DateLivraison TEXT,
                           Etat TEXT NOT NULL,
                           FOREIGN KEY (ClientId) REFERENCES Clients(Id)
);
CREATE TABLE Produits (
                          Id INTEGER PRIMARY KEY AUTOINCREMENT,
                          Nom TEXT NOT NULL,
                          Description TEXT,
                          Prix REAL NOT NULL,
                          QuantiteEnStock INTEGER NOT NULL,
                          CategorieId INTEGER,
                          FOREIGN KEY (CategorieId) REFERENCES Categories(Id)
);

CREATE TABLE Clients (
                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                         Nom TEXT NOT NULL,
                         Adresse TEXT,
                         Telephone TEXT,
                         Email TEXT,
                         GroupeId INTEGER,
                         FOREIGN KEY (GroupeId) REFERENCES Groupes(Id)
);
 
CREATE TABLE LignesCommande (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                CommandeId INTEGER,
                                ProduitId INTEGER,
                                Quantite INTEGER NOT NULL,
                                PrixUnitaire REAL NOT NULL,
                                FOREIGN KEY (CommandeId) REFERENCES Commandes(Id),
                                FOREIGN KEY (ProduitId) REFERENCES Produits(Id)
);

CREATE TABLE Paiements (
                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                           CommandeId INTEGER,
                           TypePaiement TEXT NOT NULL,
                           Montant REAL NOT NULL,
                           DatePaiement TEXT NOT NULL,
                           FOREIGN KEY (CommandeId) REFERENCES Commandes(Id)
);


