namespace DataBase.Model
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Number { get; set; }

        public virtual List<EntertainmentCenter> EntertainmentCenters { get; set; }
    }
}