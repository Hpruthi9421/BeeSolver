namespace BeeSolver1
{
    public interface IMagicApiService
    {
        public Task<HttpResponseMessage> Getdata(string sb);
    }
}
