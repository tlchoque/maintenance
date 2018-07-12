using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class PeriodoProgramacion
    {
        //PPRO_Interno,PPRO_Periodo,PPRO_DiaSemana,PPRO_DiaMes
        int _PPRO_Interno;
        public int PPRO_Interno
        {
            get { return _PPRO_Interno; }
            set { _PPRO_Interno = value; }
        }

        string _PPRO_Periodo;
        public string PPRO_Periodo
        {
            get { return _PPRO_Periodo; }
            set { _PPRO_Periodo = value; }
        }

        int? _PPRO_DiaSemana;
        public int? PPRO_DiaSemana
        {
            get { return _PPRO_DiaSemana; }
            set { _PPRO_DiaSemana = value; }
        }

        int? _PPRO_DiaMes;
        public int? PPRO_DiaMes
        {
            get { return _PPRO_DiaMes; }
            set { _PPRO_DiaMes = value; }
        }

        public PeriodoProgramacion()
        {
        }
    }
}
