using Cosmos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControleItem : ControllerBase
    {
        private readonly IDocumentClient _documentClient;
        readonly string databaseID;
        readonly string collectionID;
        public IConfiguration Configuration { get; }

        public ControleItem(IDocumentClient documentClient, IConfiguration configuration)
        {
            _documentClient = documentClient;
            Configuration = configuration;

            databaseID = Configuration["DatabaseID"];
            collectionID = "Itens";

            BuildCollection().Wait();
        }

        private async Task BuildCollection()
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseID });
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseID),
                new DocumentCollection { Id = collectionID });
        }
        [HttpGet]
        public IQueryable<Item> Get()
        {
            return _documentClient.CreateDocumentQuery<Item>(UriFactory.CreateDocumentCollectionUri(databaseID, collectionID), new FeedOptions { MaxItemCount = 20 });

        }

        [HttpGet("{Id}")]
        public IQueryable<Item> Get(string ID)
        {
            return _documentClient.CreateDocumentQuery<Item>(UriFactory.CreateDocumentCollectionUri(databaseID,collectionID),
                new FeedOptions { MaxItemCount = 1}).Where((i) => i.Id == ID);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            var response = await _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseID, collectionID), item);
            return Ok();
        }
    }
}
