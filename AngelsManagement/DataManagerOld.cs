using AngelsManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelsManagement
{
    public class DataManagerOld
    {
        private DbManager dbManager;
        
        private String[] cities = { "Gdansk", "Wroclaw", "Poznan" };
        //todo- this is for the future (keep a map city-{volo,stud,par}?)
        //right now we're just doing one city

        //todo enum not string for city
        //dictionary of volunteers in form <city, volunteers list>
        public Dictionary<String, ObservableCollection<Volunteer>> VolunteersDict { get; set; }
        public Dictionary<String, ObservableCollection<Student>> StudentsDict { get; set; }
        /*
        Dictionary<String, ObservableCollection<Parent>> ParentsDict;*///todo

        public DataManagerOld()
        {
            dbManager = new DbManager();
            
            PrepareDictionaries();

            GetAllData();

        }

        //for each dictionary (volunteers, students, parents)
        //creates entries <city, empty observable collection>
        //this is to ensure that while setting data sources app doesn't crash
        private void PrepareDictionaries()
        {
            //volunteers dict
            VolunteersDict = new Dictionary<string, ObservableCollection<Volunteer>>();

            ObservableCollection<Volunteer> vCollection;
            foreach (string city in cities)
            {
                vCollection = new ObservableCollection<Volunteer>();
                VolunteersDict.Add(city, vCollection);

            }

            //students dict
            StudentsDict = new Dictionary<string, ObservableCollection<Student>>();

            ObservableCollection<Student> sCollection;
            foreach (string city in cities)
            {
                sCollection = new ObservableCollection<Student>();
                StudentsDict.Add(city, sCollection);

            }

            //todo parents
        }

        //prepares the database (app directory, file, tables)
        //sets Volunteers, Students (todo) and Parents (todo) 
        //properties for each city (todo)
        private void GetAllData()
        {
            dbManager.PrepareDb();

            //get volunteers and put them in volunteers dictionary (key is the city)
            List<Volunteer> vList;
            ObservableCollection<Volunteer> vCollection;

            List<Student> sList;
            ObservableCollection<Student> sCollection;

            foreach (string city in cities)
            {
                //--------------volunteers
                //get a list of all volunteers from city
                vList = dbManager.GetVolunteersByCity(city);
                
                //add them to the collection
                vCollection = VolunteersDict[city];

                //clear collection //todo this is very ugly
                vCollection.Clear();

                foreach (Volunteer volunteer in vList)
                {
                    vCollection.Add(volunteer);
                }

                VolunteersDict[city]= vCollection;

                //--------------students
                sList = dbManager.GetStudentsByCity(city);
                sCollection = StudentsDict[city];
                sCollection.Clear();

                foreach (Student student in sList)
                {
                    sCollection.Add(student);
                }

                StudentsDict[city] = sCollection;

            }//todo same for parents
            
        }

        public void CreateVolunteer(Volunteer volunteer)
        {
            dbManager.InsertIntoVolunteers(volunteer);
            GetAllData();
        }
        public void CreateStudent(Student student)
        {
            dbManager.InsertIntoStudents(student);
            GetAllData();
        }
    }
}
