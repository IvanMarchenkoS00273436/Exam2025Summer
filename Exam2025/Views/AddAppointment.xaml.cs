﻿using Exam2025.DatabaseSets;
using Exam2025.Models;
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
using System.Windows.Shapes;

namespace Exam2025.Views
{
    public partial class AddAppointment : Window
    {
        private PatientData _context;
        private Patient _patient;

        public AddAppointment(PatientData context, Patient patient)
        {
            InitializeComponent();

            _context = context;
            _patient = patient;
        }

        private void addAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from the input fields
            DateTime appDate = appointmentDatePicker.SelectedDate.Value;
            DateTime appTime = appointmentTimePicker.SelectedTime.Value;
            string appNotes = appointmentNotesTextBox.Text;

            // Create a new appointment object
            Appointment appointment = new Appointment()
            {
                AppointmentDate = appDate,
                AppointmentTime = appTime,
                AppointmentNotes = appNotes,
                PatientId = _patient.PatientId
            };

            // Add the appointment to the database
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            MessageBox.Show("Appointment added successfully!");

            // Close the window
            this.Close();
        }
    }
}
