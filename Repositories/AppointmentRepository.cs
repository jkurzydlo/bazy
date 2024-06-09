using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.Json;
using bazy1;

namespace bazy1.Repositories
{
    public class AppointmentRepository : RepositoryBase
    {
        private string connectionString;

        private DoctorRepository doctorRepository = new();       
        public List<Appointment> GetAppointmentsByPatientId(int id) {

			List<Appointment> appointments = new List<Appointment>();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "SELECT DISTINCT a.id,a.date,a.goal, a.patient_ID,a.doctor_id  FROM appointment a join patient p where a.patient_id=@id && !p.deleted";
                    MySqlCommand cmd = new();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@id", id);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Appointment appointment = new Appointment()
							{
								Id = Convert.ToInt32(reader["Id"]),
								Date = reader.GetDateTime("date"),
								Goal = reader["Goal"].ToString(),
								PatientId = Convert.ToInt32(reader["Patient_id"]),
								DoctorId = reader.GetInt32("doctor_id"),
								Doctor = doctorRepository.GetById(reader.GetInt32("doctor_id"))

							};
							appointments.Add(appointment);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie appointments z bazy: " + ex.Message);
			}
			return appointments;
		}

		public Appointment GetAppointmentById(int id) {

            Appointment appointment = new();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "SELECT * FROM appointment where id=@id";
					MySqlCommand cmd = new();
					cmd.Connection = conn;
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@id", id);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Appointment tempAppointment = new Appointment()
							{
								Id = Convert.ToInt32(reader["Id"]),
								Date = reader.GetDateTime("date"),
								Goal = reader["Goal"].ToString(),
								PatientId = Convert.ToInt32(reader["Patient_id"]),
								DoctorId = reader.GetInt32("doctor_id"),
								Doctor = doctorRepository.GetById(reader.GetInt32("doctor_id"))

							};
                            appointment = tempAppointment;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie appointments z bazy: " + ex.Message);
			}
            return appointment;
		}


		public List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM appointment";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Date = reader.GetDateTime("date"),
                                Goal = reader["Goal"].ToString(),
                                PatientId = Convert.ToInt32(reader["Patient_id"]),
                                DoctorId = reader.GetInt32("doctor_id"),
                                Doctor = doctorRepository.GetById(reader.GetInt32("doctor_id"))

                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sciaganie appointments z bazy: " + ex.Message);
            }
            return appointments;
        }
        public bool AddAppointment(Appointment appointment)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO appointments (DateTime, Goal, NotificationId, PatientId) " +
                                   "VALUES (@DateTime, @Goal, @NotificationId, @PatientId)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("@DateTime", appointment.DateTime);
                    cmd.Parameters.AddWithValue("@Goal", appointment.Goal);
                   // cmd.Parameters.AddWithValue("@NotificationId", appointment.NotificationId);
                    cmd.Parameters.AddWithValue("@PatientId", appointment.PatientId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error dodawanie appointment do bazy: " + ex.Message);
                return false;
            }
        }

        public void RemoveAppointmentById(int id) {

			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "DELETE FROM appointment where id=@id";
					MySqlCommand cmd = new();
					cmd.Connection = conn;
					cmd.CommandText = query;
					cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie appointments z bazy: " + ex.Message);
			}
		}

        public List<DateTime> GetAvailableAppointments(int doctorId, DateTime date)
        {
            // Pobierz godziny pracy lekarza
            var workhours = DatabaseService.getDbContext().Workhours.Where(w => w.DoctorId == doctorId).ToList();

            List<DateTime> availableAppointments = new List<DateTime>();

            foreach (var workhour in workhours)
            {
                /*
                if (workhour.DayOfWeek == date.DayOfWeek)
                {
                    DateTime start = date.Date.Add(workhour.Start.Value.TimeOfDay);
                    DateTime end = date.Date.Add(workhour.End.Value.TimeOfDay);

                    while (start < end)
                    {
                        // Sprawdź, czy termin jest już zajęty
                       // if (!DatabaseService.getDbContext().Appointments.Any(a => DateTime.Parse(a.DateTime) == start))
                       // {
                       //     availableAppointments.Add(start);
                       // }

                        // Przejdź do następnego możliwego terminu wizyty
                        start = start.AddMinutes(20);
                    }
                }*/
            }

            return availableAppointments;
        }
        public int GetReminderTimeBeforeAppointment()
        {
            int reminderTime = 24; // domyślna wartość

            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT reminder_time_before_appointment FROM reminder_settings LIMIT 1";

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        reminderTime = Convert.ToInt32(result);
                    }
                }
            }

            return reminderTime;
        }
    }

}

