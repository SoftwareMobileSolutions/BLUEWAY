namespace BLUEWAY.Models
{
    public class LoginModel
    {
        public int userid { get; set; }
        public int rolid { get; set; }
        public int companyid { get; set; }
        public int perfilid { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tel { get; set; }
        public string correo { get; set; }
        public string username { get; set; }
        public int timezone { get; set; }
	    public string notificacion_correo { get; set; }
		public int tipousuarioid { get; set; }
        public string tipoplan { get; set; }
		public string notificacion_telefono { get; set; }
	}
}
