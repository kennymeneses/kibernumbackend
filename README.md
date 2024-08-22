# Kibernum - Crud Application API - Code Challenge

# Running the web API

This guide provides step-by-step instructions to run this api locally

## Prerequisites
Before proceeding, ensure that the following prerequisites are met:

- **.NET Version 8:** Make sure to have .NET version 8 installed on your machine.
- Make sure you have run this command first: `dotnet dev-certs https --trust`

## Running the Project Locally

**Use the `https` profile from `launchSettings.json`:**

Execute `dotnet run --launch-profile https` command in: `src/KibernumCrud.Api/`

You can also open this endpoint in your web browser:
`https://localhost:7208/swagger/index.html?urls.primaryName=V1`


## Endpoints
To get auth token you can hit `https://localhost:7208/api/v1/Auth/login`
and use this body request:
`
{
  "email": "admin@example.com",
  "password": "1234"
}
`
* https://localhost:7208/api/v1/Contacts?UserId=46bc2ec4-ee32-4a92-a46b-539697d4dce0 List Contacts
* https://localhost:7208/api/v1/Contacts/cef3d3c7-80d6-406f-af26-66dbbb5656c9 Get Contact