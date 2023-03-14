using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using BLUEWAY.Models;

namespace BLUEWAY.Interfaces
{
    public interface IadSolicitarViaje
    {
        //Task<IEnumerable<adSolicitudAsignacion.treeDelictivos>> gettreeMobiles(int userid);
       
        Task<IEnumerable<adSolicitudAsignacion.gridSolicitudes>> getGridSolicitudesPorUsuario(string fechaini, string fechafin, int companyid, int userid, int rolid);
        Task<IEnumerable<adSolicitudAsignacion.ddl_brigada>> ddl_brigada(int userid);
        Task<IEnumerable<adSolicitudAsignacion.cantones>> ddl_cantones();
        Task<IEnumerable<adSolicitudAsignacion.eventosBitacora>> ddl_eventos();

        Task<IEnumerable<mensajesModel>> set_datosSolicitadosGuardar(int op, int userid, int companyid, string fechaSalida, string fechaEntrada, int supervisorid,bool pernoctara, int estadoId, string observacion, string brigada, string detallemunicipios, int? bitacoraId = null);
        Task<IEnumerable<adSolicitudAsignacion.motoristadata>> getddl_mobilemotorista(int userid);
        Task<(IEnumerable<adSolicitudAsignacion.sendMail.encabezado>, IEnumerable<adSolicitudAsignacion.sendMail.detalle>)> sendMail (int bitacoraid, int? userId);
        Task<IEnumerable<adSolicitudAsignacion.estadoBitacora>> getEstadoBitacora(int bitacoraid);
        Task<IEnumerable<mensajesModel>> setCancelar(int bitacoraid, string cancelarcomentario);
        Task<IEnumerable<mensajesModel>> setAsignarReasignar(int bitacoraid, int mobileid, int motoristaid, int userid, int op_reasignar, int eventoid, string observacion);
    }
}
