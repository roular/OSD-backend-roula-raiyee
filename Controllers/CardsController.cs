using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private CardContext _cardcontext;
        public CardsController(CardContext context)
        {
            _cardcontext = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> Get()
        {
            return await _cardcontext.Cards.ToListAsync();
        }

        [HttpPost("all")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest("Missing data");
            }
            Console.WriteLine("id");
            Console.WriteLine(person.Personid);
            IQueryable<Card> c = _cardcontext.Cards.Where(card => card.Personid == person.Personid);
            return await c.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddCard([FromBody] Card card)
        {
            if (card == null)
            {
                return NotFound("card data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _cardcontext.Cards.Add(card);
            await _cardcontext.SaveChangesAsync();
            return Json(new { result = "Card Created Successfully" });
            
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Card card)
        {
            if (card == null)
            {
                return NotFound("Person data is not supplied");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Card? existingCard = _cardcontext.Cards.FirstOrDefault(c => c.CardID == card.CardID);
            if (existingCard == null)
            {
                return NotFound("Student does not exist in the database");
            }
            existingCard.CardTitle = card.CardTitle;
            existingCard.CardCategory = card.CardCategory;
            existingCard.CardDuedate = card.CardDuedate;
            existingCard.CardEstimate = card.CardEstimate;
            existingCard.CardImportance = card.CardImportance;
            existingCard.CardType = card.CardType;
            existingCard.Personid = card.Personid;
            _cardcontext.Attach(existingCard).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _cardcontext.SaveChangesAsync();
            return Ok(existingCard);
        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteTodoItem([FromBody] Card card)
        {
            Card? existingCard = await _cardcontext.Cards.FindAsync(card.CardID);
            if (existingCard == null)
            {
                return NotFound();
            }

            _cardcontext.Cards.Remove(existingCard);
            await _cardcontext.SaveChangesAsync();
            return Json(new { result = "Card Deleted Successfully" });
            
        }
        ~CardsController()
        {
            _cardcontext.Dispose();
        }
    }
}