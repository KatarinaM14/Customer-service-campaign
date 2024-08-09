using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedProject.DTOs;

namespace Infrastructure.Data.Mappings
{
    public class GenerateFavoriteColorsDtoConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var favoriteColors = value as List<FavoriteColorDTO>;
            return string.Join(", ", favoriteColors.Select(c => c.Color));
        }
    }
}
