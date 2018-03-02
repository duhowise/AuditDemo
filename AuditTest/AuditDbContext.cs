using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;

namespace AuditTest
{
    public class AuditDbContext : DbContext
    {
        public AuditDbContext():base("AuditDb")
        {
            
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
       [StringLength(35)] public string FirstName { get; set; }
        [StringLength(35)] public string LastName { get; set; }
        public string Age { get; set; }
        public char Gender { get; set; }
    }

    public class Audit
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }
    public class AuditTrail
    {
        public int Id { get; set; }
        public int KeyField { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DataModel { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
        public string Changes { get; set; }
        public AuditAction AuditAction { get; set; }


        public static void CreateTrail(int key,string model,object before,object after, AuditAction action)
        {
            var compObjects = new CompareLogic { Config = { MaxDifferences = 99 } };
            var compResult = compObjects.Compare(before, after);
            var changes=new List<Audit>();
            foreach (var difference in compResult.Differences)
            {
                var delta = new Audit();
                if (difference.PropertyName.Substring(0, 1) == ".")
                    delta.FieldName = difference.PropertyName.Substring(1, difference.PropertyName.Length - 1);
                delta.ValueBefore = difference.Object1Value;
                delta.ValueAfter = difference.Object2Value;
                changes.Add(delta);
            }
            using (var dbcontext=new AuditDbContext())
            {
                dbcontext.AuditTrails.Add(new AuditTrail
                {
                    AuditAction = action,
                    KeyField = key,
                    DataModel = model,
                    ValueBefore = JsonConvert.SerializeObject(before) ,
                    ValueAfter =JsonConvert.SerializeObject(after),
                    Changes =JsonConvert.SerializeObject(changes),
                    TimeStamp = DateTime.Now
                });
                dbcontext.SaveChanges();
            }
        }
    }

    public enum AuditAction:byte
    {
        Create=1,
        Update =2,
        Delete =3
    }
}
