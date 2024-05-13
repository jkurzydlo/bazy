using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace bazy1.Repositories
{
    public class PatientRepository : RepositoryBase
    {
        public PatientRepository() : base() // Wywołaj konstruktor klasy bazowej, aby ustawić połączenie
        {
        }

        public List<Disease> GetPatientDiseases(int patientId)
        {
            List<Disease> patientDiseases = new List<Disease>();
            try
            {
                using (MySqlConnection conn = GetConnection()) // Użyj połączenia zdefiniowanego w klasie bazowej
                {
                    conn.Open();
                    string query = @"SELECT d.* 
                             FROM disease d
                             JOIN patient_disease pd ON d.Id = pd.Disease_id
                             WHERE pd.Patient_id = @PatientId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PatientId", patientId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Disease disease = new Disease
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                DateFrom = Convert.ToDateTime(reader["dateFrom"]),
                                DateTo = Convert.ToDateTime(reader["dateTo"])
                                // można dodać inne pola...
                            };
                            patientDiseases.Add(disease);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving patient diseases from database: " + ex.Message);
            }
            return patientDiseases;
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            try
            {
                using (MySqlConnection conn = GetConnection()) // Użyj połączenia zdefiniowanego w klasie bazowej
                {
                    conn.Open();
                    string query = "SELECT * FROM patient";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Surname = reader["Surname"].ToString(),
                                Pesel = reader["Pesel"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                            patients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving patients from database: " + ex.Message);
            }
            return patients;
        }

        public bool AddPatient(Patient patient)
        {
            try
            {
                using (MySqlConnection conn = GetConnection()) // Użyj połączenia zdefiniowanego w klasie bazowej
                {
                    conn.Open();
                    string query = "INSERT INTO pacjenci (Name, Surname, Pesel, PhoneNumber, Email) VALUES (@Name, @Surname, @Pesel, @PhoneNumber, @Email)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", patient.Name);
                    cmd.Parameters.AddWithValue("@Surname", patient.Surname);
                    cmd.Parameters.AddWithValue("@Pesel", patient.Pesel);
                    cmd.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", patient.Email);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding patient to the database: " + ex.Message);
                return false;
            }
        }
    }
}



