using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmos.API.Models
{
    public class Item 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        
        [Newtonsoft.Json.JsonProperty(PropertyName = "Nome")]
        public string Nome { get; set; }
        
        [Newtonsoft.Json.JsonProperty(PropertyName = "Descricao")]
        public string Descricao { get; set; }
    }
    
  
}
