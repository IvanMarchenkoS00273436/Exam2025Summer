using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Exam2025.DatabaseSets;
using Exam2025.Models;

namespace Exam2025
{
    public partial class MainWindow : Window
    {
        private PatientData _context = new PatientData();
        private List<Patient> _patients;
        private List<Appointment> _appointments;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the database
            //InitializeDatabase();

            LoadData();
        }

        public void InitializeDatabase()
        {
            // Initialize the database context
            using (var context = new PatientData())
            {
                Patient p1 = new Patient()
                {
                    FirstName = "John",
                    Surname = "Smith",
                    DOB = new DateTime(2000, 01, 31),
                    ContactNumber = "086 123 4567"
                };

                Patient p2 = new Patient()
                {
                    FirstName = "Mary",
                    Surname = "Jones",
                    DOB = new DateTime(1980, 06, 15),
                    ContactNumber = "087 323 2585"
                };

                Patient p3 = new Patient()
                {
                    FirstName = "Jim",
                    Surname = "Ryan",
                    DOB = new DateTime(2005, 03, 10),
                    ContactNumber = "086 568 7896"
                };

                // Add the patients to the context
                context.Patients.Add(p1);
                context.Patients.Add(p2);
                context.Patients.Add(p3);

                // Save changes to the database
                context.SaveChanges();
            }
        }
    
        public void LoadData()
        {
            var query = from p in _context.Patients
                        select p;
            _patients = query.ToList();

            PatientsListBox.ItemsSource = _patients;
        }

        private void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from the input fields
            string firstName = PatientsFirstName.Text;
            string surname = PatientsSurname.Text;
            DateTime dob = PatientsDateOfBirth.SelectedDate.Value;
            string contactNumber = PatientsPhoneNumber.Text;

            // New patient object
            Patient newPatient = new Patient()
            {
                FirstName = firstName,
                Surname = surname,
                DOB = dob,
                ContactNumber = contactNumber
            };

            // Add the new patient to the database
            _context.Patients.Add(newPatient);
            _context.SaveChanges();

            // Update listbox
            LoadData();
        }
    }
}
