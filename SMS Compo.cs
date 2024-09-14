
using System;
using System.ComponentModel;

namespace StudentManagementSystem
{
    public partial class SMS_Compo : Component
    {
        // User controls as properties
        public AddStudentForm AddStudentForm { get; private set; }
        public AddFacultyForm AddFacultyForm { get; private set; }
        public AddRegisterForm AddRegisterForm { get; private set; }

        public SMS_Compo()
        {
            InitializeComponent();
            InitializeUserControls();
        }

        public SMS_Compo(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            InitializeUserControls();
        }

        // Method to initialize user controls
        private void InitializeUserControls()
        {
            // Initialize user controls and add them to the component if needed
            AddStudentForm = new AddStudentForm();
            AddFacultyForm = new AddFacultyForm();
            AddRegisterForm = new AddRegisterForm();

            // If you need to add them to a parent control, you can do so here
            // For example, you could add them to a form or panel:
            //someParentControl.Controls.Add(AddStudentForm);
            // someParentControl.Controls.Add(AddFacultyForm);
            // someParentControl.Controls.Add(AddRegisterForm);
        }
    }
}
