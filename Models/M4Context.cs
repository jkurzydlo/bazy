﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bazy1.Models;

public partial class M4Context : DbContext
{
    public M4Context()
    {
    }

    public M4Context(DbContextOptions<M4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Receptionist> Receptionists { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workhour> Workhours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=m4;Uid=root;Pwd=12345;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BuildingNumber)
                .HasMaxLength(45)
                .HasColumnName("buildingNumber");
            entity.Property(e => e.City)
                .HasMaxLength(45)
                .HasColumnName("city");
            entity.Property(e => e.Street)
                .HasMaxLength(45)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId }).HasName("PRIMARY");

            entity.ToTable("administrator");

            entity.HasIndex(e => e.UserId, "fk_Administrator_User1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("phoneNumber");

            entity.HasOne(d => d.User).WithMany(p => p.Administrators)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Administrator_User1");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.NotificationId, e.PatientId }).HasName("PRIMARY");

            entity.ToTable("appointment");

            entity.HasIndex(e => e.PatientId, "fk_Appointment_Patient1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NotificationId).HasColumnName("Notification_id");
            entity.Property(e => e.PatientId).HasColumnName("Patient_id");
            entity.Property(e => e.DateTime)
                .HasMaxLength(45)
                .HasColumnName("date");
            entity.Property(e => e.Goal)
                .HasMaxLength(45)
                .HasColumnName("goal");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Appointment_Patient1");

            entity.HasMany(d => d.Receptionists).WithMany(p => p.Appointments)
                .UsingEntity<Dictionary<string, object>>(
                    "AppointmentHasReceptionist",
                    r => r.HasOne<Receptionist>().WithMany()
                        .HasForeignKey("ReceptionistId", "ReceptionistUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Appointment_has_Receptionist_Receptionist1"),
                    l => l.HasOne<Appointment>().WithMany()
                        .HasForeignKey("AppointmentId", "AppointmentNotificationId", "AppointmentPatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Appointment_has_Receptionist_Appointment1"),
                    j =>
                    {
                        j.HasKey("AppointmentId", "AppointmentNotificationId", "AppointmentPatientId", "ReceptionistId", "ReceptionistUserId").HasName("PRIMARY");
                        j.ToTable("appointment_has_receptionist");
                        j.HasIndex(new[] { "AppointmentId", "AppointmentNotificationId", "AppointmentPatientId" }, "fk_Appointment_has_Receptionist_Appointment1_idx");
                        j.HasIndex(new[] { "ReceptionistId", "ReceptionistUserId" }, "fk_Appointment_has_Receptionist_Receptionist1_idx");
                        j.IndexerProperty<int>("AppointmentId").HasColumnName("Appointment_id");
                        j.IndexerProperty<int>("AppointmentNotificationId").HasColumnName("Appointment_Notification_id");
                        j.IndexerProperty<int>("AppointmentPatientId").HasColumnName("Appointment_Patient_id");
                        j.IndexerProperty<int>("ReceptionistId").HasColumnName("Receptionist_id");
                        j.IndexerProperty<int>("ReceptionistUserId").HasColumnName("Receptionist_User_id");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("disease");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments)
                .HasMaxLength(45)
                .HasColumnName("comments");
            entity.Property(e => e.DateFrom)
                .HasMaxLength(45)
                .HasColumnName("dateFrom");
            entity.Property(e => e.DateTo)
                .HasMaxLength(45)
                .HasColumnName("dateTo");
            entity.Property(e => e.IsEnded).HasColumnName("isEnded");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");

            entity.HasMany(d => d.Patients).WithMany(p => p.Diseases)
                .UsingEntity<Dictionary<string, object>>(
                    "DiseaseHasPatient",
                    r => r.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Disease_has_Patient_Patient1"),
                    l => l.HasOne<Disease>().WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Disease_has_Patient_Disease1"),
                    j =>
                    {
                        j.HasKey("DiseaseId", "PatientId").HasName("PRIMARY");
                        j.ToTable("disease_has_patient");
                        j.HasIndex(new[] { "DiseaseId" }, "fk_Disease_has_Patient_Disease1_idx");
                        j.HasIndex(new[] { "PatientId" }, "fk_Disease_has_Patient_Patient1_idx");
                        j.IndexerProperty<int>("DiseaseId").HasColumnName("Disease_id");
                        j.IndexerProperty<int>("PatientId").HasColumnName("Patient_id");
                    });
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId }).HasName("PRIMARY");

            entity.ToTable("doctor");

            entity.HasIndex(e => e.UserId, "fk_Doctor_User1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");

            entity.HasOne(d => d.User).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Doctor_User1");

            entity.HasMany(d => d.Patients).WithMany(p => p.Doctors)
                .UsingEntity<Dictionary<string, object>>(
                    "DoctorPatient",
                    r => r.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Doctor_has_Patient_Patient1"),
                    l => l.HasOne<Doctor>().WithMany()
                        .HasForeignKey("DoctorId", "DoctorUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Doctor_has_Patient_Doctor1"),
                    j =>
                    {
                        j.HasKey("DoctorId", "DoctorUserId", "PatientId").HasName("PRIMARY");
                        j.ToTable("doctor_patient");
                        j.HasIndex(new[] { "DoctorId", "DoctorUserId" }, "fk_Doctor_has_Patient_Doctor1_idx");
                        j.HasIndex(new[] { "PatientId" }, "fk_Doctor_has_Patient_Patient1_idx");
                        j.IndexerProperty<int>("DoctorId").HasColumnName("Doctor_id");
                        j.IndexerProperty<int>("DoctorUserId").HasColumnName("Doctor_User_id");
                        j.IndexerProperty<int>("PatientId").HasColumnName("Patient_id");
                    });

            entity.HasMany(d => d.Specializations).WithMany(p => p.Doctors)
                .UsingEntity<Dictionary<string, object>>(
                    "DoctorHasSpecialization",
                    r => r.HasOne<Specialization>().WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Doctor_has_Specialization_Specialization1"),
                    l => l.HasOne<Doctor>().WithMany()
                        .HasForeignKey("DoctorId", "DoctorUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Doctor_has_Specialization_Doctor1"),
                    j =>
                    {
                        j.HasKey("DoctorId", "DoctorUserId", "SpecializationId").HasName("PRIMARY");
                        j.ToTable("doctor_has_specialization");
                        j.HasIndex(new[] { "DoctorId", "DoctorUserId" }, "fk_Doctor_has_Specialization_Doctor1_idx");
                        j.HasIndex(new[] { "SpecializationId" }, "fk_Doctor_has_Specialization_Specialization1_idx");
                        j.IndexerProperty<int>("DoctorId").HasColumnName("Doctor_id");
                        j.IndexerProperty<int>("DoctorUserId").HasColumnName("Doctor_User_id");
                        j.IndexerProperty<int>("SpecializationId").HasColumnName("Specialization_id");
                    });
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medicine");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dose)
                .HasMaxLength(45)
                .HasColumnName("dose");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Patients).WithMany(p => p.Medicines)
                .UsingEntity<Dictionary<string, object>>(
                    "PatientMedicine",
                    r => r.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Medicine_has_Patient_Patient1"),
                    l => l.HasOne<Medicine>().WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Medicine_has_Patient_Medicine1"),
                    j =>
                    {
                        j.HasKey("MedicineId", "PatientId").HasName("PRIMARY");
                        j.ToTable("patient_medicine");
                        j.HasIndex(new[] { "MedicineId" }, "fk_Medicine_has_Patient_Medicine1_idx");
                        j.HasIndex(new[] { "PatientId" }, "fk_Medicine_has_Patient_Patient1_idx");
                        j.IndexerProperty<int>("MedicineId").HasColumnName("Medicine_id");
                        j.IndexerProperty<int>("PatientId").HasColumnName("Patient_id");
                    });
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.AppointmentId, e.AppointmentNotificationId, e.AppointmentPatientId }).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => new { e.AppointmentId, e.AppointmentNotificationId, e.AppointmentPatientId }, "fk_Notification_Appointment1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("Appointment_id");
            entity.Property(e => e.AppointmentNotificationId).HasColumnName("Appointment_Notification_id");
            entity.Property(e => e.AppointmentPatientId).HasColumnName("Appointment_Patient_id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Hour)
                .HasColumnType("time")
                .HasColumnName("hour");
            entity.Property(e => e.IsSent).HasColumnName("isSent");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Notifications)
                .HasForeignKey(d => new { d.AppointmentId, d.AppointmentNotificationId, d.AppointmentPatientId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notification_Appointment1");
        });



        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("office");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("phoneNumber");

            entity.HasMany(d => d.Addresses).WithMany(p => p.Patients)
                .UsingEntity<Dictionary<string, object>>(
                    "PatientAddress",
                    r => r.HasOne<Address>().WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Patient_has_Address_Address1"),
                    l => l.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Patient_has_Address_Patient1"),
                    j =>
                    {
                        j.HasKey("PatientId", "AddressId").HasName("PRIMARY");
                        j.ToTable("patient_address");
                        j.HasIndex(new[] { "AddressId" }, "fk_Patient_has_Address_Address1_idx");
                        j.HasIndex(new[] { "PatientId" }, "fk_Patient_has_Address_Patient1_idx");
                        j.IndexerProperty<int>("PatientId").HasColumnName("Patient_id");
                        j.IndexerProperty<int>("AddressId").HasColumnName("Address_id");
                    });
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.PatientId }).HasName("PRIMARY");

            entity.ToTable("prescription");

            entity.HasIndex(e => e.PatientId, "fk_Prescription_Patient1_idx");

            entity.HasIndex(e => e.Id, "idRecepty_UNIQUE").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.PatientId).HasColumnName("Patient_id");
            entity.Property(e => e.Code)
                .HasMaxLength(45)
                .HasColumnName("code");
            entity.Property(e => e.DateOfPrescription)
                .HasColumnType("date")
                .HasColumnName("dateOfPrescription");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expirationDate");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Prescription_Patient1");

            entity.HasMany(d => d.Medicines).WithMany(p => p.Prescriptions)
                .UsingEntity<Dictionary<string, object>>(
                    "PrescriptionMedicine",
                    r => r.HasOne<Medicine>().WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Prescription_has_Medicine_Medicine1"),
                    l => l.HasOne<Prescription>().WithMany()
                        .HasForeignKey("PrescriptionId", "PrescriptionPatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Prescription_has_Medicine_Prescription1"),
                    j =>
                    {
                        j.HasKey("PrescriptionId", "PrescriptionPatientId", "MedicineId").HasName("PRIMARY");
                        j.ToTable("prescription_medicine");
                        j.HasIndex(new[] { "MedicineId" }, "fk_Prescription_has_Medicine_Medicine1_idx");
                        j.HasIndex(new[] { "PrescriptionId", "PrescriptionPatientId" }, "fk_Prescription_has_Medicine_Prescription1_idx");
                        j.IndexerProperty<int>("PrescriptionId").HasColumnName("Prescription_id");
                        j.IndexerProperty<int>("PrescriptionPatientId").HasColumnName("Prescription_Patient_id");
                        j.IndexerProperty<int>("MedicineId").HasColumnName("Medicine_id");
                    });
        });

        modelBuilder.Entity<Receptionist>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId }).HasName("PRIMARY");

            entity.ToTable("receptionist");

            entity.HasIndex(e => e.UserId, "fk_Receptionist_User1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");

            entity.HasOne(d => d.User).WithMany(p => p.Receptionists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Receptionist_User1");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("specialization");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstLogin)
                .HasDefaultValueSql("'1'")
                .HasColumnName("firstLogin");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
            entity.Property(e => e.Type)
                .HasColumnType("enum('recepcjonista','admin','lekarz')")
                .HasColumnName("type");
        });

        modelBuilder.Entity<Workhour>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.DoctorId, e.DoctorUserId, e.ReceptionistId, e.ReceptionistUserId }).HasName("PRIMARY");

            entity.ToTable("workhours");

            entity.HasIndex(e => new { e.DoctorId, e.DoctorUserId }, "fk_WorkHours_Doctor1_idx");

            entity.HasIndex(e => new { e.ReceptionistId, e.ReceptionistUserId }, "fk_WorkHours_Receptionist1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_id");
            entity.Property(e => e.DoctorUserId).HasColumnName("Doctor_User_id");
            entity.Property(e => e.ReceptionistId).HasColumnName("Receptionist_id");
            entity.Property(e => e.ReceptionistUserId).HasColumnName("Receptionist_User_id");
            entity.Property(e => e.End)
                .HasColumnType("datetime(5)")
                .HasColumnName("end");
            entity.Property(e => e.Start)
                .HasColumnType("datetime(5)")
                .HasColumnName("start");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Workhours)
                .HasForeignKey(d => new { d.DoctorId, d.DoctorUserId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WorkHours_Doctor1");

            entity.HasOne(d => d.Receptionist).WithMany(p => p.Workhours)
                .HasForeignKey(d => new { d.ReceptionistId, d.ReceptionistUserId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WorkHours_Receptionist1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
