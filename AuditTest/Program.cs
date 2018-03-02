namespace AuditTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Insert

            var person = new Person
            {
                FirstName = "some",
                LastName = "Name",
                Age = 13,
                Gender = "M",
                Deleted = false
            };


            using (var db = new AuditDbContext())
            {
                db.Persons.Add(person);
                db.SaveChanges();
                AuditTrail.CreateTrail(person.Id, typeof(Person).Name, new Person(), person, AuditAction.Create);

            }

            #endregion



            #region Update

            //using (var db = new AuditDbContext())
            //{
            //    var person = db.Persons.Find(1);

            //    if (person != null)
            //    {
            //        var updateperson=new Person
            //        {
            //            Id = person.Id,
            //            Gender = "M",
            //            Age =11,
            //            FirstName = person.FirstName,
            //            LastName = person.LastName
            //        };

            //        db.Persons.Attach(person);

            //        AuditTrail.CreateTrail(person.Id, typeof(Person).Name,person, updateperson, AuditAction.Update);

            //    }
            //}

            #endregion
        }
    }
}
