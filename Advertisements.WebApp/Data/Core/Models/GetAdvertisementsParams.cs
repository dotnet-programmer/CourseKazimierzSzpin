namespace Advertisements.WebApp.Data.Core.Models;

public class GetAdvertisementsParams
{
	public bool IsActive { get; set; } = true;
    //public int SubcategoryId { get; set; }
    public int CategoryId { get; set; }
	public string Title { get; set; } = string.Empty;
	public decimal MinPrice { get; set; }
	public decimal MaxPrice { get; set; }
}