using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2025.DatabaseSets
{
    public class PatientData : DbContext
    {
        public PatientData() : base("OODExam_IvanMarchenko") { }

        public DbSet<Models.Patient> Patients { get; set; }
        public DbSet<Models.Appointment> Appointments { get; set; }
    }
}
