namespace Themepark{

    // Reservation class
    // This class is used to store information about a reservation, including customer email
    // Getters Setters Constructors ToString ToFile Equals and MaxId Functionality Included
    class Reservation{

        private static int MaxId=1;
        private int Id;
        private string CustomerEmail;
        private int AttractionId;
        private string AttractionType;
        private string AttractionName;
        private string DateTime;
        private bool Cancelled;

        public Reservation(int id, string customerEmail, int attractionId, string attractionType, string attractionName, string dateTime){
            Id = id;
            CustomerEmail = customerEmail;
            AttractionId = attractionId;
            AttractionType = attractionType;
            AttractionName = attractionName;
            //MM/DD/YYYY HH:MM in 24 hour time
            DateTime = dateTime;
            Cancelled = false;
            IncrementMaxId();
        }

        public Reservation(){
            Id = MaxId;
            CustomerEmail = "";
            AttractionId = -1;
            AttractionType = "";
            AttractionName = "";
            DateTime = "";
            Cancelled = false;
            IncrementMaxId();
        }

        public Reservation(string inFile){
            string[] data = inFile.Split('#');
            Id = int.Parse(data[0]);
            if(Id>=MaxId){
                MaxId=Id+1;
            }
            CustomerEmail = data[1];
            AttractionId = int.Parse(data[2]);
            AttractionType = data[3];
            AttractionName = data[4];
            DateTime = data[5];
            Cancelled = bool.Parse(data[6]);
        }

        public int GetId(){
            return Id;
        }

        public string GetCustomerEmail(){
            return CustomerEmail;
        }

        public int GetAttractionId(){
            return AttractionId;
        }

        public string GetAttractionType(){
            return AttractionType;
        }

        public string GetAttractionName(){
            return AttractionName;
        }

        public string GetDateTime(){
            return DateTime;
        }

        public bool GetCancelled(){
            return Cancelled;
        }

        public void SetId(int id){
            Id = id;
        }

        public void SetCustomerEmail(string customerEmail){
            CustomerEmail = customerEmail;
        }


        public void SetAttractionId(int attractionId){
            AttractionId = attractionId;
        }

        public void SetAttractionType(string attractionType){
            AttractionType = attractionType;
        }

        public void SetAttractionName(string attractionName){
            AttractionName = attractionName;
        }

        public void SetDateTime(string dateTime){
            DateTime = dateTime;
        }

        public void SetCancelled(bool cancelled){
            Cancelled = cancelled;
        }

        public int getMaxId(){
            return MaxId;
        }

        public void IncrementMaxId(){
            MaxId++;
        }

        public void setMaxId(int i){
            MaxId = i;
        }

        public bool Equals(Reservation r){
            return Id == r.GetId();
        }

        public void ToggleCancelled(){
            Cancelled = !Cancelled;
        }

        public override string ToString(){
            return "Reservation:\n\tID: " + Id + "\n\tCustomer Email: " + CustomerEmail + "\n\tAttraction ID: " + AttractionId + "\n\tAttraction Type: " + AttractionType + "\n\tAttraction Name: " + AttractionName + "\n\tDate and Time: " + DateTime + "\n\tCancelled: " + Cancelled + "\n";
        }

        public string ToFile(){
            return Id + "#" + CustomerEmail + "#" + AttractionId + "#" + AttractionType + "#" + AttractionName + "#" + DateTime + "#" + Cancelled + "#";
        }

    }

}