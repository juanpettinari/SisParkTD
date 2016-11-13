using SisParkTD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Builder Pattern
/// </summary>
namespace SisParkTD.Managers
{
    public class TicketManager
    {
        public Ticket CreateTicket()
        {
            // Create director and builders
            TicketDirector director = new TicketDirector();

            //TODO Evaluar que tipo de ticket se creara
            Builder builder;
            if (true)
            {
                builder = new TicketBuilder();
            }
            else
            {
                builder = new AbonoBuilder();
            }

            // Construct two products
            director.Construct(builder);
            Ticket ticket = builder.GetResult();
            
            //TODO agregar el resto de los datos necesarios (aplicar tempate method)
            
            //CASO BASE Desarrollar ACA
            //ticket.llllll
            return ticket; 
        }
    }

    /// <summary>
    /// The 'Director' class
    /// </summary>
    class TicketDirector
    {
        // Builder uses a complex series of steps
        public void Construct(Builder builder)
        {
            builder.BuildTicket();
        }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    abstract class Builder
    {
        public abstract void BuildTicket();
        public abstract Ticket GetResult();
    }

    /// <summary>
    /// The 'TicketBuilder' class
    /// </summary>
    class TicketBuilder : Builder
    {
        private Ticket Ticket = new Ticket();

        public override void BuildTicket()
        {
            //TODO definir crearion de Ticket normal
            //Ticket.Vehiculo.....
        }


        public override Ticket GetResult()
        {
            return Ticket;
        }
    }

    /// <summary>
    /// The 'AbonoBuilder' class
    /// </summary>
    class AbonoBuilder : Builder
    {
        private Ticket Ticket = new Ticket();

        public override void BuildTicket()
        {
            //TODO definir crearion de Ticket abono
        }

        public override Ticket GetResult()
        {
            return Ticket;
        }
    }

    ///// <summary>
    ///// The 'Product' class
    ///// </summary>
    //class Product
    //{
    //    private List<string> _parts = new List<string>();

    //    public void Add(string part)
    //    {
    //        _parts.Add(part);
    //    }

    //    public void Show()
    //    {
    //        Console.WriteLine("\nProduct Parts -------");
    //        foreach (string part in _parts)
    //            Console.WriteLine(part);
    //    }
    //}
}

