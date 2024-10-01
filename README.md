# <p align="center">ChatApp: A Real-Time Chatting Application</p>

## About

  **ChatApp** is a real-time chatting application that use **React** for an interactive frontend and the **.NET Core Framework** for a robust backend. 
  Utilizing **Microsoft SQL Server** for database management and **SignalR** for instant messaging, ChatApp enables seamless communication, allowing users to connect and converse effortlessly in real time. Layered architecture is used to maintain code base efficiently.
  
    Key functionalities:  
                      - SignIn , SignUp
                      - Search available user by name or email
                      - One to one realtime chatting
                      - Update user about
                      - Change Password
                      - Option to switch between dark theme and light theme
                      - Store previous conversations
                      - Message Delete
                      - signout 
## Technology Used

### Backend (.NET Core)

  - SignalR
  - AutoMapper
  - Authentication and Authorization (JWT)
  - RESTful API

### Database
 
  - Microsoft SQL Server

### Frontend (ReactJS)

  - Material UI
  - Microsoft SignalR

## Installation Instructions

### Setting up environment
  - .Net Core framework Version : 8.0
  - ReactJS version: ^18.3.1
### Run Project

  To run this project locally, follow these steps:
  
  1. Clone the repository / download code.
  2. Navigate to the project directory for both the back-end and front-end.
  3. Set up database connection:
     - Navigate to `appsettings.json` -> `ConnectionStrings` -> `default` and change the value of `Data Source`, `User Id` and `Password`
     - Using Entity framework create `Migrations` file using the following commands from Terminal:
       - Create `Migrations` File: `dotnet ef --startup-project .\back-end\ChatApp.Presentation\ChatApp.Presentation.csproj  migrations add MyMigration --context ChatContext  --output-dir Migrations --project .\back-end\ChatApp.Persistence\ChatApp.Persistence.csproj`
       - Update the Database: `dotnet ef database update --context ChatContext --project .\back-end\ChatApp.Persistence\ChatApp.Persistence.csproj --startup-project .\back-end\ChatApp.Presentation\ChatApp.Presentation.csproj`
  5. Install backend dependencies: `dotnet restore`
  6. Start the backend server: `dotnet run`
  7. Install frontend dependencies: `npm install`
  8. Start the frontend development server: `npm start`
  9. Open your browser and go to `http://localhost:3000` to access the website.

## Usage Instructions
  - SignUp first if you are a new user. Provide proper information in the sign-up form. After successful registration it will redirect to sign-in page.
  - From sign-in page login to the system using mail and password.
  - In left side bar the list of previous chats will shown. Click on a specific user to load previous conversations and continue chat.
  - You can find settings, theme change and sign-out option at the top of left side `three dot` menu.
  - Inside `settings` update `about` and `change password` functionality is given.
  - Press the right side top `info` icon to see chat receiver details.
  - To delete message, hover mouse pointer over the message and press mouse right-click, the delete button will appear.
  - If you delete your sended message it will show as _`Message Removed`_ to both sender and receiver side.
  - If you delete a received message then it will simply removed from your side but still visible in the receiver end.
