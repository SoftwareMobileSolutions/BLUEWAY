using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BLUEWAY.Interfaces;
using BLUEWAY.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using BLUEWAY.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using System;

namespace BLUEWAY.Services
{
    public class adSolicitarAsignacionService : IadSolicitarViaje
    {
        private readonly SqlCnConfigMain _context;
        public adSolicitarAsignacionService(SqlCnConfigMain context)
        {
            _context = context;
        }


        //public async Task<IEnumerable<adSolicitudAsignacion.treeDelictivos>> gettreeMobiles(int userid)
        //{
        //    IEnumerable<adSolicitudAsignacion.treeDelictivos> data;
        //    var conn = _context.CreateConnection();
        //    using (conn)
        //    {
        //        string query = @"exec blueway_getSubflota @userid";
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }
        //        data = await conn.QueryAsync<adSolicitudAsignacion.treeDelictivos>(query, new { userid }, commandType: CommandType.Text);
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //    return data;
        //}

        public async Task<IEnumerable<adSolicitudAsignacion.gridSolicitudes>> getGridSolicitudesPorUsuario(string fechaini, string fechafin, int companyid, int userid, int rolid)
        {
            IEnumerable<adSolicitudAsignacion.gridSolicitudes> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_ObtenerViajesBCRByUser @fechaini, @fechafin, @companyid, @userid, @rolid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.gridSolicitudes>(query, new { fechaini, fechafin, companyid, userid, rolid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<adSolicitudAsignacion.ddl_brigada>> ddl_brigada(int userid)
        {
            IEnumerable<adSolicitudAsignacion.ddl_brigada> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_ObtenerBrigadaBCR @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.ddl_brigada>(query, new { userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<adSolicitudAsignacion.cantones>> ddl_cantones()
        {
            IEnumerable<adSolicitudAsignacion.cantones> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_obtenerCantones";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.cantones>(query, new { }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<adSolicitudAsignacion.eventosBitacora>> ddl_eventos()
        {
            IEnumerable<adSolicitudAsignacion.eventosBitacora> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_getEventosReasignacion";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.eventosBitacora>(query, new { }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<mensajesModel>> set_datosSolicitadosGuardar(int op, int userid, int companyid, string fechaSalida, string fechaEntrada, int supervisorid, bool pernoctara, int estadoId, string observacion, string brigada, string detallemunicipios, int? bitacoraId = null)
        {
            //int? bitacoraId = null;
            int? mobileid = null;
            int? motoristaid = null;
            int? mobileidReasignado = null;
            int? eventoId = null;
            int? motoristaReasignado = null;

            IEnumerable<mensajesModel> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_CU_ViajeBCR @op, @userid, @companyid, @bitacoraId, @mobileid, @motoristaid, @fechaSalida, @fechaEntrada, @supervisorid, @pernoctara, @estadoId, @mobileidReasignado, @eventoId, @motoristaReasignado, @observacion, @brigada, @detallemunicipios";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<mensajesModel>(query, new { op, userid, companyid, bitacoraId, mobileid, motoristaid, fechaSalida, fechaEntrada, supervisorid, pernoctara, estadoId, mobileidReasignado, eventoId, motoristaReasignado, observacion, brigada, detallemunicipios }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        //Asignación de vehículo
        public async Task<IEnumerable<adSolicitudAsignacion.motoristadata>> getddl_mobilemotorista(int userid)
        {
            IEnumerable<adSolicitudAsignacion.motoristadata> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_ObtenerMobileMotorista @userid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.motoristadata>(query, new { userid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }



        public async Task <(IEnumerable<adSolicitudAsignacion.sendMail.encabezado>, IEnumerable<adSolicitudAsignacion.sendMail.detalle>)> sendMail(int bitacoraid, int? userId)
        {
            IEnumerable<adSolicitudAsignacion.sendMail.encabezado> encabezado;
			IEnumerable<adSolicitudAsignacion.sendMail.detalle> detalle;
            string fechaIni = null;
            string fechaFin = null;
            string strMobileid = null;
           // int? userId = null;
            int? tipoReporte = 1;

            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec Fleets_rpViajesBCR @fechaIni, @fechaFin, @strMobileid, @userId, @bitacoraId, @tipoReporte";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                encabezado = await conn.QueryAsync<adSolicitudAsignacion.sendMail.encabezado>(query, new { fechaIni, fechaFin, strMobileid, userId, bitacoraid, tipoReporte }, commandType: CommandType.Text);
                tipoReporte = 0;
                detalle = await conn.QueryAsync<adSolicitudAsignacion.sendMail.detalle>(query, new { fechaIni, fechaFin, strMobileid, userId, bitacoraid, tipoReporte }, commandType: CommandType.Text);

				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
            return  (encabezado, detalle);
        }
        //Obtener Estados
        public async Task<IEnumerable<adSolicitudAsignacion.estadoBitacora>> getEstadoBitacora(int bitacoraid)
        {
            IEnumerable<adSolicitudAsignacion.estadoBitacora> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_obtenerEstadoPorBitacora @bitacoraid";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<adSolicitudAsignacion.estadoBitacora>(query, new { bitacoraid }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        } 
        
        public async Task<IEnumerable<mensajesModel>> setCancelar(int bitacoraid, string cancelarcomentario)
        {
            IEnumerable<mensajesModel> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_cancelar @bitacoraid, @cancelarcomentario";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<mensajesModel>(query, new { bitacoraid, cancelarcomentario }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }

        public async Task<IEnumerable<mensajesModel>> setAsignarReasignar(int bitacoraid, int mobileid, int motoristaid, int userid, int op_reasignar, int eventoid, string observacion)
        {
            IEnumerable<mensajesModel> data;
            var conn = _context.CreateConnection();
            using (conn)
            {
                string query = @"exec mBlueWay_asignar_reasignarVehiculoMotorista @bitacoraid, @mobileid, @motoristaid, @userid, @op_reasignar, @eventoid, @observacion";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                data = await conn.QueryAsync<mensajesModel>(query, new { bitacoraid, mobileid, motoristaid, userid, op_reasignar, eventoid, observacion }, commandType: CommandType.Text);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return data;
        }
    }
}
