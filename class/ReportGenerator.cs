namespace Themepark{
    using System.Globalization;

    // ReportGenerator class
    // This class is used to generate reports based on the data in current Database instance
    // Reports to not alter the data in any way
    // Reports are printed to the console
    // Reports are generated based on current date using System.DateTime
    class ReportGenerator{

        private Database db;

        public ReportGenerator(Database Db){
            db = Db;
        }

        public void GenerateFullReport(){
            System.Console.WriteLine("Generating Full Report:\n");
            System.Console.WriteLine("Attractions:\n");

            foreach(Attraction a in db.GetAttractions()){
                System.Console.WriteLine(a);
            }

            System.Console.WriteLine("Customers:\n");
            foreach(Customer c in db.GetCustomers()){
                System.Console.WriteLine(c);
            }

            System.Console.WriteLine("Reservations:\n");
            foreach(Reservation r in db.GetReservations()){
                System.Console.WriteLine(r);
            }
    
        }

        public void GenerateAttractionReport(){
            System.Console.WriteLine("Generating Attraction Report:\n");
            foreach(Attraction a in db.GetAttractions()){
                System.Console.WriteLine(a);
            }
        }

        public void GenerateCustomerReport(){
            System.Console.WriteLine("Generating Customer Report:\n");
            foreach(Customer c in db.GetCustomers()){
                System.Console.WriteLine(c);
            }
        }

        public void GenerateReservationReport(){
            System.Console.WriteLine("Generating Reservation Report:\n");
            foreach(Reservation r in db.GetReservations()){
                System.Console.WriteLine(r);
            }
        }

        public void ViewCustomerHistory(string custEmail){
            System.Console.WriteLine("Generating Customer History Report:\n");
            foreach(Reservation r in db.GetReservations()){
                if(r.GetCustomerEmail().Equals(custEmail)){
                    System.Console.WriteLine(r);
                }
            }
        }

        //Top Five Attractions by Number of Active Reservations
        //Working?
        public void AttractionByRes(){
            System.Console.WriteLine("Top 5 Attractions by Reservation");
            List<Attraction> attractions = db.GetAttractions();

            List<int> quant = new List<int>();

            //In the event that the attraction ids are thrown off, we need to create a buffer to account for the difference
            int buffer = 20;
            int size = attractions[0].getMaxId() + buffer;

            for(int i=0; i<size; i++){
                quant.Add(0);
            }

            foreach(Reservation r in db.GetReservations()){
                quant[r.GetAttractionId()]++;
            }

            int max = -1;

            for(int r=0;r<5;r++){
                for(int c=0;c<quant.Count;c++){
                    if(quant[c]>max){
                        max=quant[c];
                    }
                }
                System.Console.WriteLine($"#{r+1}: {attractions[quant.IndexOf(max)].GetName()} ( with {max} current unfulfilled reservations)");
                quant[quant.IndexOf(max)] = -1;
                max = -1;
            }

            // System.Console.WriteLine("Top 5 Attractions by Reservation");
            // List<Attraction> attractions = db.GetAttractions();

            // int[] ids = {-1,-1,-1,-1,-1};
            // int[] quantities = {0,0,0,0,0};

            // for(int i=0; i<5; i++){
            //     foreach(Reservation r in db.GetReservations()){
            //         int maxQuantity =
            //     }

            // }
                


            // int[] quantityById = new int[attractions[0].getMaxId()];

            // for(int i=0; i<quantityById.Length; i++){
            //     quantityById[i]=0;
            // }

            // foreach(Reservation r in db.GetReservations()){
            //     quantityById[r.GetAttractionId()-1]++;
            // }

            // for(int place = 1; place < 6; place++){
            //     int maxQuantity = -1;
            //     int maxIndex = -1;

            //     for(int i = 0; i < quantityById.Length; i++){
            //         if(quantityById[i] > maxQuantity){
            //             maxQuantity = quantityById[i];
            //             maxIndex = i;
            //         }
            //     }

            //     if(maxIndex != -1){
            //         System.Console.WriteLine($"#{place}: {attractions[maxIndex].GetName()} ({maxQuantity} reservations)");
            //         quantityById[maxIndex] = -1;
            //     }
            // }
        }

        //View Reservations by Attraction Type
        //Based on current date
        public void ReservationsByType(){
            List<Attraction> attractions = db.GetAttractions();
            List<Reservation> reservations = db.GetReservations();

            // Indices of quantityByType correspond to the indices of attractions in order Thrill, Water, Simulator, Kiddie, Family-Friendly, Other
            //0 - thrill , 1 - water, 2 - simulator, 3 - kiddie, 4 - family, 5 - other
            int[] quantityByType = {0,0,0,0,0,0};

            

            DateTime currentDate= DateTime.Now;

            foreach(Reservation r in reservations){
                string tempDate = r.GetDateTime();

                int year = int.Parse(tempDate.Substring(6,4));
                int month = int.Parse(tempDate.Substring(0,2));
                int day = int.Parse(tempDate.Substring(3,2));

                DateTime reservationDate = new DateTime(year, month, day);


                switch(attractions[r.GetAttractionId()].GetAttractionType()){
                    case "Thrill":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[0]++;
                            }
                        }
                        break;
                    case "Water":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[1]++;
                            }
                        }
                        break;
                    case "Simulator":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[2]++;
                            }
                        }
                        break;
                    case "Kiddie":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[3]++;
                            }
                        }
                        break;
                    case "Family-Friendly":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[4]++;
                            }
                        }
                        break;
                    case "Other":
                        if(!r.GetCancelled()){
                            if(DateTime.Compare(reservationDate, currentDate) >= 0){
                                quantityByType[5]++;
                            }
                        }
                        break;
                }
            
            }

            System.Console.WriteLine("Reservations by Attraction Type:\n");
            System.Console.WriteLine("-------------------------------------------\n");
            System.Console.WriteLine($"\tThrill: {quantityByType[0]}");
            System.Console.WriteLine($"\tWater: {quantityByType[1]}");
            System.Console.WriteLine($"\tSimulator: {quantityByType[2]}");
            System.Console.WriteLine($"\tKiddie: {quantityByType[3]}");
            System.Console.WriteLine($"\tFamily-Friendly: {quantityByType[4]}");
            System.Console.WriteLine($"\tOther: {quantityByType[5]}\n");
            System.Console.WriteLine("-------------------------------------------\n");
        }

        //View Active Reservations by Ride
        //Based on current date
        public void ReservationByRide(){
            List<Attraction> attractions = db.GetAttractions();
            List<Reservation> reservations = db.GetReservations();

            DateTime currentDate = DateTime.Now;
        
            System.Console.WriteLine("Current Active Reservations by Attraction:\n");
            System.Console.WriteLine("-------------------------------------------\n");
            foreach(Attraction a in attractions){
                int quantity = 0;
                foreach(Reservation r in reservations){
                    string tempDate = r.GetDateTime();
                
                    int year = int.Parse(tempDate.Substring(6,4));
                    int month = int.Parse(tempDate.Substring(0,2));
                    int day = int.Parse(tempDate.Substring(3,2));

                    DateTime reservationDate = new DateTime(year, month, day);

                    if(!r.GetCancelled()){
                        if(DateTime.Compare(reservationDate, currentDate) >= 0){
                            if(r.GetAttractionId() == a.GetId()){
                                quantity++;
                            }
                        }
                    }
                }
                System.Console.WriteLine($"{a.GetName()}: {quantity}");
            }
            System.Console.WriteLine("-------------------------------------------\n");
        }

        //View Most Ridden Ride by ALL TIME Reservations
        // Functionality added for more date time viewing stipulations, did not implement
        public void MostRidden(){
            List<Attraction> attractions = db.GetAttractions();
            List<Reservation> reservations = db.GetReservations();

            // System.Console.WriteLine("Press 1 to view all time most ridden, press 2 to view most ridden as of a specific date, press 3 to view most ridden today");
            // string input = System.Console.ReadLine();

            // while(!input.Equals("1") && !input.Equals("2") && !input.Equals("3")){
            //     System.Console.WriteLine("Invalid input, please try again");
            //     input = System.Console.ReadLine();
            // }

            int maxQuantity = -1;
            int maxId = -1;

            // switch(input){
            //     case "1":
                    
                    foreach(Attraction a in attractions){
                        int quantity = 0;
                        foreach(Reservation r in reservations){
                            if(!r.GetCancelled() && r.GetAttractionId() == a.GetId()){
                                quantity++;
                            }
                        }
                        if(quantity > maxQuantity){
                            maxQuantity = quantity;
                            maxId = a.GetId();
                        }
                    }

            //         break;
            //     case "2":
            //         System.Console.WriteLine("Enter the current date\"MM-DD-YYYY\"  :");
            //         string currentDate = System.Console.ReadLine();
            //         int currMonth = int.Parse(currentDate.Substring(0,2));
            //         int currDay = int.Parse(currentDate.Substring(3,2));
            //         int currYear = int.Parse(currentDate.Substring(6,4));

            //         maxQuantity = -1;
            //         maxId = -1;

            //         foreach(Attraction a in attractions){
            //             int quantity = 0;
            //             foreach(Reservation r in reservations){
            //                 int resMonth = int.Parse(r.GetDateTime().Substring(0,2));
            //                 int resDay = int.Parse(r.GetDateTime().Substring(3,2));
            //                 int resYear = int.Parse(r.GetDateTime().Substring(6,4));

            //                 if(!r.GetCancelled() && resYear <= currYear && resMonth <= currMonth && resDay <= currDay && r.GetAttractionId() == a.GetId()){
            //                     quantity++;
            //                 }
            //             }
            //             if(quantity > maxQuantity){
            //                 maxQuantity = quantity;
            //                 maxId = a.GetId();
            //             }
            //         }
            //         break;
            //     case "3":
            //         System.Console.WriteLine("Enter the current date \"MM-DD-YYYY\"  :");
            //         currentDate = System.Console.ReadLine();
            //         currMonth = int.Parse(currentDate.Substring(0,2));
            //         currDay = int.Parse(currentDate.Substring(3,2));
            //         currYear = int.Parse(currentDate.Substring(6,4));

            //         maxQuantity = -1;
            //         foreach(Attraction a in attractions){
            //             int quantity = 0;
            //             foreach(Reservation r in reservations){
            //                 int resMonth = int.Parse(r.GetDateTime().Substring(0,2));
            //                 int resDay = int.Parse(r.GetDateTime().Substring(3,2));
            //                 int resYear = int.Parse(r.GetDateTime().Substring(6,4));

            //                 if(!r.GetCancelled() && resYear == currYear && resMonth == currMonth && resDay == currDay && r.GetAttractionId() == a.GetId()){
            //                     quantity++;
            //                 }
            //             }
            //             if(quantity > maxQuantity){
            //                 maxQuantity = quantity;
            //                 maxId = a.GetId();
            //             }
            //         }
            //         break;
            // }
            System.Console.WriteLine("-------------------------------------------\n");
            System.Console.WriteLine($"Most Ridden Attraction: {db.FetchAttractionName(maxId)} ({maxQuantity} rides)");
            System.Console.WriteLine("-------------------------------------------\n");

        }

        public void ViewCustomerHistory(){
            System.Console.WriteLine("Enter the customer's email address:");
            string email = System.Console.ReadLine();
            bool found = false;

            System.Console.WriteLine("Generating Customer History Report:\n");
            System.Console.WriteLine("-------------------------------------------\n");
            foreach(Reservation r in db.GetReservations()){
                if(r.GetCustomerEmail().Equals(email)){
                    System.Console.WriteLine(r);
                    found = true;
                }
            }

            if(!found){
                System.Console.WriteLine("No reservations found for this customer");
            }
            System.Console.WriteLine("-------------------------------------------\n");
        }

        public void ViewOperationalRides(){
            System.Console.WriteLine("Generating Operational Rides Report:\n");
            System.Console.WriteLine("-------------------------------------------\n");
            foreach(Attraction a in db.GetAttractions()){
                if(a.GetOperational()){
                    System.Console.WriteLine(a);
                }
            }
            System.Console.WriteLine("-------------------------------------------\n");
        }
    }

}