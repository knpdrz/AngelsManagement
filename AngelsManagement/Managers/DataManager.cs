﻿using AngelsManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement
{
    public class DataManager
    {
        public String[] Cities = { "Gdansk", "Wroclaw", "Poznan" };
        public Dictionary<String, ObservableCollection<Volunteer>> VolunteersDict { get; set; }
        public Dictionary<String, ObservableCollection<Student>> StudentsDict { get; set; }
        public Dictionary<String, ObservableCollection<Parent>> ParentsDict { get; set; }

        public DataManager()
        {
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
            ParentsDict = new Dictionary<string, ObservableCollection<Parent>>();

        }

        //for each dictionary (volunteers, students, parents)
        //creates entries <city, list with volunteers/students/parents>
        private void GetPeopleData()
        {
            List<Volunteer> volunteers;
            List<Student> students;
            List<Parent> parents;

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

                //---------parents
                foreach (string city in Cities)
                {
                    parents = ctx.Parents
                        .Where(p => p.City.Equals(city)).ToList();

                    if (ParentsDict.ContainsKey(city))
                    {
                        ParentsDict[city].Clear();
                    }
                    else
                    {
                        ParentsDict[city] = new ObservableCollection<Parent>();

                    }

                    foreach (Parent parent in parents)
                    {
                        ParentsDict[city].Add(parent);
                    }

                }
            }
        }

        //todo watch it! both need to have id set already! (id is given upon adding to db)
        //adds parent to database AND (todo?) adds them to correct student
        public void AddParentToStudent(Student student, Parent parent)
        {
            using (var ctx = new PeopleContext())
            {
                ctx.Parents.Add(parent);
                ctx.SaveChanges();
            }

            //adding new parent to observable collection
            //in correct city
            ParentsDict[parent.City].Add(parent);

            Console.WriteLine("PARENT ID = " + parent.ParentId);

            //adding connection between student and parent
            StuPar stuPar;

            using (var ctx = new PeopleContext())
            {
                stuPar = new StuPar
                {
                    StudentId = student.StudentId,
                    ParentId = parent.ParentId
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
            //todo- this should be separated in a function?
            VolunteersDict[volunteer.City].Add(volunteer);

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
        //todo differentiation between already added ones..?
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

        //returns a list of parents that student from parameter has
        public List<Parent> GetStudentParents(Student student)
        {
            List<Parent> parents = new List<Parent>();

            using (var ctx = new PeopleContext())
            {
                //get student by id
                List<Student> students = ctx.Students
                    .Where(s => s.StudentId == student.StudentId)
                    .Include(e => e.StudentParents)
                    .ThenInclude(e => e.Parent)
                    .ToList();

                //get parents of that student
                parents =
                  students.First().StudentParents
                  .Select(e => e.Parent)
                  .ToList();
            }
            return parents;
        }
    }
}