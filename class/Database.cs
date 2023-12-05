using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace Themepark{

    // Database class
    // This class is used to store information about the park, including attractions, customers, and reservations
    // Adding, Removing, Editting, and Verification functionality all included in Database
    // Database is loaded and saved to files on startup and shutdown
    // Database allows to search for other customer fields given one field

    //Customer records are kept active and deleted in customers.txt and deleted-customers.txt respectively under the datafiles folder

    //Attraction records are kept active and deleted in park-rides.txt and deleted-park-rides.txt respectively under the datafiles folder

    //Reservation records are kept active and deleted in customer-interactions.txt and deleted-customer-interactions.txt respectively under the datafiles folder

    //Manager records are kept in manager-registry.txt under the datafiles folder
    class Database{

            List<Attraction> Attractions = new List<Attraction>();
            List<Customer> Customers = new List<Customer>();
            List<Reservation> Reservations = new List<Reservation>();

            public Database(){
    
            }

            // Load Function
            // Read all files and convert to Objects
            public void Load(){
                //Attractions
                using(StreamReader sr = new StreamReader("datafiles/park-rides.txt")){
                        Attractions.Clear();
                        while(!sr.EndOfStream){
                            Attraction a = new Attraction(sr.ReadLine().Trim());
                            Attractions.Add(a);
                        }
                }

                //Updating new MaxId
                using(StreamReader sr = new StreamReader("datafiles/deleted-park-rides.txt")){
                        while(!sr.EndOfStream){
                            string[] data = sr.ReadLine().Trim().Split("#");
                            if(int.Parse(data[0])>Attractions[0].getMaxId()){
                                Attractions[0].setMaxId(int.Parse(data[0]));
                            }
                        }
                }


                //Customer
                using(StreamReader sr = new StreamReader("datafiles/customers.txt")){
                        Customers.Clear();
                        while(!sr.EndOfStream){
                            Customer c = new Customer(sr.ReadLine().Trim());
                            Customers.Add(c);
                        }
                }

                //Updating new Customer MaxId
                using(StreamReader sr = new StreamReader("datafiles/deleted-customers.txt")){
                        while(!sr.EndOfStream){
                            string[] data = sr.ReadLine().Trim().Split("#");
                            if(int.Parse(data[0])>Customers[0].getMaxId()){
                                Attractions[0].setMaxId(int.Parse(data[0]));
                            }
                        }
                }

                //RES
                using(StreamReader sr = new StreamReader("datafiles/customer-interactions.txt")){
                        Reservations.Clear();
                        while(!sr.EndOfStream){
                            Reservation r = new Reservation(sr.ReadLine().Trim());
                            Reservations.Add(r);
                        }
                }

                using(StreamReader sr = new StreamReader("datafiles/deleted-customers.txt")){
                        while(!sr.EndOfStream){
                            string[] data = sr.ReadLine().Trim().Split("#");
                            if(int.Parse(data[0])>Reservations[0].getMaxId()){
                                Reservations[0].setMaxId(int.Parse(data[0]));
                            }
                        }
                }
            }

            // Save Function
            // Convert all Objects to File Format and Write to Files
            public void Save(){
                using(StreamWriter sw = new StreamWriter("datafiles/park-rides.txt")){
                    foreach(Attraction a in Attractions){
                        sw.WriteLine(a.ToFile());
                    }
                }

                using(StreamWriter sw = new StreamWriter("datafiles/customers.txt")){
                    foreach(Customer c in Customers){
                        sw.WriteLine(c.ToFile());
                    }
                }

                using(StreamWriter sw = new StreamWriter("datafiles/customer-interactions.txt")){
                    foreach(Reservation r in Reservations){
                        if(r.GetCancelled()){
                            using(StreamWriter sw2 = new StreamWriter("datafiles/deleted-customer-interactions.txt",append: true)){
                                sw2.WriteLine(r.ToFile());
                            }
                        }
                        else{
                            sw.WriteLine(r.ToFile());  
                        }
                    }
                }
            }

            // Add, Remove, Edit Functions for Attractions, Customers, and Reservations
            // Contain Error Handling and UI for Function Only
            public void AddAttraction(){
                Attraction a = new Attraction();
        
                System.Console.WriteLine("Please enter the Name of the attraction: ");
                a.SetName(System.Console.ReadLine());

                System.Console.WriteLine("Please enter the Type of the attraction (Thrill, Water, Simulator, Kiddie, Family-Friendly, Other) (Case Sensitive):");
                string type = System.Console.ReadLine();
                while(!verifyAttractionType(type)){
                    System.Console.WriteLine("Invalid Attraction Type!");
                    System.Console.WriteLine("Please enter the Type of the attraction: ");
                    type = System.Console.ReadLine();
                }
                a.SetAttractionType(type);

                System.Console.WriteLine("Please enter the Operational status of the attraction: ");
                string input = System.Console.ReadLine();
                while(!input.Equals("true") && !input.Equals("false") && !input.Equals("True") && !input.Equals("False") && !input.Equals("TRUE") && !input.Equals("FALSE") && !input.Equals("t") && !input.Equals("f") && !input.Equals("T") && !input.Equals("F") && !input.Equals("0") && !input.Equals("1") && !input.Equals("yes") && !input.Equals("no") && !input.Equals("Yes") && !input.Equals("No") && !input.Equals("YES") && !input.Equals("NO")){
                    System.Console.WriteLine("Invalid input!");
                    System.Console.WriteLine("Please enter the Operational status of the attraction: ");
                    input = System.Console.ReadLine();
                }

                bool operational;
                if(input.Equals("true") || input.Equals("True") || input.Equals("TRUE") || input.Equals("t") || input.Equals("T") || input.Equals("1") || input.Equals("yes") || input.Equals("Yes") || input.Equals("YES")){
                    operational = true;
                }
                else{
                    operational = false;
                }
                a.SetOperational(operational);

                Attractions.Add(a);
            }
            public void EditAttraction(){
                Console.Clear();
                bool cont = true;

                while(cont){
                    System.Console.WriteLine("Please enter the ID or Name of the attraction you wish to edit: ");
                    string input = Console.ReadLine();
                    int dex;
                    bool found=false;

                    foreach (Attraction a in Attractions){
                        if(a.GetId().ToString().Equals(input) || a.GetName().Equals(input)){
                            found=true;
                            dex = Attractions.IndexOf(a);
                            System.Console.WriteLine("Please enter the new Name of the attraction: ");
                            Attractions[dex].SetName(System.Console.ReadLine());

                            System.Console.WriteLine("Please enter the Type of the attraction (Thrill, Water, Simulator, Kiddie, Family-Friendly, Other) (Case Sensitive):");
                            string type = System.Console.ReadLine();
                            while(!verifyAttractionType(type)){
                                System.Console.WriteLine("Invalid Attraction Type!");
                                System.Console.WriteLine("Please enter the Type of the attraction (Thrill , Water, Simulator, Kiddie, Family-Friendly, Other) (Case Sensitive):");
                                type = System.Console.ReadLine();
                            }
                            Attractions[dex].SetAttractionType(type);

                            System.Console.WriteLine("Please enter the new Operational status of the attraction: ");
                            Attractions[dex].SetOperational(bool.Parse(System.Console.ReadLine()));
                        }
                    }
                    
                    if(!found){
                        System.Console.WriteLine("Attraction not found!");
                    }
                    else{
                        System.Console.WriteLine("Attraction edited successfully!");
                        cont = false;
                    }

                }
            }
            public void RemoveAttraction(){
                            Console.Clear();
                            bool cont = true;

                            //While allows reentry if !found
                            while(cont){
                                System.Console.WriteLine("Please enter the ID or Name of the attraction you wish to remove: ");
                                string input = Console.ReadLine();
                                
                                bool found=false;
                                int currDex = 0;
                                int foundDex = -1;

                                foreach (Attraction a in Attractions){
                                    if(a.GetId().ToString().Equals(input) || a.GetName().Equals(input)){
                                        found=true;
                                        foundDex = currDex;
                                    }
                                    currDex++;
                                }

                                //Back to top
                                if(!found){
                                    System.Console.WriteLine("Attraction not found!");
                                }
                                //Remove Attraction
                                else{
                                    try{
                                        //Set !Operational
                                        Attractions[foundDex].SetOperational(false);
                                        //Add to `deleted-park-files`
                                        using(StreamWriter sw = new StreamWriter("datafiles/deleted-park-rides.txt",append: true)){
                                            sw.WriteLine(Attractions[foundDex].ToFile());
                                            //Remove from list
                                            Attractions.RemoveAt(foundDex);
                                        }
                                        System.Console.WriteLine("Attraction removed successfully!");
                                        Thread.Sleep(600);
                                    }
                                    catch{
                                            System.Console.WriteLine("REMOVE ERROR!");
                                            Thread.Sleep(800);
                                    }
                                    cont = false;
                                }

                            }
                        }
            public void AddCustomer(){
                Customer c = new Customer();

                System.Console.WriteLine("Please enter the First Name of the customer: ");
                string input = System.Console.ReadLine();
                while(input.Equals("")){

                    System.Console.WriteLine("Invalid input!");
                    System.Console.WriteLine("Please enter the First Name of the customer: ");
                    input = System.Console.ReadLine();
                }
                c.SetFirst(input);

                System.Console.WriteLine("Please enter the Last Name of the customer: ");
                input = System.Console.ReadLine();
                while(input.Equals("")){

                    System.Console.WriteLine("Invalid input!");
                    System.Console.WriteLine("Please enter the Last Name of the customer: ");
                    input = System.Console.ReadLine();
                }
                c.SetLast(input);

                System.Console.WriteLine("Please enter the Email of the customer: ");
                input = System.Console.ReadLine();
                while(input.Equals("")){

                    System.Console.WriteLine("Invalid input!");
                    System.Console.WriteLine("Please enter the Email of the customer: ");
                    input = System.Console.ReadLine();
                }
                c.SetEmail(input);

                System.Console.WriteLine("Please enter the Age of the customer: ");
                input = System.Console.ReadLine();
                while(input.Equals("")){

                    System.Console.WriteLine("Invalid input!");
                    System.Console.WriteLine("Please enter the Age of the customer: ");
                    input = System.Console.ReadLine();
                }
                c.SetAge(int.Parse(input));

                Customers.Add(c);
            }
            public void RemoveCustomer(){
                Console.Clear();
                bool cont = true;

                while(cont){
                    System.Console.WriteLine("Please enter the ID or Name of the customer you wish to remove: ");
                    string input = Console.ReadLine();
                    int dex;
                    bool found=false;

                    foreach (Customer c in Customers){
                        if(c.GetId().ToString().Equals(input) || input.Equals(c.GetFirst()+" "+c.GetLast()) || input.Equals(c.GetLast()+" "+c.GetFirst())){
                            found=true;
                            dex = Customers.IndexOf(c);
                            Customers.RemoveAt(dex);
                        }
                    }
                    
                    if(!found){
                        System.Console.WriteLine("Customer not found!");
                    }
                    else{
                        System.Console.WriteLine("Customer removed successfully!");
                        cont = false;
                    }

                }
            }
            public void EditCustomer(){
                Console.Clear();
                bool cont = true;

                while(cont){
                    System.Console.WriteLine("Please enter the ID or Name of the customer you wish to edit: ");
                    string input = Console.ReadLine();
                    int dex;
                    bool found=false;

                    foreach (Customer c in Customers){
                        if(c.GetId().ToString().Equals(input) || input.Equals(c.GetFirst()+" "+c.GetLast()) || input.Equals(c.GetLast()+" "+c.GetFirst())){
                            found=true;                                                                                                                                
                            dex = Customers.IndexOf(c);
                            System.Console.WriteLine("Please enter the new First Name of the customer: ");
                            Customers[dex].SetFirst(System.Console.ReadLine());

                            System.Console.WriteLine("Please enter the new Last Name of the customer: ");
                            Customers[dex].SetLast(System.Console.ReadLine());

                            System.Console.WriteLine("Please enter the new Email of the customer: ");
                            Customers[dex].SetEmail(System.Console.ReadLine());

                            System.Console.WriteLine("Please enter the new Age of the customer: ");
                            Customers[dex].SetAge(int.Parse(System.Console.ReadLine()));
                        }
                    }
                    
                    if(!found){
                        System.Console.WriteLine("Customer not found!");
                        Thread.Sleep(500);
                    }
                    else{
                        System.Console.WriteLine("Customer edited successfully!");
                        Thread.Sleep(200);
                        cont = false;
                    }

                }
            }

            public void EditCustomer(string input){
                Console.Clear();
            
                int dex;
                bool found=false;

                foreach (Customer c in Customers){
                    if(c.GetId().ToString().Equals(input) || input.Equals(c.GetFirst()+" "+c.GetLast()) || input.Equals(c.GetLast()+" "+c.GetFirst()) || input.Equals(c.GetEmail())){
                        found=true;                                                                                                                                
                        dex = Customers.IndexOf(c);
                        System.Console.WriteLine("Please enter the new First Name of the customer: ");
                        Customers[dex].SetFirst(System.Console.ReadLine());

                        System.Console.WriteLine("Please enter the new Last Name of the customer: ");
                        Customers[dex].SetLast(System.Console.ReadLine());

                        System.Console.WriteLine("Please enter the new Email of the customer: ");
                        Customers[dex].SetEmail(System.Console.ReadLine());

                        System.Console.WriteLine("Please enter the new Age of the customer: ");
                        Customers[dex].SetAge(int.Parse(System.Console.ReadLine()));
                    }
                }
                    
                if(!found){
                    System.Console.WriteLine("Customer not found!");
                    Thread.Sleep(500);
                }
                else{
                    System.Console.WriteLine("Customer edited successfully!");
                    Thread.Sleep(200);
                }
 
            }
            public void AddReservation(string email){
                Reservation r = new Reservation();

                System.Console.WriteLine("Please enter the ID of the attraction: ");
                r.SetAttractionId(int.Parse(System.Console.ReadLine()));

                foreach(Attraction a in Attractions){
                    if(a.GetId()==r.GetAttractionId()){
                        r.SetAttractionName(a.GetName());
                        r.SetAttractionType(a.GetAttractionType());
                    }
                }

                r.SetCustomerEmail(email);

                System.Console.WriteLine("Please enter the Date and Time in the format \"MM/DD/YYYY TT:TT\" in 24 hour time: ");
                r.SetDateTime(System.Console.ReadLine());

                Reservations.Add(r);
            }
            public void AddReservation(){
                Reservation r = new Reservation();

                System.Console.WriteLine("Please enter the ID of the attraction: ");
                r.SetAttractionId(int.Parse(System.Console.ReadLine()));

                System.Console.WriteLine("Please enter the email of the customer: ");
                r.SetCustomerEmail(System.Console.ReadLine());

                System.Console.WriteLine("Please enter the Date and Time in the format \"MM/DD/YYYY TT:TT\" in 24 hour time: ");
                r.SetDateTime(System.Console.ReadLine());

                Reservations.Add(r);
            }
            public void CancelReservation(){
                Console.Clear();
                bool cont = true;

                while(cont){
                    System.Console.WriteLine("Please enter the ID of the reservation you wish to cancel: ");
                    string input = Console.ReadLine();
                    int dex;
                    bool found=false;

                    foreach (Reservation r in Reservations){
                        if(r.GetId().ToString().Equals(input)){
                            found=true;
                            dex = Reservations.IndexOf(r);
                            Reservations[dex].SetCancelled(true);
                        }
                    }
                    
                    if(!found){
                        System.Console.WriteLine("Reservation not found!");
                    }
                    else{
                        System.Console.WriteLine("Reservation cancelled successfully!");
                        cont = false;
                    }
                }
            }
            public void RemoveReservation(){
                Console.Clear();
                bool cont = true;

                while(cont){
                    System.Console.WriteLine("Please enter the ID of the reservation you wish to remove: ");
                    string input = Console.ReadLine();
                    int dex;
                    bool found=false;

                    foreach (Reservation r in Reservations){
                        if(r.GetId().ToString().Equals(input)){
                            found=true;
                            dex = Reservations.IndexOf(r);
                            Reservations.RemoveAt(dex);
                        }
                    }
                    
                    if(!found){
                        System.Console.WriteLine("Reservation not found!");
                    }
                    else{
                        System.Console.WriteLine("Reservation removed successfully!");
                        cont = false;
                    }

                }
            }
            public void AddReservation(Reservation r){
                Reservations.Add(r);
            }
            public void RemoveAttraction(Attraction a){
                Attractions.Remove(a);
            }
            public void RemoveCustomer(Customer c){
                Customers.Remove(c);
            }
            public void RemoveReservation(Reservation r){
                Reservations.Remove(r);
            }
            public void FullPrint(){
                foreach(Attraction a in Attractions){
                    System.Console.WriteLine(a.ToString());
                }
                foreach(Customer c in Customers){
                    System.Console.WriteLine(c.ToString());
                }
                foreach(Reservation r in Reservations){
                    System.Console.WriteLine(r.ToString());
                }

            }
            public string FetchAttractionName(string id){
                foreach(Attraction a in Attractions){
                    if(int.Parse(id)==a.GetId()){
                        return a.GetName();
                    }
                }
                return "Does not exist";
            }

            public string FetchAttractionName(int id){
                foreach(Attraction a in Attractions){
                    if(id==a.GetId()){
                        return a.GetName();
                    }
                }
                return "Does not exist";
            }
            public List<Attraction> GetAttractions(){
                return Attractions;
            }
            public List<Customer> GetCustomers(){
                return Customers;
            }
            public List<Reservation> GetReservations(){
                return Reservations;
            }
            public bool verifyAttractionType(string s){
                string[] types = {"Thrill" , "Water", "Simulator", "Kiddie", "Family-Friendly", "Other"};
                foreach(String curr in types){
                    if(curr.Equals(s)){
                        return true;
                    }
                }
                return false;
            }
            public string VerifyManager(string id){
                bool ret = false;

                using(StreamReader sr = new StreamReader("datafiles/manager-registry.txt")){
                    while(!sr.EndOfStream){
                        string[] data = sr.ReadLine().Split('#');
                        if(data[0] == id || ((data[1]+" "+data[2]) == id)){
                            ret = true;
                            return data[1]+" "+data[2];
                            break;
                        }
                    }
                }
                return "-2";
            }

            public string VerifyCustomer(string id){
                bool ret = false;

                foreach(Customer c in Customers){
                    if(c.GetId().ToString() == id || ((c.GetFirst()+" "+c.GetLast()) == id)){
                        ret = true;
                        return c.GetFirst()+" "+c.GetLast();
                        break;
                    }
                }
                return "-1";
            }
            
            public string GetCustomerEmailFromName(string name){
                foreach(Customer c in Customers){
                    if((c.GetFirst()+" "+c.GetLast()) == name){
                        return c.GetEmail();
                    }
                }
                return "-1";
            }

            public string GetCustomerNameFromEmail(string email){
                foreach(Customer c in Customers){
                    if(c.GetEmail() == email){
                        return c.GetFirst()+" "+c.GetLast();
                    }
                }
                return "-1";
            }

            public string GetCustomerNameFromId(int id){
                foreach(Customer c in Customers){
                    if(c.GetId().ToString() == id.ToString()){
                        return c.GetFirst()+" "+c.GetLast();
                    }
                }
                return "-1";
            }

            public int GetCustomerIdFromName(string name){
                foreach(Customer c in Customers){
                    if((c.GetFirst()+" "+c.GetLast()) == name){
                        return c.GetId();
                    }
                }
                return -1;
            }

    }
}