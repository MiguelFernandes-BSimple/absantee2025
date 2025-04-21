using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Subject : ISubject
    {
        private long _id;
        private string _title;
        private string _description;

        public Subject(string title, string description)
        {
            Regex titleRegex = new Regex(@"^[a-zA-Z0-9\s]{1,20}$");
            Regex descriptionRegex = new Regex(@"^[a-zA-Z0-9\s]{1,100}$");
            if (!titleRegex.IsMatch(title) || !descriptionRegex.IsMatch(description))
            {
                throw new ArgumentException("Invalid Arguments");
            }

            _title = title;
            _description = description;
        }

        public Subject(long id, string title, string description)
        {
            _id = id;
            _title = title;
            _description = description;
        }

        public long GetId() { return _id; }
        public string GetTitle() { return _title; }
        public string GetDescription() { return _description; }


    }
}