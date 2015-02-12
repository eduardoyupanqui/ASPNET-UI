using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace DataAccessLayer
{
    public interface IConsolas
    {
        void AgregarConsolas(ConsolasDTO consolasDTO);
        ConsolasDTO ObtenerConsola(int consolaId);
        List<ConsolasDTO> ObtenerConsolas();
        void ActualizarConsolas(ConsolasDTO consolasDTO);
        void EliminarConsola(int consolaId);
    }
}
