using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nate.Core.Features.ModelState
{
    public class ModelStateFeature : IModelStateFeature
    {
        public ModelStateDictionary ModelState { get; set; } = new ModelStateDictionary();
    }
}
