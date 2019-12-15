using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HybridAi.TestTask.Data;
using HybridAi.TestTask.Data.Models;
using HybridAi.TestTask.Data.Services.WebApi;

namespace HybridAi.TestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityLocationInfo : ControllerBase
    {
        private readonly IpDbContext _context;

        public CityLocationInfo(IpDbContext context)
        {
            _context = context;
        }

        // GET: api/GetIpLocation/1.1.1.1
        [HttpGet]
        public async Task< string > Get()
        {
            return "usage: scheme://host/citylocationinfo/<ip>";
        }


        // GET: api/GetIpLocation/1.1.1.1
        [HttpGet("{ip}")]
        public async Task< ActionResult< Models.CityLocationInfo > > GetIpLocation(string ip)
        {
            if ( IPAddress.TryParse( ip, out var ipAddress ) ) {
                var bytes = ipAddress.GetAddressBytes();
                var id = BitConverter.ToString( bytes );
                CityBlock block;
                if ( bytes.Length == 4 ) {
                    block = await _context.GetCityBlockIpv4( ipAddress );
                }
                else {
                    block = await _context.GetCityBlockIpv6( ipAddress );
                }

                if ( block == null ) return BadRequest();

                var info = (Models.CityLocationInfo)block;
                info.Ip = ip;

                return info;
            }

            return BadRequest();
        }

    }
}
