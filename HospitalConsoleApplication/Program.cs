// See https://aka.ms/new-console-template for more information


using HospitalConsoleApplication;

List<Patient> patients;
List<Doctor> doctors;

// doctors.Add(new Doctor("doc", "23214", "doc@gmail.com", "asdij", 24234));
// patients.Add(new Patient("alex", "123", "alex@gmail.com", "nicholas av", 098334, doctors.First()));

// FileManager.SavePatients(patients);
// FileManager.SaveDoctors(doctors);

doctors = FileManager.LoadDoctors();
patients = FileManager.LoadPatients(doctors);

Console.WriteLine(doctors.Count);
Console.WriteLine(patients.Count);