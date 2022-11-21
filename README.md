Voting System

Application for voting election candidates.  
In order to vote a candidate you need to create an user account.  

Users have access to:  
-Vote a candidate (only once and ireversible)  
-See the voting status  

Admins have access to:  
-Add candidates  
-Edit candidates creditals  
-Delete candidates  
-Delete users  

Anyone can create an account and see the candidates.  

How to run the application:  

-Download and extract the archive  
-In appsettings.json add email creditals (email account that sends notifications)  
-Install Docker and create redis container (used for distributed caching)  
-In appsettings.json add your redis port (example: http://localhost:5002)  
-In appsettings.json add your database connection string  
-In Visual Studio, go to Voting-System.Infrastructure and execute [Update-Database] in Package Manager Console.  
