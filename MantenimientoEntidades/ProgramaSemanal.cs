using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class ProgramaSemanal
    {
        private int? _PERI_Interno;
        public int? PERI_Interno
        {
            get { return _PERI_Interno; }
            set { _PERI_Interno = value; }
        }

        private int _PERI_NumSemana;
        public int PERI_NumSemana
        {
            get { return _PERI_NumSemana; }
            set { _PERI_NumSemana = value; }
        }

        private int _PERI_Anio;
        public int PERI_Anio
        {
            get { return _PERI_Anio; }
            set { _PERI_Anio = value; }
        }

        public ProgramaSemanal()
        {
        }
    }
}
