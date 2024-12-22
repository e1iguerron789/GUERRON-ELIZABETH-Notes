using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.ViewModels
{
    internal class EGNoteViewModel
    {
        // Propiedad para identificar la nota
        public string Identifier { get; private set; }

        // Constructor que recibe una instancia de EGNote
        public EGNoteViewModel(EGNote note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            // Inicializa la propiedad Identifier u otras propiedades necesarias
            Identifier = note.Id; // Ajusta según la clase `EGNote`
        }

        // Método para recargar los datos de la nota
        public void Reload()
        {
            // Aquí puedes agregar la lógica para recargar la nota si es necesario
            // Por ejemplo, volver a cargar los datos desde el modelo EGNote
        }
    }
}
