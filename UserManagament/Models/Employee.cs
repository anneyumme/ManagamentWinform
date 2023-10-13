namespace DevExpress.UITemplates.Collection.Models {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Person : Entity<long> {
        public object Photo { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName {
            get { return FirstName + " " + LastName; }
        }
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        public override bool Equals(object obj) {
            Person p = obj as Person;
            if(ReferenceEquals(p, null) || !base.Equals(obj))
                return false;
            return FirstName == p.FirstName && LastName == p.LastName && BirthDate == p.BirthDate && Photo == p.Photo;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override void Assign(Entity<long> source) {
            base.Assign(source);
            var personSource = source as Person;
            if(personSource != null) {
                FirstName = personSource.FirstName;
                LastName = personSource.LastName;
                BirthDate = personSource.BirthDate;
                Photo = personSource.Photo;
            }
        }
        public override string ToString() {
            return FullName;
        }
    }
    public enum PersonPrefix {
        Dr = 0,
        Mr = 1,
        Ms = 2,
        Miss = 3,
        Mrs = 4
    }
    //
    public class Employee : Person {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }
        [Required, Display(Name = "Mobile Phone"), Phone]
        public string MobilePhone { get; set; }
        [Required, Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }
        public EmployeeDepartment Department { get; set; }
        [Required]
        public string Title { get; set; }
        public PersonPrefix Prefix { get; set; }
        public EmployeeStatus Status { get; set; }
        public virtual Address Address { get; set; }
        public override bool Equals(object obj) {
            Employee e = obj as Employee;
            if(ReferenceEquals(e, null) || !base.Equals(obj))
                return false;
            return
                Title == e.Title &&
                Email == e.Email &&
                HomePhone == e.HomePhone &&
                MobilePhone == e.MobilePhone &&
                HireDate == e.HireDate &&
                Prefix == e.Prefix &&
                Department == e.Department &&
                Status == e.Status &&
                Address.Equals(e.Address);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override void Assign(Entity<long> source) {
            base.Assign(source);
            var employeeSource = source as Employee;
            if(employeeSource != null) {
                Title = employeeSource.Title;
                Email = employeeSource.Email;
                HomePhone = employeeSource.HomePhone;
                MobilePhone = employeeSource.MobilePhone;
                HireDate = employeeSource.HireDate;
                Prefix = employeeSource.Prefix;
                Department = employeeSource.Department;
                Status = employeeSource.Status;
                Address.Assign(employeeSource.Address);
            }
        }
    }
    public enum EmployeeStatus {
        Salaried = 0,
        Commission = 1,
        Terminated = 2,
        OnLeave = 3
    }
    public enum EmployeeDepartment {
        Sales = 1,
        Support = 2,
        Shipping = 3,
        Engineering = 4,
        [Display(Name = "Human Resources")]
        HumanResources = 5,
        Management = 6,
        IT = 7
    }
    //
    public class Subordinate : Employee {
        public long SupervisorID { 
            get;
            set; 
        }
        public override bool Equals(object obj) {
            Subordinate s = obj as Subordinate;
            if(ReferenceEquals(s, null) || !base.Equals(obj))
                return false;
            return SupervisorID == s.SupervisorID;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override void Assign(Entity<long> source) {
            base.Assign(source);
            var subordinateSource = source as Subordinate;
            if(subordinateSource != null) 
                SupervisorID = subordinateSource.SupervisorID;
        }
    }
}
