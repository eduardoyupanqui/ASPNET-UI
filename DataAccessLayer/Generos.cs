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
        private TrailersDeVideoJuegosEntities _dbcontext;

        public Generos(TrailersDeVideoJuegosEntities dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AgregarGeneros(GenerosDTO GenerosDTO)
        {
            var nuevaGenero = new Genero();
            nuevaGenero.Nombre = GenerosDTO.Nombre;
            _dbcontext.Genero.Add(nuevaGenero);
            _dbcontext.SaveChanges();
        }

        public GenerosDTO ObtenerGenero(int GeneroId)
        {
            var Genero = _dbcontext.Genero.FirstOrDefault(c => c.Id == GeneroId);

            return Genero != null ? new GenerosDTO() { Id = Genero.Id, Nombre = Genero.Nombre } : new GenerosDTO();
        }

        public List<GenerosDTO> ObtenerGeneros()
        {
            List<GenerosDTO> lista = new List<GenerosDTO>();
            List<GenerosDTO> lista2 = new List<GenerosDTO>();
            _dbcontext.Genero.ToList().ForEach(c => lista.Add(new GenerosDTO() { Id = c.Id, Nombre = c.Nombre }));
            lista2 = _dbcontext.Genero.ToList().Select(c => new GenerosDTO() { Id = c.Id, Nombre = c.Nombre }).ToList();

            return lista;
        }

        public void ActualizarGeneros(GenerosDTO GenerosDTO)
        {
            var Genero = _dbcontext.Genero.FirstOrDefault(c => c.Id == GenerosDTO.Id);
            if (Genero != null)
            {
                Genero.Nombre = GenerosDTO.Nombre;
                _dbcontext.SaveChanges();
            }
        }

        public void EliminarGenero(int GeneroId)
        {
            var Genero = _dbcontext.Genero.FirstOrDefault(c => c.Id == GeneroId);
            if (Genero != null)
            {
                _dbcontext.Genero.Remove(Genero);
                _dbcontext.SaveChanges();
            }
        }

        public void Dispose()
        {
            this._dbcontext.Dispose();
        }
    }
}
