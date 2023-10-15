# Car-Rental-App-Framework

This is a WinForms application that uses ADO.net and Microsoft SQL Server Management Studio. The application allows you to add, edit, and delete cars with all their details (VIN, year, make, model, etc). 
These cars can be attached to rental records with the times that they were checked in and out. These rental records can also be added, edited, and deleted.

There is also a user login screen. When you go into the application the password is initially admin and password. The 'admin' can change all the 'passwords' of the users in a separate tab that only appears if you are an administrator. 
A user can receive a new password and enter a new one that only they know and is hashed after being entered in the database. It is based on a many-to-many sql set of tables where the users are linked with their different roles. There are three roles in the initial
implementation of the database. Administrator, user, and viewer.
