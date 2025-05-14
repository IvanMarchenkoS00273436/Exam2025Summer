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
using Exam2025.Views;

namespace Exam2025
{
    public partial class MainWindow : Window
    {
        private PatientData _context = new PatientData();
        private List<Patient> _patients;
        private List<Appointment> _appointments;
        private Patient _selectedPatient;
        private int countAppointments = 0;

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
            var queryPatients = from p in _context.Patients
                        select p;
            _patients = queryPatients.ToList();
            PatientsListBox.ItemsSource = _patients;

            if(_selectedPatient != null)
            {
                var queryAppointments = from a in _context.Appointments
                                        where a.PatientId == _selectedPatient.PatientId
                                        select a;
                _appointments = queryAppointments.ToList();
                countAppointments = _appointments.Count;
                AppointmentsListBox.ItemsSource = _appointments;
            }
            
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

        private void AddAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatient = PatientsListBox.SelectedItem as Patient;
            if (selectedPatient != null)
            {
                // Open the AddAppointment window
                AddAppointment addAppointmentWindow = new AddAppointment(_context, selectedPatient);
                addAppointmentWindow.ShowDialog();
                addAppointmentWindow.Owner = this;

                // Refresh the appointments list
                LoadData();
            }
            else
            {
                MessageBox.Show("Please select a patient to add an appointment.");
            }
        }

        private void PatientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPatient = PatientsListBox.SelectedItem as Patient;
            LoadData();
        }

        private void EditAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppointment = AppointmentsListBox.SelectedItem as Appointment;
            if (selectedAppointment != null) 
            { 

            }
        }
    }
}
