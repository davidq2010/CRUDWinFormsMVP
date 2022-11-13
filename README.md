# CRUDWinFormsMVP
## SQL Stuff
1. Installed Microsoft SQL Server Management Studio
2. Command-line tool "localdb" came with it
3. sqllocaldb create "LocalDBDemo" -s
4. In SSMS, when prompted to Connect to a server, connected to (LocalDb)\LocalDBDemo
5. Executed VeterinaryDb.sql to create a new DB
6. In SSMS Object Explorer, right-clicked Databases directory and refreshed

## Misc Learnings
- MVP vs MVC: In MVP views and presenters are more tightly coupled (1:1). In MVC there's a 1:many relationship b/w controllers and views.
- Views should be dumb, i.e., no business logic.
- In MVP, user interaction with views triggers events that presenters subscribe to to perform business logic (e.g., send updated data packaged as a model to storage).
- We 100% need validation of model objects before sending them to storage.