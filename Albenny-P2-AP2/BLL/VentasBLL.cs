using Albenny_P2_AP2.DAL;
using Albenny_P2_AP2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Albenny_P2_AP2.BLL
{
    public class VentasBLL
    {

        public static Ventas Buscar(int id)
        {
            Ventas venta = new Ventas();
            Contexto contexto = new Contexto();

            try
            {
                venta = contexto.Ventas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return venta;
        }

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> venta)
        {
            List<Ventas> Lista = new List<Ventas>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Ventas.Where(venta).ToList();
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

        public static async Task<List<CobrosDetalle>> GetVentasPendiente(int clienteId)
        {
            var pendientes = new List<CobrosDetalle>();
            Contexto contexto = new Contexto();

            var ventas = await contexto.Ventas
                .Where(v => v.ClienteId == clienteId && v.Balance > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                pendientes.Add(new CobrosDetalle
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return pendientes;
        }

        public static async Task<List<CobrosDetalle>> GetVentasCobradas(int clienteId)
        {
            var pendientes = new List<CobrosDetalle>();
            Contexto contexto = new Contexto();

            var ventas = await contexto.Ventas
                .Where(v => v.ClienteId == clienteId && v.Balance == 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in ventas)
            {
                pendientes.Add(new CobrosDetalle
                {
                    VentaId = item.VentaId,
                    Venta = item,
                    Cobrado = 0
                });
            }

            return pendientes;
        }
    }
}
