using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace DataAccessLayer
{
    public class Consolas : IConsolas
    {
        public void AgregarConsolas(ConsolasDTO consolasDTO)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var nuevaConsola = new Consola();
                nuevaConsola.Nombre = consolasDTO.Nombre;
                dbcontext.Consola.Add(nuevaConsola);
                dbcontext.SaveChanges();
            }
        }

        public ConsolasDTO ObtenerConsola(int consolaId)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var consola = dbcontext.Consola.FirstOrDefault(c => c.Id == consolaId);

                return consola != null ? new ConsolasDTO() { Id = consola.Id, Nombre = consola.Nombre } : new ConsolasDTO();
                
            }
        }

        public List<ConsolasDTO> ObtenerConsolas()
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                List<ConsolasDTO> lista = new List<ConsolasDTO>();
                List<ConsolasDTO> lista2 = new List<ConsolasDTO>();
                dbcontext.Consola.ToList().ForEach(c => lista.Add( new ConsolasDTO(){Id = c.Id,Nombre=c.Nombre}));
                lista2 = dbcontext.Consola.ToList().Select(c => new ConsolasDTO() { Id = c.Id, Nombre = c.Nombre }).ToList();

                return lista; 
            }
        }

        public void ActualizarConsolas(ConsolasDTO consolasDTO)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var consola = dbcontext.Consola.FirstOrDefault(c => c.Id == consolasDTO.Id);
                if (consola != null)
	            {
                    consola.Nombre = consolasDTO.Nombre;
                    dbcontext.SaveChanges();
	            }
            }
        }

        public void EliminarConsola(int consolaId)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var consola = dbcontext.Consola.FirstOrDefault(c => c.Id == consolaId);
                if (consola != null)
                {
                    dbcontext.Consola.Remove(consola);
                    dbcontext.SaveChanges();
                }
            }
        }
    }
}
