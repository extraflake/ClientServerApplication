using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }

        public virtual Job Job { get; set; }
        [ForeignKey("Job")]
        public int Job_Id { get; set; }

        public virtual Employee Manager { get; set; }
        [ForeignKey("Manager")]
        public int Manager_Id { get; set; }

        public virtual Department Department { get; set; }
        [ForeignKey("Department")]
        public int Department_Id { get; set; }

        public Employee(string FirstName, string LastName, string Email, string PhoneNumber, DateTime HireDate, int Salary)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.HireDate = HireDate;
            this.Salary = Salary;
        }
    }
}
