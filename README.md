# Seminarski Rad

This .NET application consists of three linked projects that compose an e-Commerce Webshop Application.

The main idea that the Weshop revolves around is Pokemon universe. 1 PokeDollar = 1 Japanese Yen.
How cool would it be to buy Pokemon that you want?

## How to setup?

You must have an sql server with a connection string as well as .NET 6.
To run this application you need to add your connection string inside the /Poke.MVC/appsettings.json 
file under "Defaultconnection". Application is using code-first approach and Entity Framework to generate 
all of needed relations for database. 

In /Poke.MVC/Data/Migrations/ AddAdminAccount migration can be configured to set a password for admin 
account, otherwise it is set to a default password: 'Password12345' and username/email: admin@admin.com. 
After setting up your connection string and you are happy with your password you can use entity framework 
to update the database and run the application from Poke.MVC root selected.

## External Resources

[PokeApiNet](https://github.com/mtrdp642/PokeApiNet) NuGet package by mtrdp642 (a wrapper package for 
[pokeapi.co](https://pokeapi.co/))
