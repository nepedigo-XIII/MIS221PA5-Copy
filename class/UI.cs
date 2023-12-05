namespace Themepark{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    // UI class
    // This class is used to handle all user input and output, menu routing, etc.
    // This class wrangles the Database class and ReportGenerator class
    // Most menu selections include error handling

    // The supporting classes in /class are designed so that the UI experience as well as protocol for saving/loading data can be easily changed
    // Attraction.cs, Customer.cs, and Reservation.cs contained in Database.cs all have additional methods to support the UI modifications
    // Additionally, the files located in /graphics can be changed to change the look of the program's menus

    class UI{
        Database db;
        ReportGenerator rg;

        // Constructor
        // Takes a Database object as a parameter
        // Prepares up to date ReportGenerator object
        public UI(Database Db){
            db = Db;
            rg = new ReportGenerator(db);
        }

        // Run
        // Loads Data from .txt files, clears the console, runs the main menu, saves data to .txt files, and exits the program
        public void Run(){
            db.Load();
            Console.Clear();
            MainMenu();
            db.Save();
            System.Console.WriteLine("\u001b[1m"+"\nThank you for using the Theme Park Database System!");
            Thread.Sleep(500);
        }   

        // Main Menu
        // Directs User to Customer Menu, Manager Menu, or exits the program

        public void MainMenuPrint(){
                Console.Clear();
                
                using(StreamReader sr = new StreamReader("graphics/main-menu.txt")){
                    string line;
                    while((line = sr.ReadLine()) != null){
                        System.Console.WriteLine(line);
                    }
                }

                System.Console.WriteLine("\u001b[1m"+"Welcome to the Theme Park Database System!\n");
                System.Console.WriteLine("\u001b[1m"+"Please select an option:\n");
                System.Console.WriteLine("\u001b[1m"+"1. Customer Menu");
                System.Console.WriteLine("\u001b[1m"+"2. Manager Menu");
                System.Console.WriteLine("\u001b[1m"+"3. Exit");
                System.Console.WriteLine("\u001b[1m"+"\nSelect Option: ");
        }  
        public void MainMenu(){
            MainMenuPrint();

            bool cont = true;
            while(cont){
                MainMenuPrint();
                string input = System.Console.ReadLine();
                while(input.Equals("1")==false && input.Equals("2")==false && input.Equals("3")==false){
                    Console.Clear();
                    System.Console.WriteLine("Invalid input!\n");
                    MainMenuPrint();
                    input = System.Console.ReadLine();
                }

                if(input.Equals("1")){
                    CustomerMenu();
                    Console.Clear();
                }
                else if(input.Equals("2")){
                    ManagerMenu();
                    Console.Clear();
                }
                else{
                    cont = false;
                }
            }
        }

        // Manager Menu
        // Directs Manager to Add Attraction, Remove Attraction, Edit Attraction, Generate Report, Add Customer until Exit

        public void ManagerMenuPrint(){
            System.Console.WriteLine("Please select an option:\n");
            System.Console.WriteLine("1. Add Attraction");
            System.Console.WriteLine("2. Remove Attraction");
            System.Console.WriteLine("3. Edit Attraction");
            System.Console.WriteLine("4. Generate Report");
            System.Console.WriteLine("5. Add Customer");
            System.Console.WriteLine("6. Exit");
            System.Console.WriteLine("\nSelect Option: ");
        }

        public void ManagerMenu(){
            Console.Clear();
                
                using(StreamReader sr = new StreamReader("graphics/manager-menu.txt")){
                    string line;
                    while((line = sr.ReadLine()) != null){
                        System.Console.WriteLine(line);
                    }
                }
            System.Console.WriteLine("Welcome to the Manager Menu!\n");
            System.Console.WriteLine("Please sign in before accessing the manager menu.\n");
            System.Console.WriteLine("Please enter your employee ID or name (Type -1 to exit): ");

            string input = System.Console.ReadLine();

            if(input.Equals("-1")){
                    return;
            }

            string nameofmanager = db.VerifyManager(input);


            while(nameofmanager.Equals("-2")){
                Console.Clear();
                System.Console.WriteLine("Invalid employee ID!");
                System.Console.WriteLine("Please enter your employee ID or name(Type -1 to exit): ");
                input = System.Console.ReadLine();
                if(input.Equals("-1")){
                    return;
                }
                nameofmanager = db.VerifyManager(input);
            }

            bool cont = true;
            while(cont){
                Console.Clear();
                System.Console.WriteLine("Welcome " + nameofmanager + "!\n");
                ManagerMenuPrint();

                input = System.Console.ReadLine();

                while(input.Equals("1")==false && input.Equals("2")==false && input.Equals("3")==false && input.Equals("4")==false && input.Equals("5")==false && input.Equals("6")==false){
                    Console.Clear();
                    System.Console.WriteLine("Invalid input!\n");
                    ManagerMenuPrint();
                    input = System.Console.ReadLine();
                }

                switch(input){
                    case "1":
                        db.AddAttraction();
                        Pause();
                        break;
                    case "2":
                        db.RemoveAttraction();
                        Pause();
                        break;
                    case "3":
                        db.EditAttraction();
                        Pause();
                        break;
                    case "4":
                        ReportMenu();
                        Pause();
                        break;
                    case "5":
                        db.AddCustomer();
                        Pause();
                        break;
                    case "6":
                        cont = false;
                        break;
                }
            }
        }

        // Customer Menu
        // Directs Customer to View Operational Rides, Reserve a Ride, View Ride History, Update Customer Account, Cancel Reservation until Exit
        public void CustomerMenuPrint(){
            System.Console.WriteLine("Please select an option:\n");
            System.Console.WriteLine("1. View Operational Rides");
            System.Console.WriteLine("2. Reserve a Ride");
            System.Console.WriteLine("3. View Ride History");
            System.Console.WriteLine("4. Update Customer Account");
            System.Console.WriteLine("5. Cancel Reservation");
            System.Console.WriteLine("6. Exit");
            System.Console.WriteLine("\nSelect Option: ");
        }

        public void CustomerMenu(){
            Console.Clear();

            using(StreamReader sr = new StreamReader("graphics/customer-menu.txt")){
                string line;
                while((line = sr.ReadLine()) != null){
                    System.Console.WriteLine(line);
                }
            }
            
            System.Console.WriteLine("Welcome to the Customer Menu!\n");
            System.Console.WriteLine("Please sign in before accessing the manager menu.\n");
            System.Console.WriteLine("Please enter your Customer ID or full name (Type -1 to exit): ");

            string input = System.Console.ReadLine();

            if(input.Equals("-1")){
                return;
            }

            string nameofcustomer = db.VerifyCustomer(input);

            while(nameofcustomer.Equals("-1")){
                Console.Clear();
                System.Console.WriteLine("Invalid Customer info!");
                System.Console.WriteLine("Please enter your Customer ID or full name (Type -1 to exit): ");
                input = System.Console.ReadLine();
                if(input.Equals("-1")){
                    return;
                }
                nameofcustomer = db.VerifyCustomer(input);
            }
            int currentCustomerid = db.GetCustomerIdFromName(nameofcustomer);
            string currentCustomerEmail = db.GetCustomerEmailFromName(nameofcustomer);

            bool cont = true;
            while(cont){
                Console.Clear();
                System.Console.WriteLine("\nWelcome " + nameofcustomer + "!\n");
                CustomerMenuPrint();

                input = System.Console.ReadLine();

                while(input.Equals("1")==false && input.Equals("2")==false && input.Equals("3")==false && input.Equals("4")==false && input.Equals("5")==false && input.Equals("6")==false){
                    Console.Clear();
                    System.Console.WriteLine("Invalid input!\n");
                    CustomerMenuPrint();
                    input = System.Console.ReadLine();
                }

                switch(input){
                    case "1":
                        rg = new ReportGenerator(db);
                        rg.ViewOperationalRides();
                        Pause();
                        Console.Clear();
                        break;
                    case "2":
                        db.AddReservation(currentCustomerEmail);
                        Pause();
                        Console.Clear();
                        break;
                    case "3":
                        rg = new ReportGenerator(db);
                        rg.ViewCustomerHistory();
                        Pause();
                        Console.Clear();
                        break;
                    case "4":
                        db.EditCustomer(currentCustomerid.ToString());
                        nameofcustomer = db.GetCustomerNameFromId(currentCustomerid);
                        currentCustomerEmail = db.GetCustomerEmailFromName(nameofcustomer);
                        Pause();
                        Console.Clear();
                        break;
                    case "5":
                        db.CancelReservation();
                        Pause();
                        Console.Clear();
                        break;
                    case "6":
                        cont = false;
                        break;
                }
            }
        }  

        // Report Menu
        // Directs Manager to Most Ridden Ride, Current Active Reservations By Ride, Total Completed Reservations By Ride Type, Top 5 Rides By Active Reservations until Exit
        public void ReportMenuPrint(){
            System.Console.WriteLine("Report Menu\n");
            System.Console.WriteLine("Please select an option:\n");
            System.Console.WriteLine("\t1. Most Ridden Ride");
            System.Console.WriteLine("\t2. Current Active Reservations By Ride");
            System.Console.WriteLine("\t3. Total Completed Reservations By Ride Type");
            System.Console.WriteLine("\t4. Top 5 Rides By Active Reservations");
            System.Console.WriteLine("\t5. Exit");
            System.Console.WriteLine("\nSelect Report: ");

        }
        public void ReportMenu(){
            Console.Clear();
            ReportMenuPrint();

            string input = System.Console.ReadLine();
            while(!input.Equals("1") && !input.Equals("2") && !input.Equals("3") && !input.Equals("4") && !input.Equals("5")){
                Console.Clear();
                System.Console.WriteLine("\nInvalid input!\n");
                ReportMenuPrint();
                input = System.Console.ReadLine();
                Console.Clear();
            }

            while(!input.Equals("5")){
                switch(input){
                    case "1":
                        rg = new ReportGenerator(db);
                        rg.MostRidden();
                        Pause();
                        break;
                    case "2":
                        rg = new ReportGenerator(db);
                        rg.ReservationByRide();
                        Pause();
                        break;
                    case "3":
                        rg = new ReportGenerator(db);
                        rg.ReservationsByType();
                        Pause();
                        break;
                    case "4":
                        rg = new ReportGenerator(db);
                        rg.AttractionByRes();
                        Pause();
                        break;
                }

                ReportMenuPrint();
                input = System.Console.ReadLine();
                while(!input.Equals("1") && !input.Equals("2") && !input.Equals("3") && !input.Equals("4") && !input.Equals("5")){
                    System.Console.WriteLine("\nInvalid input!\n");
                    ReportMenuPrint();
                    input = System.Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        // Pause
        // Pauses the program until the user presses a key
        void Pause(){
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
            Console.Clear();
        }



    }




}