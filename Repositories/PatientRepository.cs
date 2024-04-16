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
                                Pesel = Convert.ToInt32(reader["Pesel"]),
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
    }
}

