using AngelsManagement.DataModels;
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
        private DbManager dbManager;
        //todo delete if unused public ObservableCollection<Volunteer> Volunteers { get; set; }
        /*  public ObservableCollection<Student> Students { get; set; }
          public ObservableCollection<Parent> Parents { get; set; }
          */

        private String[] cities = { "Gdansk", "Wroclaw", "Poznan" };
        //todo- this is for the future (keep a map city-{volo,stud,par}?)
        //right now we're just doing one city

        //todo enum not string for city
        //dictionary of volunteers in form <city, volunteers list>
        public Dictionary<String, ObservableCollection<Volunteer>> VolunteersDict { get; set; }
       /* Dictionary<String, ObservableCollection<Student>> StudentsDict;
        Dictionary<String, ObservableCollection<Parent>> ParentsDict;*///todo

        public DataManager()
        {
            dbManager = new DbManager();

            VolunteersDict = new Dictionary<string, ObservableCollection<Volunteer>>();//todo two more

            /*Students = new ObservableCollection<Student>();
            Parents = new ObservableCollection<Parent>();*/
            PrepareDictionaries();

            GetAllData();

        }

        //for each dictionary (volunteers, students, parents)
        //creates entries <city, empty observable collection>
        //this is to ensure that while setting data sources app doesn't crash
        private void PrepareDictionaries()
        {
            ObservableCollection<Volunteer> vCollection;
            foreach (string city in cities)
            {
                vCollection = new ObservableCollection<Volunteer>();
                VolunteersDict.Add(city, vCollection);

            }
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

            Console.WriteLine("---getting volunteers by city");
            foreach (string city in cities)
            {
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
                Console.WriteLine(">>" + city + " : " +vCollection.Count());

            }//todo same for stud and parents
            Console.WriteLine("-----got volos----");
            
        }

        public void CreateVolunteer(Volunteer volunteer)
        {
            dbManager.InsertIntoVolunteers(volunteer);
            GetAllData();
        }
    }
}
