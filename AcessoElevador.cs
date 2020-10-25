using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul;

namespace provaApisul
{
    /// <summary>
    /// Define os registros de acesso aos coletados pela pesquisa
    /// </summary>
    public class AcessoElevador 
    {
        [JsonProperty("andar")]
        public int Andar { get; private set; }

        [JsonProperty("elevador")]
        public char Elevador { get; private set; }

        [JsonProperty("turno")]
        public char Turno { get; private set; }

    }
}
