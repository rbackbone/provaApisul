using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provaApisul
{
    /// <summary>
    /// Lista AcessoElevador;
    /// </summary>
    public class ListaAcessoElevador : List<AcessoElevador>, IElevadorService
    {
        /// <summary>
        /// Retorna os Andares menos utilizados
        /// </summary>
        /// <returns></returns>
        public List<int> andarMenosUtilizado()
        {
            var lista = new List<int>();

            var result = _andarMenosUtilizado();

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

        /// <summary>
        /// Retorna os Andares menos utilizados conforme quantidade informada
        /// </summary>
        /// <param name="quantidade">Quantidade (> 0)</param>
        /// <returns></returns>
        public List<int> andarMenosUtilizado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }

            var lista = new List<int>();
            
            var result = _andarMenosUtilizado();

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }

            return lista;
        }

        private  IEnumerable<dynamic> _andarMenosUtilizado()
        {

            var result = this.GroupBy(ae => ae.Andar)
                               .OrderBy(ae => ae.Key)
                               .Select(n => 
                               new {
                                    id = n.Key,
                                    qt = n.Count()
                                   });

            var result1 = result.OrderBy(ae => ae.qt);

            return result1;
        }

        /// <summary>
        /// Retorna os Elevadores mais frequentados
        /// </summary>
        /// <returns></returns>
        public List<char> elevadorMaisFrequentado()
        {
            var lista = new List<char>();
            var result = _elevadorFrequencia(true );

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

        /// <summary>
        /// Retorna os Elevadores mais frequentados conforme a quantidade informada
        /// </summary>
        /// <param name="quantidade">Quantidade (> 0)</param>
        /// <returns></returns>
        public List<char> elevadorMaisFrequentado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }

            var lista = new List<char>();
            var result = _elevadorFrequencia(true);

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }

        private IEnumerable<dynamic> _elevadorFrequencia(bool maiorFrequencia)
        {
            var result = this.GroupBy(ae => ae.Elevador)
                              .OrderBy(ae => ae.Key)
                              .Select(n => 
                              new {
                                   id = n.Key,
                                   qt = n.Count()
                                  });
            
            IEnumerable<dynamic> result1;
            if (maiorFrequencia)
            {
                result1 = result.OrderByDescending(ae => ae.qt);
            }
            else
            {
                result1 = result.OrderBy(ae => ae.qt);
            }

            return result1;
        }

        /// <summary>
        /// Retorna os Elevadores menos frequentados
        /// </summary>
        /// <returns></returns>
        public List<char> elevadorMenosFrequentado()
        {
            var lista = new List<char>();
            var result = _elevadorFrequencia(false);

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }

        /// <summary>
        /// Retorna os Elevadores menos frequentados conforme a quantidade informada
        /// </summary>
        /// <param name="quantidade">Quantidade (> 0)</param>
        /// <returns></returns>
        public List<char> elevadorMenosFrequentado(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("Valor inválido; Esperado > 0", nameof(quantidade));
            }
            var lista = new List<char>();
            var result = _elevadorFrequencia(false);

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }

        /// <summary>
        /// Retorna o percentual de uso do elevador A
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorA()
        {
            float percUso = _percentualDeUsoElevador('A');

            return percUso;
        }

        /// <summary>
        /// Retorna o percentual de uso do elevador B
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorB()
        {
            float percUso = _percentualDeUsoElevador('B');

            return percUso;
        }

        /// <summary>
        /// Retorna o percentual de uso do elevador C
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorC()
        {
            float percUso = _percentualDeUsoElevador('C');

            return percUso;
        }

        /// <summary>
        /// Retorna o percentual de uso do elevador D
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorD()
        {
            float percUso = _percentualDeUsoElevador('D');

            return percUso;
        }

        /// <summary>
        /// Retorna o percentual de uso do elevador E
        /// </summary>
        /// <returns></returns>
        public float percentualDeUsoElevadorE()
        {
            float percUso = _percentualDeUsoElevador('E');

            return percUso;
        }

        private float _percentualDeUsoElevador(char elevador)
        {
            float percUso;
            float quantiE = 0;
            var result = this.Where(ae => ae.Elevador == elevador )
                            .GroupBy(ae => ae.Elevador)
                            .OrderBy(ae => ae.Key)
                            .Select(n =>
                            new {
                                id = n.Key,
                                qt = n.Count()
                            });

            foreach (var item in result)
            {
                quantiE += item.qt;
            }

            float quantiT = quantiE;

            var result1 = this.Where(ae => ae.Elevador != elevador)
                        .GroupBy(ae => ae.Elevador)
                        .OrderBy(ae => ae.Key)
                        .Select(n =>
                        new {
                            id = n.Key,
                            qt = n.Count()
                        });

            foreach (var item in result1)
            {
                quantiT += item.qt;
            }

            percUso =  (100 * quantiE) / quantiT;

            return percUso;

        }

        /// <summary>
        /// Retorna os Turnos com maior Fluxo de acordo com o Elevador mais frequentado
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            var lista = new List<char>();
            IEnumerable<dynamic> result1;
            var result = elevadorMaisFrequentado(2);

            foreach (var item0 in result)
            {
                result1 = _periodoUtilizacao(true, item0);

                foreach (var item in result1)
                {
                    lista.Add(item.id );
                    break;
                }
            }

            return lista;
        }

        /// <summary>
        /// Resolvi fazer este método porque achei interessante retornar por elevador, já que todos os lists foram definidos no Interface somente como char
        /// </summary>
        /// <param name="elevador"></param>
        /// <returns></returns>
        public List<char> periodoMaiorFluxoElevador(char elevador)
        {
            var lista = new List<char>();
            var result1 = _periodoUtilizacao(true, elevador);

            foreach (var item in result1)
            {
                lista.Add(item.id);
                
            }

            return lista;
        }
        /// <summary>
        /// Retorna os Turnos de maior utilização do conjunto de elevadores
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            var lista = new List<char>();
            var result = _periodoUtilizacao(true, ' ');

            foreach (var item in result)
            {
                lista.Add(item.id);
            }
            return lista;
        }
        /// <summary>
        /// Turno(s) com maior utilização do conjunto de elevadores conforme quantidaade infromada
        /// </summary>
        /// <param name="quantidade">Quantidade (> 0)</param>
        /// <returns></returns>
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores(int quantidade)
        {
            var lista = new List<char>();
            var result = _periodoUtilizacao(true, ' ');

            foreach (var item in result)
            {
                lista.Add(item.id);
                if (lista.Count >= quantidade)
                    break;
            }
            return lista;
        }
        private IEnumerable<dynamic> _periodoUtilizacao(bool maiorFluxo, char elevador)
        {

            IEnumerable<dynamic> result;
            if (elevador != ' ')
            {
                result = this.Where(ae => ae.Elevador == elevador)
                            .GroupBy(ae => ae.Turno)
                            .OrderBy(ae => ae.Key)
                            .Select(n =>
                             new {
                                   id = n.Key,
                                   qt = n.Count()
                                 });
            }
            else
            {
                result = this.GroupBy(ae => ae.Turno)
                             .OrderBy(ae => ae.Key)
                             .Select(n =>
                             new {
                                 id = n.Key,
                                 qt = n.Count()
                             });
            }

            IEnumerable<dynamic> result1;
            if (maiorFluxo)
            {
                result1 = result.OrderByDescending(ae => ae.qt);
            }
            else
            {
                result1 = result.OrderBy(ae => ae.qt);
            }

            return result1;
        }

        /// <summary>
        /// Retorna os Turnos com menor fluxo de acordo com o Elevador menos frequentado
        /// </summary>
        /// <returns></returns>
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            var lista = new List<char>();
            IEnumerable<dynamic> result1;
            var result = elevadorMenosFrequentado(2);

            foreach (var item0 in result)
            {
                result1 = _periodoUtilizacao(false, item0);

                foreach (var item in result1)
                {
                    lista.Add(item.id);
                    break;
                }
            }

            return lista;
        }
        /// <summary>
        /// Resolvi fazer este método porque achei interessante retornar por elevador, já que todos os lists foram definidos no Interface somente como char
        /// </summary>
        /// <param name="elevador"></param>
        /// <returns></returns>
        public List<char> periodoMenorFluxoElevador(char elevador)
        {
            var lista = new List<char>();
            var result1 = _periodoUtilizacao(false, elevador);

            foreach (var item in result1)
            {
                lista.Add(item.id);
                break;
            }

            return lista;
        }


    }

}
