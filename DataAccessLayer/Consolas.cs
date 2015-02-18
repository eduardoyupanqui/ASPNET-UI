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
        private TrailersDeVideoJuegosEntities _dbcontext;
        public Consolas(TrailersDeVideoJuegosEntities dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AgregarConsolas(ConsolasDTO consolasDTO)
        {
            var nuevaConsola = new Consola();
            nuevaConsola.Nombre = consolasDTO.Nombre;
            _dbcontext.Consola.Add(nuevaConsola);
            _dbcontext.SaveChanges();
        }

        public ConsolasDTO ObtenerConsola(int consolaId)
        {
            var consola = _dbcontext.Consola.FirstOrDefault(c => c.Id == consolaId);

            return consola != null ? new ConsolasDTO() { Id = consola.Id, Nombre = consola.Nombre } : new ConsolasDTO();

        }

        public List<ConsolasDTO> ObtenerConsolas()
        {
            List<ConsolasDTO> lista = new List<ConsolasDTO>();
            List<ConsolasDTO> lista2 = new List<ConsolasDTO>();
            _dbcontext.Consola.ToList().ForEach(c => lista.Add(new ConsolasDTO() { Id = c.Id, Nombre = c.Nombre }));
            //lista2 = dbcontext.Consola.ToList().Select(c => new ConsolasDTO() { Id = c.Id, Nombre = c.Nombre }).ToList();

            return lista;
        }

        public void ActualizarConsolas(ConsolasDTO consolasDTO)
        {
            var consola = _dbcontext.Consola.FirstOrDefault(c => c.Id == consolasDTO.Id);
            if (consola != null)
            {
                consola.Nombre = consolasDTO.Nombre;
                _dbcontext.SaveChanges();
            }
        }

        public void EliminarConsola(int consolaId)
        {
            var consola = _dbcontext.Consola.FirstOrDefault(c => c.Id == consolaId);
            if (consola != null)
            {
                _dbcontext.Consola.Remove(consola);
                _dbcontext.SaveChanges();
            }
        }

        public void Dispose()
        {
            this._dbcontext.Dispose();
        }
    }
}
