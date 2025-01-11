using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JsonResultConverter
    {
        public static Result<T> Deserialise<T>(string value)
        {
			try
			{
				var result = JsonConvert.DeserializeObject<T>(value);

				if(result == null)
				{
					return Result.Failure<T>("invalid content");
				}

                return Result.Success(result);
			}
			catch (Exception ex)
			{
				return Result.Failure<T>(ex.Message);
			}
        }
    }
}
