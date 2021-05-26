# CourseWork2021
# About project

This project is a client-server application. The goal is to create inverted index in parallel environment, provide access to it and to give an opportunity to client to find files  which contain given word. 

## Progect structure

This project consists of 3 parts :
1. Library that contains interfaces, models and services for creating inverted index
2. Server. It provides access for clients to use services via network sockets.
3. Client. This part provides communication with user and exchanging information between server and user.  
Files:

### CourseWork2021
#### Interfaces:
- ```IIndex.cs ```  - interface for Index
- ```IIndexer.cs``` - interface for Indexer
#### Models

- ```Index.cs``` - contains dictionary created as inverted index
#### Services
- ```IndexerService``` - creates inverted index and provides access to it
- ```CompareService``` - service for comparison serial and parallel creating index
- ```TimerService``` - service for measure time of processing on different amount of threads

#### Program
- ```Program.cs``` - contains main function

### Server
- ```Server.cs``` - contains runServer function that runs server
- ```ServerManager.cs``` - server manager do exhanging and processing data from clients
- ```Program.cs``` - contains main function
### Client
- ```ClientManager.cs``` - client manager communicate with user and exchange information between user and server
- ```Program.cs``` - contains main function

## How to run the program
1. Download project [from here](https://github.com/KaterynaKub/CourseWork2021/archive/refs/heads/main.zip) or clone the repo
2. Unzip zip file.
3. Go into the folder ```CourseWork2021\Server``` via terminal
4. Execute command ```dotnet run``` - server will run
5. Go into the folder ```CourseWork2021\Client``` via another terminal
6. Execute command ```dotnet run``` - client will run
7. Enjoy!

> P.S.1 U can also run the project ```dotnet --project "path to project" run``` without going to project folder

> P.S.2 Command ```dotnet run``` automaticaly builds the project then runs it. If you want to build the project explicitly try ```dotnet --project "path to project" build```
