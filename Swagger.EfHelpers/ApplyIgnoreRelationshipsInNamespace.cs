using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SystemOut.Swagger.EfHelpers
{
    /// <summary>
    /// Apply this filter to ignore all related objects that could cause  a self referencing loop
    /// </summary>
    /// <typeparam name="TNsType"></typeparam>
    public class ApplyIgnoreRelationshipsInNamespace<TNsType> : ISchemaFilter
    {
        /// <summary>
        /// Required by interface
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (model.Properties == null)
                return;
            var excludeList = new List<string>();

            if (context.SystemType.Namespace == typeof(TNsType).Namespace)
            {
                excludeList.AddRange(
                    from prop in context.SystemType.GetProperties()
                    where prop.PropertyType.Namespace == typeof(TNsType).Namespace
                    select prop.Name.ToCamelCase());
            }

            foreach (var prop in excludeList)
            {
                if (model.Properties.ContainsKey(prop))
                    model.Properties.Remove(prop);
            }
        }
    }
}
