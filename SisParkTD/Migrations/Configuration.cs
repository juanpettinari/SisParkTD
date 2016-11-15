namespace SisParkTD.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.SpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(DAL.SpContext context)
        //{
        //    var tiposDeMovimientoFinanciero = new List<TipoDeMovimientoFinanciero>
        //    {
        //        new TipoDeMovimientoFinanciero {TipoDeMovimientoFinancieroId = 1, Nombre = "Facturación Ticket Abono", Descripcion = "La facturación mensual del ticket de un abono", Signo = Signo.Negativo},
        //        new TipoDeMovimientoFinanciero {TipoDeMovimientoFinancieroId = 2, Nombre = "Pago Ticket Abono", Descripcion = "El pago del ticket de abono mensual", Signo = Signo.Positivo},
        //        new TipoDeMovimientoFinanciero {TipoDeMovimientoFinancieroId = 3, Nombre = "Pago Ticket Ocasional", Descripcion = "El pago de un ticket ocasional", Signo = Signo.Positivo}
        //    };
        //    tiposDeMovimientoFinanciero.ForEach(t => context.TiposDeMovimientoFinanciero.AddOrUpdate(n => n.Nombre, t));
        //    context.SaveChanges();

        //}


        //protected override void Seed(DAL.SpContext context)
        //{
        //    var tiposDeVehiculo = new List<TipoDeVehiculo>
        //    {
        //        new TipoDeVehiculo {Nombre = "Moto",     TamanioVehiculo = TamanioVehiculo.Chico,TarifaMensualDecimal = 800,TarifaOcasionalDecimal = 4},
        //        new TipoDeVehiculo {Nombre = "Automóvil",TamanioVehiculo = TamanioVehiculo.Mediano,TarifaMensualDecimal = 2000,TarifaOcasionalDecimal = 10},
        //        new TipoDeVehiculo {Nombre = "Camioneta",TamanioVehiculo = TamanioVehiculo.Grande,TarifaMensualDecimal = 2400,TarifaOcasionalDecimal = 12}
        //    };

        //    tiposDeVehiculo.ForEach(tdv => context.TiposDeVehiculo.AddOrUpdate(p => p.Nombre, tdv));

        //    context.SaveChanges();

        //    var parcelas = new List<Parcela>
        //    {
        //        new Parcela {Disponible = true, NumeroParcela = 1,TipoDeVehiculo = tiposDeVehiculo[0]},
        //        new Parcela {Disponible = true, NumeroParcela = 2, TipoDeVehiculo = tiposDeVehiculo[0]},
        //        new Parcela {Disponible = true, NumeroParcela = 3, TipoDeVehiculo = tiposDeVehiculo[1]},
        //        new Parcela {Disponible = true, NumeroParcela = 4, TipoDeVehiculo = tiposDeVehiculo[1]},
        //        new Parcela {Disponible = true, NumeroParcela = 5, TipoDeVehiculo = tiposDeVehiculo[1]},
        //        new Parcela {Disponible = true, NumeroParcela = 6, TipoDeVehiculo = tiposDeVehiculo[1]},
        //        new Parcela {Disponible = true, NumeroParcela = 7, TipoDeVehiculo = tiposDeVehiculo[2]},
        //        new Parcela {Disponible = true, NumeroParcela = 8, TipoDeVehiculo = tiposDeVehiculo[2]}
        //    };

        //    parcelas.ForEach(p => context.Parcelas.AddOrUpdate(pa => pa.NumeroParcela, p));
        //}
    }
}
