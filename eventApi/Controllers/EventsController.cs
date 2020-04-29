 using eventApi.BusinessLayer;
using eventApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CalendarApi.Controllers
{
    public class EventsController : ApiController
    {
        // GET: api/Events
        public List<Event> Get()
        {
            List<Event> eventList = new List<Event>();
            IBussinessEvent bussinessEvent = new BusinessEvent();
            eventList = bussinessEvent.events();
            return eventList;
        }

        public List<Event> Get(string username)
        {

            List<Event> eventList = new List<Event>();
            IBussinessEvent bussinessEvent = new BusinessEvent();
            eventList = bussinessEvent.events(username);
            return eventList;
        }

        // GET: api/Events/5
        public Event Get(int id)
        {
            IBussinessEvent bussinessEvent = new BusinessEvent();
            List<Event> eventsList = bussinessEvent.events();
            Event formEvent = eventsList.Find(x => x.id == id);
            return formEvent;
        }


        //may have to create seperat class for appointments
        [Route("api/events/appointments")]
        [HttpGet]
        public List<Invite> GetInvite(string username)
        {
            IBusinessInvite businessInvite = new BusinessInvite();
            List<Invite> invites = new List<Invite>();
            invites = businessInvite.getInvite(username);
            return invites;
        }

        // POST: api/Events
        /*  public void Post([FromBody]string date, [FromBody]string location, [FromBody]string setBy, [FromBody]string title)
          {
              Event temp_event = new Event() {
                  name = title,
                  day  = DateTime.Now,
                  location = location,
                  setBy = setBy,

              };
              IBussinessEvent createEvent = new BusinessEvent();
              createEvent.CreateEvent(temp_event);


          }*/
        public void Post([FromBody]Event ev)
        {
            IBussinessEvent createEvent = new BusinessEvent();
            createEvent.CreateEvent(ev);
        }


        public void Put(int id, [FromBody]Event ev)
        {
            //Event event;
            IBussinessEvent events = new BusinessEvent();
            events.UpdateEvent(id, ev);
        }





        // DELETE: api/Events/5
        public void Delete(int id)
        {
            IBussinessEvent events = new BusinessEvent();
            events.DeleteEvent(id);
        }
    }
}
