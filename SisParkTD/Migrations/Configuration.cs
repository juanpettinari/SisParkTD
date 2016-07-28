using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using SisParkTD.Models;

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
        //    var tama�os = new List<Tama�o>
        //    {
        //        new Tama�o {Descripcion = "Peque�os", ValorTama�o = 1},
        //        new Tama�o {Descripcion = "Mediano", ValorTama�o = 2},
        //        new Tama�o {Descripcion = "Grande", ValorTama�o = 3}
        //    };

        //    tama�os.ForEach(t => context.Tama�os.AddOrUpdate(p=>p.Descripcion,t));

        //    context.SaveChanges();

        //    var parcelas = new List<Parcela>
        //    {
        //        new Parcela {Disponible = true, NumeroParcela = 1, Tama�oId = tama�os[0].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 2, Tama�oId = tama�os[0].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 3, Tama�oId = tama�os[1].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 4, Tama�oId = tama�os[1].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 5, Tama�oId = tama�os[1].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 6, Tama�oId = tama�os[1].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 7, Tama�oId = tama�os[2].Tama�oId},
        //        new Parcela {Disponible = true, NumeroParcela = 8, Tama�oId = tama�os[2].Tama�oId}
        //    };

        //    parcelas.ForEach(p => context.Parcelas.AddOrUpdate(pa=> pa.NumeroParcela,p));



        //    var tiposDeVehiculo = new List<TipoDeVehiculo>
        //    {
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Moto",Tama�oId = tama�os[0].Tama�oId,TarifaMensualDecimal = 800,TarifaOcasionalDecimal = 4},
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Autom�vil",Tama�oId = tama�os[1].Tama�oId,TarifaMensualDecimal = 2000,TarifaOcasionalDecimal = 10},
        //        new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Camioneta",Tama�oId = tama�os[2].Tama�oId,TarifaMensualDecimal = 2400,TarifaOcasionalDecimal = 12}
        //    };

        //    tiposDeVehiculo.ForEach(tdv => context.TiposDeVehiculo.AddOrUpdate(p=> p.NombreDeTipoDeVehiculo,tdv));

        //    context.SaveChanges();
        //}
    }
}
