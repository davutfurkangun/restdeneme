using BookStore.Entity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Client;

namespace BookStore.Dto
{
    public class OrderRequestDto
    {
        
        public List<string> BookIsbn { get; set; }
    }
}
