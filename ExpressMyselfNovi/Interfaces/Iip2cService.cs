namespace ExpressMyselfNovi.Interfaces
{
	public interface Iip2cService
	{
		Task<string[]> GetIPinfoAsync(string ip);

	}
}
