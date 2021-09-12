using System;
using System.IO;
using System.Threading;

namespace ExercicioLeitorDeArquivosCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            var aberto = true;
            while (aberto)
            {
                Console.WriteLine("Bem vindo ao leitor de arquivos csv.");
                Console.WriteLine("Por favor, informe o caminho onde o arquivo a ser lido encontra-se:");
                var caminho = Console.ReadLine();

                try
                {
                    if (!AcessoDiretorio.VerificaSeDiretorioExiste(caminho))
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("O arquivo não foi encontrado :(");
                        Console.WriteLine("Por favor, verifique o caminho e tente novamente.");

                        Console.WriteLine("");
                        ContadorDeReinicio();
                        continue;
                    }
                    Console.WriteLine("");

                    var conteudoArquivo = LerArquivo(caminho);
                    CriarSummary(conteudoArquivo, caminho);
                    Console.WriteLine("Seu arquivo foi criado com sucesso!");
                    ContadorDeReinicio();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocorreu um erro no processo de leitura e escrita :(\nPor favor, tente novamente.");
                    Console.WriteLine($"Erro: {e.Message}");
                    Console.WriteLine($"StackTrace: {e.StackTrace}");
                    ContadorDeReinicio();
                    continue;
                }                
            }

        }

        private static void CriarSummary(string[] conteudoArquivo, string caminho)
        {
            string caminhoNovoArquivo = CriarDiretorio(caminho,"out");
            using (var novoArquivo = new FileInfo(caminhoNovoArquivo + @"\summary.csv").CreateText())
            {
                foreach (var linha in conteudoArquivo)
                {
                    var conteudoLinha = linha.Split(',');
                    var item = conteudoLinha[0];
                    var valor = int.TryParse(conteudoLinha[1], out int valorConvertido) ? valorConvertido : 0;
                    var quantidade = int.TryParse(conteudoLinha[2], out int quantidadeConvertida) ? quantidadeConvertida : 0;
                    var linhaTemp = item + ',' + (valor * quantidade);
                    novoArquivo.WriteLine(linhaTemp);
                }
            }
        }

        private static string CriarDiretorio(string caminho, string nomeNovoDiretorio)
        {
            var caminhoNovoArquivo = Path.GetDirectoryName(caminho) + @"\" + nomeNovoDiretorio;            
            Directory.CreateDirectory(caminhoNovoArquivo);
            return caminhoNovoArquivo;
        }

        private static string[] LerArquivo(string caminho)
        {
            return File.ReadAllLines(caminho);
        }

        private static void ContadorDeReinicio()
        {
            Thread.Sleep(3000);
            for (var i = 5; i >= 0; i--)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine($"Reiniciando em...  {i}");
            }
            Console.Clear();
        }
    }
}
