using Microsoft.EntityFrameworkCore;
using Albenny_P2_AP2.DAL;
using Albenny_P2_AP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Albenny_P2_AP2.BLL
{
    public class CobrosBLL
    {
        public static bool Guardar(Cobros cobro)
        {
            return Insertar(cobro);
        }


        private static bool Insertar(Cobros cobro)
        {
            bool Insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                foreach (var item in cobro.Detalle)
                {
                    item.Venta = contexto.Ventas.Find(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    contexto.Entry(item.Venta).State = EntityState.Modified;
                }
                contexto.Cobros.Add(cobro);
                Insertado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Insertado;
        }


        public static bool Eliminar(int id)
        {
            bool Eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var cobro = Buscar(id);

                contexto.Entry(cobro).State = EntityState.Deleted;
                Eliminado = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Eliminado;
        }

        public static Cobros Buscar(int id)
        {
            Cobros Cobro = new Cobros();
            Contexto contexto = new Contexto();

            try
            {
                Cobro = contexto.Cobros
                    .Where(e => e.CobroId == id)
                    .Include(e => e.Detalle)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Cobro;
        }

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> cobro)
        {
            List<Cobros> Lista = new List<Cobros>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Cobros.Where(cobro).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
