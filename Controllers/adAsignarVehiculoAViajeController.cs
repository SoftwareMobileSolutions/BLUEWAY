using BLUEWAY.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace BLUEWAY.Controllers
{
    public class adAsignarVehiculoAViajeController : Controller
    {

        private readonly IadSolicitarViaje IadSolicitarViaje;
        public adAsignarVehiculoAViajeController(IadSolicitarViaje _IadSolicitarViaje)
        {
            IadSolicitarViaje = _IadSolicitarViaje;
        }

        public IActionResult adAsignarVehiculoAViaje()
        {
            return PartialView();
        }


        //Árbol
        //public async Task<JsonResult> ObtenerSubflotaVehiculos(int userid = 4450)
        //{
        //    var data = await IadSolicitarViaje.gettreeMobiles(userid);

        //    return await Task.Run(() =>
        //    {
        //        if (data.Count() > 0)
        //        {
        //            /*if (data.Select(x => x.nivel).Max() > 2)
        //            {*/
        //            var company = data.Select(t => new
        //            {
        //                //text = (t.plate != "" ? "<label>" + t.name + "</label><label class='EAddltreePlaca'>(" + t.plate + ") </label>" : t.name),
        //                text = t.name,
        //                Name = t.name,
        //                codigo = t.cod,
        //                parent = t.parent,
        //                granparent = t.grandparent,
        //                Nivel = t.nivel,
        //                //Icon = t.urlimagen,
        //                placa = t.plate,
        //                ImageUrl = t.urlimagen.Replace("../imagenes", "img"),
        //                //mobileid_origen = t.cod + t.origen
        //            }).Where(t => t.Nivel == 0);

        //            var ingenios = data.Select(t => new
        //            {
        //                //text = (t.plate != "" ? "<label>" + t.name + "</label><label class='EAddltreePlaca'>(" + t.plate + ") </label>" : t.name),
        //                text = t.name,
        //                Name = t.name,
        //                codigo = t.cod,
        //                parent = t.parent,
        //                granparent = t.grandparent,
        //                Nivel = t.nivel,
        //                //Icon = t.urlimagen,
        //                placa = t.plate,
        //                ImageUrl = t.urlimagen.Replace("../imagenes", "img"),
        //                // mobileid_origen = t.cod + t.origen
        //            }).Where(t => t.Nivel == 1);

        //            var grupos = data.Select(t => new
        //            {
        //                //origen = t.origen,
        //                //text = (t.plate != "" ? "<label>" + t.name + "</label><label class='EAddltreePlaca'>(" + t.plate + ") </label>" : t.name),
        //                text = t.name,
        //                Name = t.name,
        //                codigo = t.cod,
        //                parent = t.parent,
        //                granparent = t.grandparent,
        //                Nivel = t.nivel,
        //                //Icon = t.urlimagen,
        //                placa = t.plate,
        //                ImageUrl = t.urlimagen.Replace("../imagenes", "img"),
        //                // mobileid_origen = t.cod + t.origen
        //            }).Where(t => t.Nivel == 2);

        //            var flotas = data.Select(t => new
        //            {
        //                //origen = t.origen,
        //                //text = (t.plate != "" ? "<label>" + t.name + "</label><label class='EAddltreePlaca'>(" + t.plate + ") </label>" : t.name),
        //                text = t.name,
        //                Name = t.name,
        //                codigo = t.cod,
        //                parent = t.parent,
        //                granparent = t.grandparent,
        //                Nivel = t.nivel,
        //                //Icon = t.urlimagen,
        //                placa = t.plate,
        //                ImageUrl = t.urlimagen.Replace("../imagenes", "img"),
        //                // mobileid_origen = t.cod + t.origen
        //            }).Where(t => t.Nivel == 3);

        //            var vehiculos = data.Select(t => new
        //            {
        //                //origen = t.origen,
        //                //text = (t.plate != "" ? "<label>" + t.name + "</label><label class='EAddltreePlaca'>(" + t.plate + ") </label>" : t.name),
        //                text = t.name,
        //                Name = t.name,
        //                codigo = t.cod,
        //                parent = t.parent,
        //                granparent = t.grandparent,
        //                Nivel = t.nivel,
        //                //Icon = t.urlimagen,
        //                ImageUrl = t.urlimagen.Replace("../imagenes", "img"),
        //                placa = t.plate,
        //                // mobileid_origen = t.cod + t.origen
        //            }).Where(t => t.Nivel == 4);


        //            var arbolSubflota = company.Select(p => new
        //            {
        //                //origen = p.origen,
        //                expanded = true,
        //                text = p.text,

        //                codigo = p.codigo,
        //                parent = p.parent,
        //                granparent = p.granparent,
        //                Nivel = p.Nivel,
        //                placa = p.placa,
        //                imageUrl = p.ImageUrl,
        //                //mobileid_origen = p.mobileid_origen,
        //                HasChildren = ingenios.Any(),
        //                encoded = false,

        //                items = ingenios.Select(c => new
        //                {
        //                    //origen = c.origen,
        //                    expanded = true,
        //                    text = c.text,

        //                    codigo = c.codigo,
        //                    parent = c.parent,
        //                    granparent = c.granparent,
        //                    Nivel = c.Nivel,
        //                    placa = c.placa,
        //                    imageUrl = c.ImageUrl,
        //                    // mobileid_origen = c.mobileid_origen,
        //                    HasChildren = grupos.Any(),
        //                    encoded = false,
        //                    items = grupos.Select(d => new
        //                    {
        //                        //origen = d.origen,
        //                        expanded = true,
        //                        text = d.text,

        //                        codigo = d.codigo,
        //                        parent = d.parent,
        //                        granparent = d.granparent,
        //                        Nivel = d.Nivel,
        //                        placa = d.placa,
        //                        imageUrl = d.ImageUrl,
        //                        //mobileid_origen = d.mobileid_origen,
        //                        HasChildren = flotas.Any(),
        //                        encoded = false,
        //                        items = flotas.Select(e => new
        //                        {
        //                            //origen = e.origen,
        //                            expanded = true,
        //                            text = e.text,

        //                            codigo = e.codigo,
        //                            parent = e.parent,
        //                            granparent = e.granparent,
        //                            Nivel = e.Nivel,
        //                            placa = e.placa,
        //                            imageUrl = e.ImageUrl,
        //                            //mobileid_origen = e.mobileid_origen,
        //                            HasChildren = vehiculos.Any(),
        //                            encoded = false,
        //                            items = vehiculos.Select(t => new
        //                            {
        //                                //origen = t.origen,
        //                                expanded = true,
        //                                text = t.text,
        //                                codigo = t.codigo,
        //                                parent = t.parent,
        //                                granparent = t.granparent,
        //                                Nivel = t.Nivel,
        //                                placa = t.placa,
        //                                imageUrl = t.ImageUrl,
        //                                //mobileid_origen = t.mobileid_origen,
        //                                HasChildren = false,
        //                                encoded = false
        //                            }).Where(t => t.parent == e.codigo).OrderBy(t => t.text).ToList()
        //                        }).Where(e => e.parent == d.codigo).OrderBy(e => e.text)
        //                    }).Where(d => d.parent == c.codigo).OrderBy(d => d.text)
        //                }).Where(c => c.parent == p.codigo).OrderBy(c => c.text)
        //            }).OrderBy(p => p.text);
        //            return Json(arbolSubflota.ToList());
        //            /* }
        //             else
        //             {
        //                 var subflota = data.Select(c => new
        //                 {
        //                     text = c.name,
        //                     codigo = c.cod,
        //                     parent = c.parent,
        //                     granparent = c.grandparent,
        //                     imageUrl = c.urlimagen.Replace("../imagenes", "img"),
        //                     nivel = c.nivel,
        //                     HasChildren = false
        //                 }).Where(c => c.nivel == 1).OrderBy(c => c.text);

        //                 var Company = data.Select(p => new
        //                 {
        //                     text = p.name,
        //                     codigo = p.cod,
        //                     parent = p.parent,
        //                     granparent = p.grandparent,
        //                     PageUrl = p.grandparent,
        //                     imageUrl = p.urlimagen.Replace("../imagenes", "img"),
        //                     nivel = p.nivel,
        //                     HasChildren = subflota.Any(),
        //                     items = subflota
        //                 }).Where(p => p.nivel == 0).OrderBy(p => p.text);
        //                 return Json(Company.ToList());
        //             }*/

        //        }
        //        else
        //        {
        //            return Json(new { });
        //        }
        //    });
        //    /*
        //    var data = await IrpEventosDelictivos.Get_GridrpDelictivos(BD, companyid, fechai, fechafin, geozonasid, mobilesid, tipoincidentesid, motoristasid, userid);
        //    return await Task.Run(() =>
        //    {
        //        return Json(data);
        //    });*/
        //}

    }
}
