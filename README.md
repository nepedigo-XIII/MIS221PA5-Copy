# Programming Assignment 5 Amusement Park Management Information System

 ***Nate Pedigo***

<br>
<br>
<br>
<br>
<br>


## `Overview`

The Amusement Park Management Information System is a fictional project designed to manage and streamline the operations of a virtual amusement park. The system incorporates six key objects: Customer, Attraction, Reservation, Database, UI (User Interface), and ReportManager.

`Customer`, `Attraction`, and `Reservation` are all developed under the `Themepark` namespace. The Themepark namespace was designed with the intention of allowing the hypothetical client to control the front end UI with minimal effort.<br>

## `Objects`

### Customer

The `Customer` object represents individuals visiting the amusement park. It stores information about the customer, including their name, age, and ID number. These fields correspond to the Reservation object, which tracks the customer's reservations.<br>

### Attraction

The `Attraction` object encapsulates information about the various attractions within the amusement park. This includes details like attraction type, name, and current status. Reservations will include the ID of the attraction they correspond to. <br>

### Reservation

The `Reservation` object manages reservations made by customers for specific attractions. It tracks reservation details, including the customer, attraction, and reservation status, as well as the date and time of the reservation.<br>

### Database

The `Database` object serves as the underlying data storage and retrieval system. It interacts with the other objects to persistently store and retrieve information. Handles all reading and writing to files.<br>

### UI (User Interface)

The `UI` object is responsible for providing a user-friendly experience and maintaing the flow of control. It facilitates user input, displays information, and enables navigation. Wrangles and directs Database and ReportManager objects.<br>

### ReportManager

The `ReportManager` object handles the generation and presentation of reports based on the data stored in the system. It provides insights into park operations, customer behavior, and attraction popularity. Some functions are only available to managers and customers respectively.<br>

## `Instructions for Use`

1. **Installation:**
   - Clone the project repository from [https://github.com/UAMIS221-321/mis221-pa5-nepedigo-XIII.git].
   - Run the application using `dotnet run --property WarningLevel=0`.

2. **Usage:**
   - Select Either the Customer or Manager Menu and Login with Credentials Below.
   - Create, Update, and Delete Customer and Reservation Information in the Customer Menu.
   - Manage Attraction Details and Availability, Manage Customer Base, and Generate Reports in the Manager Menu.
    - Exit the Application by Selecting the Exit Option in the Main Menu (**CHANGES MADE DURING RUN TIME WILL NOT BE SAVED UNLESS EXITED PROPERLY TO AVOID CORRUPTION FROM CRASHES**)

3. **Sample Manager Accounts:**
    ***see `manager-registry.txt` for full list of managers and passwords*** <br>

   - **Associated Name:** Jeff Lucas<br>**Password:** J3FF

   - **Associated Name:** Chase Callahan<br>**Password:** CH4SE 

   - **Associated Name:** Nate Pedigo<br>**Password:** 6283

   - **Associated Name:** Bob Taylor<br>**Password:** L0RDB0B


4. **Sample Customer Accounts:**
    ***see `customers.txt` for full list of customers and passwords***

   - **Associated Name:** Jeff Lucas<br>**ID #:**  11

   - **Associated Name:** Chase Callahan<br>**ID #:**  12

   - **Associated Name:** Nate Pedigo<br>**ID #:**  1


