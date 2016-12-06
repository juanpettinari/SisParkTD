using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highsoft.Web.Mvc.Charts;
using SisParkTD.Common;
using SisParkTD.DAL;
using SisParkTD.Models;
using SisParkTD.Models.ViewModels;

namespace SisParkTD.Controllers
{
    public class ReportesController : Controller
    {
        private readonly SpContext _db = new SpContext();
        // GET: Reportes

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        [HttpGet]
        public ActionResult ReporteSegunTipoDeVehiculo()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReporteSegunTipoDeVehiculo(FechaViewModel fechaViewModel)
        {
            var listaId = new List<int>();
            var listaValue = new List<decimal?>();

            foreach (var tipoDeVehiculo in _db.TiposDeVehiculo)
            {
                listaId.Add(tipoDeVehiculo.TipoDeVehiculoId);
            }


            foreach (var i in listaId)
            {
                listaValue.Add(
                    _db.MovimientosFinancieros.Where(
                        mf =>
                            mf.Ticket.Vehiculo.TipoDeVehiculo.TipoDeVehiculoId == i &&
                            mf.TipoDeMovimientoFinanciero.Signo == Signo.Positivo &&
                            mf.Fecha > fechaViewModel.FechaDesde && mf.Fecha < fechaViewModel.FechaHasta)
                        .Sum(mf => mf.Ticket.PrecioTotalDecimal));
            }

            var pieData = new List<PieSeriesData>();

            for (int i = 0; i < listaId.Count; i++)
            {
                var valueDouble = Convert.ToDouble(listaValue[i]);
                pieData.Add(new PieSeriesData { Name = _db.TiposDeVehiculo.Find(listaId[i]).Nombre, Y = valueDouble });
            }

            ViewData["pieData"] = pieData;

            return View();

        }
        public ActionResult ReporteHeatmap()
        {
            // TODO sacar el hardcode
            // 21 al 27 nov
            var startDate = DateTime.Parse("21/11/2016");
            var endDate = DateTime.Parse("27/11/2016");
            // array 2D que tendría los valores
            var array = new int[7, 24];
            List<HeatmapSeriesData> data = new List<HeatmapSeriesData>();
            foreach (var day in EachDay(startDate, endDate))
            {
                for (int i = 0; i < 24; i++)
                {
                    var fechaDesde = day.AddHours(i);
                    var fechaHasta = day.AddHours(i + 1);

                    var value = _db.MovimientosDeVehiculo.Count(mv => mv.Fecha > fechaDesde &&
                    mv.Fecha < fechaHasta && mv.TipoDeMovimientoDeVehiculo == TipoDeMovimientoDeVehiculo.Entrada);

                    var diaSemana = ((int)day.DayOfWeek == 0) ? 7 : (int)day.DayOfWeek;
                    var y = diaSemana - 1;
                    var x = i;
                    array[diaSemana - 1, i] = 1;
                    data.Add(new HeatmapSeriesData { X = x, Y = y, Value = value });
                }
            }
            //(DateTime.Parse("01/12/2016"), DateTime.Parse("31/12/2016")
            ViewData["heatMapData"] = data;
            return View();

        }

        public ActionResult ReporteMensualSegunHoras()
        {
            // TODO SACAR HARDCODE!! MEs
            var startDate = DateTime.Parse("01/11/2016");
            var endDate = DateTime.Parse("30/11/2016");

            var lista = new List<int>();

            for (int i = 0; i <= 9; i++)
            {
                var topeAbajo = (i) * 3600;
                var topeArriba = (i + 1) * 3600;

                var value = _db.Tickets.Count(t => t.TiempoTotal < topeArriba &&
                t.TiempoTotal > topeAbajo && t.TipoDeTicket == TipoDeTicket.Ocasional);
                lista.Add(value);
            }
            lista.Add(_db.Tickets.Count(t => t.TiempoTotal > 10 * 3600));

            List<double?> reportesValues = new List<double?>
            {
                lista[0],lista[1], lista[2],lista[3],lista[4],lista[5],
                lista[6],lista[7],lista[8],lista[9],lista[10]
            };

            List<BarSeriesData> reportesData = new List<BarSeriesData>();

            reportesValues.ForEach(p => reportesData.Add(new BarSeriesData { Y = p }));

            ViewData["reportesData"] = reportesData;

            return View();
        }


        public ActionResult ReporteMensual()
        {
            //TODO SACAR HARDCODE DEL AÑO
            var array = new double[3, 12];

            var j = 0;
            foreach (var tipoDeVehiculo in _db.TiposDeVehiculo)
            {
                for (int i = 0; i < 12; i++)
                {
                    var fechaDesde = DateTime.Parse("01/" + (i + 1) + "/2016");
                    var fechaHasta = new DateTime();
                    if (i != 11)
                    {
                        fechaHasta = DateTime.Parse("01/" + (i + 2) + "/2016");
                    }
                    else
                    {
                        fechaHasta = DateTime.Parse("01/01/2017");
                    }

                    var value =
                        _db.MovimientosFinancieros.Where(mf => mf.Ticket.Vehiculo.TipoDeVehiculo.TipoDeVehiculoId
                                                               == tipoDeVehiculo.TipoDeVehiculoId &&
                                                               mf.TipoDeMovimientoFinanciero.Signo == Signo.Positivo
                                                               && mf.Fecha > fechaDesde && mf.Fecha < fechaHasta)
                            .Sum(mf => mf.Ticket.PrecioTotalDecimal) ?? 0;

                    array[j, i] = Convert.ToDouble(value);

                }
                j++;
            }

            var camionetasValues = new List<double>();
            var autosValues = new List<double>();
            var motosValues = new List<double>();

            for (int i = 0; i < 12; i++)
            {
                camionetasValues.Add(array[0, i]);
                autosValues.Add(array[1, i]);
                motosValues.Add(array[2, i]);
            }


            //List<double> tokyoValues = new List<double> { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 };
            //List<double> nyValues = new List<double> { -0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5 };
            //List<double> berlinValues = new List<double> { -0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0 };

            List<LineSeriesData> camionetasData = new List<LineSeriesData>();
            List<LineSeriesData> autosData = new List<LineSeriesData>();
            List<LineSeriesData> motosData = new List<LineSeriesData>();

            camionetasValues.ForEach(p => camionetasData.Add(new LineSeriesData { Y = p }));
            autosValues.ForEach(p => autosData.Add(new LineSeriesData { Y = p }));
            motosValues.ForEach(p => motosData.Add(new LineSeriesData { Y = p }));


            ViewData["camionetasData"] = camionetasData;
            ViewData["autosData"] = autosData;
            ViewData["motosData"] = motosData;


            return View();









        }
    }
}