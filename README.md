# SKYROUTES
Application that solves drone routing with Dijkstra's algorithm

## Setup
1. Create a new repo with the template, and then clone the repo generated for your own account
1. In the top-level directory of the cloned project on your computer, run `dotnet user-secrets init`
1. Run `dotnet user-secrets set AdminPassword password` (you can choose a different password if you wish)
1. Run `dotnet user-secrets set SkyRoutesDbConnectionString 'Host=localhost;Port=5432;Username=postgres;Password=password;Database=SkyRoutes'`
1. Run `dotnet restore`
1. Run `dotnet build`
1. Run `dotnet ef migrations add InitialCreate`
1. Run `dotnet ef database update`
1. Run `cd client`
1. Run `npm install`

## Test the Setup
1. Start debugging the API and run `npm start` in the `client` directory. 
1. You should see the login view when the UI opens. 
1. Attempt to login with `admina@skyroutes.com` and the password you set the value of `AdminPassword` to in the user-secrets
1. If the setup succeeded, you should see navbar links and logout button. 


## Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).
