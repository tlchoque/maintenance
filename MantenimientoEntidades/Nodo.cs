using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mantenimiento.Entidades
{
    public class Nodo
    {
        public AtributoNodo attr;
        public List<Nodo> children;
        public string data
        {
            get;
            set;
        }
        public int IdServerUse
        {
            get;
            set;
        }
        public string state
        {
            get;
            set;
        }
    }

    public class AtributoNodo
    {
        public string id;
        public bool selected;
    }
}
