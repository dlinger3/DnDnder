# Tavern
Tavern a group finder and organizer for Dungeons and Dragons

**********************************************************************************************************************************
-After downloading the project files from GitHub, unzip them to somewhere on your machine where you
 can find them. Navigate to the location you unzipped the file, and click the DnDFolder

-right click and copy the file path address as text to be used in command prompt
  >when pasting, you should have a filepath that looks something like: C:\...\DnDnder
-Open your command prompt and run the following commands
-----------------------------------------------------------------------------------------------------
cd path/to/project (where  path/to/project is what you paste in. Should point to DnDnder folder)
dotnet tool install --global dotnet-ef --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef database update
dotnet run
------------------------------------------------------------------------------------------------------
-You should have seen output lines indicating that the program
  ran and is listening on a local host

- Look for line in the cosole that says: 
  "Now listing on local host: https//localhost:####"
  where #### will be replaced with some numbers

-open broswer and go to the localhost URL that was in your command prompt (there may be more than 1)

::::::::::::::::::::::TROUBLESHOOTING::::::::::::::::::::::::::::::::::::::::
- if you receive an error that looks like:
  "database [name] already exist. choose a different database name"
  > Run these commands: 
    sqllocaldb stop
    sqllocaldb delete
    --WARNING: I am not entirely sure if these commands might delete
      unexpected files on your machine if you have other DB files

- if your receive an error that looks like:
  "Can not create db file..."
    > delete it from c:/users/(yourUser), it will have a name similar to:
      aspnet-DnDnder-53bc9b9d-9d6a-45d4-8429-2a2761773502.mdf
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::::::::::::::::::::::USING THE WEBSITE::::::::::::::::::::::::::::::::::::::
-Main Features:
     >Register an account using a legitimate email
       --Email confirmation will be sent to this email. IT IS VERY LIKELY TO BE IN YOUR SPAM
       --You must confirm your account through the email confirmation before login
     >Upon login, the following features becomes available	
     >CRUD Operations if you use nav bar to navigate to Characters and/or Campaigns
       --After creating a campaign, you can list the campaign using the list campaign button
       --listed campaigns can be viewed in the Campaign Listings page
        	*Users who have created characters can join the campaigns you list
        	*You can register a second account and use an incognito window to login in to it and test this
                *You can also delist your listed campaigns here, or delist them on the campaigns index page 
                  that shows all of your campaigns
       --If you view a campaign listings details, and are assocaited with the lising, you will see a view chat button
       		*Users who are in a listing together can share messages with each other
		*This is a real-time chat feature. If can be tested by registering a second account
		*After registering a second account. Login to it using an incognito browser. 
                *You can communicate between the two accounts in this manner and see the real-time functionality
                *Messages are saved and displayed to the chat if you leave and come back. 
       --The campaign listing details page will also have a dice roller. This accepts input for types of dice
          and returns output based on the input given. 
**********************************************************************************************************************************
