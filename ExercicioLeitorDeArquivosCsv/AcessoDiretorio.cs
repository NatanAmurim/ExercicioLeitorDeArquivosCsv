using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExercicioLeitorDeArquivosCsv
{
    public static class AcessoDiretorio
    {
        public static bool VerificaSeDiretorioExiste(string caminho) 
        {
            var caminhoFiltrado = Path.GetDirectoryName(caminho);
            
            if (!string.IsNullOrEmpty(caminhoFiltrado))
                return Directory.Exists(caminhoFiltrado)? true : false;
            
            return false;                
        }

    }
}
