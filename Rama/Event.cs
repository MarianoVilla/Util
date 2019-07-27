using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rama
{
    class Event
    {
        public List<Invite> ListOfInvites { get; set; } = new List<Invite>();

        public Event(string[] InvitedUserEmails)
        {
            int i = 0;
            foreach(var invited in InvitedUserEmails)
            {
                ListOfInvites.Add(new Invite { EmailId = invited });
            }
        }

        public List<Invite> GetInvited(string userEmailId)
        {
            var events = ListOfInvites.Where(x => x.EmailId.Contains(userEmailId)).ToList();
            return events;
        }
    }
}
