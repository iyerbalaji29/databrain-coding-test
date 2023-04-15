using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace DataBrain.PAYG.Api.Filters
{
    /// <summary>
    ///     This is invoked by Swagger API to show enums as string in swagger documentation
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        ///     Invoked when Swagger API is initialized to show enum values as string in api documentation
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var values = Enum.GetValues(context.Type);
                schema.Enum.Clear();
                foreach (var value in values)
                {
                    var member = context.Type.GetMember(value.ToString())[0];
                    var enumMemberAttribute = member.GetCustomAttribute<EnumMemberAttribute>();
                    if (enumMemberAttribute != null)
                    {
                        schema.Enum.Add(new OpenApiString(enumMemberAttribute.Value));
                    }
                    else
                    {
                        schema.Enum.Add(new OpenApiString(value.ToString()));
                    }
                }
            }
        }
    }
}
