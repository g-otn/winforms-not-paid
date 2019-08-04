using System;
using System.Windows.Forms;

namespace WinformsNotPaid
{
    public static class NotPaid
    {
        /// <summary>
        /// Changes the <see cref="Form.Opacity"/> depending of how close <see cref="DateTime.Today"/> is from <paramref name="dueDate"/>.
        /// The <see cref="Form.Opacity"/> does not change if the number of days until <paramref name="dueDate"/> 
        /// is higher or equal to <paramref name="daysDeadline"/>.
        /// </summary>
        /// <param name="dueDate">The date when the <see cref="Form"/> should be invisible and the Form.Controls should be cleared.</param>
        /// <param name="daysDeadline">The number of days the deadline has and how many days before the <paramref name="dueDate"/> 
        /// the <see cref="Form"/> will start dissapearing.</param>
        public static void ChangeNotPaidOpacity(this Form form, DateTime dueDate, int daysDeadline)
        {
            if (DateTime.Compare(DateTime.Today, dueDate) > 0)             // Past due date
            {
                form.Opacity = 0;

                // Removes all controls so it's not possible to use them even while invisible
                form.Controls.Clear();
            }
            else if (dueDate.Subtract(DateTime.Today).Days < daysDeadline) // Inside deadline
            {
                form.Opacity = Math.Max(0.01, ((double)dueDate.Subtract(DateTime.Today).Days / daysDeadline));
            }
        }
    }
}
