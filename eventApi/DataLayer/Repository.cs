using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using eventApi.Models;

namespace eventApi.DataLayer
{
    public class Repository : IRepositoryAuthentication, IRepositoryEvent, IRepositoryInvite, IRepositoryDash
    {
        IDataAccess _idac = null;

        public Repository()
        {
            _idac = new DataAccess();
        }

        public Repository(IDataAccess idac)
        {
            _idac = idac;
        }




        public bool DeleteEvent(int id)
        {
            bool ret = false;
            try
            {
              
                string sql = String.Format("Delete from [dbo].[Events] where eventId ={0}", id);



                object obj = _idac.InsertUpdateDelete(sql);
                if (obj != null)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public Event GetEvent(int id)
        {
            Object ret = false;

            Event tempEvent = new Event();

            try
            {
                string sql = "select * from dbo.Events where " +
                    "eventId='" + id + "' ";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);
                if (obj != null)
                {
                    DataRow row = obj.Tables[0].Rows[0];
                    object[] data = row.ItemArray;
                    tempEvent.id = (int)data[0];
                    tempEvent.day = data[1].ToString();
                    tempEvent.location = data[2].ToString();
                    tempEvent.setBy = data[3].ToString();
                    tempEvent.name = data[4].ToString();
                    

                }
            }
            catch (Exception)
            {
                throw;
            }
            return tempEvent;
        }



        public List<Event> GetEvents()
        {
            List<Event> ret = new List<Event>();
            try
            {
                //HttpContext current = HttpContext.Current;
                //string username = current.Session["LOGGEDIN"].ToString();
                //string sql = "select * from dbo.Events where setBy = '" + username + "'";
                string sql = "select * from dbo.Events where setBy = 'scott'";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);


                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    object[] data = row.ItemArray;
                    int id = (int)data[0];
                    string date = data[1].ToString();
                    string location = data[2].ToString();
                    string setby = data[3].ToString();
                    string name = data[4].ToString();

                    Event tempEvent = new Event();
                    tempEvent.id = id;
                    tempEvent.location = location;
                    tempEvent.setBy = setby;
                    tempEvent.name = name;
                    tempEvent.day = date;
                    ret.Add(tempEvent);

                }


            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        public List<Event> GetEvents(string username)
        {
            List<Event> ret = new List<Event>();
            try
            {
                //HttpContext current = HttpContext.Current;
                //string username = current.Session["LOGGEDIN"].ToString();
                //string sql = "select * from dbo.Events where setBy = '" + username + "'";
                string sql = "select * from dbo.Events where setBy = '"+username+"'";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);


                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    object[] data = row.ItemArray;
                    int id = (int)data[0];
                    string date = data[1].ToString();
                    string location = data[2].ToString();
                    string setby = data[3].ToString();
                    string name = data[4].ToString();

                    Event tempEvent = new Event();
                    tempEvent.id = id;
                    tempEvent.location = location;
                    tempEvent.setBy = setby;
                    tempEvent.name = name;
                    tempEvent.day = date;
                    ret.Add(tempEvent);

                }


            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public List<Event> GetDayEvents(Day day)
        {
            List<Event> ret = new List<Event>();

            Day d = day;

            DateTime d1 = new DateTime(d.year, d.month, d.day);
            d1.ToString("yyyy-mm-dd HH:mm:ss");

            try
            {
                HttpContext current = HttpContext.Current;
                string setBy = current.Session["LOGGEDIN"].ToString();

                string sql = String.Format("select * from dbo.Events where setBy='{2}' AND date >= '{0}' AND date < '{1}'", d1.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"), d1.AddHours(23).ToString("yyyy-MM-d HH:mm:ss"), setBy);

                DataSet obj = _idac.DataSetXEQDynamicSql(sql);

                if (obj != null)
                {
                    foreach (DataRow row in obj.Tables[0].Rows)
                    {
                        object[] data = row.ItemArray;
                        int id = (int)data[0];
                        string date = data[1].ToString();
                        string location = data[2].ToString();
                        string setby = data[3].ToString();
                        string name = data[4].ToString();

                        Event tempEvent = new Event();
                        tempEvent.id = id;
                        tempEvent.location = location;
                        tempEvent.setBy = setby;
                        tempEvent.name = name;
                        tempEvent.day = date;
                        ret.Add(tempEvent);

                    }
                }
                else
                {
                    return ret;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }


        public bool SaveEvent(Event events)
        {

            bool ret = false;
            try
            {
                //events.attendees.Add("scott");
                HttpContext current = HttpContext.Current;
                string dates = events.day;
                string location = events.location;
               // string setBy = current.Session["LOGGEDIN"].ToString();
                string sql = "INSERT INTO[dbo].[Events] ([date], [location],[setBy],[title]) VALUES ('" + dates + "' ,'" + location + "' ,'"+events.setBy+"', '" + events.name.Trim() + "')";
               

                object obj = _idac.InsertUpdateDelete(sql);
                if (obj != null)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
            }

            // only call if attendes not null
            if(events.attendent != null)
            {
               SaveInvites(events);
            }
            
            return ret;

        }

        public void SaveInvites(Event events)
        {

            bool ret = false;
            try
            {
                HttpContext current = HttpContext.Current;
                string dates = events.day;
                string location = events.location;
                string setBy = events.setBy;
                int id1 = getUserId(setBy);
                object obj = null;
                string sql = "";
                //foreach (string username in events.attendees)
                ///{
                    //int id2 = getUserId(events.attendent);
                    int id2 = getUserId(events.attendent);
                    int eventid = getEventId(setBy, events);
                     sql += " INSERT INTO[dbo].[Invites] ([userId1], [userId2],[eventId],[CreaterId],[status]) VALUES ('" + id1 + "' ,'" + id2 + "' ,'" + eventid + "', '" + id1 + "', 'Pending' )";

               // }
                obj = _idac.InsertUpdateDelete(sql);
                if (obj != null)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
            }

        }



        public List<Invite> getInvite()
        {
            List<Event> ret = new List<Event>();
            List<Invite> invites = new List<Invite>();
            try
            {
                HttpContext current = HttpContext.Current;
                string username = current.Session["LOGGEDIN"].ToString();
                int id = getUserId(username);
                //string sql = "select * from dbo.Invites where userId1 = '" + id + "'";
                string sql = "select dbo.Invites.inviteId,dbo.Invites.userId1,dbo.Invites.userId2,dbo.Invites.eventId,dbo.Invites.createrId,dbo.Invites.Status, ev.title, us.username, ev.date from((dbo.Invites inner join dbo.Events as ev on dbo.Invites.eventId = ev.eventId) inner join dbo.Users as us on dbo.Invites.createrId = us.userId) where dbo.Invites.userId2 = '" + id + "'";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);
                List<int> ids = new List<int>();
                
                

                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    Invite invite = new Invite();
                    object[] data = row.ItemArray;
                    invite.id = (int)data[0];
                    invite.UserId1 = (int)data[1];
                    invite.UserId2 = (int)data[2];
                    invite.eventId = (int)data[3];
                    invite.createrId = (int)data[4];
                    invite.status = data[5].ToString();
                    invite.invitetitle = data[6].ToString();
                    invite.inviteusername = data[7].ToString();
                    invite.invitedate = data[8].ToString();

                    invites.Add(invite);
                }


                foreach(Invite irows in invites)
                {
                    int eId = irows.eventId;
                    Event e = new Event();
                    e = GetEvent(eId);
                    irows.events = e;
                }

                
            }
            catch (Exception)
            {
                throw;
            }
            return invites;

        }


        public List<Invite> getInvite(string uname)
        {
            List<Event> ret = new List<Event>();
            List<Invite> invites = new List<Invite>();
            try
            {
                HttpContext current = HttpContext.Current;
                //string username = current.Session["LOGGEDIN"].ToString();
                string username = uname;
                int id = getUserId(uname);
                //string sql = "select * from dbo.Invites where userId1 = '" + id + "'";
                string sql = "select dbo.Invites.inviteId,dbo.Invites.userId1,dbo.Invites.userId2,dbo.Invites.eventId,dbo.Invites.createrId,dbo.Invites.Status, ev.title, us.username, ev.date from((dbo.Invites inner join dbo.Events as ev on dbo.Invites.eventId = ev.eventId) inner join dbo.Users as us on dbo.Invites.createrId = us.userId) where dbo.Invites.userId2 = '" + id + "'";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);
                List<int> ids = new List<int>();



                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    Invite invite = new Invite();
                    object[] data = row.ItemArray;
                    invite.id = (int)data[0];
                    invite.UserId1 = (int)data[1];
                    invite.UserId2 = (int)data[2];
                    invite.eventId = (int)data[3];
                    invite.createrId = (int)data[4];
                    invite.status = data[5].ToString();
                    invite.invitetitle = data[6].ToString();
                    invite.inviteusername = data[7].ToString();
                    invite.invitedate = data[8].ToString();

                    invites.Add(invite);
                }


                foreach (Invite irows in invites)
                {
                    int eId = irows.eventId;
                    Event e = new Event();
                    e = GetEvent(eId);
                    irows.events = e;
                }


            }
            catch (Exception)
            {
                throw;
            }
            return invites;

        }







        private int getUserId(string uname)
        {
            int id = 0;
            try
            {
                string sql = String.Format("select userId from dbo.Users where email='{0}' ", uname);

            DataSet obj = _idac.DataSetXEQDynamicSql(sql);

            if (obj != null)
            {
                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    object[] data = row.ItemArray;
                    id = (int)data[0];

                }
            }
            else
            {
                return id;
            }

        }
            catch (Exception)
            {
                throw;
            }
            return id;
        }



        private int getEventId(string uname, Event events)
        {

            // make the select more accurate . select by date also
            int id = 0;
            try
            {
                string sql = String.Format("select eventId from dbo.Events where setby='{0}' and title='{1}' and location='{2}' ", uname, events.name, events.location);

                DataSet obj = _idac.DataSetXEQDynamicSql(sql);

                if (obj != null)
                {
                    foreach (DataRow row in obj.Tables[0].Rows)
                    {
                        object[] data = row.ItemArray;
                        id = (int)data[0];

                    }
                }
                else
                {
                    return id;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return id;
        }













        public bool UpdateEvent(int id,Event events)
        {
            bool ret = false;
            try
            {
                string dates = events.day;
                string location = events.location;
                string setBy = events.setBy;
                string sql = String.Format(@"UPDATE [dbo].[Events] set date = '{0}', location = '{1}', setBy = '{2}', title = '{3}' where eventId = {4}",
                    dates, location, setBy, events.name, id);


                object obj = _idac.InsertUpdateDelete(sql);
                if (obj != null && (int)obj != 0)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }


        public bool VerifyLogin(string name, string pwd)
        {
            bool ret = false;
            try
            {
                string sql = "select username from dbo.Users where " +
                    "Username='" + name + "' and pass='" +
                    pwd + "'";
                object obj = _idac.GetSingleAnswer(sql);
                if (obj != null)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }



        public void createUser(User user)
        {
            string connStr = ConfigurationManager.ConnectionStrings["Calendar"].ConnectionString;

            string query = "INSERT INTO[dbo].[Users] ([username] ,[pass] ,[email] ,[fname] ,[lname]) VALUES (@username,@pass,@email,@fname,@lname)";


            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand commands = new SqlCommand(query, connection);
                commands.Parameters.AddWithValue("@username", user.username);
                commands.Parameters.AddWithValue("@pass", user.pass);
                commands.Parameters.AddWithValue("@email", user.email);
                commands.Parameters.AddWithValue("@fname", user.fname);
                commands.Parameters.AddWithValue("@lname", user.lname);


                try
                {
                    connection.Open();
                    int result = commands.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public bool acceptInvite(int id)
        {

            bool ret = false;
            try
            {
                
                string sql = String.Format(@"UPDATE [dbo].[Invites] set status = '{0}' where inviteId = {1}",
                     "Accepted", id);


                object obj = _idac.InsertUpdateDelete(sql);
                if (obj != null && (int)obj != 0)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public bool denyInvite(int id)
        {

            bool ret = false;
            try
            {
                //add user id / we dont want users to update invte that dont belong to them
                string sql = String.Format(@"UPDATE [dbo].[Invites] set status = '{0}' where inviteId = {1}",
                     "Deny", id);


                object obj = _idac.InsertUpdateDelete(sql);
                if (obj != null && (int)obj != 0)
                {
                    ret = true;
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> ret = new List<Employee>();
            try
            {
                HttpContext current = HttpContext.Current;
                string username = current.Session["LOGGEDIN"].ToString();
                string sql = "select * from dbo.Employees";
                DataSet obj = _idac.DataSetXEQDynamicSql(sql);


                foreach (DataRow row in obj.Tables[0].Rows)
                {
                    Employee tempEmployee = new Employee();
                    object[] data = row.ItemArray;
                    int id = (int)data[0];
                    tempEmployee.lname = data[1].ToString();
                    tempEmployee.fname  = data[2].ToString();
                    tempEmployee.address  = data[3].ToString();
                    tempEmployee.city  = data[4].ToString();

                    
                    ret.Add(tempEmployee);

                }


            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
    }
}