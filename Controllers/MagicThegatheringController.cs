using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO.Pipelines;


namespace BeeSolver1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MagicThegatheringController : ControllerBase
    {

  
        private readonly ILogger<MagicThegatheringController> _logger;
        private readonly IMagicApiService _magicApiService;
      

        public MagicThegatheringController(ILogger<MagicThegatheringController> logger, IConfiguration iConfig, IMagicApiService magicApiService )
        {
      
            _logger = logger;
            _magicApiService = magicApiService;
        }
        /// <summary>
        ///The accepted delimiters when querying fields are the pipe character or a comma character.The pipe represents a logical “or”, and a 
        ///comma represents a logical “and”. The comma can only be used with fields that accept multiple values (like colors).
        ///Example:name= nissa, worldwaker|jace|ajani, caller More examples: colors= red, white, blue versus colors= red | white | blue

        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     get /Todo
        ///     {
        ///        "name": nissa, worldwaker|jace|ajani,
        ///        "Colors":  red, white, blue ,
        ///         "Types": Creature,Angel,
        ///         "Page" : numbers of page
        ///     }
        ///
        /// </remarks>
        /// 
        ///  <response code="200">Returns filter data</response>

        [HttpGet(Name = "GetCards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public  async Task<ActionResult> Get([FromQuery] Cardgetrequestdto cardRequestDTOS)
        {
            _logger.LogInformation("Get Card Request: {dt}", DateTime.Now);
            try
            {


                StringBuilder sb = new StringBuilder();


                sb.Append("?page=" + cardRequestDTOS.Page.ToString());
                if (!string.IsNullOrEmpty(cardRequestDTOS.Name))
                    sb.Append("&name=" + cardRequestDTOS.Name);
                if (!string.IsNullOrEmpty(cardRequestDTOS.Colors))
                    sb.Append("&colors=" + cardRequestDTOS.Colors);
                if (!string.IsNullOrEmpty(cardRequestDTOS.Types))
                    sb.Append("&type=" + cardRequestDTOS.Types);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

                httpResponseMessage = await _magicApiService.Getdata(sb.ToString());
                var OutPutdata = await httpResponseMessage.Content.ReadAsStringAsync();
                _logger.LogInformation("Get request completed: {dt}", DateTime.Now);
                return Ok(OutPutdata);
            }
            catch(Exception ex)
            {
                _logger.LogError ("Error in get card Request {error}", ex.Message.ToString());
                return BadRequest();
            }
        }

     
        
            
    }
}