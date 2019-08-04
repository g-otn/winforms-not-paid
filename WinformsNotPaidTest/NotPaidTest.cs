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
    }
}
