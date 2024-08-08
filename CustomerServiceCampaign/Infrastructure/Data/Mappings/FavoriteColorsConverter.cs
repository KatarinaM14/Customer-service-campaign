using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Domain.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Mappings
{
    public class FavoriteColorsConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            var favoriteColors = new List<FavoriteColor>();

            if (!string.IsNullOrEmpty(text))
            {
                var colors = text.Split(',');

                foreach (var color in colors)
                {
                    favoriteColors.Add(new FavoriteColor { Color = color });
                }
            }

            return favoriteColors;
        }
    }
}
