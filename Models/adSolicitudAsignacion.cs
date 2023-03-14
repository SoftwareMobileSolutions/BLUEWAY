namespace BLUEWAY.Models
{
    public class adSolicitudAsignacion
    {
        //Solicitud
        public class gridSolicitudes
        {
            public int bitacoraId { get; set; }
            public int mobileid { get; set; }
            public string placa { get; set; }
            public string mobileName { get; set; }
            public int motoristaid { get; set; }
            public string motorista { get; set; }
            public string fsalida { get; set; }
            public string fentrada { get; set; }
            //public string salidaHoraEstimada { get; set; }
            public float kmSalida { get; set; }
            public float kmEntrada { get; set; } 
            public int supervisorId { get; set; }
            public string supervisor { get; set;}
            public int reponsableId { get; set; }
            public string responsable { get; set; }
            public bool pernoctara { get; set; }
            public int estadoId { get; set; }
            public string estado { get; set; }
            public int mobileidReasignado { get; set; }
            public string placaR { get; set; }
            public string mobileNameR { get; set; }
            public int motoristaidReasignado { get; set; }
            public string motoristaNameR { get; set; }
            public string observacion { get; set; }
            public int eventoId { get; set; }
            public string evento { get; set; }
            public string fcreacion { get; set; }
            public string brigada { get; set; }
            public string municipios { get; set; }
        }

        public class ddl_brigada
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string puesto { get; set; }
        }

        public class cantones
        {
            public string canton { get; set; }
            //public string cdm { get; set; }
            public string cdmc { get; set; }
            public string mpio { get; set; }
            public string dpto { get; set; }
            public float lat { get; set; }
            public float lng { get; set; }
        }

		//Asignación borrar clase treeDelictivos
		public class treeDelictivos
        {
            public int nivel { get; set; }
            public string cod { get; set; }
            public string name { get; set; }
            public int valorid { get; set; }
            public string parent { get; set; }
            public string grandparent { get; set; }
            public string urlimagen { get; set; }
            public string plate { get; set; }
            public int motoristaid { get; set; }
            public string nombremostorista { get; set; }
        }

        public class motoristadata
        {
			public int motoristaid { get; set; }
			public string motorista { get; set; }
            public int mobileid { get; set; }
            public string placa { get; set; }
            public string alias { get; set; }
        }

        public class estadoBitacora 
        {
            public int estadoid { get; set; }
        }

        public class eventosBitacora
        {
            public int id { get; set; }
            public string nombre { get; set; }
        }

        public class sendMail
        {
            public class encabezado
			{
				public string salidaProgramada { get; set; }
				public string llegadaProgramada { get; set; }
				public string supervisor { get; set; }
				public string brigada { get; set; }
				public string proyecto { get; set; }
				public string motorista { get; set; }
				public string vehiculo { get; set; }
				public string placa { get; set; }
				public string marca { get; set; }
				public string tipo { get; set; }
				public string responsable { get; set; }
				public string correo { get; set; }
			}

			public class detalle
			{
				public string fecha { get; set; }
				public string destinos { get; set; }
			}
		}
    }
}
