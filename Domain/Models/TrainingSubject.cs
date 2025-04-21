using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingSubject : ITrainingSubject
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public TrainingSubject(string subject, string description)
        {
            if (string.IsNullOrWhiteSpace(subject) || subject.Length > 20 || !Regex.IsMatch(subject, @"^[a-zA-Z0-9 ]+$"))
                throw new ArgumentException("Subject must be alphanumeric and no longer than 20 characters.");

            if (string.IsNullOrWhiteSpace(description) || description.Length > 100 || !Regex.IsMatch(description, @"^[a-zA-Z0-9 ]+$"))
                throw new ArgumentException("Description must be alphanumeric and no longer than 100 characters.");

            Subject = subject;
            Description = description;
        }
    }
}
