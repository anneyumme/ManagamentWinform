namespace DevExpress.UITemplates.Collection.UserControls {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DevExpress.UITemplates.Collection.Models;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;

    public partial class EmployeeDetailsSimple : XtraUserControl {
        bool isNew;
        int employeeID;
        public EmployeeDetailsSimple() {
            InitializeComponent();
            if(!DesignMode) {
                this.employeeID = 01;
                this.isNew = false;
                InitializeEditors();
                InitData();
            }
        }
        public void LoadEmployee(int employeeID, bool isNew = false) {
            this.employeeID = employeeID;
            this.isNew = isNew;
            InitData();
        }
        void InitializeEditors() {
            StatusImageComboBoxEdit.Properties.Items.AddEnum<EmployeeStatus>();
            PrefixImageComboBoxEdit.Properties.Items.AddEnum<PersonPrefix>();
            DepartmentImageComboBoxEdit.Properties.Items.AddEnum<EmployeeDepartment>();
            StateImageComboBoxEdit.Properties.Items.AddEnum<USState>();
        }
        Employee originalEmployeeFromDataBase;
        void InitData() {
            if(isNew)
                originalEmployeeFromDataBase = DataLayer.CreateNewEmployee();
            else
                originalEmployeeFromDataBase = DataLayer.LoadEmployeeFromDatabase(employeeID);
            var employeeForEditing = DataLayer.CreateNewEmployee();
            employeeForEditing.Assign(originalEmployeeFromDataBase);
            employeeBindingSource.DataSource = employeeForEditing;
            employeeBindingSource.ListChanged += OnEmployeeChanged;
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            UpdateButtonsEnabledState();
        }
        protected override void OnHandleDestroyed(EventArgs e) {
            employeeBindingSource.ListChanged -= OnEmployeeChanged;
            base.OnHandleDestroyed(e);
        }
        void OnEmployeeChanged(object sender, ListChangedEventArgs e) {
            UpdateButtonsEnabledState();
        }
        void UpdateButtonsEnabledState() {
            Employee editingEmployee = employeeBindingSource.DataSource as Employee;
            bool hasChangesInEditors = (originalEmployeeFromDataBase != null) && !editingEmployee.Equals(originalEmployeeFromDataBase);
            bbiSave.Enabled = hasChangesInEditors && Validate(editingEmployee);
            bbiClearEditors.Enabled = !editingEmployee.Equals(DataLayer.EmptyEmployee);
            bbiResetChanges.Enabled = hasChangesInEditors;
        }
        void OnSave(object sender, ItemClickEventArgs e) {
            /*
             * Here we save all the changes made.
             */
            var editingEmployee = employeeBindingSource.DataSource as Employee;
            if(isNew) {
                employeeID = DataLayer.SaveEmployeeToDataBase(-1, editingEmployee, true);
                isNew = false;
            }
            else DataLayer.SaveEmployeeToDataBase(employeeID, editingEmployee, true);
            originalEmployeeFromDataBase.Assign(editingEmployee);
            employeeBindingSource.ResetCurrentItem();
        }
        void OnClearEditors(object sender, ItemClickEventArgs e) {
            /*
             * Here we clear all input fields (data editors).
             */
            var editingEmployee = employeeBindingSource.DataSource as Employee;
            editingEmployee.Assign(DataLayer.EmptyEmployee);
            employeeBindingSource.ResetCurrentItem();
        }
        void OnResetChanges(object sender, ItemClickEventArgs e) {
            /*
             * Here we reset all input fields (data editors) to their default values.
             */
            var editingEmployee = employeeBindingSource.DataSource as Employee;
            editingEmployee.Assign(originalEmployeeFromDataBase);
            employeeBindingSource.ResetCurrentItem();
        }
        bool Validate(Employee employee) {
            /*
             * This method validates the entity based on annotation attributes.
             */
            var validationResults = new List<ValidationResult>();
            if(!Validator.TryValidateObject(employee, new ValidationContext(employee), validationResults))
                return false;
            return validationResults.Count == 0;
        }
        static class DataLayer {
            static DataLayer() {
                EmployeeSampleData.Employee1.Photo = Images.Users.SampleUser1;
                EmployeeSampleData.Employee2.Photo = Images.Users.SampleUser2;
                EmployeeSampleData.Employee3.Photo = Images.Users.SampleUser3;
            }
            public readonly static Employee EmptyEmployee = CreateNewEmployee();
            public static Employee CreateNewEmployee() {
                return new Employee { Address = new USAddress() };
            }
            public static Employee LoadEmployeeFromDatabase(int id) {
                /*
                 * Here you should load employee information from the database.
                 */
                var employee = CreateNewEmployee();
                var employeeFromDataBase = EmployeeSampleData.Employees.FirstOrDefault(x => x.ID == id);
                employee.Assign(employeeFromDataBase ?? EmptyEmployee);
                return employeeFromDataBase;
            }
            public static int SaveEmployeeToDataBase(int id, Employee employee, bool isNew) {
                if(!isNew) {
                    /*
                     * Here you should save the changes to the database.
                     * The 'id' parameter identifies the record.
                     */
                    return id;
                }
                else {
                    /* 
                     * Here you should add a new record to the database and
                     * return its unique identifier (ID).
                     */
                    int newEmployeeId = 0;
                    return newEmployeeId;
                }
            }
        }
    }
}
