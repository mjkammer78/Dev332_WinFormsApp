﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrescriptionManager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="ContosoMedicalDB")]
	public partial class ContosoMedicalDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPrescription(Prescription instance);
    partial void UpdatePrescription(Prescription instance);
    partial void DeletePrescription(Prescription instance);
    partial void InsertPatient(Patient instance);
    partial void UpdatePatient(Patient instance);
    partial void DeletePatient(Patient instance);
    #endregion
		
		public ContosoMedicalDataClassesDataContext() : 
				base(global::PrescriptionManager.Properties.Settings.Default.ContosoMedicalDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ContosoMedicalDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContosoMedicalDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContosoMedicalDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ContosoMedicalDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Prescription> Prescriptions
		{
			get
			{
				return this.GetTable<Prescription>();
			}
		}
		
		public System.Data.Linq.Table<Patient> Patients
		{
			get
			{
				return this.GetTable<Patient>();
			}
		}
	}
	
	[Table(Name="dbo.Prescriptions")]
	public partial class Prescription : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PrescriptionID;
		
		private int _PatientID;
		
		private string _Description;
		
		private System.DateTime _IssueDate;
		
		private int _RepeatCount;
		
		private EntityRef<Patient> _Patient;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPrescriptionIDChanging(int value);
    partial void OnPrescriptionIDChanged();
    partial void OnPatientIDChanging(int value);
    partial void OnPatientIDChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIssueDateChanging(System.DateTime value);
    partial void OnIssueDateChanged();
    partial void OnRepeatCountChanging(int value);
    partial void OnRepeatCountChanged();
    #endregion
		
		public Prescription()
		{
			this._Patient = default(EntityRef<Patient>);
			OnCreated();
		}
		
		[Column(Storage="_PrescriptionID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PrescriptionID
		{
			get
			{
				return this._PrescriptionID;
			}
			set
			{
				if ((this._PrescriptionID != value))
				{
					this.OnPrescriptionIDChanging(value);
					this.SendPropertyChanging();
					this._PrescriptionID = value;
					this.SendPropertyChanged("PrescriptionID");
					this.OnPrescriptionIDChanged();
				}
			}
		}
		
		[Column(Storage="_PatientID", DbType="Int NOT NULL")]
		public int PatientID
		{
			get
			{
				return this._PatientID;
			}
			set
			{
				if ((this._PatientID != value))
				{
					if (this._Patient.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPatientIDChanging(value);
					this.SendPropertyChanging();
					this._PatientID = value;
					this.SendPropertyChanged("PatientID");
					this.OnPatientIDChanged();
				}
			}
		}
		
		[Column(Storage="_Description", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[Column(Storage="_IssueDate", DbType="DateTime NOT NULL")]
		public System.DateTime IssueDate
		{
			get
			{
				return this._IssueDate;
			}
			set
			{
				if ((this._IssueDate != value))
				{
					this.OnIssueDateChanging(value);
					this.SendPropertyChanging();
					this._IssueDate = value;
					this.SendPropertyChanged("IssueDate");
					this.OnIssueDateChanged();
				}
			}
		}
		
		[Column(Storage="_RepeatCount", DbType="Int NOT NULL")]
		public int RepeatCount
		{
			get
			{
				return this._RepeatCount;
			}
			set
			{
				if ((this._RepeatCount != value))
				{
					this.OnRepeatCountChanging(value);
					this.SendPropertyChanging();
					this._RepeatCount = value;
					this.SendPropertyChanged("RepeatCount");
					this.OnRepeatCountChanged();
				}
			}
		}
		
		[Association(Name="Patient_Prescription", Storage="_Patient", ThisKey="PatientID", IsForeignKey=true)]
		public Patient Patient
		{
			get
			{
				return this._Patient.Entity;
			}
			set
			{
				Patient previousValue = this._Patient.Entity;
				if (((previousValue != value) 
							|| (this._Patient.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Patient.Entity = null;
						previousValue.Prescriptions.Remove(this);
					}
					this._Patient.Entity = value;
					if ((value != null))
					{
						value.Prescriptions.Add(this);
						this._PatientID = value.PatientID;
					}
					else
					{
						this._PatientID = default(int);
					}
					this.SendPropertyChanged("Patient");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.Patients")]
	public partial class Patient : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PatientID;
		
		private string _FirstName;
		
		private string _LastName;
		
		private char _Gender;
		
		private System.DateTime _DateOfBirth;
		
		private EntitySet<Prescription> _Prescriptions;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPatientIDChanging(int value);
    partial void OnPatientIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnGenderChanging(char value);
    partial void OnGenderChanged();
    partial void OnDateOfBirthChanging(System.DateTime value);
    partial void OnDateOfBirthChanged();
    #endregion
		
		public Patient()
		{
			this._Prescriptions = new EntitySet<Prescription>(new Action<Prescription>(this.attach_Prescriptions), new Action<Prescription>(this.detach_Prescriptions));
			OnCreated();
		}
		
		[Column(Storage="_PatientID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PatientID
		{
			get
			{
				return this._PatientID;
			}
			set
			{
				if ((this._PatientID != value))
				{
					this.OnPatientIDChanging(value);
					this.SendPropertyChanging();
					this._PatientID = value;
					this.SendPropertyChanged("PatientID");
					this.OnPatientIDChanged();
				}
			}
		}
		
		[Column(Storage="_FirstName", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[Column(Storage="_LastName", DbType="NVarChar(30) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[Column(Storage="_Gender", DbType="Char(1) NOT NULL")]
		public char Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		[Column(Storage="_DateOfBirth", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfBirth
		{
			get
			{
				return this._DateOfBirth;
			}
			set
			{
				if ((this._DateOfBirth != value))
				{
					this.OnDateOfBirthChanging(value);
					this.SendPropertyChanging();
					this._DateOfBirth = value;
					this.SendPropertyChanged("DateOfBirth");
					this.OnDateOfBirthChanged();
				}
			}
		}
		
		[Association(Name="Patient_Prescription", Storage="_Prescriptions", OtherKey="PatientID")]
		public EntitySet<Prescription> Prescriptions
		{
			get
			{
				return this._Prescriptions;
			}
			set
			{
				this._Prescriptions.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Prescriptions(Prescription entity)
		{
			this.SendPropertyChanging();
			entity.Patient = this;
		}
		
		private void detach_Prescriptions(Prescription entity)
		{
			this.SendPropertyChanging();
			entity.Patient = null;
		}
	}
}
#pragma warning restore 1591
