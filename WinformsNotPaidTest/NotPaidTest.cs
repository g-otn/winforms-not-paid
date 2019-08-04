using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using WinformsNotPaid;

namespace WinformsNotPaidTest
{
    [TestClass]
    public class NotPaidTest
    {
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

        [TestMethod]
        public void TestPastDueDate()
        {
            DateTime dueDate = new DateTime(2018, 1, 1);
            int daysDeadLine = 30;

            Form form = createTestForm();

            Assert.IsTrue(form.Controls.Count == 1); // Has label

            form.ChangeNotPaidOpacity(dueDate, daysDeadLine);

            Assert.IsTrue(form.Opacity == 0);        // Past due date
            Assert.IsTrue(form.Controls.Count == 0); // Label removed
        }

        [TestMethod]
        public void TestInsideDeadline()
        {
            DateTime dueDate = DateTime.Today.Add(new TimeSpan(24, 0, 0, 0)); // 24 days until dueDate
            int daysDeadLine = 60;

            Form form = createTestForm();

            Assert.IsTrue(form.Controls.Count == 1); // Has label

            form.ChangeNotPaidOpacity(dueDate, daysDeadLine);

            // Form opacity visualization along with test values
            form.Show();
            MessageBox.Show(
                 $"Today's date: {DateTime.Today.ToShortDateString()}"
               + $"\nDue date: {dueDate.ToShortDateString()} ({dueDate.Subtract(DateTime.Today).Days} days left)"
               + $"\nDeadline date: {dueDate.Subtract(new TimeSpan(daysDeadLine, 0, 0, 0)).ToShortDateString()} ( {dueDate.Subtract(DateTime.Today).Days - daysDeadLine} days left)"
               + $"\nCurrent opacity: {form.Opacity * 100}% ({dueDate.Subtract(DateTime.Today).Days}/{daysDeadLine})"
               , "TestInsideDeadline values");

            Assert.IsTrue(form.Opacity < 1);         // Inside deadline
            Assert.IsTrue(form.Controls.Count == 1); // Label not removed
        }
    }
}
