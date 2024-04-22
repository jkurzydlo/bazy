using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace bazy1.Repositories
{
    public class PatientRepository
    {
        private string connectionString;

        public PatientRepository()
        {
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            try
            {
                string json = File.ReadAllText("dbinfo.json");
                dynamic jsonObj = JsonSerializer.Deserialize<dynamic>(json);
                connectionString = jsonObj["ConnectionStrings"]["BazaPrzychodnia"];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading database connection string: " + ex.Message);
            }
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM pacjenci";
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
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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


