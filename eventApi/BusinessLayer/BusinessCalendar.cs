using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventApi.BusinessLayer
{
    public class BusinessCalendar : IBusinessCalendar
    {
        public Month nextMonth(int month)
        {
            Month m1 = new Month();
            DateTime nMonths = new DateTime(2020,month,1);
            DateTime newMonth = new DateTime(nMonths.AddMonths(1).Year, nMonths.AddMonths(1).Month, 1);
        
            
            m1.name = newMonth.ToString("MMMM");
            m1.days = DateTime.DaysInMonth(newMonth.Year, newMonth.Month);
            m1.number = newMonth.Month;
            m1.year = newMonth.Year;


            return m1;
        }

        public Month preMonth(int month)
        {
            Month m1 = new Month();
            DateTime nMonths = new DateTime(2020, month, 1);
            DateTime prevMonth = new DateTime(nMonths.AddMonths(-1).Year, nMonths.AddMonths(-1).Month, 1);

            
            m1.name = prevMonth.ToString("MMMM");
            m1.days = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
            m1.number = prevMonth.Month;
            m1.year = prevMonth.Year;
            


            return m1;

        }
    }
}