using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_Api_Class_02.Models;

namespace work_Api_Class_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly VehicleDbContext _context;

        public VehicleTypesController(VehicleDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.ToListAsync();
        }

        //Get:api/VehicleTypes/5
        [HttpGet("id")]
        public async Task<ActionResult<VehicleType>>GetVehicleType(int id)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType !=null)
            {
                return NotFound();
            }
            return vehicleType;
        }
        [HttpPost]
        public async Task<ActionResult<VehicleType>>PostValueType(VehicleType vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleType", new /*object[]*/ { id=vehicleType.VehicleTypeId },vehicleType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, VehicleType vehicleType)
        {
            if (id !=vehicleType.VehicleTypeId)
            {
                return BadRequest();
            }
            _context.Entry(vehicleType).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }             
            }
            return NoContent();
        }
        private bool VehicleTypeExists(int id)
        {
            return _context.VehicleTypes.Any(e => e.VehicleTypeId == id);
        }
    }
}
