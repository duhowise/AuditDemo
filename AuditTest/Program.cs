namespace AuditTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var person=new Person
            {
                FirstName = "some",
                LastName = "Name",
                Age = 13.ToString(),
                Gender = 'M'
            };


            using (var db=new AuditDbContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
                AuditTrail.CreateTrail(person.Id,typeof(Person).Name,new Person(), person,AuditAction.Create);

            }
        }
    }
}
