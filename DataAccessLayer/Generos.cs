using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace DataAccessLayer
{
    public class Generos : IGeneros
    {
        public void AgregarGeneros(GenerosDTO GenerosDTO)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var nuevaGenero = new Genero();
                nuevaGenero.Nombre = GenerosDTO.Nombre;
                dbcontext.Genero.Add(nuevaGenero);
                dbcontext.SaveChanges();
            }
        }

        public GenerosDTO ObtenerGenero(int GeneroId)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var Genero = dbcontext.Genero.FirstOrDefault(c => c.Id == GeneroId);

                return Genero != null ? new GenerosDTO() { Id = Genero.Id, Nombre = Genero.Nombre } : new GenerosDTO();
                
            }
        }

        public List<GenerosDTO> ObtenerGeneros()
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                List<GenerosDTO> lista = new List<GenerosDTO>();
                List<GenerosDTO> lista2 = new List<GenerosDTO>();
                dbcontext.Genero.ToList().ForEach(c => lista.Add( new GenerosDTO(){Id = c.Id,Nombre=c.Nombre}));
                lista2 = dbcontext.Genero.ToList().Select(c => new GenerosDTO() { Id = c.Id, Nombre = c.Nombre }).ToList();

                return lista; 
            }
        }

        public void ActualizarGeneros(GenerosDTO GenerosDTO)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var Genero = dbcontext.Genero.FirstOrDefault(c => c.Id == GenerosDTO.Id);
                if (Genero != null)
	            {
                    Genero.Nombre = GenerosDTO.Nombre;
                    dbcontext.SaveChanges();
	            }
            }
        }

        public void EliminarGenero(int GeneroId)
        {
            using (var dbcontext = new TrailersDeVideoJuegosEntities())
            {
                var Genero = dbcontext.Genero.FirstOrDefault(c => c.Id == GeneroId);
                if (Genero != null)
                {
                    dbcontext.Genero.Remove(Genero);
                    dbcontext.SaveChanges();
                }
            }
        }
    }
}
