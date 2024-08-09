using Domain.Interfaces.ExternalServices;
using Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices
{
    public class CustomerCampaignExternalService : ICustomerCampaignExternalService
    {
        private readonly HttpClient _httpClient;

        public CustomerCampaignExternalService(HttpClient httpClient, IOptions<CustomerServiceCampaignUrlModel> apiUrlsOptions) 
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiUrlsOptions.Value.CustomerServiceCampaignUrl);
        }
        public async Task NotifyCustomerCampaignExternalServiceAsync(int id)
        {
            try
            {
                string responseBody = string.Empty;

                var url = $"/Customer/RewardCustomer";
                var content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error notifying customer campaign service: {response.StatusCode}");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
