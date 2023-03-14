using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BLUEWAY.Interfaces;
using System.Linq;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System;
using BLUEWAY.Models;
using BLUEWAY.Util;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace BLUEWAY.Controllers
{
    public class adSolicitarViajeController : Controller
    {
        private readonly IadSolicitarViaje IadSolicitarViaje;
        public adSolicitarViajeController(IadSolicitarViaje _IadSolicitarViaje)
        {
            IadSolicitarViaje = _IadSolicitarViaje;
        }
        public IActionResult adSolicitarViaje()
        {
            //ViewData["datausuario"] = usuario;
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario"));
			ViewData["datausuario"] = usuario;
            return PartialView();
        }

        [HttpGet]
        public async Task<JsonResult> getSolicitudes(string fechaini, string fechafin)
        {
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.getGridSolicitudesPorUsuario(fechaini, fechafin, usuario.companyid, usuario.userid, usuario.rolid);

            return await Task.Run(() => {
                return Json(data);
            });
        }

        //DDL Brigada
        [HttpGet]
        public async Task<JsonResult> ObtenerBrigadas()
        {
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.ddl_brigada(usuario.userid);
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //DDL Cantones
        [HttpGet]
        public async Task<JsonResult> ObtenerCantones()
        {
          
            var data = await IadSolicitarViaje.ddl_cantones();
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }

        //GuardarDatos de solicitud de viaje
        [HttpPost]
        public async Task<JsonResult> guardarDatos(string fechaSalida, string fechaEntrada, bool pernoctara, int supervisorid, string observacion, string brigada, string detallemunicipios, int op, int? bitacoraId = null)
        {
          //  int op = 1;
            int estadoId = 5;
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.set_datosSolicitadosGuardar(op, usuario.userid, usuario.companyid, fechaSalida, fechaEntrada, supervisorid, pernoctara, estadoId, observacion, brigada, detallemunicipios, bitacoraId);

			//Enviar correo
			await sendMail(data.FirstOrDefault().bitacoraid);

			return await Task.Run(() =>
            {
                return Json(data.Select( x => new { x.op, x.msj }).FirstOrDefault());
            });
        }



		//Asignación de vehículo
		[HttpGet]
		public async Task<JsonResult> getMobileMotoristas()
		{
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.getddl_mobilemotorista(usuario.userid);
			return await Task.Run(() =>
			{
				return Json(data);
			});
		}

		private async Task<bool> sendMail(int bitacoraid)
		{

            //var data = ViajesDAL.ObtenerDataEmail(bitacoraid/*, DatosUsuario.Userid*/);
            //string SendMailFrom = "gproyectos.soportenovusgd@gmail.com";
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.sendMail(bitacoraid, usuario.userid);
			
			if (data.Item1.Count() == 0) //Si no vienen datos
			{
				return false;
			}


			var head = data.Item1.FirstOrDefault();
			var detalle = data.Item2.ToList();


			string SendMailFrom = "smsnotificaciones@sms-open.com";
			string SendMailPassword = "Notificac_ones09183$192";
			string SendMailTo = head.correo;  //"jms_br7@hotmail.es";

			//***********logo 1
			string htmlBody = "<img src=\"cid:filename\">";
			AlternateView avHtml = AlternateView.CreateAlternateViewFromString
			   (htmlBody, null, MediaTypeNames.Text.Html);


			/*
            string file = Server.MapPath("~/Images/logo_BCR.png");

			LinkedResource inline = new LinkedResource(file, MediaTypeNames.Image.Jpeg);
			inline.ContentId = Guid.NewGuid().ToString();
			avHtml.LinkedResources.Add(inline);

			MailMessage mail = new MailMessage();
			mail.AlternateViews.Add(avHtml);

			Attachment att = new Attachment(file);
			att.ContentDisposition.Inline = true;
            */

			string tituloCorreo = "Viaje #" + bitacoraid.ToString();

			//Variables basicas

			string imglogo = "data:image/png;base64,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";
			//string imglogo = "http://www.bluefenyx.com/Fleetmanager/Images/logo_bcr.png";
			string SendMailSubject = "BCR - " + tituloCorreo + " " + DateTime.Now.ToString("yyyy-MM_dd-hh-mm", CultureInfo.InvariantCulture);

			string destinos = "";
			for (int i = 0, len = detalle.Count(); i < len; i++)
			{
				destinos +=
						"<tr>" +
							"<td align='center'>" + detalle[i].fecha.Split(' ')[0] + "</td> " +
							"<td align='center'></td> " +
							"<td align='center'></td>" +
							"<td align='left' colspan='2'>" + detalle[i].destinos + "</td>" +
							"<td align='center'></td> " +
							"<td align='center' class='tdOnlyeRight'></td>" +
						"</tr>";
			}
			string css =
				(@"<style type='text/css'>" +
					"body{" +
						"font-family: sans-serif;" +
					"}" +
					"table {" +
						"border: none;" +
					"}" +
					"td { padding: 7px; }" +
					".tdnone {" +
						"border: none;" +
					"}" +
					".tdnoneExTop {" +
						"border-left: none;" +
						"border-right: none;" +
						"border-bottom: none;" +
					"}" +
					".tdnoneExLeftRight {" +
						"border-top: none;" +
						"border-bottom: none;" +
					"}" +
					".tdnoneExLeft {" +
						"border-top: none;" +
						"border-bottom: none;" +
						"border-right: none;" +
				   " }" +
					".tdOnlyeRight { border-right: 1px solid black; }" +

					".tdonlyBottom {" +
						"border-bottom: 1px solid #b9b9b9;" +
						"border-top: 0px;" +
						"border-left: 0px;" +
						"border-right: 0px;" +
					"}" +
					".tdAlto {" +
						"height: 20px;" +
						"display:block" +
					"}" +
					".verticalmiddle {" +
						"vertical-align: middle;" +
					"}" +
					".tdremoveleft {  " +
					"border-right: 1px solid black;" +
					"border-left: none;" +
					"}" +
					".paddingnone { padding: 0px; }" +
				"</style>").Replace("{", "{{").Replace("}", "}}");
			string SendMailBody = string.Format(
				css +
				"<div width='600px'>" +
					"<table role='presentation' border='1' cellspacing='0' width='100%'>" +
						/* Encabezado*/
						"<tr>" +
							"<td rowspan='2' class='tdnone paddingnone'>" +
							  //  @"<img src=""cid:{0}"" />"+ 
							  @"<img class='paddingnone' src='" + imglogo + "' />" +
							"</td>" +
							"<td colspan='5' align='center' class='tdnone paddingnone'><b>ONEC</b></td>" +
							"<td align='right' class='tdnone'>Correlativo</td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='5' align='center' class='tdnone paddingnone'><b>Hoja de salida Semandial Misión, oficial y bitácora</b></td>" +
							"<td bgcolor='#ececec' align='right' class='tdnone'><b>" + bitacoraid.ToString() + "</b></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='7' class='tdnone tdAlto'></td>" +
						"</tr>" +
						"<tr>" +
							"<td class='tdnone'></td>" +
							"<td align='center' colspan='2'>VEHÍCULO</td>" +
							"<td align='center' colspan='2'>PLACA</td>" +
							"<td align='center'>MARCA</td>" +
							"<td align='center' class='tdOnlyeRight'>TIPO</td>" +
						"</tr>" +
						"<tr> " +
							"<td class='tdnone'></td>" +
							"<td>EQ</td>" +
							"<td align='center' bgcolor='#ececec'>" + head.vehiculo + "</td>" +
							"<td align='center' colspan='2'>" + head.placa + "</td>" +
							"<td align='center'>" + head.marca + "</td>" +
							"<td align='center' class='tdOnlyeRight'>" + head.tipo + "</td>" +
						"</tr>" +
						"<tr>" +
							"<td class='tdnone'></td>" +
							"<td>MOTORISTA</td>" +
							"<td align='center' class='tdOnlyeRight' colspan='5'>" + head.motorista + "</td>" +
						"</tr>" +
						"<tr>" +
							"<td class='tdnone'></td>" +
							"<td>PROYECTO</td>" +
						   "<td align='center' colspan='5' class='tdOnlyeRight'>" + head.proyecto + "</td>" +
						"</tr>" +
						"<tr>" +
							"<td class='tdnone tdAlto'></td>" +
							"<td colspan='7' class='tdnoneExTop tdAlto'></td>" +
						"</tr>" +
						"<tr>" +
							"<td align='right' class='tdnone'>FECHA DE SALIDA</td>" +
							"<td align='center'>" + head.salidaProgramada.Split(' ')[0] + "</td> " +
							"<td class='tdnoneExLeftRight'></td>" +
							"<td colspan='2' rowspan='3' align='center'><h1><b></b></h1></td>" +
							"<td align='right' class='tdnoneExLeftRight'>FECHA DE ENTRADA</td>" +
							"<td align='center' class='tdOnlyeRight'>" + head.llegadaProgramada.Split(' ')[0] + "</td> " +
						"</tr>" +
						"<tr>" +
							"<td align='right' class='tdnone'>HORA DE SALIDA</td>" +
							"<td align='center'>" + head.salidaProgramada.Split(' ')[1] + "</td>" +
							"<td class='tdnoneExLeftRight'></td>" +
							"<td align='right' class='tdnoneExLeftRight'>HORA DE ENTRADA</td>" +
							"<td align='center' class='tdOnlyeRight'>" + head.llegadaProgramada.Split(' ')[1] + "</td>" +
						"</tr>" +
						"<tr>" +
							"<td align='right' class='tdnone'>KM DE SALIDA</td>" +
							"<td align='center'></td> " +
							"<td class='tdnoneExLeftRight'></td>" +
							"<td align='right' class='tdnoneExLeftRight'>KM DE ENTRADA</td>" +
							"<td align='center' class='tdOnlyeRight'></td> " +
						"</tr>" +
					   "<tr> " +
							"<td class='tdnone tdAlto'></td>" +
							"<td class='tdnoneExTop tdAlto'></td>" +
							"<td class='tdnone tdAlto'></td>" +
							"<td colspan='2' class='tdnoneExTop tdAlto'></td>" +
							"<td class='tdnone tdAlto'></td>" +
							"<td colspan='2' class='tdnoneExTop tdAlto'></td>" +
						"</tr>" +
						"<tr>" +
							"<td align='right' rowspan='2' class='tdnone'>USUARIOS</td>" +
							"<td align='center' colspan='2'>SUPERVISOR</td>" +
							"<td align='center' colspan='4' class='tdOnlyeRight'>BRIGADA</td>" +
						"</tr>" +
						"<tr>" +
							"<td align='center' colspan='2'>" + head.supervisor + "</td>" +
							"<td align='center' colspan='4' class='tdOnlyeRight'>" + head.brigada + "</td>" +
						"</tr>" +
						"<tr>" +
							"<td class='tdnone'></td>" +
							"<td colspan='7' class='tdnoneExTop tdAlto'></td>" +
						"</tr>" +
						/* tabla de destinos */
						"<tr>" +
							"<td align='center'>FECHA</td>" +
							"<td align='center'>KM LLEGADA</td>" +
							"<td align='center'>HORA DE LLEGADA</td>" +
							"<td align='center' colspan='2'>DESTINOS</td>" +
							"<td align='center'>KM SALIDA</td>" +
							"<td align='center' class='tdOnlyeRight'>HORA DE SALIDA</td>" +
						"</tr>" +
					   /* Repetir acá todos los destinos */
					   destinos +
						"<tr><td colspan='8' class='tdnoneExTop tdAlto'></td></tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone'></td>" +
							"<td></td>" +
							"<td>#Vales:</td>" +
							"<td colspan='3' class='tdnoneExLeft'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone'></td>" +
							"<td align='center'>DESDE</td> " +
							"<td align='center'>HASTA</td>" +
							"<td colspan='3' class='tdnoneExLeft'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone tdAlto'></td>" +
							"<td class='tdAlto'></td>" +
							"<td class='tdAlto'></td>" +
							"<td colspan='3' class='tdnoneExLeft class='tdAlto''></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone'></td>" +
							"<td align='right'>Cantidad</td> " +
							"<td></td> " +
							"<td colspan='3' class='tdnoneExLeft'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone'></td>" +
							"<td align='right'>Total</td> " +
							"<td align='left'>$</td>" +
							"<td colspan='3' class='tdnoneExLeft'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' class='tdnone'></td>" +
							"<td colspan='2' class='tdnoneExTop'></td>" +
							"<td colspan='3' class='tdnone tdAlto'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='6' align='left' class='tdAlto'>Visitas no programadas</td>" +
							"<td align='center' class='tdremoveleft'></td>" +
						"</tr>" +
						/* PIE */
						"<tr>" +
							"<td align='center'>FECHA</td> " +
							"<td align='center'>KM LLEGADA</td>" +
							"<td align='center'>HORA DE LLEGADA</td>" +
							"<td align='center' colspan='2'>DESTINOS</td> " +
							"<td align='center'>KM SALIDA</td>" +
							"<td align='center' class='tdOnlyeRight'>HORA DE SALIDA</td>" +
						"</tr>" +
						"<tr>" +
							"<td align='left' class='tdAlto'></td> " +
							"<td align='left'></td> " +
							"<td align='left'></td> " +
							"<td align='left' colspan='2'></td> " +
							"<td align='left'></td> " +
							"<td align='left' class='tdOnlyeRight'></td> " +
						"</tr> " +
						"<tr> " +
							"<td align='left' class='tdAlto'></td> " +
							"<td align='left'></td> " +
							"<td align='left'></td> " +
							"<td align='left' colspan='2'></td> " +
							"<td align='left'></td> " +
							"<td align='left' class='tdOnlyeRight'></td> " +
						"</tr> " +
						"<tr> " +
							"<td align='left' class='tdAlto'></td> " +
							"<td align='left'></td> " +
							"<td align='left'></td> " +
							"<td align='left' colspan='2'></td> " +
							"<td align='left'></td> " +
							"<td align='left' class='tdOnlyeRight'></td> " +
						"</tr> " +
						"<tr> " +
							"<td align='left' class='tdAlto'></td> " +
							"<td align='left'></td> " +
							"<td align='left'></td> " +
							"<td align='left' colspan='2'></td> " +
							"<td align='left'></td> " +
							"<td align='left' class='tdOnlyeRight'></td> " +
						"</tr> " +
						"<tr> " +
							"<td align='left' class='tdAlto'></td> " +
							"<td align='left'></td> " +
							"<td align='left'></td> " +
							"<td align='left' colspan='2'></td> " +
							"<td align='left'></td> " +
							"<td align='left' class='tdOnlyeRight'></td> " +
						"</tr> " +
						"<tr><td colspan='7' class='tdnoneExTop tdAlto'></td></tr>" +
						"<tr><td colspan='7' class='tdnone tdAlto></td></tr>" +
						"<tr><td colspan='7' class='tdnone tdAlto'></td></tr>" +
						"<tr><td colspan='7' class='tdnone tdAlto'></td></tr>" +

						/* Firma */
						"<tr>" +
							"<td align='left' colspan='2' class='tdonlyBottom'></td>" +
							"<td colspan='3' class='tdnone'></td>" +
							"<td align='left' colspan='2' class='tdonlyBottom'></td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' align='center' class='tdnone'>RESPONSABLE</td>" +
							"<td colspan='3' class='tdnone'></td>" +
							"<td colspan='2' align='center' class='tdnone'>MOTORISTA</td>" +
						"</tr>" +
						"<tr>" +
							"<td colspan='2' align='center' class='tdnone'>" + head.responsable + "</td>" +
							"<td colspan='3' class='tdnone'></td>" +
							"<td colspan='2' align='center' class='tdnone'>" + head.motorista + "</td>" +
						"</tr>" +
					"</table>" +
				"</div>"
			/*, att.ContentId*/);


			try
			{

				//SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
				SmtpClient SmtpServer = new SmtpClient("smtp.mydomain.com", 587);
				SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
				MailMessage email = new MailMessage();
				// START
				email.From = new MailAddress(SendMailFrom);

			   string[] correos = SendMailTo.Split(';');
               for(int i = 0, len = correos.Length; i < len; i++)
				{
					if (correos[i].Trim() != "")
					{
                        email.To.Add(correos[i].Trim());
                    }
                }              

                //email.To.Add(SendMailTo);


				//email.CC.Add(SendMailFrom);
				email.Subject = SendMailSubject;
				email.Body = SendMailBody;
				email.IsBodyHtml = true;

				//email.Attachments.Add(att);
				//email.Attachments.Add(att2);
				//END
				//SmtpServer.Timeout = 5000;

				SmtpServer.EnableSsl = true;
				SmtpServer.UseDefaultCredentials = false;
				//SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "fgntfmkqaraadasa");
				SmtpServer.Credentials = new NetworkCredential(SendMailFrom, SendMailPassword);
				SmtpServer.Send(email);

				return true;
			}
			catch (Exception ex)
			{
				return false;
				/*
				Console.WriteLine(ex.ToString());
				Console.ReadKey();
                */
			}
		}

		//Eventos 
        [HttpGet]
        public async Task<JsonResult> ddl_eventos()
        {
            var data = await IadSolicitarViaje.ddl_eventos();
            return await Task.Run(() =>
            {
                return Json(data);
            });
        }


        //Get Estado de bitacora
        [HttpGet]
        public async Task<JsonResult> getEstadoId(int bitacoraid)
		{
			var data = await IadSolicitarViaje.getEstadoBitacora(bitacoraid);
            return await Task.Run(() =>
            {
                return Json(data.Select(x => new { x.estadoid }).FirstOrDefault());
            });
        }

		//Set Cancelar viaje bitacora
		[HttpPost]
		public async Task<JsonResult> SetCancelar(int bitacoraid, string cancelarcomentario)
		{
			var data = await IadSolicitarViaje.setCancelar(bitacoraid, cancelarcomentario);
            return await Task.Run(() =>
            {
                return Json(data.FirstOrDefault());
            });
        }

		//
        [HttpPost]
        public async Task<JsonResult> setAsignarReasignar(int bitacoraid, int mobileid, int motoristaid, int op_reasignar, int eventoid, string observacion)
		{ 
            var usuario = (_Session.Get<IEnumerable<LoginModel>>(HttpContext.Session, "usuario")).FirstOrDefault();
            var data = await IadSolicitarViaje.setAsignarReasignar(bitacoraid, mobileid, motoristaid, usuario.userid, op_reasignar, eventoid, observacion);
            return await Task.Run(() =>
            {
                return Json(data.FirstOrDefault());
            });
        }



    }
}
