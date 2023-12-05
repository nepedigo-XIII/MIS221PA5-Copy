namespace Themepark{

    // Customer class
    // Getters Setters Constructors ToString ToFile Equals and MaxId Functionality Included
    class Customer{

        private static int MaxId=1;
        private int Id;
        private string Email;
        private string First;
        private string Last;
        private int Age;

        public Customer(int id, string email, string first, string last, int age){
            Id = id;
            Email = email;
            First = first;
            Last = last;
            Age = age;
            IncrementMaxId();
        }

        public Customer(){
            Id = MaxId;
            Email = "";
            First = "";
            Last = "";
            Age = -1;
            IncrementMaxId();
        }

        public Customer(string inFile){
            string[] data = inFile.Split('#');
            Id = int.Parse(data[0]);
            if(Id>=MaxId){
                MaxId=Id+1;
            }
            Email = data[1];
            First = data[2];
            Last = data[3];
            Age = int.Parse(data[4]);
        }

        public int GetId(){
            return Id;
        }

        public string GetEmail(){
            return Email;
        }

        public string GetFirst(){
            return First;
        }

        public string GetLast(){
            return Last;
        }

        public int GetAge(){
            return Age;
        }

        public void SetId(int id){
            Id = id;
        }

        public void SetEmail(string email){
            Email = email;
        }

        public void SetFirst(string first){
            First = first;
        }

        public void SetLast(string last){
            Last = last;
        }

        public void SetAge(int age){
            Age = age;
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

        public bool Equals(Customer c){
            return Id == c.GetId();
        }


        public override string ToString(){
            return "Customer:\n\tID: " + Id + "\n\tEmail: " + Email + "\n\tFirst:" + First + "\n\tLast: " + Last + "\n\tAge: " + Age + "\n";
        }
        
        public string ToFile(){
            return Id + "#" + Email + "#" + First + "#" + Last + "#" + Age+"#";
        }


    }

}