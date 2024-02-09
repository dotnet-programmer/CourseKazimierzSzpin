namespace Advertisements.WebApp.Data.Core.Models;

public class GetAdvertisementsParams
{
	public string UserId { get; set; }
	public string Title { get; set; }
    public int CategoryId { get; set; }
    //public int SubcategoryId { get; set; }
	public decimal MinPrice { get; set; }
	public decimal MaxPrice { get; set; }
	public bool IsArchival { get; set; }
}