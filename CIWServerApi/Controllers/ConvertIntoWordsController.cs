using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CurrencyInWordsApp.CIWConverter;
using CurrencyInWordsApp.CIWConverter.Currency;

namespace CIWServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertIntoWordsController : ControllerBase
    {
        readonly CurrencyConvert Convert = new CurrencyConvert(CurrencyTicker.Ticker.USD);

        [HttpGet("{numerical}")]
        public string Get(string numerical)
        {
            try
            {
                return Convert.ToWords(numerical);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
