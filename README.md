# Hotel Overstay Calculator
This API will calclate overstay fees for customers of a hotel. The solution primarily contains 5 projects and 1 test project.

1. Domain
2. Core
3. Infrastructure
3. Api
4. Checkout

# Domain
A class library project containing enterprise wide logic and types.

# Core
A class library project containing application specific or business logic and types.

# Infrastructure
A class library project containing data access logic and types.

# Api
The hotel overstay web api microservice.

# Checkout
A worker background microservice continually calculating the overstay fee for customers.

# Getting Started
1. <code>cd</code> into the solution root folder and run the following command which automatically restores required nuget packages.

    <code>dotnet build</code>
    

2. Open a cli to start the Checkout worker background microservice:

    <code>cd Checkout</code>

    <code>dotnet run</code>

2. Open a cli to start the web Api project:

    <code>cd Api</code>

    <code>dotnet run</code>
3. Use postman or your browser to hit the Bills endpoint at https://localhost:5001/bills?customerid=12323

# Tests
To run the unit tests
<code>cd</code> into the Tests project and run the following command:

<code>dotnet test</code>



    