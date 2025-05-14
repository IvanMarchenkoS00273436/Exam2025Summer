using Exam2025.DatabaseSets;
using Exam2025.Models;
using System;
using System.Windows;

namespace Exam2025.Views
{
    public partial class EditAppointment : Window
    {
        private PatientData _context;
        private Appointment _appointment;

        public EditAppointment(PatientData context, Appointment appointment)
        {
            InitializeComponent();
            _context = context;
            _appointment = appointment;

            // Populate fields with existing appointment data
            appointmentDatePicker.SelectedDate = _appointment.AppointmentDate;
            appointmentTimePicker.SelectedTime = _appointment.AppointmentTime;
            appointmentNotesTextBox.Text = _appointment.AppointmentNotes;
        }

        private void UpdateAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a date for the appointment.");
                return;
            }

            if (appointmentTimePicker.SelectedTime == null)
            {
                MessageBox.Show("Please select a time for the appointment.");
                return;
            }

            // Update appointment with new values
            _appointment.AppointmentDate = appointmentDatePicker.SelectedDate.Value;
            _appointment.AppointmentTime = appointmentTimePicker.SelectedTime.Value;
            _appointment.AppointmentNotes = appointmentNotesTextBox.Text;

            // Save changes to the database
            _context.SaveChanges();

            // Close the window
            DialogResult = true;
            Close();
        }

        private void DeleteAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // Ask for confirmation before deleting
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Remove the appointment from the database
                _context.Appointments.Remove(_appointment);
                _context.SaveChanges();

                // Close the window
                DialogResult = true;
                Close();
            }
        }
    }
}
