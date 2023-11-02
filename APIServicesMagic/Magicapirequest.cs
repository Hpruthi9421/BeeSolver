using System;
using System.Threading.Tasks;

namespace BeeSolver1
{
    public class Magicapirequest : IMagicApiService
    {
        private int _MaxRetries = 1;
        private readonly string _ApiURL = "";
        private IConfiguration Configuration;


        public Magicapirequest( IConfiguration iConfig)
        {
            Configuration = iConfig;
            _ApiURL = Configuration["magicthegathering"].ToString();
            _MaxRetries = Convert.ToInt32(Configuration["Retry"].ToString());
        }
        public async Task<HttpResponseMessage> Getdata(string sb)
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < _MaxRetries; i++)
            {
                using (var client = new HttpClient())
                {
                    response = client.GetAsync(_ApiURL + "/cards" + sb.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }

                }
            }
            return response;

        }
    }
}
