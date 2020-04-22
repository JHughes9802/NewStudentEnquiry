using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewStudentEnquiry
{
    public partial class Form1 : Form
    {

        SortedList<String, String[]> programs;
        string[] ITPrograms = {"Programming", "Security", "Networking"};
        string[] EngineeringPrograms = {"Electrical", "Mechanical"};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            programs = new SortedList<string, string[]>
            {
                {"Information Technology", ITPrograms},
                {"Engineering", EngineeringPrograms}
            };

            cbxDepartment.Items.AddRange(programs.Keys.ToArray());
            cbxDepartment.SelectedIndex = 0;

            cbxHowDidYouHear.Items.Add("Another student");
            cbxHowDidYouHear.Items.Add("Another school");
            cbxHowDidYouHear.Items.Add("Internet search");
        }

        private void cbxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Is there a way to directly invoke the lstDegrees_SelectedIndexChanged method,
             * or is this the only way to cause it to activate? Because what if what's being
             * designed is more complicated than clearing one or two lists? */
            lstDegrees.ClearSelected();

            lstDegrees.Items.Clear();

            string department = cbxDepartment.Text;

            if (department != null)
            {
                string[] degrees = programs[department];
                lstDegrees.Items.AddRange(degrees);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            /* Is this if statement necessary if a selected index is forced when the form loads?
             * If it is, then the error should tell the user to select a department or field,
             * but the slides have us tell the user to select a semester instead */
            if (cbxDepartment.SelectedIndex == -1)
            {
                errors.Add("Please select a department");
            }

            if (lstDegrees.SelectedIndex == -1)
            {
                errors.Add("Please select at least one degree");
            }

            if (String.IsNullOrEmpty(cbxHowDidYouHear.Text))
            {
                errors.Add("Please type or select how you heard about us");
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(String.Join("\n", errors), "Error");
                return;
            }

            StringBuilder summaryBuilder = new StringBuilder();

            summaryBuilder.Append("Summary");
            summaryBuilder.Append("\n\nDepartment: ");
            summaryBuilder.Append(cbxDepartment.Text);

            summaryBuilder.Append("\n\nPrograms: ");
            summaryBuilder.Append(lstDegrees.SelectedItems.Count);
            summaryBuilder.Append(" selected\n");

            foreach (object degree in lstDegrees.SelectedItems)
            {
                summaryBuilder.Append(degree);
                summaryBuilder.Append("\n");
            }

            summaryBuilder.Append("\nYou heard about us from: ");
            summaryBuilder.Append(cbxHowDidYouHear.Text);

            string summary = summaryBuilder.ToString();

            MessageBox.Show(summary, "Thank you!");

            this.Close();
        }

        private void lstDegrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberSelected = lstDegrees.SelectedItems.Count;
            lblDegreeCount.Text = $"{numberSelected} selected";
        }
    }
}
