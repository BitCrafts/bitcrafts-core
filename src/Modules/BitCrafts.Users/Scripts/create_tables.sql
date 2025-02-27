CREATE TABLE User (
      Id INT AUTO_INCREMENT PRIMARY KEY,
      FirstName VARCHAR(255),
      LastName VARCHAR(255),
      Email VARCHAR(255) UNIQUE,
      PhoneNumber VARCHAR(20),
      BirthDate DATE,
      NationalNumber VARCHAR(50),
      PassportNumber VARCHAR(50)
);
CREATE TABLE UserAccount (
     Id INT AUTO_INCREMENT PRIMARY KEY,
     UserId INT UNIQUE,
     HashedPassword VARCHAR(255),
     PasswordSalt VARCHAR(255),
     FOREIGN KEY (UserId) REFERENCES User(Id) ON DELETE CASCADE
);
