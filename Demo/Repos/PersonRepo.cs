using Demo.Contracts;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repos
{
    public class PersonRepo : IPersonRepo
    {
        private readonly List<Person> _people;

        public PersonRepo() 
        {
            if (_people?.Count() != 0)
            {
                _people = new List<Person>();
                SeedPerson();
            }
        }

        public SuccessResponse AddPerson(Person person)
        {
            var result = new SuccessResponse();

            if (person.Id == 0)
                result.Reason = $"Invalid data.";
            else if (_people.Any(x => x.Id == person.Id))
                result.Reason = $"Person with ID {person.Id} existed.";
            else
            {
                _people.Add(person);
                result.Success = true;
            }

            return result;
        }

        public List<Person> GetListPerson(int page, int itemsPerPage)
        {
            return _people.Skip(itemsPerPage * (page - 1))
                          .Take(itemsPerPage)
                          .ToList();
        }

        public Person GetPersonById(long id)
        {
            return _people.Find(x => x.Id == id);
        }

        private void SeedPerson()
        {
            _people.AddRange(
                new List<Person>
                {
                    new Person { Id= 1, Name = "Alan", Age = 16, Occupation = "Student", Emails = new List<string>{"al4an@gmail.com", "alan@gmail.com" } },
                    new Person { Id = 2, Name = "Budi", Age= 47, Occupation = "Lecture", Emails= new List<string>{ "budi@gmail.com" } },
                    new Person { Id = 3, Name = "Charlie", Age = 65, Occupation = "Headmaster", Emails = new List<string>{ "charlie@gmail.com" } },
                }
           );
        }
    }
}