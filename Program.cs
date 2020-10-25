using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaApisul
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string contactsJson = "";

            try
            {
                var rc = new System.Net.Http.HttpClient();

                var json = await rc.GetAsync("https://5f947a2d9ecf720016bfc732.mockapi.io/elevadores");
                contactsJson = await json.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Houve algum problema ao acessar/converter API: " + ex.Message);
            }

            try { 

                if (contactsJson == "")
                {
                    Console.WriteLine("varificando json1.json localmente . . ." );

                    var linha = "";
                    var enderecoArquivo = "json1.json";
                    using (var fluxoDoArquivo = new FileStream(enderecoArquivo, FileMode.Open))
                    using (var leitor = new StreamReader(fluxoDoArquivo))
                    {
                        while (!leitor.EndOfStream)
                        {
                            linha = leitor.ReadToEnd();
                        }
                    }
                    contactsJson = linha;
                }

                if (contactsJson != "")
                {
                    var listaConv = JsonConvert.DeserializeObject<ListaAcessoElevador>(contactsJson);

                    Console.WriteLine("===== Lista de andares menos utilizados =====");
                    
                    var lista = listaConv.andarMenosUtilizado();

                    foreach (var item in lista)
                    {
                        Console.WriteLine($"Andar:{item}");
                    }

                    Console.WriteLine($"Total registros:{lista.Count}\n");

                    Console.WriteLine("===== Lista de elevadores mais frequentados =====");

                    var lista1 = listaConv.elevadorMaisFrequentado(2);

                    foreach (var item in lista1)
                    {
                        Console.WriteLine($"Elevador:{item}");
                    }

                    Console.WriteLine($"Total registros:{lista1.Count}\n");

                    Console.WriteLine("===== Lista de elevadores menos frequentados =====");

                    var lista2 = listaConv.elevadorMenosFrequentado(2);

                    foreach (var item in lista2)
                    {
                        Console.WriteLine($"Elevador:{item}");
                    }

                    Console.WriteLine($"Total registros:{lista2.Count}\n");

                    var percUso = listaConv.percentualDeUsoElevadorA();
                    Console.WriteLine($"Percentual de uso do elevador A: {percUso.ToString("0.00")}\n");
                    percUso = listaConv.percentualDeUsoElevadorB();
                    Console.WriteLine($"Percentual de uso do elevador B: {percUso.ToString("0.00")}\n");
                    percUso = listaConv.percentualDeUsoElevadorC();
                    Console.WriteLine($"Percentual de uso do elevador C: {percUso.ToString("0.00")}\n");
                    percUso = listaConv.percentualDeUsoElevadorD();
                    Console.WriteLine($"Percentual de uso do elevador D: {percUso.ToString("0.00")}\n");
                    percUso = listaConv.percentualDeUsoElevadorE();
                    Console.WriteLine($"Percentual de uso do elevador E: {percUso.ToString("0.00")}\n");

                    Console.WriteLine("===== Lista de turnos com maior fluxo =====");

                    var lista3 = listaConv.periodoMaiorUtilizacaoConjuntoElevadores(1);

                    foreach (var item in lista3)
                    {
                        Console.WriteLine($"Turno:{item}");
                    }

                    Console.WriteLine("===== Turno com maior fluxo pelo Elevador mais Frequentado =====");

                    var lista4 = listaConv.periodoMaiorFluxoElevadorMaisFrequentado();

                    foreach (var item in lista4)
                    {
                        Console.WriteLine($"Turno:{item}");
                    }

                    Console.WriteLine("===== Turno com menor fluxo pelo Elevador menos Frequentado =====");

                    var lista5 = listaConv.periodoMenorFluxoElevadorMenosFrequentado();

                    foreach (var item in lista5)
                    {
                        Console.WriteLine($"Turno:{item}");
                    }

                    Console.WriteLine("===== -------------------------------------------------------------- =====");
                    Console.WriteLine("===== Lista de Elevadores mais utilizados e os Turnos com maior Fluxo =====");

                    foreach (var item in lista1)
                    {
                        Console.WriteLine($"Elevador:{item}");

                        var lista6 = listaConv.periodoMaiorFluxoElevador(item);
                        foreach (var item1 in lista6)
                        {
                            Console.WriteLine($"           Turno:{item1}");
                                                        
                        }
                    }

                    Console.WriteLine("===== Lista de Elevadores menos utilizados e os Turnos com menor Fluxo =====");

                    foreach (var item in lista2)
                    {
                        Console.WriteLine($"Elevador:{item}");

                        var lista7 = listaConv.periodoMenorFluxoElevador(item);
                        foreach (var item1 in lista7)
                        {
                            Console.WriteLine($"           Turno:{item1}");

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Não foi possivel encontrar dados do arquivo.\nVerificar conexão internet ou colocar json1.json no mesmo caminho da aplicação.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nAplicação finalizada. Tecle <enter> para sair.");

            Console.ReadKey();
        }
    }
}
