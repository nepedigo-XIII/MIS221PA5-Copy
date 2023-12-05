namespace Themepark{

    // Attraction class for all rides and shops etc.
    // Getters Setters Constructors ToString ToFile Equals and MaxId Functionality Included
    class Attraction{
        private int Id;

        private static int MaxId = 1;
        private string Name;
        private string Type;
        private bool Operational;


        public Attraction(int i, string name, string type, bool operational){
            Id = i;
            Name = name;
            Type = type;
            Operational = operational;
            IncrementMaxId();
        }

        public Attraction(){
            Id = MaxId;
            Name = "";
            Type = "";
            Operational = true;
            IncrementMaxId();
        }
        //DOES NOT INCREMENT MAXID BY DEFAULT
        public Attraction(string inFile){
            string[] data = inFile.Split('#');
            Id = int.Parse(data[0]);
            if(Id>=MaxId){
                MaxId=Id+1;
            }
            Name = data[1];
            Type = data[2];
            Operational = bool.Parse(data[3]);
        }

        public int GetId(){
            return Id;
        }
        
        public string GetName(){
            return Name;
        }

        public string GetAttractionType(){
            return Type;
        }

        public bool GetOperational(){
            return Operational;
        }

        public void SetId(int id){
            Id = id;
        }

        public void SetName(string name){
            Name = name;
        }

        public void SetAttractionType(string type){
            Type = type;
        }

        public void SetOperational(bool operational){
            Operational = operational;
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


        public void ToggleOperational(){
            Operational = !Operational;
        }

        public bool Equals(Attraction a){
            return Id == a.GetId();
        }

        public override string ToString(){
            return "Attraction:\n\tID: " + Id + "\n\tName: " + Name + "\n\tType: " + Type + "\n\tOperational: " + Operational + "\n";
        }

        public string ToFile(){
            return Id.ToString() + "#" + Name + "#" + Type + "#" + Operational.ToString()+"#";
        }

    }
}