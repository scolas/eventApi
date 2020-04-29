using eventApi.Models;
using System.Collections.Generic;

namespace eventApi.BusinessLayer
{
    public interface IBussinessEvent
    {
        Event getEvent(int day, int month, int year);
        List<Event> events();

        List<Event> events(string username);

        bool SaveEvent(Event events);

        bool CreateEvent(Event e);

        bool UpdateEvent(int id,Event events);

        bool DeleteEvent(int id);

        List<Event> GetDayEvents(Day day);
    }
}