using Alura.ListaLeitura.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Alura.WebAPI.Api.Modelos
{
    public static class LivroPaginadoExtensions
    {
        public static LivroPaginado ToLivroPaginado(this IQueryable<LivroApi> query, LivroPagina pagina)
        {
            int totalItens = query.Count();
            int totalPaginas = (int)Math.Ceiling(totalItens / (double)pagina.Tamanho);
            return new LivroPaginado
            {
                Total= totalItens,
                TotalPaginas = totalPaginas,
                NumeroPagina = pagina.Pagina,
                TamanhoPagina = pagina.Tamanho,
                Resultado = query
                    .Skip(pagina.Tamanho * (pagina.Pagina-1))
                    .Take(pagina.Tamanho).ToList(),
                Anterior = (pagina.Pagina > 1) ? 
                    $"livros?tamanho={pagina.Tamanho}" +
                    $"&pagina={pagina.Pagina - 1}" : "",
                Proximo = (pagina.Pagina < totalPaginas) ?
                    $"livros?tamanho={pagina.Tamanho }" +
                    $"&pagina={pagina.Pagina + 1}" : ""
            };
        }
    }

    public  class LivroPaginado
    {
        public int Total { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IList<LivroApi> Resultado { get; set; }
        public string Anterior { get; set; }
        public string Proximo { get; set; }

    }

    public class LivroPagina
    {
        public int Pagina { get; set; } = 1;
        public int Tamanho { get; set; } = 25;
    }
}
