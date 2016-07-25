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

        protected override void Seed(DAL.SpContext context)
        {
            var tama�os = new List<Tama�o>
            {
                new Tama�o {Descripcion = "Peque�o", ValorTama�o = 1},
                new Tama�o {Descripcion = "Mediano", ValorTama�o = 2},
                new Tama�o {Descripcion = "Grande", ValorTama�o = 3}
            };

            tama�os.ForEach(t => context.Tama�os.AddOrUpdate(t));

            var parcelas = new List<Parcela>
            {
                new Parcela {Disponible = true, NumeroParcela = 1, Tama�o = tama�os[0]},
                new Parcela {Disponible = true, NumeroParcela = 2, Tama�o = tama�os[0]},
                new Parcela {Disponible = true, NumeroParcela = 3, Tama�o = tama�os[1]},
                new Parcela {Disponible = true, NumeroParcela = 4, Tama�o = tama�os[1]},
                new Parcela {Disponible = true, NumeroParcela = 5, Tama�o = tama�os[1]},
                new Parcela {Disponible = true, NumeroParcela = 6, Tama�o = tama�os[1]},
                new Parcela {Disponible = true, NumeroParcela = 7, Tama�o = tama�os[2]},
                new Parcela {Disponible = true, NumeroParcela = 8, Tama�o = tama�os[2]}
            };

            parcelas.ForEach(p => context.Parcelas.AddOrUpdate(p));

            var tiposDeVehiculo = new List<TipoDeVehiculo>
            {
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Moto",Tama�o = tama�os[0],TarifaMensualDecimal = 1500,TarifaOcasionalDecimal = 5M},
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Autom�vil",Tama�o = tama�os[1],TarifaMensualDecimal = 2900,TarifaOcasionalDecimal = 10M},
                new TipoDeVehiculo {NombreDeTipoDeVehiculo = "Camioneta",Tama�o = tama�os[2],TarifaMensualDecimal = 3500,TarifaOcasionalDecimal = 13M}
            };



        }
    }
}
