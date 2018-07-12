using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mantenimiento.DAO;
using Mantenimiento.Entidades;
using System.Data;//para el DataTable
namespace Mantenimiento.ReglasNegocio
{
    public class ControlGrupo
    {
        public ControlGrupo()
        {
        }
        public int ObtenerTotalGrupos()
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.ObtenerTotalGrupos();               
        }
        public List<Grupo> ObtenerGrupos(int TamanioPagina,int NumeroPagina){
            GrupoDAO DGrupo = new GrupoDAO();
            List<Grupo> listaGrupos = DGrupo.ObtenerGrupos(TamanioPagina,NumeroPagina);
            //obtenemos el usuario
            List<Grupo> grupos = new List<Grupo>();
            foreach (Grupo grupo in listaGrupos)
            {
                UsuarioDAO DU = new UsuarioDAO();
                Usuario usuario = new Usuario();
                if(grupo.AUDI_UsuarioCrea != null){
                usuario.USUA_Interno = grupo.AUDI_UsuarioCrea;
                usuario = DU.ObtenerUsuarioPorID(usuario);
                
                grupo.UsuarioCreador = usuario.USUA_Usuario;
                }
                grupos.Add(grupo);
            }
            return grupos;
        }
        public List<Grupo> ObtenerGrupos()
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.ObtenerTodosGrupos();
            
        }
        public List<Grupo> ObtenerGruposFiltradoPorNombre(int TamanioPagina,int NumeroPagina,string str)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            List<Grupo> listaGrupos = DGrupo.ObtenerGruposFiltradoPorNombre(TamanioPagina, NumeroPagina,str);
            //obtenemos el usuario
            List<Grupo> grupos = new List<Grupo>();
            foreach (Grupo grupo in listaGrupos)
            {
                UsuarioDAO DU = new UsuarioDAO();
                Usuario usuario = new Usuario();
                usuario.USUA_Interno = grupo.AUDI_UsuarioCrea;
                usuario = DU.ObtenerUsuarioPorID(usuario);
                grupo.UsuarioCreador = usuario.USUA_Usuario;

                grupos.Add(grupo);
            }
            return grupos;
        }
        public Grupo ObtenerGrupo(Grupo _grupo)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            Grupo grupo = DGrupo.ObtenerGrupoPorID(_grupo);
            //obtenemos el usuario
            UsuarioDAO DU = new UsuarioDAO();
            Usuario usuario = new Usuario();
            usuario.USUA_Interno = grupo.AUDI_UsuarioCrea;
            usuario = DU.ObtenerUsuarioPorID(usuario);

            grupo.UsuarioCreador = usuario.USUA_Usuario;
            return grupo;
        }
        public int InsertarGrupo(Grupo grupo, int AUDI_Usuario){
            GrupoDAO DGrupo = new GrupoDAO();
            int IDGrupo = DGrupo.InsertarGrupo(grupo, AUDI_Usuario);
            
            if (IDGrupo > 0)
            {
                if (grupo.GRUP_Tareas != null)
                {
                    //entonces modificamos las tareas del grupo para ello hCEMOS:
                    //1º eliminamos todas las tareas del Grupo, SI ES QUE HAY
                    if (grupo.GRUP_Interno != null)
                    {
                        DGrupo.EliminarTareasDeGrupo(grupo);
                    }
                    string[] IDTareas = grupo.GRUP_Tareas.Split('|');
                    foreach (string ID in IDTareas)
                    {
                        TareaGrupo tareaGrupo = new TareaGrupo();
                        tareaGrupo.TARE_Interno = int.Parse(ID);
                        if (grupo.GRUP_Interno != null)
                        {
                            
                            //insertamos las nuevas tareas del grupo
                            tareaGrupo.GRUP_Interno = grupo.GRUP_Interno;
                            DGrupo.InsertarTareasDelGrupo(tareaGrupo);
                        }
                        else
                        {   //solo insertamos las nuevas tareas del grupo
                            tareaGrupo.GRUP_Interno = IDGrupo;
                            DGrupo.InsertarTareasDelGrupo(tareaGrupo);
                        }
                    }
                }
                
            }
            return IDGrupo;
        }
        public int EliminarGrupo(Grupo grupo,int AUDI_Usuario)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.EliminarGrupo(grupo,AUDI_Usuario);
        }
        public int ActivarGrupo(Grupo grupo, int AUDI_Usuario)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.ActivarGrupo(grupo, AUDI_Usuario);
        }
        public int DesactivarGrupo(Grupo grupo, int AUDI_Usuario)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.DesactivarGrupo(grupo, AUDI_Usuario);
        }

        public List<Tarea> ObtenerTareasPorModulo()
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.ObtenerTareasPorModulo();
        }
        public List<TareaGrupo> ObtenerTareasDeGrupo(Grupo grupo)
        {
            GrupoDAO DGrupo = new GrupoDAO();
            return DGrupo.ObtenerTareasDeGrupo(grupo);
        }
    }
}
