using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;

        public ReservationController(IRepository repo) => repository = repo;
        
        // GET api/<controller>
        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Reservation Get(int id) => repository[id];

        // POST api/<controller>
        [HttpPost]
        public Reservation Post([FromBody]Reservation res) =>
            repository.AddReservation(new Reservation
            {
                ClientName = res.ClientName,
                Location = res.Location
            });

        // PUT api/<controller>/5 ??
        [HttpPut]
        public Reservation Put([FromBody]Reservation res) =>
            repository.UpdateReservation(res);

        // PATCH api/<controller>/5
        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, 
            [FromBody]JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
