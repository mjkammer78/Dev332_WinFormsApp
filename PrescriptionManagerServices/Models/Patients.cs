using System;
using System.Collections.Generic;

namespace PrescriptionManagerServices.Models
{
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
            Prescriptions = new HashSet<Prescriptions>();
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Photo { get; set; }

        public ICollection<Appointments> Appointments { get; set; }
        public ICollection<Prescriptions> Prescriptions { get; set; }
    }
}
