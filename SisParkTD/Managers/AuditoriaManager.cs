using SisParkTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisParkTD.Managers
{
    public interface IAuditoriaManager
    {
        AuditoriaLogIn Create(Ticket tkt);
    }
    
    public class AuditoriaTicketManager : IAuditoriaManager
    {
        public AuditoriaLogIn Create(Ticket tkt)
        {
            var model = new AuditoriaLogIn();
            //TODO Aca hace la magia
            return model;
        }
    }
    
    public class AuditoriaLogInManager : IAuditoriaManager
    {
        public AuditoriaLogIn Create(Ticket tkt)
        {
            var model = new AuditoriaLogIn();
            //TODO Aca hace la magia
            return model;
        }
    }

}