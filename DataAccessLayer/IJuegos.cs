using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace DataAccessLayer
{
    public interface IJuegos
    {
        void AgregarJuegos(JuegosDTO JuegoDTO);
        JuegosDTO ObtenerJuego(int juegoId, bool incluirPortada = true); //Read
        JuegosDTO ObtenerJuego(string nombreConsola, string nombreJuego, bool incluirPortada = true);
        List<JuegosDTO> ObtenerJuegos();
        List<JuegosDTO> ObtenerJuegos(string criteriodeBusqueda, string campoOrdenamiento);
        List<JuegosDTO> ObtenerJuegos(string nombreConsola, int generoId, string criterioBusqueda);
        void ActualizarJuegos(JuegosDTO JuegoDTO);
        void EliminarJuego(int JuegoId);



        
    }
}
