using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace DataAccessLayer
{
    public interface IGeneros
    {
        void AgregarGeneros(GenerosDTO GenerosDTO);
        GenerosDTO ObtenerGenero(int GeneroId);
        List<GenerosDTO> ObtenerGeneros();
        void ActualizarGeneros(GenerosDTO GenerosDTO);
        void EliminarGenero(int GeneroId);
    }
}
