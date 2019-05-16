using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace Highway.API
{
    public static class SwaggerInfo
    {
        public static Info GetDocumentationInfo()
        {
            var info = new Info
            {
                Version = "v1",
                Title = "Highway API .NET Core",
                Description =
                    "Documentation for API which supports higway toll station. It considers only cases for entering gate."
            };

            return info;
        }
    }
}
