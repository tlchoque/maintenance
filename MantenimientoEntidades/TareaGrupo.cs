using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class TareaGrupo
    {
        //TARE_Interno,GRUP_Interno
        private int _TARE_Interno;
        public int TARE_Interno
        {
            get { return _TARE_Interno; }
            set { _TARE_Interno = value; }
        }

        private int? _GRUP_Interno;
        public int? GRUP_Interno
        {
            get { return _GRUP_Interno; }
            set { _GRUP_Interno = value; }
        }
        public TareaGrupo()
        {
        }
    }
}
