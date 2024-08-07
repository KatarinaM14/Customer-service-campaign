using Azure;
using Domain.Interfaces.ExternalServices;
using Domain.Models;
using Domain.Models.BaseModels;
using Microsoft.Extensions.Options;
using SharedProject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.ExternalServices
{
    public class UserExternalService : IUserExternalService
    {
        private readonly HttpClient _httpClient;

        public UserExternalService(HttpClient httpClient, IOptions<SOAPDemoUrlModel> apiUrlsOptions) 
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiUrlsOptions.Value.SOAPDemoUrl);
        }
        public async Task<CustomerDTO> GetUserByIdAsync(int id)
        {
            try
            {
                string responseBody = string.Empty;
                var customerDto = new CustomerDTO();

                var url = $"?soap_method=FindPerson&id={id}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    responseBody = await response.Content.ReadAsStringAsync();

                    XDocument xDoc = XDocument.Parse(responseBody);
                    XNamespace ns = "http://tempuri.org";

                    var customerElement = xDoc.Descendants(ns + "FindPersonResult").FirstOrDefault();

                    if (customerElement != null)
                    {
                        customerDto = new CustomerDTO
                        {
                            ExternalId = id,
                            Name = customerElement.Element(ns + "Name")?.Value,
                            SSN = customerElement.Element(ns + "SSN")?.Value,
                            DateOfBirth = DateTime.Parse(customerElement.Element(ns + "DOB")?.Value),
                            Age = int.Parse(customerElement.Element(ns + "Age")?.Value)
                        };

                        var homeDTO = new AddressDTO
                        {
                            Street = customerElement.Element(ns + "Home")?.Element(ns + "Street")?.Value,
                            City = customerElement.Element(ns + "Home")?.Element(ns + "City")?.Value,
                            State = customerElement.Element(ns + "Home")?.Element(ns + "State")?.Value,
                            Zip = customerElement.Element(ns + "Home")?.Element(ns + "Zip")?.Value,
                        };
                        
                        var officeDTO = new AddressDTO
                        {
                            Street = customerElement.Element(ns + "Office")?.Element(ns + "Street")?.Value,
                            City = customerElement.Element(ns + "Office")?.Element(ns + "City")?.Value,
                            State = customerElement.Element(ns + "Office")?.Element(ns + "State")?.Value,
                            Zip = customerElement.Element(ns + "Office")?.Element(ns + "Zip")?.Value,
                        };

                        var favoriteColors = customerElement.Element(ns + "FavoriteColors")
                                ?.Elements(ns + "FavoriteColorsItem")
                                .Select(x => x.Value)
                                .ToList();

                        customerDto.Home = homeDTO;
                        customerDto.HomeId = homeDTO.Id;
                        customerDto.Office = officeDTO;
                        customerDto.OfficeId = officeDTO.Id;

                        customerDto.FavoriteColors =new List<FavoriteColorDTO>();
                        foreach (var favoriteColor in favoriteColors)
                        {
                            var favColor = new FavoriteColorDTO
                            {
                                Color = favoriteColor
                            };
                            customerDto.FavoriteColors.Add(favColor);
                        }
                    }
                }
                else
                {
                    throw new Exception($"Error fetching customer: {response.StatusCode}");
                }

                return customerDto;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
