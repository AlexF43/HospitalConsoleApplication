// See https://aka.ms/new-console-template for more information


using HospitalConsoleApplication;

List<Patient> patients = new List<Patient>();
List<Doctor> doctors = new List<Doctor>();

doctors.Add(new Doctor("doc", "23214", "doc@gmail.com", "asdij", 24234));
patients.Add(new Patient("alex", "123", "alex@gmail.com", "nicholas av", 098334, doctors.First()));

FileManager.SavePatients(patients);
FileManager.SaveDoctors(doctors);