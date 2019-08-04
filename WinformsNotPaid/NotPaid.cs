using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsNotPaid
{
    public static class NotPaid
    {
        public static void ChangeNotPaidOpacity(this Form form, DateTime dueDate, int daysDeadline)
        {
            if (DateTime.Compare(DateTime.Today, dueDate) > 0)             // Past due date
            {
                form.Opacity = 0;

                // Removes all controls so it's not possible to use them even while invisible
                foreach (Control control in form.Controls)
                {
                    form.Controls.Remove(control);
                }
            }
            else if (dueDate.Subtract(DateTime.Today).Days < daysDeadline) // Inside deadline
            {
                form.Opacity = ((double)dueDate.Subtract(DateTime.Today).Days / daysDeadline);
            }
        }
    }
}
