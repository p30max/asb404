using System;
using System.Globalization;
using System.IO;

namespace Asb404.Models
{
    public class PDate
    {
        public int ShamsiDate(DateTime D)
        {
            PersianCalendar P = new PersianCalendar();
            string PersDate, Year, Month, Day;
            int IntDate;
            Year = P.GetYear(D).ToString();
            Month = P.GetMonth(D).ToString();
            if (Month.Length == 1) Month = "0" + Month;
            Day = P.GetDayOfMonth(D).ToString();
            if (Day.Length == 1) Day = "0" + Day;
            PersDate = Year + Month + Day;
            IntDate = Convert.ToInt32(PersDate);
            return IntDate;
        }
        public string Dater(DateTime D)
        {
            PersianCalendar P = new PersianCalendar();
            string Shamsi, Year, Month, Day;

            Year = P.GetYear(D).ToString();
            Month = P.GetMonth(D).ToString();
            if (Month.Length == 1) Month = "0" + Month;
            Day = P.GetDayOfMonth(D).ToString();
            if (Day.Length == 1) Day = "0" + Day;
            Shamsi = Year + "/" + Month + "/" + Day;

            return Shamsi;
        }
        public string CurrentDate()
        {
            DateTime D = DateTime.Now;
            
            PersianCalendar P = new PersianCalendar();
            string Shamsi, Year, Month, Day;

            Year = P.GetYear(D).ToString();
            Month = P.GetMonth(D).ToString();
            if (Month.Length == 1) Month = "0" + Month;
            Day = P.GetDayOfMonth(D).ToString();
            if (Day.Length == 1) Day = "0" + Day;
            Shamsi = Year + "/" + Month + "/" + Day;

            return Shamsi;
        }
        public string New3Date()
        {
            DateTime D = DateTime.Now;

            PersianCalendar P = new PersianCalendar();
            string Shamsi, Year, Month, Day;
            Year =(P.GetYear(D)+3).ToString();
            Month = P.GetMonth(D).ToString();
            if (Month.Length == 1) Month = "0" + Month;
            Day = P.GetDayOfMonth(D).ToString();
            if (Day.Length == 1) Day = "0" + Day;
            Shamsi = Year + "/" + Month + "/" + Day;

            return Shamsi;
        }
        public string StandardDate(string Date)
        {
            string Sdate, Year, Month, Day;
            bool flag = false;
            Sdate = Date;
            if (Date.Length == 8)
            {
                Year = Date.Substring(0, 4);
                Month = "0" + Date.Substring(5, 1);
                Day = "0" + Date.Substring(7, 1);
                Sdate = Year + "/" + Month + "/" + Day;
                return Sdate;
            }
            if (Date.Length == 9)
            {
                Year = Date.Substring(0, 4);
                Month = Date.Substring(5, 2);
                for (int i = 0; i < Month.Length; i++)
                {
                    if (Month[i] == '/')
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    Year = Date.Substring(0, 4);
                    Month = "0" + Date.Substring(5, 1);
                    Day = Date.Substring(7, 2);
                }
                else
                {
                    Year = Date.Substring(0, 4);
                    Month = Date.Substring(5, 2);
                    Day = "0" + Date.Substring(8, 1);
                }

                Sdate = Year + "/" + Month + "/" + Day;
                return Sdate;
            }

            return Sdate;
        }
        public DateTime PersiantoGeorgian(int year,int month, int day)
        {
            DateTime dt = new DateTime(year, month, day, new PersianCalendar());
            return dt;
        }
        public string CurrentDate_Time()
        {
            DateTime D = DateTime.Now;

            PersianCalendar P = new PersianCalendar();
            string Shamsi, Year, Month, Day;

            Year = P.GetYear(D).ToString();
            Month = P.GetMonth(D).ToString();
            if (Month.Length == 1) Month = "0" + Month;
            Day = P.GetDayOfMonth(D).ToString();
            if (Day.Length == 1) Day = "0" + Day;
            Shamsi = Year + "/" + Month + "/" + Day;

            return Shamsi + DateTime.Now.ToShortTimeString() ;
        }
        public string CurrentDay(DateTime d)
        {

            PersianCalendar P = new PersianCalendar();
            string Day;
            Day =P.GetDayOfWeek(d).ToString();
            switch (Day)
            {
                case "Sunday": { Day = "یک‌شنبه";break; }
                case "Monday": { Day = "دوشنبه"; break; }
                case "Tuesday": { Day = "سه‌شنبه"; break; }
                case "Wednesday": { Day = "چهارشنبه"; break; }
                case "Thursday": { Day = "پنجشنبه"; break; }
                case "Friday": { Day = "جمعه"; break; }
                case "Saturday ": { Day = "شنبه"; break; }
            }
            return Day;
        }
        public string FileNameCreator(int id, string Filename)
        {
            string FileNameByDate = "";
            FileNameByDate = DateTime.Now.ToShortDateString() + DateTime.Now.TimeOfDay.ToString();
            FileNameByDate = FileNameByDate.Replace(":", "-");
            FileNameByDate = FileNameByDate.Replace("/", "-");
            FileNameByDate = FileNameByDate.Replace(".", "-");
            string Ext = Path.GetExtension(Filename);
            return id + FileNameByDate + Ext;

        }
    }
}
 
 