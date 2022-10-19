Code Struture: MVC

Models: C# classes. These are the entities of our program with related properties. Customer, Product etc.
Views: CSHTML pages. These are the screens the user actually sees when running our program. cshtml uses HTML tags to structure and stylise the page and, using @{}, we can write regular C# code in the page.
Controllers: C# classes. These are linked to the model, view and database. Methods will perform checks and then return a View, passing through a certain model to that view. This is also where we add, remove and alter entries in the database
Database: This is created using DbContext. This class is provided DbSets of our models, which are automatically convert to tables and uses the classes properties as columns.

Code Contribution:

David
Models: Product.cs, Restaurant.cs, ProductCategory.cs, SampleData.cs
Controllers: ProductController.cs, RestaurantController.cs, ProductCategoryController.cs, AdminController.cs, ErrorController.cs, HomeController.cs
Views: Product/Index, Restaurant/Index, Admin/Index, Error/Index

Abishek
Models:OrderDetails.CS
Controllers:OrderManagerController.CS
View: index.cshtml, OrderDetails.cshtml, layout.cshtml

Jaghadishan
Models: Customer.cs, CustomerLogin.cs
Controllers: LoginController.cs, CustomerController.cs
Views: Login/Register, Login/Login, Login/Index

Patrick.L
Models: -
Controllers: LoginController.cs, CustomerController.cs, RestaurantController.cs
Views: Login/EditUserDetails, Restaurant/Index

Brendan
Models: Order.cs, Payment.cs, Cart.cs
Controllers: PaymentController.cs, OrderController.cs
Views: Product/Index, Payment/Index, Payment/PaymentHistory, Payment/PaymentSuccess, Payment/PaymentPage

Patrick.E
Models: -
Controllers: ProductController.cs
Views: Product/AddProduct, Product/EditProduct, Product/DeleteProduct

More detailed contributions can be found in our report
