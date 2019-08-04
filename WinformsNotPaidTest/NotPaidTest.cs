using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using WinformsNotPaid;

namespace WinformsNotPaidTest
{
    [TestClass]
    public class NotPaidTest
    {
        /// <summary>
        /// Creats a form and fill it with a label
        /// </summary>
        private Form createTestForm()
        {
            Form form = new Form();
            form.Width = 200;
            form.Height = 100;
            form.Padding = new Padding(50);

            Label label = new Label();
            label.Text = "Please pay or I'll dissapear";
            label.AutoSize = true;

            form.Controls.Add(label);

            return form;
        }
        
        /// <summary>
        /// Shows the Form and pauses code flow for opacity and data visualization
        /// </summary>
        private void visualizeFormAndValues(string testName, Form form, DateTime dueDate, int daysDeadline)
        {
            form.Show();
            MessageBox.Show(
                 $"Today's date: {DateTime.Today.ToShortDateString()}"
               + $"\nDue date: {dueDate.ToShortDateString()} ({dueDate.Subtract(DateTime.Today).Days} days left)"
               + $"\nDeadline date: {dueDate.Subtract(new TimeSpan(daysDeadline, 0, 0, 0)).ToShortDateString()} ( {dueDate.Subtract(DateTime.Today).Days - daysDeadline} days left)"
               + $"\nCurrent opacity: {form.Opacity * 100}% ({dueDate.Subtract(DateTime.Today).Days}/{daysDeadline})"
               , testName);
        }

        [TestMethod]
        public void TestPastDueDate()
        {
            DateTime dueDate = new DateTime(2018, 1, 1);
            int daysDeadline = 30;

            Form form = createTestForm();

            Assert.IsTrue(form.Controls.Count == 1); // Has label

            form.ChangeNotPaidOpacity(dueDate, daysDeadline);

            visualizeFormAndValues("TestPastDueDate", form, dueDate, daysDeadline);

            Assert.IsTrue(form.Opacity == 0);        // Past due date
            Assert.IsTrue(form.Controls.Count == 0); // Label removed
        }

        [TestMethod]
        public void TestInsideDeadline()
        {
            DateTime dueDate = DateTime.Today.Add(new TimeSpan(24, 0, 0, 0)); // 24 days until dueDate
            int daysDeadline = 60;

            Form form = createTestForm();

            Assert.IsTrue(form.Controls.Count == 1); // Has label

            form.ChangeNotPaidOpacity(dueDate, daysDeadline);

            visualizeFormAndValues("TestInsideDeadline", form, dueDate, daysDeadline);

            Assert.IsTrue(form.Opacity < 1);         // Inside deadline
            Assert.IsTrue(form.Controls.Count == 1); // Label not removed
        }

        [TestMethod]
        public void TestOutsideDeadline()
        {
            DateTime dueDate = DateTime.Today.Add(new TimeSpan(23, 0, 0, 0)); // 23 days until dueDate
            int daysDeadline = 10;

            Form form = createTestForm();

            Assert.IsTrue(form.Controls.Count == 1); // Has label

            form.ChangeNotPaidOpacity(dueDate, daysDeadline);

            visualizeFormAndValues("TestOutsideDeadline", form, dueDate, daysDeadline);

            Assert.IsTrue(form.Opacity == 1);        // Outside deadline
            Assert.IsTrue(form.Controls.Count == 1); // Label not removed
        }
    }
}
