# WiredPlayers RolePlay GameMode
WiredPlayers is a RolePlay project made for RAGE Multiplayer, it uses C# as main language for both server and client. I started with it back in March 2017 and I'm still upgrading its functionality with suggestions received from people using this gamemode.

## Helping the project
There are multiple ways of helping to this project. You can add changes to the source, suggest new features or, if you feel so, you can also become a patron on Patreon: https://www.patreon.com/wprp

## Update Log

* The update log can be seen by checking this page -> [Update Log](https://github.com/xabier1989/WiredPlayers-RP/wiki/Update-Log) 

## Getting Started

### 1.Prerequisites

* [RAGE Multiplayer](https://cdn.gtanet.work/RAGE_Multiplayer.zip) - The client to login into the server
* [Bridge plugin](https://cdn.rage.mp/public/files/bridge-package.zip) - The plugin allowing use to use C# server-side
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) - The database to store the data
* [.NET Core SDK](https://www.microsoft.com/net/download) - The SDK to develop C# resources
* [XAMPP](https://www.apachefriends.org/ro/index.html) - The mysql server/client. Easy to use but you can choose others from the internet. That's only the suggested one.

**Note:** This project has only been tested under Windows environments, and it's not working right now on Linux distributions

### 2.Installing the Server
1. Install the .msi file that comes into RAGE Multiplayer's .zip file
2. Execute the **updater.exe** located on the root folder where you installed RAGE Multiplayer
3. Unzip the Bridge plugin into the folder called **server-files** replacing the files if needed
4. Execute again the **updater.exe** located on the root folder where you installed RAGE Multiplayer in order for it to update to the bridge plugin
5. Create a file called **enable-clientside-cs.txt** on the root folder where you installed RAGE Multiplayer (on your local PC, not on the server)
6. Make sure your router has opened 22005 UDP port and 22006 TCP/IP, if you dont know how to do that just google for router port forwarding

**Note:** Fore more informations check also the RAGE Wiki: [Click Here](https://wiki.rage.mp/index.php?title=Getting_Started_with_Server)

### 3.Installing the GameMode
1. Get all the files from this GitHub and place them into the same folder as before, replacing the files you're asked for
2. Open your MySQL client and import the **wprp.sql** database located under **server-files** folder
3. Import to Visual Studio the **WiredPlayers.csproj** file, located on the following path: **%RAGEMP Installed folder%/server-files/bridge/resources/WiredPlayers/**
#### Database Connection:
4. Change the database connection settings under **meta.xml** located on the following path: **%RAGEMP Installed folder%/server-files/bridge/resources/WiredPlayers/** or in your Visual Studio Project! You may get an error regarding to Database SSL Connection, check the **F.A.Q.** page bellow.
5. Make sure your solution has linked the **MySql.Data** Nuget, if not, add it to the project
6. On Visual Studio, clean and build the solution in order to generate the required **WiredPlayers.dll** library
7. Execute the **server.exe** located under the **server-files** folder
8. Log into your server and enjoy it

If you followed all this steps, you should be able to login with your newly registered account, if not please check the **[F.A.Q.](https://github.com/xabier1989/WiredPlayers-RP/wiki/FAQ)** in order to try solve the errors, some of them are solved there!
