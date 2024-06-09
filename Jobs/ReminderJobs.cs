using bazy1.Repositories;
using MySql.Data.MySqlClient;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace bazy1.Jobs
{
    public class ReminderJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Wywołaj metodę do wysyłania przypomnień
            await SendReminders();
        }
        public ReminderJob() { }

        private async Task SendReminders()
        {

            await Console.Out.WriteLineAsync("Wysyłanie...");

            int reminderTime = new AppointmentRepository().GetReminderTimeBeforeAppointment();

            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT a.id, p.phoneNumber, p.email, a.date 
                        FROM appointments a
                        JOIN patient p ON a.patient_id = p.id
                        WHERE a.date > NOW() 
                        AND a.date <= DATE_ADD(NOW(), INTERVAL @reminderTime HOUR)";

                    command.Parameters.AddWithValue("@reminderTime", reminderTime);
                    await Console.Out.WriteLineAsync("rt: "+reminderTime.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        await Console.Out.WriteLineAsync("czyt");
                        while (reader.Read())
                        {
                            int appointmentId = reader.GetInt32("id");
                            string phone = reader["phoneNumber"] as string;
							string email = reader["email"] as string;
                            DateTime appointmentDate = reader.GetDateTime("appointment_date");

                            string message = $"Przypomnienie o wizycie: {appointmentDate}";

                            if (!string.IsNullOrEmpty(phone))
                            {
                                // Wyślij SMS (np. używając Twilio)
                                await SendSms(phone, message);
                            }
                            else if (!string.IsNullOrEmpty(email))
                            {
                                // Wyślij email
                                await SendEmail(email, "Przypomnienie o wizycie", message);
                            }
                            await Console.Out.WriteLineAsync(message);
                        }
                    }
                }
            }
        }

        private Task SendSms(string phoneNumber, string message)
        {
            const string accountSid = "AC1e9328bff47258fd271efdc0ba9238ac";
            const string authToken = "d1ab6658d40a407287ca18e180307e29";
            const string fromPhoneNumber = "+15672457041";

            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber))
            {
                From = new PhoneNumber(fromPhoneNumber),
                Body = message
            };

            var messageResource = MessageResource.Create(messageOptions);
            Console.WriteLine($"Wysyłanie SMS na numer: {phoneNumber}, Treść: {message}, SID: {messageResource.Sid}");

            return Task.CompletedTask;
        }

        private Task SendEmail(string email, string subject, string message)
        {
            // Implementacja wysyłania emaili (SMTP)
            Console.WriteLine($"Wysyłanie emaila na adres: {email}, Temat: {subject}, Treść: {message}");
            return Task.CompletedTask;
        }

        private MySqlConnection GetConnection()
        {
            // Implementacja połączenia z bazą danych
            string connectionString = "Server=localhost;Database=przychodnia9;Uid=root;Pwd=12345";
            return new MySqlConnection(connectionString);
        }
    }
}
