using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class GenerateFavoriteColorsConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var favoriteColors = value as List<FavoriteColor>;
            return string.Join(", ", favoriteColors.Select(c => c.Color));
        }
    }
}
