namespace DevExpress.UITemplates.Collection.Models {
    using System;
    using System.Collections.Generic;

    public static class EmployeeSampleData {
        static EmployeeSampleData() {
            Employee1 = new Employee() {
                ID = 0x01,
                FirstName = "Bradley",
                LastName = "Jameson",
                BirthDate = new DateTime(1988, 12, 10),
                Title = "Software Developer",
                Prefix = PersonPrefix.Mr,
                Address = new USAddress() {
                    Line = "1100 Pico St",
                    City = "San Fernando",
                    State = USState.CA,
                    ZipCode = "91340",
                },
                HomePhone = "(818) 555-9098",
                MobilePhone = "(818) 555-4646",
                Email = "bradleyj@dx-email.com",
                Department = EmployeeDepartment.IT,
                Status = EmployeeStatus.Salaried,
                HireDate = new DateTime(2011, 2, 3)
            };
            Employee2 = new Employee() {
                ID = 0x02,
                FirstName = "Leah",
                LastName = "Simpson",
                BirthDate = new DateTime(1983, 4, 19),
                Title = "Test Coordinator",
                Prefix = PersonPrefix.Mrs,
                Address = new USAddress() {
                    Line = "6755 Newline Ave",
                    City = "Whittier",
                    State = USState.AK,
                    ZipCode = "90601",
                },
                HomePhone = "(562) 555-7372",
                MobilePhone = "(562) 559-5830",
                Email = "leahs@dx-email.com",
                Department = EmployeeDepartment.Engineering,
                Status = EmployeeStatus.Salaried,
                HireDate = new DateTime(2009, 2, 14)
            };
            Employee3 = new Employee() {
                ID = 0x03,
                FirstName = "John",
                LastName = "Heart",
                BirthDate = new DateTime(1964, 3, 16),
                Title = "CEO",
                Prefix = PersonPrefix.Dr,
                Address = new USAddress() {
                    Line = "351 S Hill St.",
                    City = "Los Angeles",
                    State = USState.CA,
                    ZipCode = "90013",
                },
                HomePhone = "(213) 555-9208",
                MobilePhone = "(213) 555-9392",
                Email = "jheart@dx-email.com",
                Department = EmployeeDepartment.Management,
                Status = EmployeeStatus.Commission,
                HireDate = new DateTime(1995, 1, 15)
            };
            Employee4 = new Employee() {
                ID = 0x04,
                FirstName = "Wally",
                LastName = "Hobbs",
                BirthDate = new DateTime(1984, 12, 24),
                Title = "Software Developer",
                Prefix = PersonPrefix.Mr,
                Address = new USAddress() {
                    Line = "10385 Shadow Oak Dr",
                    City = "Chatsworth",
                    State = USState.CA,
                    ZipCode = "91311",
                },
                HomePhone = "(818) 555-2478",
                MobilePhone = "(818) 555-8872",
                Email = "wallyh@dx-email.com",
                Department = EmployeeDepartment.IT,
                Status = EmployeeStatus.Salaried,
                HireDate = new DateTime(2011, 2, 17)
            };
            Employee5 = new Employee() {
                ID = 0x05,
                FirstName = "Karen",
                LastName = "Goodson",
                BirthDate = new DateTime(1987, 4, 26),
                Title = "Software Developer",
                Prefix = PersonPrefix.Miss,
                Address = new USAddress() {
                    Line = "309 Monterey Rd",
                    City = "South Pasadena",
                    State = USState.CA,
                    ZipCode = "91030",
                },
                HomePhone = "(626) 555-0822",
                MobilePhone = "(626) 555-0908",
                Email = "kareng@dx-email.com",
                Department = EmployeeDepartment.IT,
                Status = EmployeeStatus.OnLeave,
                HireDate = new DateTime(2011, 3, 14)
            };
            Employees = new Employee[] {
                Employee1, Employee2, Employee3, Employee4, Employee5
            };
        }
        //
        public static Employee Employee1 {
            get;
            private set;
        }
        public static Employee Employee2 {
            get;
            private set;
        }
        public static Employee Employee3 {
            get;
            private set;
        }
        public static Employee Employee4 {
            get;
            private set;
        }
        public static Employee Employee5 {
            get;
            private set;
        }
        //
        public static IReadOnlyCollection<Employee> Employees {
            get;
            private set;
        }
    }
}
