using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class EstadisticasController
    {
        //MODELO GENERICO
        ReportePagosGeneralDataGridView reportepagosgeneralDGV = new ReportePagosGeneralDataGridView();
        ReporteTransaccionesDataGridView reportetransaccionesDGV = new ReporteTransaccionesDataGridView();

        //CARGAR LOS REPORTES DE PAGOS GENERAL
        public List<ReportePagosGeneralDataGridView> reportePagosGeneral()
        {
            return reportepagosgeneralDGV.dgvPagosGeneralReporte().Where(p => p.pag_fechapagado != null).ToList();
        }
        public List<ReportePagosGeneralDataGridView> reportePagosGeneralPeriodo(DateTime fechainicio, DateTime fechafin)
        {
            return reportepagosgeneralDGV.dgvPagosGeneralReporte().Where(p => p.pag_fechapagado >= fechainicio && p.pag_fechapagado <= fechafin && p.pag_fechapagado != null).ToList();
        }

        //CARGAR LOS REPORTES DE TRANSACCIONES GENERALES

        public List<ReporteTransaccionesDataGridView> reporteTransacciones()
        {
            return reportetransaccionesDGV.dgvTransaccionesdGeneralReporte();
        }

        //CARGAR LOS REPORTES DE TRANSACCIONES GENERALES POR FECHAS INICIO-FIN
        public List<ReporteTransaccionesDataGridView> reporteTransaccionesPeriodo(DateTime fechainicio, DateTime fechafin)
        {
            return reportetransaccionesDGV.dgvTransaccionesdGeneralReporte().Where(h => h.his_fecha >= fechainicio && h.his_fecha <= fechafin).ToList();
        }
    }
}
