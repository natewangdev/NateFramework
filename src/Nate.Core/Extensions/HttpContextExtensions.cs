using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nate.Core.Features.ModelState;

namespace Nate.Core.Extensions
{
    public static class HttpContextExtensions
    {
        public static ModelStateDictionary GetModelState(this HttpContext context)
        {
            var modelState = context.Features.Get<IModelStateFeature>()?.ModelState;
            if (modelState == null)
            {
                modelState = new ModelStateDictionary();
                context.Features.Set<IModelStateFeature>(new ModelStateFeature { ModelState = modelState });
            }
            return modelState;
        }
    }
}
