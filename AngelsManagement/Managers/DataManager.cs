using AngelsManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AngelsManagement.Globals;
namespace AngelsManagement
{
    public class DataManager
    {
        public Dictionary<String, ObservableCollection<Volunteer>> VolunteersDict { get; set; }
        public Dictionary<String, ObservableCollection<Student>> StudentsDict { get; set; }
        public Dictionary<String, ObservableCollection<Guardian>> GuardiansDict { get; set; }

        public DataManager()
        {
            //create app data directory (it won't if it already exists)
            Directory.CreateDirectory(appDataFolderPath);

            //perform first migration
            using (PeopleContext context = new PeopleContext())
            {
                context.Database.Migrate();
                
            }
            CreateDictionaries();
            GetPeopleData();
        }

        private void CreateDictionaries()
        {
            VolunteersDict = new Dictionary<string, ObservableCollection<Volunteer>>();
            StudentsDict = new Dictionary<string, ObservableCollection<Student>>();
            GuardiansDict = new Dictionary<string, ObservableCollection<Guardian>>();

        }

        //for each dictionary (volunteers, students, guardians)
        //creates entries <city, list with volunteers/students/guardians>
        private void GetPeopleData()
        {
            List<Volunteer> volunteers;
            List<Student> students;
            List<Guardian> guardians;

            using (var ctx = new PeopleContext())
            {
                //----------volunteers
                //for each city add volunteers to the dictionary
                foreach(string city in Cities)
                {
                    volunteers = ctx.Volunteers
                        .Where(v => v.City.Equals(city)).ToList();

                    if (VolunteersDict.ContainsKey(city))
                    {
                        VolunteersDict[city].Clear();
                    }
                    else
                    {
                        VolunteersDict[city] = 
                            new ObservableCollection<Volunteer>();

                    }

                    foreach (Volunteer volunteer in volunteers)
                    {
                        VolunteersDict[city].Add(volunteer);
                    }
                }

                //---------students
                foreach (string city in Cities)
                {
                    students = ctx.Students
                        .Where(s => s.City.Equals(city)).ToList();

                    if (StudentsDict.ContainsKey(city))
                    {
                        StudentsDict[city].Clear();
                    }
                    else
                    {
                        StudentsDict[city] = new ObservableCollection<Student>();

                    }

                    foreach (Student student in students)
                    {
                        StudentsDict[city].Add(student);
                    }
                    
                }

                //---------guardians
                foreach (string city in Cities)
                {
                    guardians = ctx.Guardians
                        .Where(p => p.City.Equals(city)).ToList();

                    if (GuardiansDict.ContainsKey(city))
                    {
                        GuardiansDict[city].Clear();
                    }
                    else
                    {
                        GuardiansDict[city] = new ObservableCollection<Guardian>();

                    }

                    foreach (Guardian guardian in guardians)
                    {
                        GuardiansDict[city].Add(guardian);
                    }

                }
            }
        }

        //adds guardian to the database
        //[IMPORTANT] sets guardian's id field (guardianId) (operations on ctx change guardian variable)
        //then creates relation between student and guardian
        //NB: function requires student id field to be set
        public void AddGuardianToStudent(Student student, Guardian guardian)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Guardians.Add(guardian);
                ctx.SaveChanges();
            }

            //adding new guardian to observable collection
            //in correct city
            GuardiansDict[guardian.City].Add(guardian);

            //adding connection between student and guardian
            StuPar stuPar;

            using (var ctx = new PeopleContext())
            {
                stuPar = new StuPar
                {
                    StudentId = student.StudentId,
                    GuardianId = guardian.GuardianId
                };
                ctx.Add(stuPar);

                ctx.SaveChanges();
            }
        }

        public void AddVolunteer(Volunteer volunteer)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Volunteers.Add(volunteer);
                ctx.SaveChanges();
            }

            //adding new volunteer to observable collection
            //in correct city
            VolunteersDict[volunteer.City].Add(volunteer);

        }
        public void UpdateVolunteer(string oldVolunteersCity, Volunteer updatedVolunteer)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Volunteers.Update(updatedVolunteer);
                ctx.SaveChanges();
            }

            //update volunteer in observable collection
            //in correct city

            //remove volunteer with outdated info from observable collection
            var oldVolunteer = VolunteersDict[oldVolunteersCity]
                .FirstOrDefault(v => v.VolunteerId == updatedVolunteer.VolunteerId);
            VolunteersDict[oldVolunteersCity].Remove(oldVolunteer);

            //add volunteer to observable collection of their current city
            VolunteersDict[updatedVolunteer.City].Add(updatedVolunteer);
        }

        public void DeleteVolunteer(Volunteer volunteer)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Volunteers.Remove(volunteer);
                ctx.SaveChanges();
            }

            //remove volunteer from observable collection
            VolunteersDict[volunteer.City].Remove(volunteer);
        }

        public void AddStudent(Student student)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Students.Add(student);
                ctx.SaveChanges();
            }

            //adding new volunteer to observable collection
            StudentsDict[student.City].Add(student);

        }

        public void UpdateStudent(string oldStudentsCity, Student updatedStudent)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Students.Update(updatedStudent);
                ctx.SaveChanges();
            }

            //update student in observable collection
            //in correct city

            //remove student with outdated info from observable collection
            var oldStudent = StudentsDict[oldStudentsCity]
                .FirstOrDefault(s => s.StudentId == updatedStudent.StudentId);
            StudentsDict[oldStudentsCity].Remove(oldStudent);

            //add student to observable collection of their current city
            StudentsDict[updatedStudent.City].Add(updatedStudent);
        }

        public void DeleteStudent(Student student)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Students.Remove(student);
                ctx.SaveChanges();
            }

            //remove student from observable collection
            StudentsDict[student.City].Remove(student);
        }


        public void UpdateGuardian(string oldGuardianCity, Guardian updatedGuardian)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Guardians.Update(updatedGuardian);
                ctx.SaveChanges();
            }

            //update guardian in observable collection
            //in correct city

            //remove guardian with outdated info from observable collection
            var oldGuardian = GuardiansDict[oldGuardianCity]
                .FirstOrDefault(g => g.GuardianId == updatedGuardian.GuardianId);
            GuardiansDict[oldGuardianCity].Remove(oldGuardian);

            //add guardian to observable collection of their current city
            GuardiansDict[updatedGuardian.City].Add(updatedGuardian);
        }

        public void DeleteGuardian(Guardian guardian)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Guardians.Remove(guardian);
                ctx.SaveChanges();
            }

            //remove guardian from observable collection
            GuardiansDict[guardian.City].Remove(guardian);
        }

        //returns a list of students that volunteer from parameter has
        public List<Student> GetVolunteerStudents(Volunteer volunteer)
        {
            List<Student> students = new List<Student>();

            using (var ctx = new PeopleContext())
            {
                //get volunteer by id
                List<Volunteer> volunteers = ctx.Volunteers
                    .Where(v => v.VolunteerId == volunteer.VolunteerId)
                    .Include(e => e.VolunteerStudents)
                    .ThenInclude(e => e.Student)
                    .ToList();

                //get students of that volunteer
                students =
                  volunteers.First().VolunteerStudents
                  .Select(e => e.Student)
                  .ToList();
            }
            return students;
        }

        //returns a list with all students that volunteer doesn't have (as students)
        public List<Student> GetNotVolunteersStudents(Volunteer volunteer)
        {
            List<Student> volunteerStudents = GetVolunteerStudents(volunteer);
            List<Student> notVolunteersStudents;

            using (var ctx = new PeopleContext())
            {
                notVolunteersStudents = ctx.Students.ToList();
            }
            
            //exclude students that the volunteer has from the list
            //notVolunteerStudents
            foreach (Student student in volunteerStudents)
                notVolunteersStudents.Remove(student);

           
            return notVolunteersStudents;
        }

        //returns a list of students that guardian from parameter has
        public List<Student> GetGuardianStudents(Guardian guardian)
        {
            List<Student> students = new List<Student>();

            using (var ctx = new PeopleContext())
            {
                //get guardian by id
                List<Guardian> guardians = ctx.Guardians
                    .Where(g => g.GuardianId == guardian.GuardianId)
                    .Include(e => e.StudentGuardians)
                    .ThenInclude(e => e.Student)
                    .ToList();

                //get students of that volunteer
                students =
                  guardians.First().StudentGuardians
                  .Select(e => e.Student)
                  .ToList();
            }
            return students;
        }


        //creates entities <volunteerId, studentId> in the database
        //that represent many to many relationship between students and volunteers
        //EF core takes care of pairing them
        public void AddStudentsToVolunteer(Volunteer volunteer, List<Student> students)
        {
            VolStu volStu;

            using (var ctx = new PeopleContext())
            {
                foreach(Student student in students)
                {
                    volStu = new VolStu
                    {
                        VolunteerId = volunteer.VolunteerId,
                        StudentId = student.StudentId
                    };
                    ctx.Add(volStu);
                }
                ctx.SaveChanges();

            }
        }

        //returns a list of guardians that student from parameter has
        public List<Guardian> GetStudentGuardians(Student student)
        {
            List<Guardian> guardians = new List<Guardian>();

            using (var ctx = new PeopleContext())
            {
                //get student by id
                List<Student> students = ctx.Students
                    .Where(s => s.StudentId == student.StudentId)
                    .Include(e => e.StudentGuardians)
                    .ThenInclude(e => e.Guardian)
                    .ToList();

                //get guardians of that student
                guardians =
                  students.First().StudentGuardians
                  .Select(e => e.Guardian)
                  .ToList();
            }
            return guardians;
        }
    }
}
