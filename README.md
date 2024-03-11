# ecommerce
Sample web application to showcase design patterns using ASP.NET core web api as the backend, and nextjs as front-end.

Here is the page structure
- Home
- Products
  - id
- Cart

It has a Layout component too for header and footer

## Home
Displays the top 3 features products

## Products
Displays the dummy products fetched from the web api

## Product details
Displays the product details page of the selected item

## Cart
Displays the cart items added


## Instructions
To get started, clone this repo.

* Open it in Visual Studio Code
* Open new terminal for BE
* Go to EcommerceWebApi folder
* Run `dotnet restore`
* Run `dotnet run`
* Now the web api with url http://localhost:5000 is listening
* Open a new terminal for FE
* Go to EcommerceNextJS folder
* Run `npm install`
* Run `npm run dev`
